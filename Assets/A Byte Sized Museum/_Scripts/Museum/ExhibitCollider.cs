using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class ExhibitCollider : MonoBehaviour
    {
        [SerializeField]
        private GameObject tileContents;

        public bool hasPlayerEntered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tileContents.SetActive(true);
                hasPlayerEntered = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tileContents.SetActive(false);
                hasPlayerEntered = false;
            }
        }
    }
}
