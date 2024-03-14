using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Minigame_tutorial : MonoBehaviour
    {
        public dialogmanager dialog_manager;
        public GameObject Canvas;
        // Start is called before the first frame update

        void OnEnable()
        {
            Start();
        }
        void Start()
        {
            Canvas.SetActive(true);
            dialog_manager.helperbot_tutorial = true;

        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
