using System.Linq;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Exhibit : MonoBehaviour
    {
        [SerializeField] private ExhibitDoor[] doors;
        [SerializeField] private bool isExhibitLocked;

        private void Start()
        {

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isExhibitLocked = !isExhibitLocked;
                ToggleDoors(isExhibitLocked);
                Debug.Log($"Doors {(isExhibitLocked ? "Locked" : "Unlocked")}!");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Player Inside *FLUSH*");

            isExhibitLocked = true;
            ToggleDoors(isExhibitLocked);
        }

        private void ToggleDoors(bool isOpen)
        {
            foreach (var door in doors)
            {
                door.isLocked = isOpen;
                // door.doors.ToList().ForEach(openDoor => openDoor.gameObject.SetActive(isOpen));
            }
        }
    }
}
