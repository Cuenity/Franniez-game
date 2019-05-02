using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using TwitterKit.Unity;
using System;

public class FaceBookScript : MonoBehaviour
{
    private void Awake()
    {
        Twitter.Init();
    }

    public void FacebookLogin()
    {
        StartLogin();
    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }

    public void FacebookShare()
    {
        String path = "https://franniez.nl/themes/franniez/assets/images/header.jpg";
        string message = "Yeah ik heb level " + GameState.Instance.PreviousLevel.ToString() + " gehaald met " + PlayerDataController.instance.player.levels[GameState.Instance.PreviousLevel].countCoins + "!";
        Twitter.Compose(Twitter.Session, path, message, new string[] { "#Franniez", "#ThisIsHowIRoll", "#TIHIR" },
                (string tweetId) => { UnityEngine.Debug.Log("Tweet Success, tweetId = " + tweetId); },
                (ApiError error) => { UnityEngine.Debug.Log("Tweet Failed " + error.message); },
                () => { Debug.Log("Compose cancelled"); }
        );
    }

    public void StartLogin()
    {
        TwitterSession session = Twitter.Session;
        if (session == null)
        {
            Twitter.LogIn(LoginComplete, LoginFailure);
        }
        else
        {
            LoginComplete(session);
        }
    }

    public void LoginComplete(TwitterSession session)
    {
        StartComposer(Twitter.Session, "https://franniez.nl/themes/franniez/assets/images/header.jpg");
    }

    public void LoginFailure(ApiError error)
    {
        UnityEngine.Debug.Log("code=" + error.code + " msg=" + error.message);
    }

    public void StartComposer(TwitterSession session, String imageUri)
    {
        

        Twitter.Compose(session, imageUri, "My new high score!", new string[] { "#SpaceShooter" },
                (string tweetId) => { UnityEngine.Debug.Log("Tweet Success, tweetId = " + tweetId); },
                (ApiError error) => { UnityEngine.Debug.Log("Tweet Failed " + error.message); },
                () => { Debug.Log("Compose cancelled"); }
        );
    }


}
