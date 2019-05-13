using GameAnalyticsSDK;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RollingPhaseManager : MonoBehaviour
{
    private int amountCoins = 0;
    private bool pickedSticker = false;

    private int levelNumber;
    private PlayerData player;
    private Level level;


    private GameState gameState;

    public void Awake()
    {
        gameState = GameState.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        //amountCoins = 0;
        level = new Level();

        Scene scene = SceneManager.GetActiveScene();
        int.TryParse(scene.name, out levelNumber);

        if (levelNumber == 0) { return; }
        player = PlayerDataController.instance.Player;

        if (player == null) { return; }
        //INDEX OUT OF RANGE BIJ LEVELSWITCH
        //if (player.levels[levelNumber - 1] != null)
        //{
        //    // Als hij het niet vind, doet ff iets

        //    level = player.levels[levelNumber - 1];
        //}
        else
        {
            level = new Level();
        }


        if (level.gotSticker)
        {
            pickedSticker = true;
        }
        level.playedLevel = true;
    }

    public void Init()
    {
        foreach (GameObject placedPlatform in GameState.Instance.levelManager.playerPlatforms.placedPlatforms)
        {
            Destroy(placedPlatform.GetComponent<PlatformDragHandler>());
            if (!placedPlatform.GetComponent<Cannon>())
            {
                placedPlatform.GetComponent<Outline>().enabled = false;
            }
            // voor welk platform wordt onderstaande code uitgevoerd?
            if (placedPlatform.gameObject.transform.childCount > 0 && !placedPlatform.GetComponent<Cannon>())
            {
                placedPlatform.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    void OnEnable()
    {
        Coin.PickedCoin += AddCoin;
        StickerObject.PickedSticker += AddSticker;
        Finish.Finished += ReachedFinish;
        ButtonManager.ChangeEnvironment += ChangeEnvironment;
    }



    void OnDisable()
    {
        Coin.PickedCoin -= AddCoin;
        StickerObject.PickedSticker -= AddSticker;
        Finish.Finished -= ReachedFinish;
        ButtonManager.ChangeEnvironment -= ChangeEnvironment;
    }

    private void AddSticker()
    {
        pickedSticker = true;
        Debug.Log("Sticker gepakt");
    }

    private void ChangeEnvironment()
    {
        if (gameState.RollingPhaseActive)
        {
            amountCoins = 0;
            pickedSticker = false;
        }
    }

    private void AddCoin()
    {
        if (PlayerPrefs.GetInt("Vibration") == 1)
        {
            Handheld.Vibrate();
        }
        amountCoins++;
        Debug.Log("Coin gepakt");
    }

    private void ReachedFinish()
    {
        if (PhotonNetwork.InRoom)
        {
            //RPC gebruiken hier voor de code assesment
            PhotonView view = gameState.playerBallManager.activePlayer.GetComponent<PhotonView>();
            if(PhotonNetwork.IsMasterClient)
                view.RPC("FlagHit", RpcTarget.All, "masterhit");
            else
                view.RPC("FlagHit", RpcTarget.All, "clienthit");
        }
        else
        {
            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                Handheld.Vibrate();
            }
            //level.gotSticker = pickedSticker;
            level.completed = true;



            int amountOfCoinsLevel = level.countCoins;
            if (amountCoins > amountOfCoinsLevel)
            {
                level.countCoins = amountCoins;
            }



            if (player.levels == null)
            {
                player.levels = new Level[50];
            }
            player = PlayerDataController.instance.Player;
            //hier moet een check komen die kijkt of de behaalde sterren hoger zijn(eerder) dan aantal sterren nu behaald
            //voor de duidelijkheid player.level is wat is opgeslagen terwijl level het net behaalde is
            if (level.countCoins == 3 || PlayerDataController.instance.Player.levels[PlayerDataController.instance.PreviousScene - 1].gotSticker == true)
            {
                level.gotSticker = true;
            }
            if (levelNumber != 0)
            {

                if (player.levels.Length > 0 && player.levels[0] != null)
                {
                    player.levels[levelNumber - 1].completed = level.completed;
                    if (player.levels[levelNumber - 1].countCoins <= level.countCoins)
                    {
                        player.levels[levelNumber - 1].countCoins = level.countCoins;
                    }
                    player.levels[levelNumber - 1].gotSticker = level.gotSticker;
                }
                else
                {
                    player.levels[0] = level;
                }
            }

            player.coins += amountCoins;
            PlayerDataController.instance.Player = player;
            PlayerDataController.instance.Save();
            PlayerDataController.instance.PreviousScene = levelNumber;
            if (PlayerDataController.instance.PreviousSceneCoinCount < level.countCoins)
            {
                PlayerDataController.instance.PreviousSceneCoinCount = level.countCoins;
            }
            //maybe fix denk dat de progression null logt
            string stringlevelnumbervoorGA = levelNumber.ToString();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, levelNumber.ToString());
            SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("VictoryScreen");
            //DontDestroyOnLoad(gameState.playerManager.player);
            //gameState.UIManager.DeactivateInventoryButtons();
            //GameState.Instance.levelManager.AsynchronousLoadStart("VictoryScreen");
            //gameState.playerCamera.gameObject.SetActive(false);
            //gameState.levelManager.levelIsSpawned = false;
            //SceneManager.LoadScene("VictoryScreen");
        }
    }

}
