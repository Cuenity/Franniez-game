using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollingPhaseManager : MonoBehaviour
{
    private int amountCoins = 0;
    private bool pickedSticker = false;

    private int levelNumber;
    private Player player;
    private Level level;


    private GameState gameState;

    public void Awake()
    {
        gameState = GameState.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        amountCoins = 0;
        level = new Level();

        Scene scene = SceneManager.GetActiveScene();
        //int.TryParse(scene.name, out levelNumber);

        player = PlayerDataController.instance.player;

        if (player == null)
        {
            return;
        }

        if(player.levels !=null)
        {
            // Als hij het niet vind, doet ff iets
            level = player.levels[1-1];
        }
        else
        {
            level = new Level();
        }

        //level = player.levels[levelNumber - 1];

        //if (level.gotSticker)
        //{
        //    pickedSticker = true;
        //}
        //level.playedLevel = true;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
        foreach (var placedPlatform in GameState.Instance.levelManager.playerPlatforms.placedPlatforms)
        {
            PlatformDragManager drag = placedPlatform.GetComponent<PlatformDragManager>();
            drag.enabled = false;
            Destroy(drag);
            placedPlatform.GetComponent<Outline>().enabled = false;
            if (placedPlatform.gameObject.transform.childCount > 0)
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
        amountCoins++;
        Debug.Log("Coin gepakt");
    }

    private void ReachedFinish()
    {
        level.gotSticker = pickedSticker;
        level.completed = true;

        int amountOfCoinsLevel = level.countCoins;
        if (amountCoins > amountOfCoinsLevel)
        {
            level.countCoins = amountCoins;
        }

        player = PlayerDataController.instance.player;

        if(player.levels == null)
        {
            player.levels = new Level[50];
        }
        if (player.levels.Length > 0 && player.levels[0] != null)
        {
            player.levels[levelNumber - 1] = level;
        }
        else
        {
            player.levels[0] = level;
        }
        
        player.coins += amountCoins;

        PlayerDataController.instance.player = player;
        PlayerDataController.instance.Save();
        SceneManager.LoadScene("VictoryScreen");
    }

}
