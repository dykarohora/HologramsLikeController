using UnityEngine;

namespace HologramsLikeController {
    public class ScaleControllerManager : MonoBehaviour {
        private void Start() {
            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(false);

                TransformController tc = transform.GetComponentInParent<TransformController>();
                var basePos = tc.PositionControlerScale * TransformControlManager.Instance.positionCubeScale / 2.0f;

                child.localPosition = new Vector3(
                    basePos.x * ((i & 4) != 0 ? -1 : 1),
                    basePos.y * ((i & 2) != 0 ? -1 : 1),
                    basePos.z * ((i & 1) != 0 ? -1 : 1)
                    );

                // TODO:様々な大きさに対応できるようにする
                float localScaleX = 0.1f / tc.Target.transform.localScale.x;
                float localScaleY = 0.1f / tc.Target.transform.localScale.y;
                float localScaleZ = 0.1f / tc.Target.transform.localScale.z;
                child.localScale = new Vector3(localScaleX, localScaleY, localScaleZ) * TransformControlManager.Instance.controllerScale;

                child.gameObject.AddComponent<ScaleController>();
                child.gameObject.SetActive(true);
            }

        }
    }
}