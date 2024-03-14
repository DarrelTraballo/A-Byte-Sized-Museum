using System.Text.RegularExpressions;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public abstract class InteractableBase : MonoBehaviour
    {
        protected GameManager gameManager;
        protected InputManager inputManager;

        protected string subtitleText;
        protected float subtitleDelay;

        protected string className;

        public virtual void Start()
        {
            gameManager = GameManager.Instance;
            inputManager = InputManager.Instance;
            subtitleText = "Press E to INTERACT";
            subtitleDelay = 1.5f;

            className = GetType().Name;
        }

        /// <summary>
        /// Virtual method triggered when an actor enters the trigger collider of the interactable object. Override to implement specific behavior.
        /// </summary>
        /// <param name="actor">The collider of the actor that entered.</param>
        protected virtual void OnTriggerEnter(Collider actor) { }

        /// <summary>
        /// Virtual method triggered when an actor exits the trigger collider of the interactable object. Override to implement specific behavior.
        /// </summary>
        /// <param name="actor">The collider of the actor that exited.</param>
        protected virtual void OnTriggerExit(Collider actor) { }

        /// <summary>
        /// Called when the player's gaze enters the interactable object. Displays a tooltip with the object's name and a subtitle.
        /// </summary>
        public virtual void OnLookEnter()
        {
            string className = this.className;
            string formattedClassName = Regex.Replace(className, "(\\B[A-Z])", " $1");
            StartCoroutine(gameManager.SetToolTipText(formattedClassName, subtitleText, subtitleDelay));
        }

        /// <summary>
        /// Called when the player's gaze exits the interactable object. Override to implement specific behavior, such as hiding the tooltip.
        /// </summary>
        public virtual void OnLookExit()
        {
            // gameManager.DisableToolTipText();
        }

        /// <summary>
        /// Base method for handling interactions with the object. Plays a click sound effect by default. Override to implement specific interaction behavior.
        /// </summary>
        public virtual void OnInteract()
        {
            AudioManager.Instance.PlaySFX("Click");
        }
    }
}
