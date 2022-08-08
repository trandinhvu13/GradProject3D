using System;
using System.Collections;
using Game.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Game
{
    public class SceneController : MonoSingleton<SceneController>
    {
        #region Variables

        private AsyncOperation loadingAsyncOperation;
        [SerializeField] private TransitionScreen transitionScreen;

        #endregion

        #region Singleton Methods

        protected override void InternalInit()
        {
            AudioManager.instance.PlayMusic("LoginScreen");
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
            AudioManager.instance.FadeOutMusic();
            transitionScreen.Intro(() => { actionIntro?.Invoke(); });
            yield return new WaitForSecondsRealtime(transitionScreen.GetTweenTime() + 1f);

            loadingAsyncOperation = SceneManager.LoadSceneAsync(scene);

            loadingAsyncOperation.allowSceneActivation = false;

            while (!loadingAsyncOperation.isDone)
            {
                if (loadingAsyncOperation.progress >= 0.9f)
                {
                    loadingAsyncOperation.allowSceneActivation = true;

                    if (scene == "Main")
                    {
                        yield return new WaitUntil(() => LevelManager.instance);
                        yield return new WaitUntil(() => LevelManager.instance.isLevelLoad);
                    }

                    transitionScreen.Outro(() => { actionOutro?.Invoke(); });

                    if (scene == "Main")
                    {
                        int rand = Random.Range(1, 4);
                        AudioManager.instance.PlayMusic("GameScreen" + rand);
                    }
                    else if (scene == "Login")
                    {
                        AudioManager.instance.PlayMusic("LoginScreen");
                    }
                    else if (scene == "Menu")
                    {
                        AudioManager.instance.PlayMusic("MenuScreen");
                    }

                    AudioManager.instance.FadeInMusic();
                }

                yield return null;
            }
        }

        #endregion Methods
    }
}