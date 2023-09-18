using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPuzzle : InteractableBase
{
    // TODO: AMONG US FIX WIRING : ))))
    public string wallPuzzleInteractMessage = "Press [E] to interact.";

    private void Start()
    {
        onEnterInteractMessage = wallPuzzleInteractMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}
