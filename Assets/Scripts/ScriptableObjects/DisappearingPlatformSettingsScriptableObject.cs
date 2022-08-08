using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "DisappearingPlatformSettings", menuName = "ScriptableObjects/DisappearingPlatformSettings", order = 1)]
    public class DisappearingPlatformSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private float disappearingDuration = 1f;

        public float DisappearingDuration => disappearingDuration;
    }
}