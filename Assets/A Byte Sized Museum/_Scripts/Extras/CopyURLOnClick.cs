using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class CopyURLOnClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text promptText;
        private const string URL = "https://forms.gle/HDD3hEETUdLsgt8H7";

        private void Start()
        {
            promptText.enabled = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GUIUtility.systemCopyBuffer = URL;

            promptText.enabled = true;

            Debug.Log("Survey Link Copied to Clipboard!");
        }
    }
}
