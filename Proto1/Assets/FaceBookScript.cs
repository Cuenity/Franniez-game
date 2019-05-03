using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using TwitterKit.Unity;
using System;

public class TwitterScript : MonoBehaviour
{
    private void Awake()
    {
        Twitter.Init();
    }

    public void TwitterShare()
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

    public void FacebookLogout()
    {
        FB.LogOut();
    }

    public void ComposeMessage()
    {
        string level = GameState.Instance.PreviousLevel.ToString();
        string amountStars = PlayerDataController.instance.player.levels[GameState.Instance.PreviousLevel].countCoins.ToString();
        string stars = "sterren";

        if (PlayerDataController.instance.player.levels[GameState.Instance.PreviousLevel].countCoins == 1)
        {
            stars = "ster";
        }

        string substitution = $"Yeah, ik heb level {level} gehaald met {amountStars} {stars}!";

        String path = "https://franniez.nl/themes/franniez/assets/images/header.jpg";
        string message = "Yeah ik heb level " + GameState.Instance.PreviousLevel.ToString() + " gehaald met " + PlayerDataController.instance.player.levels[GameState.Instance.PreviousLevel].countCoins + " sterren!";
        Twitter.Compose(Twitter.Session, path, substitution, new string[] { "#Franniez", "#ThisIsHowIRoll", "#TIHIR" },
                (string tweetId) => { UnityEngine.Debug.Log("Tweet Success, tweetId = " + tweetId); },
                (ApiError error) => { UnityEngine.Debug.Log("Tweet Failed " + error.message); },
                () => { Debug.Log("Compose cancelled"); }
        );
    }

    public void LoginComplete(TwitterSession session)
    {
        ComposeMessage();
    }

    public void LoginFailure(ApiError error)
    {
        UnityEngine.Debug.Log("code=" + error.code + " msg=" + error.message);
    }
}
