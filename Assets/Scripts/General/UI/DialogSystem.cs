using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class DialogSystem : MonoSingleton<DialogSystem>
{
    public List<Dialog> gameDialogs;
    public List<Dialog> currentDialogs;

    protected override void InternalInit()
    {
        currentDialogs = new List<Dialog>();
    }

    protected override void InternalOnDestroy()
    {
    }

    protected override void InternalOnDisable()
    {
    }

    protected override void InternalOnEnable()
    {
    }
    
    public Dialog GetDialog(string id)
    {
        return gameDialogs.Find(x => x.id == id);
    }

    public void CloseAllOpenedDialogs()
    {
        foreach (Dialog currentDialog in currentDialogs)
        {
            currentDialog.Close();
        }
    }

}
