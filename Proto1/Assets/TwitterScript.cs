using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using TwitterKit.Unity;
using System;

public class TwitterScript : MonoBehaviour
{
    // Easy to change the hashtags
    private string[] hashtags = new string[] 
    { "#Franniez",
       "#ThisIsHowIRoll",
       "#TIHIR"
    };

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


    public void ComposeMessage()
    {
        
        string level = GameState.Instance.PreviousLevel.ToString();
        string amountStars = PlayerDataController.instance.player.levels[GameState.Instance.PreviousLevel - 1].countCoins.ToString();

        // TODO: Make Localized
        string stars = "sterren";

        if (PlayerDataController.instance.player.levels[GameState.Instance.PreviousLevel - 1].countCoins == 1)
        {
            stars = "ster";
        }
        string substitutionDescription = $"Yeah, ik heb level {level} gehaald met {amountStars} {stars}!";

        // TODO: Make own image
        string imagePath = "https://i.imgur.com/ww25JFA.jpg";

        Twitter.Compose(Twitter.Session, imagePath, substitutionDescription, hashtags,
                (string tweetId) => { Debug.Log("Tweet Success, tweetId = " + tweetId); },
                (ApiError error) => { Debug.Log("Tweet Failed " + error.message); },    // API error from Twitter
                () => { Debug.Log("Compose cancelled"); }                               // Interne error, geen Twitter session bijv.
        );
    }

    public void LoginComplete(TwitterSession session)
    {
        ComposeMessage();
    }

    public void LoginFailure(ApiError error)
    {
        Debug.Log("code=" + error.code + " msg=" + error.message);
    }
}
