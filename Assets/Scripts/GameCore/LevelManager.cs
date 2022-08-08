using System;
using System.Collections;
using DG.Tweening;
using Pool;
using ScriptableObjects;
using UnityEngine;


namespace GameCore
{
    public class LevelManager : Singleton<LevelManager>
    {
        public static event Action OnNewLevelLoaded;
        public static event Action OnLevelFailed;
        public static event Action OnLevelCompleted;

        [SerializeField] private BallBounceSettingsScriptableObject ballBounceSettings;
        
        // Called by GameManager.cs when "Game" scene is loaded. 
        public void HandleNewLevel()
        {
            SplashPool.Instance.InitializeItemPoolDict();
            GreenBottlePool.Instance.InitializeItemPoolDict();
            BlueBottlePool.Instance.InitializeItemPoolDict();
            
            OnNewLevelLoaded?.Invoke();
        }

        // Called by Ball.cs when the ball hits an unsafe platform.
        public void HandleUnsafePlatformHit()
        {
            StartCoroutine(FailLevel());
        }

        private IEnumerator FailLevel()
        {
            yield return new WaitForSeconds(ballBounceSettings.ScaleDurationOnUnsafePlatformHit);
            
            DOTween.CompleteAll();
            OnLevelFailed?.Invoke();
        }

        // Called by Ball.cs when the ball hits the last platform.
        public void HandleLastPlatformHit()
        {
            CompleteLevel();
        }

        private void CompleteLevel()
        {
            DOTween.CompleteAll();
            OnLevelCompleted?.Invoke();
        }
    }
}
