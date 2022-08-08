using Input;
using ScriptableObjects;
using Spawners;
using UnityEngine;

namespace PickUps
{
    public class PickUpRotator : MonoBehaviour, IDraggable
    {
        [SerializeField] private DragSettingsScriptableObject dragSettings;

        private Transform myTransform;
        private Transform cylinderTransform;

        //TouchController.cs OnPlayerDragged event handler that rotates the pick ups.
        public void OnPlayerDragged(Vector3 dragVector)
        {
#if UNITY_EDITOR
            myTransform.RotateAround(cylinderTransform.position,Vector3.up, -dragVector.x * dragSettings.UnityEditorDragToAngleFactor);
#else
            myTransform.RotateAround(cylinderTransform.position,Vector3.up, -dragVector.x * dragSettings.MobilePhoneDragToAngleFactor);
#endif
        }
        
        private void OnCylinderSpawned(Transform cylinderTransform)
        {
            myTransform = transform;
            this.cylinderTransform = cylinderTransform;
        }
        
        private void Awake()
        {
            TouchController.OnPlayerDragged += OnPlayerDragged;
            PlatformGroupSpawner.OnCylinderSpawned += OnCylinderSpawned;
        }

        private void OnDestroy()
        {
            myTransform = null;
            cylinderTransform = null;
        
            TouchController.OnPlayerDragged -= OnPlayerDragged;
            PlatformGroupSpawner.OnCylinderSpawned -= OnCylinderSpawned;
        }
    }
}
