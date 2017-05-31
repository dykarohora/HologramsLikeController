using UnityEngine;

public class TransformController : MonoBehaviour {
    public GameObject target;
    // TODO:エディタに説明を表示する
    public float objScaleCorrectX;
    public float objScaleCorrectY;
    public float objScaleCorrectZ;

    // TODO:回転方向の有効無効を制御できるようにする

    public void Awake() {
        target = transform.parent.gameObject;
        target.AddComponent<TransformControllerActivater>();
        gameObject.SetActive(false);
    }
}
