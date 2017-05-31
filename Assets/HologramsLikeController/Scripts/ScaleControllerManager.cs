using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HologramsLikeController {
    public class ScaleControllerManager : MonoBehaviour {
        private void Start() {
            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(false);

                float basePos = TransformControlManager.Instance.basePositionCubeScale / 2.0f;
                TransformController tc = transform.GetComponentInParent<TransformController>();

                child.localPosition = new Vector3(
                    basePos * tc.objScaleCorrectX * ((i & 4) != 0 ? -1 : 1),
                    basePos * tc.objScaleCorrectY * ((i & 2) != 0 ? -1 : 1),
                    basePos * tc.objScaleCorrectZ * ((i & 1) != 0 ? -1 : 1)
                    );

                // TODO:様々な大きさに対応できるようにする
                float localScaleX = 0.1f / tc.target.transform.localScale.x;
                float localScaleY = 0.1f / tc.target.transform.localScale.y;
                float localScaleZ = 0.1f / tc.target.transform.localScale.z;
                child.localScale = new Vector3(localScaleX, localScaleY, localScaleZ) * TransformControlManager.Instance.controllerScale;

                child.gameObject.AddComponent<ScaleController>();
                child.gameObject.SetActive(true);
            }

        }
    }
}