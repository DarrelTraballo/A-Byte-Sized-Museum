using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MiniMap : MonoBehaviour
    {
        private Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            Vector3 newPos = player.position;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }
    }
}
