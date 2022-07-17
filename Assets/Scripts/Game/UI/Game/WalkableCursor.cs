using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class WalkableCursor : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private bool isShown = true;
        [SerializeField] private SpriteRenderer cursor;

        [SerializeField] private Vector3 scaleIn;
        [SerializeField] private Vector3 scaleOut;
        [SerializeField] private float scaleTime;
        [SerializeField] private Ease scaleTween;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void OnEnable()
        {
            GameEvent.instance.OnClickOnGround += Click;
            GameEvent.instance.OnPlayerLose += Hide;
            GameEvent.instance.OnPlayerWin += Hide;
        }

        private void OnDisable()
        {
            if (GameEvent.instance) GameEvent.instance.OnClickOnGround -= Click;
            if (GameEvent.instance) GameEvent.instance.OnPlayerLose -= Hide;
            if (GameEvent.instance) GameEvent.instance.OnPlayerWin -= Hide;
        }

        private void Update()
        {
            if (LevelManager.instance.state is LevelManager.LevelState.Lose or LevelManager.LevelState.Win or LevelManager.LevelState.Pause) return;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100) && hitInfo.transform.gameObject.CompareTag("Ground"))
            {
                if (!isShown)
                {
                    Cursor.visible = false;
                    isShown = true;
                    cursor.DOFade(1, 0.2f);
                }

                transform.position = hitInfo.point;
            }
            else
            {
                if (isShown)
                {
                    Cursor.visible = true;
                    isShown = false;
                    cursor.DOFade(0, 0.2f);
                }
            }
        }

        private void Click()
        {
            transform.DOScale(scaleIn, scaleTime).SetEase(scaleTween);
            transform.DOScale(scaleOut, scaleTime).SetEase(scaleTween).SetDelay(scaleTime);
        }

        private void Hide()
        {
            Cursor.visible = true;
            transform.gameObject.SetActive(false);
        }

        public void Show()
        {
            Cursor.visible = false;
            transform.gameObject.SetActive(true);
        }
    }
}