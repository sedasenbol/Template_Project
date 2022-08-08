using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;


namespace Pool
{
    public class SplashPool : Pool
    {
        private static SplashPool instance;
        public static SplashPool Instance
        {
            get
            {
                if (instance != null) return instance;
            
                instance = FindObjectOfType<SplashPool>();

                if (instance != null) return instance;
            
                GameObject newGo = new GameObject();
                instance = newGo.AddComponent<SplashPool>();
                return instance;
            }
        }

        protected void Awake()
        {
            instance = this as SplashPool;
        }
    }
}