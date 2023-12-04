using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class RandomizeChildren : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void Awake() {
            // this is for swapping position wire
            for(int i = 0; i < transform.childCount; i++)
            {
                int newSpot = Random.Range(0, transform.childCount);
                Vector3 temp = transform.GetChild(i).position;
                transform.GetChild(i).position = transform.GetChild(newSpot).position;
                transform.GetChild(newSpot).position = temp;
            }

        }
    }
}
