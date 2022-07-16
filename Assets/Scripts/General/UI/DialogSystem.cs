using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoSingleton<DialogSystem>
{
    public List<Dialog> gameDialogs;
    private Stack<Dialog> currentDialogs;
    
    protected override void InternalInit()
    {
        currentDialogs = new Stack<Dialog>();
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

    public void AddDialog(Dialog newDialog)
    {
        currentDialogs.Push(newDialog);

        int dialogsCount = currentDialogs.Count;
        if (dialogsCount>0)
        {
            foreach (Dialog dialog in currentDialogs)
            {
                dialog.canvas.sortingOrder = 2 + dialogsCount;
                dialogsCount--;
            }
        }
    }

    public void CloseTopDialog()
    {
        currentDialogs.Peek().Close();
    }
    
    public void RemoveTopDialog()
    {
        if (currentDialogs.Count <= 0) return;
        currentDialogs.Pop();
    }
    
}
