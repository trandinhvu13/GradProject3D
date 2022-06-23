using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ItemsHUD : MonoBehaviour
{
    [SerializeField] private Image image;

    public void Setup(Sprite sprite)
    {
        image.sprite = sprite;
        GetUncollected();
    }
    
    public void GetUncollected()
    {
        image.DOColor(new Color(255, 255, 255, 255), 0.2f);
    }
    public void GetCollected()
    {
        image.DOColor(new Color(0, 0, 0, 150), 0.2f);
    }
}
