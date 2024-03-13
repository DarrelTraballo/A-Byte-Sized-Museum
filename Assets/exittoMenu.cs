using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class exittoMenu : MonoBehaviour
    {
        public GameObject LoadingScreenCanvas;
        // Start is called before the first frame update
        public void exittomenu()
        {
            FindObjectOfType<LoadingSceneScript>().LoadScene(3);
            LoadingScreenCanvas.SetActive(true);
        }

        // Update is called once per frame
    }
}
