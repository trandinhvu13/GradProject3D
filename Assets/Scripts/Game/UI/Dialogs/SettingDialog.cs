using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Main;
using UnityEngine;
using UnityEngine.UI;

public class SettingDialog : Dialog
{
    public Button backButton;
    public Button changeUsernameButton;
    public Button changeEmailButton;
    public Button changePasswordButton;
    public Button deleteAccountButton;
    public Button signOutButton;
    public Toggle fullscreenToggle;
    public Slider musicVolume;
    public Slider effectVolume;

    private bool isFullscreen = true;

    public override void Init()
    {
        base.Init();
        musicVolume.value = 1;
        effectVolume.value = 1;
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