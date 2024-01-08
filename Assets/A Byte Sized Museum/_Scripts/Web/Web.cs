using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace KaChow.AByteSizedMuseum
{
    public class BypassCertificateHandler : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            // Always return true to bypass certificate validation
            return true;
        }
    }

    public class Web : MonoBehaviour
    {
        private readonly string webURL = "http://www.example.com";
        private readonly string errorWebURL = "http://error.html";

        private readonly string registerUserURL = "https://net-otaku.com/api/Kachow/registerUser.php";
        private readonly string sendTimeURL = "https://net-otaku.com/api/Kachow/sendTime.php";

        private readonly string getDateURL = "https://net-otaku.com/api/Kachow/GetDate.php";
        private readonly string getuserURL = "https://net-otaku.com/api/Kachow/db_connect.php";

        private string firstName;
        private string lastName;
        private string section;
        private string game_time;

        public string StoredUsernameKey;

        // Function to store the username in PlayerPrefs
        public void StoreUsername(string username)
        {
            PlayerPrefs.SetString(StoredUsernameKey, username);
            PlayerPrefs.Save();
        }

        // Function to retrieve the stored username from PlayerPrefs
        public string GetStoredUsername()
        {
            return PlayerPrefs.GetString(StoredUsernameKey, "");
        }


        public void Storedata(string firstName, string lastName, string section)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.section = section;

            StartCoroutine(Login(firstName, lastName, section));
        }
        public void StoreTime(TimeSpan stopTime)
        {
            this.game_time = stopTime.ToString(@"mm\:ss");

            StartCoroutine(SendTime(firstName, section, game_time));
        }

        private IEnumerator GetDate()
        {
            using (UnityWebRequest www = UnityWebRequest.Get(getDateURL))
            {
                www.certificateHandler = new BypassCertificateHandler();
                www.timeout = 10;
                yield return www.Send();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);

                    byte[] result = www.downloadHandler.data;
                }
            }
        }
        private IEnumerator GetUser()
        {
            using (UnityWebRequest www = UnityWebRequest.Get(getuserURL))
            {
                www.certificateHandler = new BypassCertificateHandler();
                www.timeout = 10;
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);

                    byte[] result = www.downloadHandler.data;
                }
            }
        }

        public IEnumerator Login(string firstName, string lastName, string section)
        {
            WWWForm form = new WWWForm();

            form.AddField("firstName", firstName);
            form.AddField("lastName", lastName);
            form.AddField("student_section", section);

            Debug.Log("Sending login request...");
            Debug.Log("First Name: " + firstName);
            Debug.Log("Last Name: " + lastName);
            Debug.Log("Section: " + section);

            StoreUsername(firstName);

            UnityWebRequest www = UnityWebRequest.Post(registerUserURL, form);
            www.certificateHandler = new BypassCertificateHandler();
            www.timeout = 10;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Login request failed: " + www.error);
                Debug.LogError("HTTP Response Code: " + www.responseCode);
                Debug.LogError("MAY ERROR");
            }
            else
            {
                Debug.Log("Login request successful");
                string responseText = www.downloadHandler.text;
                Debug.Log("Response: " + responseText);
                Debug.Log("Stored Username: " + GetStoredUsername());
            }
        }

        public IEnumerator SendTime(String firstName, String section, String game_time)
        {

            Debug.Log("Stored Username in Send Time: " + GetStoredUsername());
            WWWForm form = new WWWForm();

            form.AddField("game_time", game_time);
            form.AddField("firstName", firstName);
            form.AddField("student_section", section);

            Debug.Log("Sending update request...");
            Debug.Log("First Name: " + firstName);
            Debug.Log("Section: " + section);
            Debug.Log("Game Time: " + game_time);

            UnityWebRequest www = UnityWebRequest.Post(sendTimeURL, form);
            www.certificateHandler = new BypassCertificateHandler();
            www.timeout = 10;
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
