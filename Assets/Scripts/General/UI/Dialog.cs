using DG.Tweening;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public string id;
    public float openTime;
    public Ease openEase;
    public float closeTime;
    public Ease closeEase;
    public bool isOpen = false;
    public bool isHaveBackground = false;

    public virtual void Init()
    {
        transform.localScale = Vector3.zero;
        transform.gameObject.SetActive(true);
    }
    
    public void Open(bool isHaveBackground = false)
    {
        if (isOpen) return;
        this.isHaveBackground = isHaveBackground;
        isOpen = true;
        if(GameUIManager.instance) GameUIManager.instance.currentDialog = this;
        Init();
        transform.DOScale(new Vector3(1, 1, 1), openTime).SetEase(openEase).OnComplete(Intro).SetUpdate(true);
        if (this.isHaveBackground)
        {
            GameUIManager.instance.FadeInBackground();
        }
    }

    public virtual void Intro()
    {
    }

    public void Close()
    {
        isOpen = false;
        if (GameUIManager.instance) GameUIManager.instance.currentDialog = null;
        transform.DOScale(Vector3.zero, closeTime).SetEase(closeEase).OnStart(Outro).OnComplete(() =>
        {
            transform.gameObject.SetActive(false);
        }).SetUpdate(true);
        
        if (isHaveBackground)
        {
            GameUIManager.instance.FadeOutBackground();
        }
    }
    
    public virtual void Outro()
    {
    }
}
