using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlatformSpawnSettings", menuName = "ScriptableObjects/PlatformSpawnSettings", order = 1)]
    public class PlatformGroupSpawnSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private int totalPlatformGroupCount = 41;
        [SerializeField] private Vector3 platformGroupStartPos = new Vector3(0f, -2f, 0f);
        [SerializeField] private Vector3 distanceBtwPlatformGroups = new Vector3(0f, 4f, 0f);
        [SerializeField] private Vector3 cylinderDefaultScale = new Vector3(2f, 1f, 2f);
        [SerializeField] private float cylinderHeightMargin = 3f;
        
        [SerializeField] private Transform firstPlatformGroupTransform;
        [SerializeField] private Transform lastPlatformGroupTransform;
        [SerializeField] private Transform cylinderTransform;

        [SerializeField] private List<Transform> randomPlatformGroupsToSpawn;
        
        public int TotalPlatformGroupCount => totalPlatformGroupCount;
        public Transform FirstPlatformGroupTransform => firstPlatformGroupTransform;
        public Transform LastPlatformGroupTransform => lastPlatformGroupTransform;
        public Vector3 PlatformGroupStartPos => platformGroupStartPos;
        public List<Transform> RandomPlatformGroupsToSpawn => randomPlatformGroupsToSpawn;
        public Vector3 DistanceBtwPlatformGroups => distanceBtwPlatformGroups;
        public Transform CylinderTransform => cylinderTransform;
        public Vector3 CylinderDefaultScale => cylinderDefaultScale;
        public float CylinderHeightMargin => cylinderHeightMargin;
    }
}