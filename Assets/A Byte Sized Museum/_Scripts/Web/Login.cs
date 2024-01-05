using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class Login : MonoBehaviour
    {

        [SerializeField] public TMP_InputField firstNameInputField;
        [SerializeField] public TMP_InputField lastNameInputField;
        [SerializeField] public TMP_InputField sectionInputField;

        [SerializeField] public Button loginButton;

        private void Start()
        {
            

            loginButton.onClick.AddListener(() => {
            string firstNameText = firstNameInputField.text;
            string lastNameText = lastNameInputField.text;
            string sectionText = sectionInputField.text;


                // UNCOMMENT IF NAKA SET-UP NA MGA URLS
                WebManager.Instance.Web.storedata(firstNameText, lastNameText, sectionText);

                

                gameObject.SetActive(false);
            });
        }
    }
}
