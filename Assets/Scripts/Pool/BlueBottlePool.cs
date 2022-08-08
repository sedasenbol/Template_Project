using System;
using System.Collections.Generic;
using PickUps;
using ScriptableObjects;
using UnityEngine;

namespace Pool
{
    public class BlueBottlePool : Pool
    {
        private static BlueBottlePool instance;
        public static BlueBottlePool Instance
        {
            get
            {
                if (instance != null) return instance;
            
                instance = FindObjectOfType<BlueBottlePool>();

                if (instance != null) return instance;
            
                GameObject newGo = new GameObject();
                instance = newGo.AddComponent<BlueBottlePool>();
                return instance;
            }
        }

        protected void Awake()
        {
            instance = this as BlueBottlePool;
        }
    }
}