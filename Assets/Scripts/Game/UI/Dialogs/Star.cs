using DG.Tweening;
using UnityEngine;

namespace Game.UI.Dialogs
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private GameObject starOn;

        public float showTime;
        [SerializeField] private Ease showEase;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
        }

        public void Show(bool isOn)
        {
            starOn.SetActive(isOn);

            transform.DOScale(new Vector3(1, 1, 1), showTime).SetEase(showEase);
        }
    }
}
