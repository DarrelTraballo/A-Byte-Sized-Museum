using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace KaChow.AByteSizedMuseum
{
    public class LoadingSceneScript : MonoBehaviour
    {
        public GameObject LoadingScreen;
        public Image LoadingBarfill;
        public int LoadingSceneNumber;
        public float delayAfterLoad = 1.0f;

        public void callscene()
        {
            StartCoroutine(LoadSceneWithDelay(LoadingSceneNumber));
        }
        public void LoadScene(int sceneId)
        {
            StartCoroutine(LoadSceneWithDelay(sceneId));
        }


        IEnumerator LoadSceneWithDelay(int sceneId)
        {
            yield return StartCoroutine(LoadSceneAsync(sceneId));
            yield return new WaitForSeconds(delayAfterLoad);

            // Perform additional actions after the delay, if needed
            Debug.Log("Scene loaded and delay completed!");

            // Example: Accessing an Animator component and triggering a parameter
            // Animator animator = GetComponent<Animator>();
            // if (animator != null)
            // {
            //     animator.SetTrigger("YourTrigger");
            // }
        }

        IEnumerator LoadSceneAsync(int sceneId)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
                LoadingBarfill.fillAmount = progressValue;

                yield return null;
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
