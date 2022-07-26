using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine.UI;

public class FirebaseManager : MonoSingleton<FirebaseManager>
{
    //Firebase variables
    [Header("Firebase")] public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference dbreference;

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
        dbreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public IEnumerator Login(string _email, string _password, LoginDialog loginDialog = null)
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

    public IEnumerator Register(string _username, string _email, string _password, RegisterDialog registerDialog = null)
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
    
    public IEnumerator SaveScoreLevel(Score score)
    {
        string userKey = user.UserId;
        int levelID = score.levelID;
        float levelScore = score.score;
        int star = score.star;
        
        var DBTask1 = dbreference.Child("levels").Child(levelID.ToString()).Child(userKey).Child("score").SetValueAsync(levelScore);
        var DBTask2 = dbreference.Child("levels").Child(levelID.ToString()).Child(userKey).Child("star").SetValueAsync(star);
        
        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);
        yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);
        
        if (DBTask1.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask1.Exception}");
        }
        else
        {
            Debug.Log($"Done upload score for level {levelID}:{levelScore}");
        }
        
        if (DBTask2.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask2.Exception}");
        }
        else
        {
            Debug.Log($"Done upload star for level {levelID}:{star} star");
        }
    }

    public Score GetPlayerHighScoreInLevel(int levelID)
    {
        return new Score();
    }

    public List<Score> ReturnLevelTop3(int levelID)
    {
        return new List<Score>();
    }

    public int GetPlayerPos(int levelID)
    {
        return 0;
    }

    private IEnumerator GetUserData()
    {
        //Get the currently logged in user data
        var DBTask = dbreference.Child("users").Child(user.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            xpField.text = "0";
            killsField.text = "0";
            deathsField.text = "0";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            xpField.text = snapshot.Child("xp").Value.ToString();
            killsField.text = snapshot.Child("kills").Value.ToString();
            deathsField.text = snapshot.Child("deaths").Value.ToString();
        }
    }

    public List<int> GetPlayerStarForEachLevel(string userKey)
    {
        
        return new List<int>();
    }

    public IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = user.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            Debug.Log($"Done upload user auth {_username}");
        }        
    }
    
    private IEnumerator UpdateUsersDatabase(string userKey, string username, string email, int currentLevel)
    {
        //Set the currently logged in user username in the database user.UserId
        var DBTask1 = dbreference.Child("users").Child(userKey).Child("username").SetValueAsync(username);
        var DBTask2 = dbreference.Child("users").Child(userKey).Child("email").SetValueAsync(email);
        var DBTask3 = dbreference.Child("users").Child(userKey).Child("currentLevel").SetValueAsync(currentLevel);

        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);
        yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);
        yield return new WaitUntil(predicate: () => DBTask3.IsCompleted);

        if (DBTask1.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask1.Exception}");
        }
        else
        {
            Debug.Log($"Done upload user with username: {username}");
        }
        
        if (DBTask2.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask2.Exception}");
        }
        else
        {
            Debug.Log($"Done upload user with email: {email}");
        }
        
        if (DBTask3.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask3.Exception}");
        }
        else
        {
            Debug.Log($"Done upload user with current level: {currentLevel}");
        }
    }
}

public class Score
{
    public string userKey;
    public int levelID;
    public float score;
    public int star;

    public Score(string userKey = null, int levelID = 0 , float score = 0, int star = 0)
    {
        this.userKey = userKey;
        this.levelID = levelID;
        this.score = score;
        this.star = star;
    }
}