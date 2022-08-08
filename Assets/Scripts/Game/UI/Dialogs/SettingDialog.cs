using Game.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class SettingDialog : Dialog
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button changeUsernameButton;
        [SerializeField] private Button changeEmailButton;
        [SerializeField] private Button changePasswordButton;
        [SerializeField] private Button deleteAccountButton;
        [SerializeField] private Button signOutButton;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private Slider musicVolume;
        [SerializeField] private Slider effectVolume;

        private bool isFullscreen = true;

        public override void Init()
        {
            base.Init();
            musicVolume.value = AudioManager.instance.GetMusicVolume();
            effectVolume.value = AudioManager.instance.GetEffectVolume();
        
            musicVolume.onValueChanged.AddListener((value) => { AudioManager.instance.ChangeVolumeMusic(value); });

            effectVolume.onValueChanged.AddListener((value) => { AudioManager.instance.ChangeVolumeEffect(value); });

            fullscreenToggle.onValueChanged.AddListener((value) =>
            {
                Debug.Log(value);
                if (value)
                {
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                }
                else
                {
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                }
            });

            backButton.onClick.AddListener(Close);
            changeEmailButton.onClick.AddListener(() =>
            {
                DialogSystem.instance.GetDialog("UpdateEmailDialog").Open(true);
            });

            changeUsernameButton.onClick.AddListener(() =>
            {
                DialogSystem.instance.GetDialog("UpdateUsernameDialog").Open(true);
            });

            changePasswordButton.onClick.AddListener(() =>
            {
                DialogSystem.instance.GetDialog("UpdatePasswordDialog").Open(true);
            });

            deleteAccountButton.onClick.AddListener(() =>
            {
                DialogSystem.instance.GetDialog("DeleteAccountConfirmDialog").Open(true);
            });

            signOutButton.onClick.AddListener((() =>
            {
                DialogSystem.instance.GetDialog("LogoutConfirmDialog").Open(true);
            }));
        }

        public override void Outro()
        {
            base.Outro();
            musicVolume.onValueChanged.RemoveAllListeners();
            effectVolume.onValueChanged.RemoveAllListeners();
            fullscreenToggle.onValueChanged.RemoveAllListeners();
        }
    }
}