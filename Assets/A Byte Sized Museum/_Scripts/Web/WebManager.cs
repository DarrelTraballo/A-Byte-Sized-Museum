using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    // Attach to Web Manager GameObject
    public class WebManager : MonoBehaviour
    {
        public static WebManager Instance { get; private set; }
        private WebManager() {}

        private Web Web;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

            Web = GetComponent<Web>();
        }


    }
}
