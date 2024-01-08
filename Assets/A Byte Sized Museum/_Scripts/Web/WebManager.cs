using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class WebManager : MonoBehaviour
    {
        public static WebManager Instance { get; private set; }
        private WebManager() { }

        [HideInInspector] public Web Web;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

            Web = GetComponent<Web>();
        }
    }
}
