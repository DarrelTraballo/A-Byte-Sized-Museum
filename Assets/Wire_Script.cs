using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

namespace KaChow.AByteSizedMuseum
{
    public class Wire_Script : MonoBehaviour
    {
        public SpriteRenderer wireEnd;
        Vector3 startPoint;
        Vector3 startPosition;
        public GameObject lightOn;

        public Main main;

        // Start is called before the first frame update
        void Start()
        {
            startPoint = transform.parent.position;
            startPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnMouseDrag() 
        {
            //Mouse position to world point
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;

            // check for nearby connection points
            Collider2D[]colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
            
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject != gameObject)
                {
                    // put them back to their position
                    UpdateWire(collider.transform.position);

                    // checks if the wires are same color
                    if (transform.parent.name.Equals(collider.transform.parent.name))
                    {
                        collider.GetComponent<Wire_Script>()?.light_on();
                        Main.Instance.updateCount();
                        light_on();
                        

                    }
                    return;
                }


                
            }
            UpdateWire(newPosition);    

        void DONE()
        {
            // turn on the light
            lightOn.SetActive(true);
            
            // destroy the script/ connects the wire to the same color
            //Destroy(this);
        }
        }
        void light_on()
        {
            // turn on the light
            lightOn.SetActive(true);

            // destroy the script/ connects the wire to the same color
            Destroy(this);
        }
        
        private void OnMouseUp() {
            UpdateWire(startPosition);
        }

        void UpdateWire(Vector3 newPosition)
        {
            //update wire position
            transform.position = newPosition;

            // update direction
           Vector3 direction = newPosition - startPoint;
            transform.right = direction * transform.lossyScale.x;
       
            // update scale

            float dist = Vector2.Distance(startPoint, newPosition);
            wireEnd.size = new Vector2(dist/2, wireEnd.size.y);

        }
        
    }
}
