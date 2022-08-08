using DG.Tweening;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BallBounceSettings", menuName = "ScriptableObjects/BallBounceSettings", order = 1)]
    public class BallBounceSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private float jumpVelocity = 12f;
        [SerializeField] private float maxVelocityMagnitude = 20f;
        [SerializeField] private Vector3 scaleOnUnsafePlatformHit = new Vector3(0.5f, 0.25f, 0.5f);
        [SerializeField] private float scaleDurationOnUnsafePlatformHit = 0.1f;
        [SerializeField] private float shakeScaleDurationOnSafePlatformHit = 0.5f;
        [SerializeField] private float shakeScaleStrengthOnSafePlatformHit = 0.1f;
        [SerializeField] private int shakeScaleVibratoOnSafePlatformHit = 1;

        public float JumpVelocity => jumpVelocity;
        public float MaxVelocityMagnitude => maxVelocityMagnitude;
        public Vector3 ScaleOnUnsafePlatformHit => scaleOnUnsafePlatformHit;
        public float ScaleDurationOnUnsafePlatformHit => scaleDurationOnUnsafePlatformHit;
        public float ShakeScaleDurationOnSafePlatformHit => shakeScaleDurationOnSafePlatformHit;
        public float ShakeScaleStrengthOnSafePlatformHit => shakeScaleStrengthOnSafePlatformHit;
        public int ShakeScaleVibratoOnSafePlatformHit => shakeScaleVibratoOnSafePlatformHit;
    }
}
