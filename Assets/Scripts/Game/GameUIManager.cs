using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameUIManager : MonoSingleton<GameUIManager>
{
    public ItemsListHUD itemsListHUD;
    public GameTimer gameTimer;
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

    private void MoveOutHUD()
    {
        
    }
    
}
