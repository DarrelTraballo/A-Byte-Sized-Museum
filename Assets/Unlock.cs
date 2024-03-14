using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Unlock : MonoBehaviour
    {
        public dialogmanager dialog_manager;
        // Start is called before the first frame update

        void OnEnable()
        {
            Start();
        }
        void Start()
        {
            dialog_manager.helperbot_tutorial = false;
            dialog_manager.unlock = true;

        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
