using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HologramsLikeController {
    public class TransformControlManager : Singleton<TransformControlManager> {
        public float distanceScale = 2f;
        public float positionCubeScale = 1.1f;
        public Material positionCubeMaterial;

        public float rotationSpeed = 50.0f;

        public float scaleMagnification = 1.0f;
        public float scaleLowerLimit = 0.05f;

        public float controllerScale = 0.1f;

        public float completePanelPositionY = 0.1f;
    }
}
