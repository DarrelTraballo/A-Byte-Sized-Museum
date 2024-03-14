using System.Collections;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class opendoor : MonoBehaviour
    {
        private bool OPENDOOR = true;
        public GameObject opendoor1;
        public GameObject closedoor;
        public GameObject DialogueContainerUnlock;
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
                    DialogueContainerUnlock.SetActive(true);
                    closedoor.SetActive(false);
                    opendoor1.SetActive(true);

                    // Start a coroutine to wait for 5 seconds and then set DialogueContainerUnlock to false
                    StartCoroutine(DisableDialogueContainer());
                }
            }
        }

        IEnumerator DisableDialogueContainer()
        {
            // Wait for 5 seconds
            yield return new WaitForSeconds(5f);

            // Set DialogueContainerUnlock to false
            DialogueContainerUnlock.SetActive(false);
        }
    }
}
