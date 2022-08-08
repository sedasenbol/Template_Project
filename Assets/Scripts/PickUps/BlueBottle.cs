using System;
using UnityEngine;

namespace PickUps
{
    public class BlueBottle : MonoBehaviour
    {
        public static event Action OnBlueBottlePickedUp;

        private int ballLayer;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != ballLayer) {return;}
        
            OnBlueBottlePickedUp?.Invoke();
            gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            ballLayer = LayerMask.NameToLayer("Ball");
        }
    }
}