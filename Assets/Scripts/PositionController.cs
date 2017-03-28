using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class PositionController : MonoBehaviour, IInputHandler, ISourceStateHandler {
    public GameObject target;
    private Interpolator interpolator;

    public bool IsDraggingEnable = true;
    private bool isDragging;

    private Camera mainCamera;

    private float objRefDistance;
    private float handRefDistance;
    private Vector3 objRefGrabPoint;
    private Quaternion gazeAngularOffset;

    private Vector3 draggingPosition;

    private IInputSource currentInputSource = null;
    private uint currentInputSourceId;

    private void OnEnable() {
        target = transform.GetComponentInParent<TransformController>().target;
        if(target == null) {
            Debug.LogError("PositionController-OnEnable: target is not set.");
            return;
        }

        if (interpolator == null) {
            interpolator = target.GetComponent<Interpolator>();
        }
        if(interpolator == null) {
            Debug.LogError("PositionController-OnEnabled: Target object isn't attached Interpolator component.");
        }

        mainCamera = Camera.main;
    }

    private void Update() {
        if (IsDraggingEnable && isDragging)
            UpdatedDragging();
    }

    public void StartDragging() {
        if (!IsDraggingEnable)
            return;
        if (isDragging)
            return;

        InputManager.Instance.PushModalInputHandler(gameObject);
        isDragging = true;

        Vector3 gazeHitPosition = GazeManager.Instance.HitInfo.point;

        Vector3 handPosition;
        currentInputSource.TryGetPosition(currentInputSourceId, out handPosition);

        Vector3 pivotPosition = GetHandPivotPosition();

        handRefDistance = Vector3.Magnitude(handPosition - pivotPosition);
        objRefDistance  = Vector3.Magnitude(gazeHitPosition - pivotPosition);

        objRefGrabPoint = mainCamera.transform.InverseTransformDirection(target.transform.position - gazeHitPosition);

        Vector3 objDirection = Vector3.Normalize(gazeHitPosition - pivotPosition);
        Vector3 handDirection = Vector3.Normalize(handPosition - pivotPosition);

        objDirection = mainCamera.transform.InverseTransformDirection(objDirection);
        handDirection = mainCamera.transform.InverseTransformDirection(handDirection);

        gazeAngularOffset = Quaternion.FromToRotation(handDirection, objDirection);
        draggingPosition = gazeHitPosition;

    }

    public void UpdatedDragging() {
        Vector3 newHandPosition;
        currentInputSource.TryGetPosition(currentInputSourceId, out newHandPosition);

        Vector3 pivotPosition = GetHandPivotPosition();

        Vector3 newHandDirection = Vector3.Normalize(newHandPosition - pivotPosition);
        newHandDirection = mainCamera.transform.InverseTransformDirection(newHandDirection);

        Vector3 targetDirection = Vector3.Normalize(gazeAngularOffset * newHandDirection);
        targetDirection = mainCamera.transform.TransformDirection(targetDirection);

        float currentHandDistance = Vector3.Magnitude(newHandPosition - pivotPosition);
        float distanceRatio = currentHandDistance / handRefDistance;
        float distanceOffset = distanceRatio > 0 ? (distanceRatio - 1f) * 
            TransformControlManager.Instance.distanceScale : 0;
        float targetDistance = objRefDistance + distanceOffset;

        draggingPosition = pivotPosition + (targetDirection * targetDistance);

        interpolator.SetTargetPosition(draggingPosition + mainCamera.transform.TransformDirection(objRefGrabPoint));
    }

    public void StopDragging() {
        if (!isDragging)
            return;
        InputManager.Instance.PopModalInputHandler();
        isDragging = false;
        currentInputSource = null;
    }

    private Vector3 GetHandPivotPosition() {
        return mainCamera.transform.position + new Vector3(0, -0.2f, 0) - mainCamera.transform.forward * 0.2f;
    }

    #region IInputHandler
    public void OnInputUp(InputEventData eventData) {
        if (currentInputSource != null && eventData.SourceId == currentInputSourceId)
            StopDragging();
    }

    public void OnInputDown(InputEventData eventData) {
        if (isDragging)
            return;

        if (!eventData.InputSource.SupportsInputInfo(eventData.SourceId, SupportedInputInfo.Position))
            return;

        currentInputSource = eventData.InputSource;
        currentInputSourceId = eventData.SourceId;

        StartDragging();
    }
    #endregion

    #region ISourceStateHandler
    public void OnSourceDetected(SourceStateEventData eventData) {
        // Nothing to do.
    }

    public void OnSourceLost(SourceStateEventData eventData) {
        if (currentInputSource != null && eventData.SourceId == currentInputSourceId)
            StopDragging();
    }
    #endregion
}
