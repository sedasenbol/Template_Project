using Pool;
using ScriptableObjects;
using UnityEngine;

namespace Spawners
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField] private GreenBottleSettingsScriptableObject greenBottleSettings;
        [SerializeField] private BlueBottleSettingsScriptableObject blueBottleSettings;
        [SerializeField] private Transform ballTransform;
        
        //Called by PlatformGroupSpawner.cs when a new random platform is spawned.
        public void ReceivePotentialSpawnPlace(Transform platformGroup)
        {
            var randomProbability = Random.Range(0f, 1f);
            var shouldSpawnGreenBottle = randomProbability < greenBottleSettings.SpawnProbabilityOnPlatform;
            var shouldSpawnBlueBottle = randomProbability > greenBottleSettings.SpawnProbabilityOnPlatform && randomProbability
                < greenBottleSettings.SpawnProbabilityOnPlatform + blueBottleSettings.SpawnProbabilityOnPlatform;

            if (shouldSpawnBlueBottle) { SpawnBlueBottle(platformGroup, GetSpawnPosition(platformGroup));}
            
            if (!shouldSpawnGreenBottle) {return;}
            
            SpawnGreenBottle(platformGroup, GetSpawnPosition(platformGroup));
        }

        private Vector3 GetSpawnPosition(Transform platformGroup)
        {
            var ballPosition = ballTransform.position;
            
            var firstSpawnPosition = new Vector3()
            {
                x = ballPosition.x,
                y = platformGroup.position.y + greenBottleSettings.HeightOnPlatformGroup,
                z = ballPosition.z
            };

            return firstSpawnPosition;
        }

        private void SpawnBlueBottle(Transform platformGroupTransform, Vector3 firstSpawnPosition)
        {
            var greenBottle = BlueBottlePool.Instance.SpawnFromPool(firstSpawnPosition, Quaternion.identity);

            greenBottle.RotateAround(platformGroupTransform.position, Vector3.up, Random.Range(0f,360f));
        } 
        
        private void SpawnGreenBottle(Transform platformGroupTransform, Vector3 firstSpawnPosition)
        {
            var greenBottle = GreenBottlePool.Instance.SpawnFromPool(firstSpawnPosition, Quaternion.identity);

            greenBottle.RotateAround(platformGroupTransform.position, Vector3.up, Random.Range(0f,360f));
        } 
    }
}