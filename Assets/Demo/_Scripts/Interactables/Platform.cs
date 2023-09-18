using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : InteractableBase
{
    public string platformInteractionMessage = "Press [Space] to jump.";

    private void Start()
    {
        onEnterInteractMessage = platformInteractionMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
