using System.Collections;
using System.Collections.Generic;
using KaChow.AByteSizedMuseum;
using UnityEngine;



public class DialogueTrigger : MonoBehaviour {
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<dialogmanager>().StartDialogue(dialogue);
        AudioManager.Instance.PlaySFX("HelperBot");
    }

}