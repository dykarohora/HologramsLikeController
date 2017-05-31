using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransformController : MonoBehaviour {
    public GameObject Target {
        get; private set;
    }

    // TODO:回転方向の有効無効を制御できるようにする
   
    public Vector3 PositionControlerScale {
        get; private set;
    }

    public void Awake() {
        Target = transform.parent.gameObject;
        Target.AddComponent<TransformControllerActivater>();
        gameObject.SetActive(false);

        List<Bounds> boundsList = new List<Bounds>();
        foreach(var rendererObj in Target.GetComponentsInChildren<Renderer>()) {
            boundsList.Add(rendererObj.bounds);
        }


        float maxX = boundsList.Max((bounds) => {
            return bounds.center.x + bounds.extents.x;
        });

        float minX = boundsList.Min((bounds) => {
            return bounds.center.x - bounds.extents.x;
        });

        float maxY = boundsList.Max((bounds) => {
            return bounds.center.y + bounds.extents.y;
        });

        float minY = boundsList.Min((bounds) => {
            return bounds.center.y - bounds.extents.y;
        });

        float maxZ = boundsList.Max((bounds) => {
            return bounds.center.z + bounds.extents.z;
        });

        float minZ = boundsList.Min((bounds) => {
            return bounds.center.z - bounds.extents.z;
        });

        // LINQとLambda使ってるので性能出ないとき用
        /*
        float maxX = boundsList[0].center.x + boundsList[0].extents.x;
        float minX = boundsList[0].center.x - boundsList[0].extents.x;
        float maxY = boundsList[0].center.y + boundsList[0].extents.y;
        float minY = boundsList[0].center.y - boundsList[0].extents.y;
        float maxZ = boundsList[0].center.z + boundsList[0].extents.z;
        float minZ = boundsList[0].center.z - boundsList[0].extents.z;

        for (int i = 1; i < boundsList.Count; ++i) {
            float tempMaxX, tempMinX;
            float tempMaxY, tempMinY;
            float tempMaxZ, tempMinZ;

            tempMaxX = boundsList[i].center.x + boundsList[i].extents.x;
            tempMinX = boundsList[i].center.x - boundsList[i].extents.x;
            tempMaxY = boundsList[i].center.y + boundsList[i].extents.y;
            tempMinY = boundsList[i].center.y - boundsList[i].extents.y;
            tempMaxZ = boundsList[i].center.z + boundsList[i].extents.z;
            tempMinZ = boundsList[i].center.z - boundsList[i].extents.z;

            if (tempMaxX > maxX) maxX = tempMaxX;
            if (tempMaxY > maxY) maxY = tempMaxY;
            if (tempMaxZ > maxZ) maxZ = tempMaxZ;

            if (tempMinX < minX) minX = tempMinX;
            if (tempMinY < minY) minY = tempMinY;
            if (tempMinZ < minZ) minZ = tempMinZ;
        }
        */

        PositionControlerScale = new Vector3(
            (maxX - minX) / Target.transform.localScale.x,
            (maxY - minY) / Target.transform.localScale.y,
            (maxZ - minZ) / Target.transform.localScale.z
        );

        if(Target.GetComponent<Collider>() == null) {
            Target.AddComponent<BoxCollider>().size = PositionControlerScale;
        }
    }
}
