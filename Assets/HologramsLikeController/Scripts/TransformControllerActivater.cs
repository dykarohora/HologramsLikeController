using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

/// <summary>
/// コントローラを起動するスクリプト
/// 起動時にコントロール対象のGameObjectにアタッチされる
/// 補間移動のため、Interpolatorを同時にアタッチする
/// </summary>
[RequireComponent(typeof(Interpolator))]
public class TransformControllerActivater : MonoBehaviour, IInputClickHandler {
    public void OnInputClicked(InputClickedEventData eventData) {
        // コントローラを有効化
        transform.Find("TransformController").gameObject.SetActive(true);
        // コントロール対象のコライダーを無効化
        GetComponent<Collider>().enabled = false;
#if UNITY_EDITOR
        Debug.Log("TransformController enabled.");
#endif
    }
}
