using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class ExhibitCollider : MonoBehaviour
    {
        [SerializeField]
        private GameObject tileContents;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tileContents.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tileContents.SetActive(false);
            }
        }
    }
}
