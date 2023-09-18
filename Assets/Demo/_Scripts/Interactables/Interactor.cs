using UnityEngine;

interface IInteractable
{
    public void OnInteract();
}

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.OnInteract();
                }
            }
        }    
    }
    
}
