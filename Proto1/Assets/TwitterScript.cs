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
        else { LoginComplete(session); }
    }


    public void ComposeMessage()
    {
        string imagePath = "https://i.imgur.com/ww25JFA.jpg";

        Twitter.Compose(Twitter.Session, imagePath, LocalizedText(), hashtags,
                (string tweetId) => { Debug.Log("Tweet Success, tweetId = " + tweetId); },
                (ApiError error) => { Debug.Log("Tweet Failed " + error.message); },    // API error from Twitter
                () => { Debug.Log("Compose cancelled"); }                               // Interne error, geen Twitter session bijv.
        );
    }

    private string LocalizedText()
    {
        /*
         *  Omdat je niet een localized text direct een substition van kan maken hebben we
         *  het verdeelt in 3 segmenten. Waardoor in de volgende talen het volgende komt te staan:
         *  
         *  Nederlands:     "Yeah, ik heb level 3 behaald met 3 sterren!"
         *  Engels:         "Yeah, I completed level 3 with 3 stars!"
         *  Spaans:         "¡He alcanzado el nivel 3 con 3 estrellas!"
         */

        string levelCompleted = LocalizationManager.instance.GetLocalizedValue("twitter_Level");
        string with = LocalizationManager.instance.GetLocalizedValue("twitter_With");
        string stars = LocalizationManager.instance.GetLocalizedValue("twitter_Stars");

        if (PlayerDataController.instance.player.levels[GameState.Instance.PreviousLevel - 1].countCoins == 1)
        {
            stars = LocalizationManager.instance.GetLocalizedValue("twitter_star");
        }

        string level = GameState.Instance.PreviousLevel.ToString();
        string amountStars = PlayerDataController.instance.player.levels[GameState.Instance.PreviousLevel - 1].countCoins.ToString();

        string substitutionDescription = $"{levelCompleted} {level} {with} {amountStars} {stars}!";
        return substitutionDescription;
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
