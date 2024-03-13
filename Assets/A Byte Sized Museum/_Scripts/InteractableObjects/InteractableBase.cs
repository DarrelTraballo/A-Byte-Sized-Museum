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

        public virtual void Start()
        {
            gameManager = GameManager.Instance;
            inputManager = InputManager.Instance;
            subtitleText = "Press E to INTERACT";
            subtitleDelay = 1.5f;
        }

        protected virtual void OnTriggerEnter(Collider actor) { }

        protected virtual void OnTriggerExit(Collider actor) { }

        public virtual void OnLookEnter()
        {
            string className = GetType().Name;
            string formattedClassName = Regex.Replace(className, "(\\B[A-Z])", " $1");
            StartCoroutine(gameManager.SetToolTipText(formattedClassName, subtitleText, subtitleDelay));
        }

        public virtual void OnLookExit()
        {
            // gameManager.DisableToolTipText();
        }

        // base method for interactable object specific interactions
        // just leave empty
        public virtual void OnInteract()
        {
            AudioManager.Instance.PlaySFX("Click");
        }
    }
}
