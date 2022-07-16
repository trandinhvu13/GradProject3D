using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Main
{
    public class SceneController : MonoSingleton<SceneController>
    {
        #region Variables

        private AsyncOperation loadingAsyncOperation = null;
        [SerializeField] private TransitionScreen transitionScreen;

        #endregion

        #region Singleton Methods

        protected override void InternalInit()
        {
            /*testButton.onClick.AddListener(() =>
            {
                Load("Main", null, () => { GameUIManager.instance.gameTimer.StartTime(); });
            });*/
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

        #endregion Singleton Methods

        #region Methods

        public void Load(string scene, Action actionIntro = null, Action actionOutro = null)
        {
            StartCoroutine(LoadSceneAsync(scene, actionIntro, actionOutro));
        }

        private IEnumerator LoadSceneAsync(string scene, Action actionIntro = null, Action actionOutro = null)
        {
            // trigger the transition screen
            transitionScreen.Intro(() => { actionIntro?.Invoke(); });
            yield return new WaitForSecondsRealtime(transitionScreen.GetTweenTime() + 1f);

            loadingAsyncOperation = SceneManager.LoadSceneAsync(scene);

            loadingAsyncOperation.allowSceneActivation = false;

            while (!loadingAsyncOperation.isDone)
            {
                if (loadingAsyncOperation.progress >= 0.9f)
                {
                    loadingAsyncOperation.allowSceneActivation = true;

                    yield return new WaitUntil(() => LevelManager.instance);
                    yield return new WaitUntil(() => LevelManager.instance.isLevelLoad);
                    transitionScreen.Outro(() => { actionOutro?.Invoke(); });
                }

                yield return null;
            }
        }

        public float GetLoadingProgress()
        {
            if (loadingAsyncOperation != null)
            {
                return loadingAsyncOperation.progress;
            }
            else
            {
                return 1f;
            }
        }

        #endregion Methods
    }
}