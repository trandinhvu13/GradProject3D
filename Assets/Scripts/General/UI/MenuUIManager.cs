using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Shapes2D;
using UnityEngine;

public class MenuUIManager : MonoSingleton<MenuUIManager>
{
    protected override void InternalInit()
    {
        GroupLoader.Instance.Cleanup();
        SyncData();
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

    private void SyncData()
    {
        StartCoroutine(FirebaseManager.instance.GetUserLevelData((snapshot) =>
        {
            int childrenCount = 0;

            int currentStar = 0;
            int level = 0;

            if (snapshot != null)
            {
                childrenCount = snapshot.Children.Count();
                foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    currentStar += int.Parse(childSnapshot.Child("star").Value.ToString()) + 1;
                    level++;
                }
            }
            
            PlayerDataManager.instance.currentLevel = childrenCount;
            PlayerDataManager.instance.currentStar = currentStar;
        }));
    }
}