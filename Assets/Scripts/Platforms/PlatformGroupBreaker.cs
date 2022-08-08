using System;
using System.Collections;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Platforms
{
    public class PlatformGroupBreaker : MonoBehaviour
    {
        [SerializeField] private PlatformBreakSettingsScriptableObject platformBreakSettings;
        
        //Called by BallProgressTracker.cs when the platform group should break.
        public void BreakMyPlatforms()
        {
            var childrenPlatformBreakers = GetComponentsInChildren<PlatformBreaker>();

            foreach (var platformBreaker in childrenPlatformBreakers)
            {
                platformBreaker.Break();
            }

            GetComponent<PlatformGroupRotator>().enabled = false;

            StartCoroutine(SetInactiveWithDelay());
        }
        private IEnumerator SetInactiveWithDelay()
        {
            yield return new WaitForSeconds(platformBreakSettings.PlatformFlyingDuration);
            
            gameObject.SetActive(false);
        }

    }
}
