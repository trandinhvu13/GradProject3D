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
        musicVolume.onValueChanged.AddListener((value) =>
        {
            AudioManager.instance.ChangeVolumeMusic(value);
        });
        
        effectVolume.onValueChanged.AddListener((value) =>
        {
            AudioManager.instance.ChangeVolumeEffect(value);
        });
        
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
        
        signOutButton.onClick.AddListener((() =>
        {
            FirebaseManager.instance.SignOut();
            SceneController.instance.Load("Login", Close, null);
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
