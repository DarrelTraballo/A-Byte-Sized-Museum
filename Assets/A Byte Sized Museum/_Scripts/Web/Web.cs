using System;
using System.Collections;
using System.Data.Common;
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
        // idk if need pa 'to, nasa tutorial lang so ginaya ko lang
        private readonly string webURL = "http://www.example.com";
        private readonly string errorWebURL = "http://error.html";

        // eto mga need yung URLs
        private readonly string registerUserURL = "https://net-otaku.com/api/Kachow/registerUser.php";
        private readonly string sendTimeURL = "https://net-otaku.com/api/Kachow/sendTime.php";

        private readonly string getDateURL = "https://net-otaku.com/api/Kachow/GetDate.php";
        private readonly string getuserURL = "https://net-otaku.com/api/Kachow/db_connect.php";
        public string firstName;
        public string lastName;
        public string section;
        public string game_time;

        // AND also change yung mga string values sa mga form.AddValue().
        // first argument is yung name niya ng column sa db
        // second argument is yung galing dito na ipapasa mo sa db

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


        public void storedata(string firstName, string lastName, string section)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.section = section;

            StartCoroutine(Login(firstName,lastName,section));
        }
        public void storeTime(TimeSpan stopTime)
        {
            this.game_time = stopTime.ToString(@"mm\:ss");

            StartCoroutine(SendTime(firstName,section,game_time));
        }


        private void Start()
        {
            // A correct website page.
            // StartCoroutine(GetRequest(testingDate));
            //StartCoroutine(GetDate());
            //StartCoroutine(GetUser());
            //StartCoroutine(Login("Darrel","Traballo","IV-ACSAD"));
            //StartCoroutine(Login);

            // A non-existing page.
            // StartCoroutine(GetRequest(errorWebURL));
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

                // Handle the response text as needed
            }
            

        }



        public IEnumerator SendTime(String firstName, String section, String game_time)
        {

            Debug.Log("Stored Username in Send Time: " + GetStoredUsername());
            WWWForm form = new WWWForm();
            // make sure yung time column sa db is naka set sa string or anything similar
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
        
                // public IEnumerator GetRequest(string uri)
        // {
        //     using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        //     {
        // Request and wait for the desired page.
        //yield return webRequest.SendWebRequest();

                // string[] pages = uri.Split('/');
                // int page = pages.Length - 1;

                // switch (webRequest.result)
                // {
                //     case UnityWebRequest.Result.ConnectionError:
                //     case UnityWebRequest.Result.DataProcessingError:
                //         Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                //         break;
                //     case UnityWebRequest.Result.ProtocolError:
                //         Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                //         break;
                //     case UnityWebRequest.Result.Success:
                //         Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                //         break;
                // }
        //     }
        // }
    }
}
