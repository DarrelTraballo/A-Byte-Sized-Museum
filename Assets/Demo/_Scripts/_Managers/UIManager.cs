using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.Demo 
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }
    }
}

