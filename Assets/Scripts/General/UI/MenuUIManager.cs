using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase;
using Firebase.Database;
using Game;
using Shapes2D;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    private void Awake()
    {
        GroupLoader.Instance.Cleanup();
        SyncData();
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