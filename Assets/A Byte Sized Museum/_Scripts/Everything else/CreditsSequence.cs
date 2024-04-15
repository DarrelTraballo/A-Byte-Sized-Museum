using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class CreditsSequence : MonoBehaviour
    {
        [SerializeField] private GameObject[] panels;
        [SerializeField] private float displayTime = 3f;
        [SerializeField] private float fadeDuration = 1f;

        public void BeginSequence()
        {
            StartCoroutine(DisplayPanels());
        }

        private IEnumerator DisplayPanels()
        {
            foreach (GameObject panel in panels)
            {
                panel.SetActive(true);
                CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();

                yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0, 1, fadeDuration));

                yield return new WaitForSeconds(displayTime);

                if (panel == panels[panels.Length - 1]) yield return null;

                else
                {
                    yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1, 0, fadeDuration));

                    panel.SetActive(false);
                }
            }
        }

        private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end, float duration)
        {
            float time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(start, end, time / duration);
                yield return null;
            }
            canvasGroup.alpha = end;
        }
    }
}
