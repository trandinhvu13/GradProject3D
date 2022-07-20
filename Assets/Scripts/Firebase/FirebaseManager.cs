using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.UI;

public class FirebaseManager : MonoSingleton<FirebaseManager>
{
    //Firebase variables
    [Header("Firebase")] public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public bool isSignedIn = false;

    //Login variables
    [Header("Login")] public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    public Button loginButton;

    //Register variables
    [Header("Register")] public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;
    public Button registerButton;

    protected override void InternalInit()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
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

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }

    public IEnumerator Login(string _email, string _password, LoginDialog loginDialog=null)
    {
        Credential credential = EmailAuthProvider.GetCredential(_email, _password);

        //Call the Firebase auth signin function passing the email and password
        var loginTask = auth.SignInWithCredentialAsync(credential);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Email is empty";
                    break;
                case AuthError.MissingPassword:
                    message = "Password is empty";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account not exist";
                    break;
            }

            if (loginDialog != null)
            {
                StartCoroutine(loginDialog.ShowError(message));
            }
        }
        else
        {
            user = loginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.Email);
            
            if (loginDialog != null)
            {
                StartCoroutine(loginDialog.ShowMessage($"Welcome {user.DisplayName}!"));
            }
        }
    }

    public IEnumerator Register(string _username, string _email, string _password, RegisterDialog registerDialog=null)
    {
        if (_username == "")
        {
            if (registerDialog != null)
            {
                registerDialog.ShowError("Missing username");
            }
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                FirebaseException firebaseEx = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.InvalidEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }

                if (registerDialog != null)
                {
                    StartCoroutine(registerDialog.ShowError(message));
                }
            }
            else
            {
                user = registerTask.Result;

                if (user != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };
                    
                    var profileTask = user.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => profileTask.IsCompleted);

                    if (profileTask.Exception != null)
                    {
                        user.DeleteAsync();
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {profileTask.Exception}");
                        FirebaseException firebaseEx =
                            profileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        string message = "Username Set Failed!";

                        if (registerDialog != null)
                        {
                            StartCoroutine(registerDialog.ShowError(message));
                        }
                    }
                    else
                    {
                        if (registerDialog != null)
                        {
                            StartCoroutine(registerDialog.ShowMessage($"Success!"));
                        }
                    }
                }
            }
        }
    }

    public void SignOut()
    {
        auth.SignOut();
    }
}