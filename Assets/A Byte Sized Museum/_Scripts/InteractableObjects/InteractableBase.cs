using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class InteractableBase : MonoBehaviour
    {
        // what happens if player enters interactable object collider
        protected virtual void OnTriggerEnter(Collider actor) 
        {
            if (actor.CompareTag("Player"))
            {
            }
        }

        // what happens if player exits interactable object collider
        protected virtual void OnTriggerExit(Collider actor) 
        {
            if (actor.CompareTag("Player"))
            {
            }
        }

        // base method for interactable object specific interactions
        // just leave empty 
        public virtual void OnInteract()
        {

        }
    }
}
