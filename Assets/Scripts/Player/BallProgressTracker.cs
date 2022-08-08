using System;
using System.Collections;
using System.Collections.Generic;
using GameCore;
using PickUps;
using Platforms;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class BallProgressTracker : Singleton<BallProgressTracker>
    {
        public static event Action<int> OnBallProgressed;
        
        [SerializeField] private PlatformGroupSpawnSettingsScriptableObject platformGroupSpawnSettings;
        [SerializeField] private GreenBottleSettingsScriptableObject greenBottleSettings;
        
        private bool isGameActive;
        private Transform ballTransform;
        private List<Transform> platformGroupTransforms;

        private int currentPlatformGroupIndex;
        private int consecutiveBallProgressCounter;
        
        //Called by PlatformGroupSpawner.cs after the platform groups are spawned.
        public void Initialize(List<Transform> platformGroupTransforms)
        {
            this.platformGroupTransforms = platformGroupTransforms;
            
            isGameActive = true;
            
            ballTransform = FindObjectOfType<Ball>().transform;
        }

        //Called by Ball.cs on collision with the platforms. 
        public bool IsBallOnConsecutiveProgress()
        {
            if (consecutiveBallProgressCounter <= 2)
            {
                consecutiveBallProgressCounter = 0;
                return false;
            }
            
            BreakNextPlatformGroup();
            consecutiveBallProgressCounter = 0;
            return true;
        }
        
        private void OnLevelEnd()
        {
            isGameActive = false;

            platformGroupTransforms = null; 
            ballTransform = null;
        }

        private void Update()
        {
            if (!isGameActive) {return;}

            if (currentPlatformGroupIndex + 1 >= platformGroupSpawnSettings.TotalPlatformGroupCount) {return;}
            
            var currentPlatformGroup = platformGroupTransforms[currentPlatformGroupIndex];

            if (ballTransform.position.y > currentPlatformGroup.position.y) {return;}

            BreakNextPlatformGroup();
        }

        private void BreakNextPlatformGroup()
        {
            if (currentPlatformGroupIndex + 1 >= platformGroupSpawnSettings.TotalPlatformGroupCount) {return;}

            platformGroupTransforms[currentPlatformGroupIndex].GetComponent<PlatformGroupBreaker>().BreakMyPlatforms();
            consecutiveBallProgressCounter++;
            currentPlatformGroupIndex++;
            
            ScoreManager.Instance.IncreaseScore(consecutiveBallProgressCounter);
            OnBallProgressed?.Invoke(BallProgressPercentage);
        }

        private void OnGreenBottlePickedUp()
        {
            StartCoroutine(BreakThreePlatformGroups());
        }

        private IEnumerator BreakThreePlatformGroups()
        {
            for (var i = 0; i < greenBottleSettings.PlatformBreakCount; i++)
            {
                if (!isGameActive) {break;}
         
                BreakNextPlatformGroup();
                
                yield return new WaitForSeconds(greenBottleSettings.DurationBetweenPlatformBreaks);                
            }
        }

        private void OnEnable()
        {
            LevelManager.OnLevelFailed += OnLevelEnd;
            LevelManager.OnLevelCompleted += OnLevelEnd;

            GreenBottle.OnGreenBottlePickedUp += OnGreenBottlePickedUp;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelFailed -= OnLevelEnd;
            LevelManager.OnLevelCompleted -= OnLevelEnd;
            
            GreenBottle.OnGreenBottlePickedUp -= OnGreenBottlePickedUp;
        }

        public int BallProgressPercentage =>
            (int)Math.Floor(100 * ((float)currentPlatformGroupIndex / (platformGroupSpawnSettings.TotalPlatformGroupCount - 1)));
    }
}
