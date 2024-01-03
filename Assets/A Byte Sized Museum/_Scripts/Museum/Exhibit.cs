using System.Linq;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Exhibit : MonoBehaviour
    {
        [SerializeField] private ExhibitDoor[] doors;
        [SerializeField] private bool isExhibitLocked;

        private bool isPuzzleSolved;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isExhibitLocked = !isExhibitLocked;

                

                // ToggleDoors(isExhibitLocked);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // isExhibitLocked = true;
            // ToggleDoors(isExhibitLocked);
        }

        private void OnTriggerExit(Collider other)
        {
            // isExhibitLocked = false;
            // ToggleDoors(isExhibitLocked);
            
        }

        private void ToggleDoors(bool isOpen)
        {
            foreach (var door in doors)
            {
                door.isLocked = isOpen;
            }
        }

        public void SetPuzzleSolved(bool isPuzzleSolved)
        {
            this.isPuzzleSolved = isPuzzleSolved;
        }
    }
}
