using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class DialogSystem : MonoSingleton<DialogSystem>
{
    public List<Dialog> gameDialogs;

    protected override void InternalInit()
    {
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
        foreach (Dialog currentDialog in gameDialogs)
        {
            if (currentDialog.isOpen)
            {
                currentDialog.Close();
            }
        }
    }

}
