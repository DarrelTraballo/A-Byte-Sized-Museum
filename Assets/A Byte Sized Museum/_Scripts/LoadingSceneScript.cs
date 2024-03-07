using System.Collections;
using System.Collections.Generic;
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

        public void callscene()
        {
            LoadScene(LoadingSceneNumber);
        }
        public void LoadScene(int sceneId)
        {
            StartCoroutine(LoadSceneAsync(sceneId));
        }

        IEnumerator LoadSceneAsync(int sceneId)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

            while(!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
                LoadingBarfill.fillAmount = progressValue;

                yield return null;
            }
            SceneManager.LoadScene("MainMenu");
        }
    }
}
