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

        //filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        //Debug.Log(filePath.ToString());

        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    filePath = Path.Combine("jar:file://" + Application.dataPath + "!assets/", fileName);
        //    Debug.Log("RunTime Android goed gegaan. FIlepath is nu:");
        //    Debug.Log(filePath.ToString());
        //}

        //// Maak hier nog een if statement of file gevonden is, NIET MET File.EXISTS()!!!!

        //if(File.Exists(filePath))
        //{
        //    Debug.Log("Bestand is gevonden");
        //}
    }

    public void Save()
    {
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
