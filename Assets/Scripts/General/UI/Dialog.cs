using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public string id;
    public float openTime;
    public Ease openEase;
    public float closeTime;
    public Ease closeEase;
    public bool isOpen = false;
    public Image fadeBackground;
    public bool isHaveBackground = false;
    public Canvas canvas;
    public List<Button> buttons;

    public virtual void Init()
    {
        transform.localScale = Vector3.zero;
        transform.gameObject.SetActive(true);
    }
    
    public void Open(bool isHaveBackground = false, bool isCloseCurrentDialog = false)
    {
        if (isOpen) return;
        isOpen = true;

        this.isHaveBackground = isHaveBackground;
        
        if(DialogSystem.instance) DialogSystem.instance.AddDialog(this);
        Init();
        transform.DOScale(new Vector3(1, 1, 1), openTime).SetEase(openEase).OnComplete(Intro).SetUpdate(true);
        AudioManager.instance.PlayEffect("DialogOpen");
        if (isHaveBackground)
        {
            FadeInBackground();
        }
    }

    public virtual void Intro()
    {
    }

    public void Close()
    {
        isOpen = false;
        UnbindAllButtons();
        if (DialogSystem.instance) DialogSystem.instance.RemoveTopDialog();
        transform.DOScale(Vector3.zero, closeTime).SetEase(closeEase).OnStart(Outro).OnComplete(() =>
        {
            transform.gameObject.SetActive(false);
        }).SetUpdate(true);
        AudioManager.instance.PlayEffect("DialogClose");
        if (isHaveBackground)
        {
            FadeOutBackground();
        }
    }

    private void FadeInBackground()
    {
        fadeBackground.gameObject.SetActive(true);
        fadeBackground.DOFade(0.75f, 0.2f).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    private void FadeOutBackground()
    {
        fadeBackground.DOFade(0, 0.2f).SetEase(Ease.InQuad).SetUpdate(true).OnComplete(() =>
        {
            fadeBackground.gameObject.SetActive(false);
        });
    }

    public void UnbindAllButtons()
    {
        
    }
    public virtual void Outro()
    {
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
