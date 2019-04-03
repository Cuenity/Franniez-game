using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerDataController : MonoBehaviour
{
    public static PlayerDataController instance;

    public Player player
    {
        get { return player; }
        set
        {   player = value;
            SavePlayerData();
        }
    }
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


    public void LoadPlayerData()
    {
        
       filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if(File.Exists(filePath))
        {
            player = new Player();
            string dataAsJSON = File.ReadAllText(filePath);
            player = JsonUtility.FromJson<Player>(dataAsJSON);
            Debug.Log(player.name);

        }
    }

    public void SavePlayerData()
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(player);
            File.WriteAllText(filePath, dataAsJson);
        }
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
