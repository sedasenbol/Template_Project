using System;
using DG.Tweening;
using GameCore;
using Platforms;
using Pool;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Rendering;

namespace Player
{
    public class Ball : MonoBehaviour
    {
        public static event Action OnBallsFirstHit;

        [SerializeField] private BallBounceSettingsScriptableObject ballBounceSettings;
        [SerializeField] private Rigidbody rb;
        
        private int safePlatformLayer;
        private int unsafePlatformLayer;
        private int lastPlatformLayer;
        private int unsafeLongPlatformLayer;

        private bool ballHitTheFirstPlatform;
        
        private Transform myTransform;
        private Vector3 initialScale;

        private bool isActive;
        
        private void FixedUpdate()
        {
            if (rb.velocity.sqrMagnitude < Mathf.Pow(ballBounceSettings.MaxVelocityMagnitude,2)) {return;}
            
            rb.AddForce(-Physics.gravity,ForceMode.Force);            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!isActive) {return;}
            
            var otherLayer = collision.gameObject.layer;
            
            if (otherLayer == unsafePlatformLayer)
            {
                if (BallProgressTracker.Instance.IsBallOnConsecutiveProgress())
                {
                    ChangeScale();
                    BounceBall();
                    return;
                }
                
                LevelManager.Instance.HandleUnsafePlatformHit();

                isActive = false;
                rb.Sleep();
                
                SpawnSplash(collision.gameObject);
                ChangeScale();
            }
            else if (otherLayer == unsafeLongPlatformLayer)
            {
                if (BallProgressTracker.Instance.IsBallOnConsecutiveProgress())
                {
                    ChangeScale();
                    BounceBall();
                    return;
                }
                
                LevelManager.Instance.HandleUnsafePlatformHit();

                isActive = false;
                rb.Sleep();
                
                ChangeScale();
            }
            else if (otherLayer == safePlatformLayer)
            {
                BallProgressTracker.Instance.IsBallOnConsecutiveProgress();
                
                SpawnSplash(collision.gameObject);
                BounceBall();
                ShakeScale();
                
                if (ballHitTheFirstPlatform) {return;}
                
                OnBallsFirstHit?.Invoke();
                ballHitTheFirstPlatform = true;
            }
            else if (otherLayer == lastPlatformLayer)
            {
                LevelManager.Instance.HandleLastPlatformHit();

                isActive = false;
            }
        }

        private void ShakeScale()
        {
            myTransform.DOShakeScale(ballBounceSettings.ShakeScaleDurationOnSafePlatformHit,
                ballBounceSettings.ShakeScaleStrengthOnSafePlatformHit, ballBounceSettings.ShakeScaleVibratoOnSafePlatformHit,
                90f, true);
        }

        private void ChangeScale()
        {
            myTransform.DOScale(ballBounceSettings.ScaleOnUnsafePlatformHit, ballBounceSettings.ScaleDurationOnUnsafePlatformHit)
            .SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    if (!isActive) {return;}
                    
                    myTransform.DOScale(initialScale, ballBounceSettings.ScaleDurationOnUnsafePlatformHit).SetEase(Ease.InOutSine);
                });
        }

        private void BounceBall()
        {
            rb.velocity = Vector3.up * ballBounceSettings.JumpVelocity;
        }

        private void SpawnSplash(GameObject platformGO)
        {
            var myPosition = myTransform.position;
            
            var spawnPos = new Vector3()
            {
                x = myPosition.x,
                y = platformGO.GetComponentInChildren<Renderer>().bounds.max.y,
                z = myPosition.z
            };

            var splash = SplashPool.Instance.SpawnFromPool(spawnPos, Quaternion.identity);
            splash.parent = platformGO.transform;
        }
    
        private void OnEnable()
        {
            myTransform = transform;
            
            safePlatformLayer = LayerMask.NameToLayer("Platform/SafePlatform");
            unsafePlatformLayer = LayerMask.NameToLayer("Platform/UnsafePlatform");
            lastPlatformLayer = LayerMask.NameToLayer("Platform/LastPlatform");
            unsafeLongPlatformLayer = LayerMask.NameToLayer("Platform/UnsafeLongPlatform");

            isActive = true;

            initialScale = myTransform.localScale;
        }

        private void OnDisable()
        {
            myTransform = null;
        }
    }
}
