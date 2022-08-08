using System;
using Pool;
using UnityEngine;

namespace PickUps
{
    public class GreenBottle : MonoBehaviour
    {
        public static event Action OnGreenBottlePickedUp;

        private int ballLayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != ballLayer) {return;}
        
            OnGreenBottlePickedUp?.Invoke();
            gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            ballLayer = LayerMask.NameToLayer("Ball");
        }
    }
}
