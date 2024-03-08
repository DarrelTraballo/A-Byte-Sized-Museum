using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class opendoor : MonoBehaviour
    {
        private bool OPENDOOR = true;
        public GameObject opendoor1;
        public GameObject closedoor;
        private Activator activator;

        // Start is called before the first frame update
        void Start()
        {
            closedoor.SetActive(true);
            opendoor1.SetActive(false);

            // Assuming the Activator component is on the same GameObject, you can use GetComponent
            activator = GetComponent<Activator>();
            if (activator == null)
            {
                Debug.LogError("Activator component not found on the same GameObject as opendoor script.");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (activator != null)
            {
                OPENDOOR = activator.tutorial;
                if (OPENDOOR == false)
                {
                    closedoor.SetActive(false);
                    opendoor1.SetActive(true);
                }
            }
        }
    }
}
