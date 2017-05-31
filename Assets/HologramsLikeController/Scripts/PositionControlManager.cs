using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: エディタ上で見た目がわかるようにしたい

namespace HologramsLikeController {
    public class PositionControlManager : MonoBehaviour {
        public GameObject cubeController;

        private void Start() {

            cubeController.SetActive(false);
            cubeController.transform.localPosition = Vector3.zero;
            cubeController.transform.rotation = Quaternion.identity;

            TransformController tc = transform.GetComponentInParent<TransformController>();
            GameObject target = tc.target;

            // コントロール対象を囲むワイヤーフレームの大きさを設定する
            Vector3 targetScale = target.transform.localScale;
            float scaleMagnificationX = 1f * tc.objScaleCorrectX / targetScale.x;
            float scaleMagnificationY = 1f * tc.objScaleCorrectY / targetScale.y;
            float scaleMagnificationZ = 1f * tc.objScaleCorrectZ / targetScale.z;

            cubeController.transform.localScale =
                new Vector3(
                    targetScale.x * scaleMagnificationX,
                    targetScale.y * scaleMagnificationY,
                    targetScale.z * scaleMagnificationZ) *
                TransformControlManager.Instance.basePositionCubeScale;

            cubeController.AddComponent<PositionController>();
            cubeController.GetComponent<MeshRenderer>().sharedMaterial =
                TransformControlManager.Instance.positionCubeMaterial;
            cubeController.SetActive(true);
        }
    }
}
