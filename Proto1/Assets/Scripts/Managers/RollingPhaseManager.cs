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
    public int countRolling = 0;


    private GameState gameState;

    public void Awake()
    {
        gameState = GameState.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        level = new Level();

        Scene scene = SceneManager.GetActiveScene();
        int.TryParse(scene.name, out levelNumber);

        levelNumber = PlayerDataController.instance.PreviousScene;

        if (levelNumber == 0) { return; }
        player = PlayerDataController.instance.Player;
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
        Finish.Finished += ReachedFinish;
        ButtonManager.ChangeEnvironment += ChangeEnvironment;
    }

    void OnDisable()
    {
        Coin.PickedCoin -= AddCoin;
        Finish.Finished -= ReachedFinish;
        ButtonManager.ChangeEnvironment -= ChangeEnvironment;
    }

    private void ChangeEnvironment()
    {
        if (gameState.RollingPhaseActive)
        {
            amountCoins = 0;
            countRolling++;
        }
    }

    private void AddCoin()
    {
        if (PlayerPrefs.GetInt("Vibration") == 1)
        {
            Handheld.Vibrate();
        }
        amountCoins++;
    }

    private void ReachedFinish()
    {
        if (PhotonNetwork.InRoom)
        {
            //zet de juiste waarde wie de flag heeft geraakt
            PhotonView view = gameState.playerBallManager.activePlayer.GetComponent<PhotonView>();
            if (PhotonNetwork.IsMasterClient)
                view.RPC("FlagHit", RpcTarget.All, "masterhit");
            else
                view.RPC("FlagHit", RpcTarget.All, "clienthit");
            return;
        }

        if (PlayerPrefs.GetInt("Vibration") == 1)
        {
            Handheld.Vibrate();
        }

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
        PlayerDataController.instance.PreviousSceneCoinCount = level.countCoins;

        GAManager.EndGame(countRolling, true);
        SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("VictoryScreen");

    }

}
