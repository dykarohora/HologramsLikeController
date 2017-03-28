using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using HoloToolkit.Unity;

[RequireComponent(typeof(Interpolator))]
public class TransformControllerActivater : MonoBehaviour, IInputClickHandler {
    private void Start() {
        // コントローラの操作対象を設定
        transform.Find("TransformController").GetComponent<TransformController>().target = gameObject;
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        // コントローラを有効化
        transform.Find("TransformController").gameObject.SetActive(true);
        // レンダラーとコライダーを無効化
        // GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        // debug
        Debug.Log("TransformController enabled.");
    }
}
