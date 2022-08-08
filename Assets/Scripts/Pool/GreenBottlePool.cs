using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Pool
{
    public class GreenBottlePool : Pool
    {
        private static GreenBottlePool instance;
        public static GreenBottlePool Instance
        {
            get
            {
                if (instance != null) return instance;
            
                instance = FindObjectOfType<GreenBottlePool>();

                if (instance != null) return instance;
            
                GameObject newGo = new GameObject();
                instance = newGo.AddComponent<GreenBottlePool>();
                return instance;
            }
        }

        protected void Awake()
        {
            instance = this as GreenBottlePool;
        }
        
    }
}