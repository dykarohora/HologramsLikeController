using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HologramsLikeController {
    public class ScaleController : MonoBehaviour, IInputHandler, ISourceStateHandler {
        public GameObject target;

        public bool IsDraggingEnable = true;
        private bool isDragging;

        private IInputSource currentInputSource = null;
        private uint currentInputSourceId;

        private Vector3 startHandPosition;
        private Vector3 startTargetPosition;
        private Vector3 targetBaseScale;
        private Vector3 scaleAxisVect;
        private float startDistance;

        private void OnEnable() {
            target = transform.GetComponentInParent<TransformController>().target;
            if (target == null) {
#if UNITY_EDITOR
                Debug.LogError("PositionController-OnEnable: target is not set.");
#endif
                return;
            }
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

            currentInputSource.TryGetPosition(currentInputSourceId, out startHandPosition);
            scaleAxisVect = transform.position - target.transform.position;
            scaleAxisVect.Normalize();

            targetBaseScale = target.transform.localScale;

            startDistance = Vector3.Distance(target.transform.position, transform.position);
            startTargetPosition = target.transform.position;
        }

        public void UpdatedDragging() {
            Vector3 newHandPosition;
            currentInputSource.TryGetPosition(currentInputSourceId, out newHandPosition);

            Vector3 moveVect = newHandPosition - startHandPosition;

            Vector3 projectMoveVect = Vector3.Project(moveVect, scaleAxisVect);

            float scaleValue =
                Vector3.Dot(projectMoveVect, scaleAxisVect) * TransformControlManager.Instance.scaleMagnification;

            if (targetBaseScale.x + scaleValue > TransformControlManager.Instance.scaleLowerLimit) {
                target.transform.localScale = targetBaseScale + new Vector3(scaleValue, scaleValue, scaleValue);
            } else {
                target.transform.localScale =
                    new Vector3(TransformControlManager.Instance.scaleLowerLimit,
                    TransformControlManager.Instance.scaleLowerLimit,
                    TransformControlManager.Instance.scaleLowerLimit);
            }

            float scaledDistance = Vector3.Distance(target.transform.position, transform.position);
            float difference = scaledDistance - startDistance;
            Vector3 correctionVect = scaleAxisVect * difference;

            target.transform.position = startTargetPosition + correctionVect;
        }

        public void StopDragging() {
            if (!isDragging)
                return;

            InputManager.Instance.PopModalInputHandler();
            isDragging = false;
            currentInputSource = null;
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
}