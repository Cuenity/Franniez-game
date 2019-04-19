using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using UnityEngine.SceneManagement;

public class ShopButtons : MonoBehaviour
{
    public void ReturnMainMenu()
    {
        SendTimeAnal();
        SceneManager.LoadScene("MainMenu");
    }

    private void SendTimeAnal()
    {
        // Verstuur hoelang de gebruiker in de shop was
        // Misschien andere naam verzinnen
    }

}
