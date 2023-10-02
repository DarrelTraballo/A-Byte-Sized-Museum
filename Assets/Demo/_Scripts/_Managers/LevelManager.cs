using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace KaChow.Demo
{    
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        private GameObject currentLevelPrefab;

        public void LoadNextLevel()
        {

        }
    }
}
