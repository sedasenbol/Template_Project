using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GreenBottleSettings", menuName = "ScriptableObjects/GreenBottleSettings", order = 1)]
    public class GreenBottleSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private float spawnProbabilityOnPlatform = 0.05f;
        [SerializeField] private float heightOnPlatformGroup = 2f;
        [SerializeField] private float durationBetweenPlatformBreaks = 0.05f;
        [SerializeField] private int platformBreakCount = 3;
    
        public float SpawnProbabilityOnPlatform => spawnProbabilityOnPlatform;
        public float HeightOnPlatformGroup => heightOnPlatformGroup;
        public float DurationBetweenPlatformBreaks => durationBetweenPlatformBreaks;
        public int PlatformBreakCount => platformBreakCount;
    }
}