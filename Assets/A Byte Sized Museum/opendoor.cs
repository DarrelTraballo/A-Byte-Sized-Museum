using System.Collections;
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
           activate();
        }

        private void activate()
        {
            if (activator != null)
            {
                OPENDOOR = activator.tutorial;
                if (OPENDOOR == false)
                {
                    // DialogueContainerUnlock.SetActive(true);
                    closedoor.SetActive(false);
                    opendoor1.SetActive(true);

                    StartCoroutine(GameManager.Instance.SetToolTipTextCoroutine("Puzzle Solved!", "Exit Unlocked!", 5f));
                }
            }
        }
    }
}
