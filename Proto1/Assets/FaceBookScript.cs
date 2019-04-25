using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FaceBookScript : MonoBehaviour
{
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Couldn't initialize");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }

    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions);
    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }

    public void FacebookShare()
    {
        string test = "Level " + PlayerDataController.instance.previousScene.ToString() + " behaald!";

        FB.ShareLink(new System.Uri("http://franniez.nl"),"This Is How I Roll",
            test,
            new System.Uri("https://franniez.nl/themes/franniez/assets/images/header.jpg"));
    }
}
