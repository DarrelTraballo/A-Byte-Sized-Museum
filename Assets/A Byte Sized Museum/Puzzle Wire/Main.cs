using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    
    public class Main : MonoBehaviour
    {
        
        public static Main Instance { get; private set; }
        #region Singleton
        private Main() {}
        private void Awake() 
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else 
                Instance = this;
        }
        #endregion
        public GameObject completed;

        public int count = 0;
        public int totalCount = 4;

        public void updateCount()
        {
            count++;
            if (count == totalCount)
            {
                completed.SetActive(true);
            }
        }
        
        
    }
}
