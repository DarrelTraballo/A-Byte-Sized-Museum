using System.Collections;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class opendoor : MonoBehaviour
    {
        public GameObject[] doors;
        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        public void Activate()
        {
            foreach (var door in doors)
            {
                door.SetActive(false);
            }

            StartCoroutine(gameManager.SetToolTipTextCoroutine("Puzzle Solved!", "<color=yellow>Exit Unlocked!</color>"));
        }
    }
}
