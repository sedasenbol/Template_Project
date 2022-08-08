using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlatformBreakSettings", menuName = "ScriptableObjects/PlatformBreakSettings", order = 1)]
    public class PlatformBreakSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private float platformFlyingXDistance = 100f;
        [SerializeField] private float platformFlyingYDistance = 200f;
        [SerializeField] private float platformFlyingDuration = 10f;

        public float PlatformFlyingXDistance => platformFlyingXDistance;
        public float PlatformFlyingYDistance => platformFlyingYDistance;
        public float PlatformFlyingDuration => platformFlyingDuration;
    }
}