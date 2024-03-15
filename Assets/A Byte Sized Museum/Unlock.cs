using System.Collections;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class unlock : MonoBehaviour
    {

        public GameObject DialogueContainerUnlock;


        // Start is called before the first frame update
      
        // Update is called once per frame
        void OnEnable()
        {
           activate();
        }

        private void activate()
        {
        
            DialogueContainerUnlock.SetActive(true);
                // Start a coroutine to wait for 5 seconds and then set DialogueContainerUnlock to false
            StartCoroutine(DisableDialogueContainer());
                
            
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
