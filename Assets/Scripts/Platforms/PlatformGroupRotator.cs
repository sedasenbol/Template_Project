using Input;
using ScriptableObjects;
using UnityEngine;

namespace Platforms
{
    public class PlatformGroupRotator : MonoBehaviour, IDraggable
    {
        [SerializeField] private DragSettingsScriptableObject dragSettings;

        private Transform myTransform;

        //TouchController.cs OnPlayerDragged event handler that rotates the platform groups.
        public void OnPlayerDragged(Vector3 dragVector)
        {
#if UNITY_EDITOR
            myTransform.Rotate(Vector3.up, -dragVector.x * dragSettings.UnityEditorDragToAngleFactor);
#else
            myTransform.Rotate(Vector3.up, -dragVector.x * dragSettings.MobilePhoneDragToAngleFactor);
#endif
        }
    
        private void OnEnable()
        {
            myTransform = transform;
        
            TouchController.OnPlayerDragged += OnPlayerDragged;
        }

        private void OnDisable()
        {
            myTransform = null;
        
            TouchController.OnPlayerDragged -= OnPlayerDragged;
        }
    }
}
