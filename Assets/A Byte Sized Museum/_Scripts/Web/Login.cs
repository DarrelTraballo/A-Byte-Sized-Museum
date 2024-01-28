using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class Login : MonoBehaviour
    {

        [SerializeField] private TMP_InputField firstNameInputField;
        [SerializeField] private TMP_InputField lastNameInputField;
        [SerializeField] private TMP_InputField sectionInputField;

        [SerializeField] private Button loginButton;

        private void Start()
        {
            loginButton.onClick.AddListener(() =>
            {
                string firstNameText = firstNameInputField.text;
                string lastNameText = lastNameInputField.text;
                string sectionText = sectionInputField.text;

                WebManager.Instance.Web.Storedata(firstNameText, lastNameText, sectionText);

                gameObject.SetActive(false);
            });
        }
    }
}
