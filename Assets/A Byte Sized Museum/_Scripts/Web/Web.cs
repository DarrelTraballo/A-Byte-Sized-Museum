using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace KaChow.AByteSizedMuseum
{
    public class Web : MonoBehaviour
    {
        // web urls
        private readonly string webURL = "https://www.example.com";
        private readonly string errorWebURL = "https://error.html";
        private readonly string loginURL = "https://www.example.com";
        private readonly string sendTimeURL = "https://www.example.com";

        private void Start()
        {
            // A correct website page.
            StartCoroutine(GetRequest(webURL));

            // A non-existing page.
            StartCoroutine(GetRequest(errorWebURL));
        }

        public IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                        break;
                }
            }
        }

        public IEnumerator Login(string firstName, string lastName, string section)
        {
            WWWForm form = new WWWForm();
            //             db field name
            form.AddField("db field name", firstName);
            form.AddField("db field name", lastName);
            form.AddField("db field name", section);

            UnityWebRequest www = UnityWebRequest.Post(loginURL, form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }

        public IEnumerator SendTime(TimeSpan stopTime)
        {
            string stopTimeString = stopTime.ToString(@"mm\:ss");

            WWWForm form = new WWWForm();
            form.AddField("name ng time field sa db", stopTimeString);

            UnityWebRequest www = UnityWebRequest.Post(sendTimeURL, form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
