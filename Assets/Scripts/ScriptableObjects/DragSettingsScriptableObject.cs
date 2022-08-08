using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "DragSettings", menuName = "ScriptableObjects/DragSettings", order = 1)]
    public class DragSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private float unityEditorDragToAngleFactor = 1500f;
        [SerializeField] private float mobilePhoneDragToAngleFactor = 3000f;
        
        public float UnityEditorDragToAngleFactor => unityEditorDragToAngleFactor;
        public float MobilePhoneDragToAngleFactor => mobilePhoneDragToAngleFactor;
    }
}