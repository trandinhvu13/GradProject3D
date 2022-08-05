using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public string dialogId;
    public float distance;

    private void Update()
    {
        if (LevelManager.instance.player &&
            Vector3.Distance(LevelManager.instance.player.transform.position, transform.position) < distance)
        {
            if (!DialogSystem.instance.GetDialog(dialogId).isOpen)
                DialogSystem.instance.GetDialog(dialogId).Open();
        }
        else
        {
            if (DialogSystem.instance.GetDialog(dialogId).isOpen)
                DialogSystem.instance.GetDialog(dialogId).Close();
        }
    }
}