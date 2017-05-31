using UnityEngine;

namespace HologramsLikeController {
    public class RotationControlManager : MonoBehaviour {
        private void Start() {
            foreach (Transform axisContainer in transform) {
                switch (axisContainer.name) {
                    case "AxisX":
                        for (int i = 0; i < axisContainer.childCount; i++) {
                            Transform child = axisContainer.GetChild(i);
                            child.gameObject.SetActive(false);
                            child.gameObject.AddComponent<RotationController>().axis = RotationController.RotationAxis.x;
                            SetChildPositionAndScale(child, RotationController.RotationAxis.x, i);
                            child.gameObject.SetActive(true);
                        }
                        break;
                    case "AxisY":
                        for (int i = 0; i < axisContainer.childCount; i++) {
                            Transform child = axisContainer.GetChild(i);
                            child.gameObject.SetActive(false);
                            child.gameObject.AddComponent<RotationController>().axis = RotationController.RotationAxis.y;
                            SetChildPositionAndScale(child, RotationController.RotationAxis.y, i);
                            child.gameObject.SetActive(true);
                        }
                        break;
                    case "AxisZ":
                        for (int i = 0; i < axisContainer.childCount; i++) {
                            Transform child = axisContainer.GetChild(i);
                            child.gameObject.SetActive(false);
                            child.gameObject.AddComponent<RotationController>().axis = RotationController.RotationAxis.z;
                            SetChildPositionAndScale(child, RotationController.RotationAxis.z, i);
                            child.gameObject.SetActive(true);
                        }
                        break;
                    default:
#if UNITY_EDITOR
                        Debug.LogError("Container name is invalid");
#endif
                        return;
                }
            }
        }

        private void SetChildPositionAndScale(Transform child, RotationController.RotationAxis axis, int i) {
            TransformController tc = transform.GetComponentInParent<TransformController>();
            var basePos = tc.PositionControlerScale * TransformControlManager.Instance.positionCubeScale /2.0f;
            switch (axis) {
                case RotationController.RotationAxis.x:
                    child.localPosition = new Vector3(
                        0,
                        basePos.y * ((i & 2) != 0 ? -1 : 1),
                        basePos.z * ((i & 1) != 0 ? -1 : 1));
                    break;
                case RotationController.RotationAxis.y:
                    child.localPosition = new Vector3(
                        basePos.x * ((i & 2) != 0 ? -1 : 1),
                        0,
                        basePos.z * ((i & 1) != 0 ? -1 : 1));
                    break;
                case RotationController.RotationAxis.z:
                    child.localPosition = new Vector3(
                        basePos.x * ((i & 2) != 0 ? -1 : 1),
                        basePos.y * ((i & 1) != 0 ? -1 : 1),
                        0);
                    break;
                default:
#if UNITY_EDITOR
                    Debug.LogError("Parameter axis is invalid");
#endif
                    return;
            }

            // TODO:さまざまな大きさに対応できるようにする
            float localScaleX = 0.1f / tc.Target.transform.localScale.x;
            float localScaleY = 0.1f / tc.Target.transform.localScale.y;
            float localScaleZ = 0.1f / tc.Target.transform.localScale.z;
            child.localScale = new Vector3(localScaleX, localScaleY, localScaleZ) * TransformControlManager.Instance.controllerScale;
        }
    }
}