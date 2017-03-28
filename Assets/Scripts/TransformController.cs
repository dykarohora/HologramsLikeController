using UnityEngine;

public class TransformController : MonoBehaviour {
    public GameObject target;

    public float objScaleCorrectX;
    public float objScaleCorrectY;
    public float objScaleCorrectZ;

    public void Awake() {
        target = transform.parent.gameObject;
        target.AddComponent<TransformControllerActivater>();
        gameObject.SetActive(false);
    }
}
