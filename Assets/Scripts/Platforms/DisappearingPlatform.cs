using System;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;

namespace Platforms
{
    public class DisappearingPlatform : MonoBehaviour
    {
        [SerializeField] private DisappearingPlatformSettingsScriptableObject disappearingPlatformSettings;
        
        private int ballLayer;
        private Transform myTransform;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != ballLayer) {return;}

            transform.DOScale(Vector3.zero, disappearingPlatformSettings.DisappearingDuration)
                .OnComplete(() => transform.parent.gameObject.SetActive(false));
        }

        private void OnEnable()
        {
            myTransform = transform;
            ballLayer = LayerMask.NameToLayer("Ball");
        }

        private void OnDisable()
        {
            myTransform.DOKill();
            myTransform = null;
        }
    }
}