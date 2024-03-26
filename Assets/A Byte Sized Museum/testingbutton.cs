using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class testingbutton : MonoBehaviour
    {
        public Button button;
        // Start is called before the first frame update
        void Start()
        {
        
            button.onClick.AddListener(buttonclick);

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void buttonclick()
        {
            Debug.Log("BUTTON IS CLICKED");
        }
        
        
    }
}
