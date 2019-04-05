using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerDataController : MonoBehaviour
{
    public static PlayerDataController instance;

    public Player player { get; set; }
    private string fileName = "PlayerData.json";
    private string filePath;

    // Use this for initialization
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


    public bool Load()
    {
        bool fileExist = false;
    
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.dat"))
        {
            fileExist = true;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
            Player playerData = (Player)bf.Deserialize(file);
            file.Close();

            player = playerData;
            Debug.Log("Player van het lezen: ");
            Debug.Log(playerData);
            Debug.Log("Player die opgeslagen is in property");
            Debug.Log(player.name);
        }
        return fileExist;
    }

    public void Save()
    {

        Player playerTest = new Player();
        playerTest.coins = 0;
        playerTest.language = 2;
        playerTest.levels = new Level[1];
        playerTest.stickers = new Sticker[1];
        playerTest.name = "Joris";

        player = playerTest;
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.dat");

        bf.Serialize(file, player);
        file.Close();
        Debug.Log("Opgeslagen");
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void SetPlayer(Player setPlayer)
    {
        player = setPlayer;
    }
}
