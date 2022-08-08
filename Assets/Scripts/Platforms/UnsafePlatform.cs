using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PickUps;
using ScriptableObjects;
using UnityEngine;

namespace Platforms
{
    public class UnsafePlatform : MonoBehaviour
    {
        [SerializeField] private BlueBottleSettingsScriptableObject blueBottleSettings;
        
        private Transform myTransform;
        private Vector3 myInitialScale;
        
        private void OnBlueBottlePickedUp()
        {
            myTransform.localScale = Vector3.zero;

            StartCoroutine(EnableUnsafePlatformWithDelay());
        }

        private IEnumerator EnableUnsafePlatformWithDelay()
        {
            yield return new WaitForSeconds(blueBottleSettings.DisabledPlatformDuration);

            myTransform.DOScale(myInitialScale, blueBottleSettings.DisabledPlatformReappearingDuration);
        }

        private void Awake()
        {
            myTransform = transform;
            myInitialScale = myTransform.localScale;
        }

        private void OnDestroy()
        {
            myTransform = null;
        }

        private void OnEnable()
        {
            BlueBottle.OnBlueBottlePickedUp += OnBlueBottlePickedUp;
        }

        private void OnDisable()
        {
            BlueBottle.OnBlueBottlePickedUp -= OnBlueBottlePickedUp;

            myTransform.DOKill();
        }
    }
}
