using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class loadmanager : MonoBehaviour
    {
        public GameObject loadingscenecanvas;
        void OnEnable()
        {
        loadingscenecanvas.SetActive(true);
        FindObjectOfType<LoadingSceneScript>().callscene();
        
        }


    }
}
