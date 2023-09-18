using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Player variables")]
    public Transform interactorSource;
    public float interactRange;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out InteractableBase interactObj))
                {
                    interactObj.OnInteract();
                }
            }
        }    
    }
}
