using System;
using System.Collections.Generic;
using GameCore;
using Player;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class PlatformGroupSpawner : MonoBehaviour
    {
        public static event Action<Transform> OnCylinderSpawned; 

        [SerializeField] private PlatformGroupSpawnSettingsScriptableObject platformGroupSpawnSettings;
        [SerializeField] private BallProgressTracker ballProgressTracker;
        [SerializeField] private Transform platformGroupContainer;
        [SerializeField] private PickUpSpawner pickUpSpawner;

        private List<Transform> platformGroupTransforms;
        private Vector3 lastSpawnPos;
        private List<Transform> randomPlatformGroupsToSpawn => platformGroupSpawnSettings.RandomPlatformGroupsToSpawn;
        
        private void OnNewLevelLoaded()
        {
            SpawnCylinder();
            SpawnPlatformGroups();

            ballProgressTracker.Initialize(platformGroupTransforms);
        }

        private void SpawnCylinder()
        {
            var cylinderHeight = (platformGroupSpawnSettings.TotalPlatformGroupCount - 1) * platformGroupSpawnSettings
            .DistanceBtwPlatformGroups.y + platformGroupSpawnSettings.CylinderHeightMargin;

            var spawnPosition = platformGroupSpawnSettings.PlatformGroupStartPos;
            spawnPosition.y -= cylinderHeight / 2f - platformGroupSpawnSettings.CylinderHeightMargin; 

            var cylinderTransform = Instantiate(platformGroupSpawnSettings.CylinderTransform, spawnPosition, Quaternion.identity);
 
            var scale = platformGroupSpawnSettings.CylinderDefaultScale;
            scale.y = cylinderHeight / 2f;
            
            cylinderTransform.localScale = scale;

            OnCylinderSpawned?.Invoke(cylinderTransform);
        }

        private void SpawnPlatformGroups()
        {
            platformGroupTransforms.Add(SpawnSpecificPlatformGroup(platformGroupSpawnSettings.FirstPlatformGroupTransform,
                platformGroupSpawnSettings.PlatformGroupStartPos));

            for (var i = 0; i < platformGroupSpawnSettings.TotalPlatformGroupCount - 2; i++)
            {
                lastSpawnPos -= platformGroupSpawnSettings.DistanceBtwPlatformGroups;
                var randomQuaternion = Quaternion.Euler(0f, Random.Range(0f,360f), 0f);
                    
                platformGroupTransforms.Add(SpawnRandomPlatformGroup(lastSpawnPos, randomQuaternion));
            }

            platformGroupTransforms.Add(SpawnSpecificPlatformGroup(platformGroupSpawnSettings.LastPlatformGroupTransform,
                lastSpawnPos - platformGroupSpawnSettings.DistanceBtwPlatformGroups));
        }

        private Transform SpawnSpecificPlatformGroup(Transform platformTransform, Vector3 position)
        {
            lastSpawnPos = position;
            return Instantiate(platformTransform, position, Quaternion.identity, platformGroupContainer);
        }

        private Transform SpawnRandomPlatformGroup(Vector3 position, Quaternion quaternion)
        {
            var randomPlatformGroupTransform = randomPlatformGroupsToSpawn[Random.Range(0, randomPlatformGroupsToSpawn.Count)];

            var spawnedPlatformGroupTransform = Instantiate(randomPlatformGroupTransform, position, quaternion, platformGroupContainer);

            pickUpSpawner.ReceivePotentialSpawnPlace(spawnedPlatformGroupTransform);
            
            return spawnedPlatformGroupTransform;
        }
        
        private void OnEnable()
        {
            platformGroupTransforms = new List<Transform>(platformGroupSpawnSettings.TotalPlatformGroupCount);
            
            LevelManager.OnNewLevelLoaded += OnNewLevelLoaded;
        }
        
        private void OnDisable()
        {
            platformGroupTransforms = null;
            
            LevelManager.OnNewLevelLoaded -= OnNewLevelLoaded;
        }
    }
}
