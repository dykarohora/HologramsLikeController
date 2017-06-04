using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HologramsLikeController {
    public class TransformControllerCompletion : MonoBehaviour, IInputClickHandler {
        public void OnInputClicked(InputClickedEventData eventData) {
            Transform transformController = transform.parent;
            GameObject target = transformController.parent.gameObject;
            // Cubeのレンダラーとコライダーを有効か
            target.GetComponent<Collider>().enabled = true;
            // 親オブジェクトを無効化
            transformController.gameObject.SetActive(false);
            // debug
            Debug.Log("TransformController disabled.");
        }

        private void Start() {
            TransformController tc = transform.GetComponentInParent<TransformController>();

            float posY = tc.PositionControlerScale.y;

            transform.localPosition = new Vector3(0, posY + TransformControlManager.Instance.completePanelPositionY, 0);
        }

        private void Update() {
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
}