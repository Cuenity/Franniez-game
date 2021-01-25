using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using GameAnalyticsSDK;


class GAManager : MonoBehaviour
{
    public static GAManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != null && instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    #region Progression
    // -----------------------------     PROGRESSION     ----------------------------- \\
    public static void StartGame()
    {
        int levelNumber = PlayerDataController.instance.PreviousScene;
        string levelNumberToString = levelNumber.ToString();

        // Formatting
        if (levelNumber < 10)
        {
            levelNumberToString = "0" + levelNumberToString;
        }

        levelNumberToString = "SinglePlayer:Level_" + levelNumberToString;

        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, levelNumberToString);
    }

    public static void EndGame(int countRolling, bool succeeded)
    {
        // Get info from completed level
        int levelNumber = PlayerDataController.instance.PreviousScene;
        string levelString = "Level_";

        // Formatting
        if (levelNumber < 10)
        {
            levelString += "0" + levelNumber.ToString();
        }
        else
        {
            levelString += levelNumber.ToString();
        }

        string countPlatformDesignEvent = "Platforms:Levels:" + levelString;
        string countRollingString = "RollingPhase:Levels:" + levelString;
        
        // Amount of platforms
        //GameAnalytics.NewDesignEvent(countRollingString, GameState.Instance.levelManager.playerPlatforms.placedPlatforms.Count());

        // Amount of Rollingphases
        //GameAnalytics.NewDesignEvent(countPlatformDesignEvent, countRolling);

        //if (succeeded)
        //{
        //    int countStars = PlayerDataController.instance.PreviousSceneCoinCount;
        //    // Send progression with stars
        //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, levelString.ToString(), countStars);
        //}
        //else
        //{
        //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, levelString.ToString());
        //}
    }
    #endregion

    #region Shop
    // -----------------------------     SHOP     ----------------------------- \\

    public static void ShopTime(float timeInSeconds)
    {
        //GameAnalytics.NewDesignEvent("Shop:Time", timeInSeconds);
    }

    public static void ShopSelectedSkin(string skinName)
    {
        //GameAnalytics.NewDesignEvent(("Shop:SelectedSkin:" + skinName));
    }

    public static void ShopBoughtSkin(string skinName)
    {
        //GameAnalytics.NewDesignEvent(("Shop:BoughtSkin:" + skinName));
    }

    public static void ShopBoughtBundle(ShopCategory bundle)
    {
        //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Coins", bundle.cost, "SkinBundles", bundle.Name);
    }

    public static void ShopDonation(int amount)
    {
        string bundleName = $"Bundle {amount} coins";

        // Int is the only type that will be allowed in GameAnalystics
        int euro = 1;

        if(amount == 210)
        {
            euro = 5;
        }
        else if( amount == 510)
        {
            euro = 10;
        }
        //GameAnalytics.NewBusinessEvent("EUR", euro, "Donation", bundleName, null);
    }
    #endregion

    #region Social Media
    // -----------------------------     SOCIAL MEDIA     ----------------------------- \\

    public static void TwitterButtonPressed()
    {
        //GameAnalytics.NewDesignEvent("SocialMedia:Twitter:Button_Pressed");
    }

    public static void TwitterSucceededTweet()
    {
        //GameAnalytics.NewDesignEvent("SocialMedia:Twitter:Succes");
    }

    public static void TwitterFailedTweet()
    {
        //GameAnalytics.NewDesignEvent("SocialMedia:Twitter:Failed");
    }

    public static void TwitterAPIError()
    {
        //GameAnalytics.NewDesignEvent("SocialMedia:Twitter:API_Error");
    }
    #endregion
}

