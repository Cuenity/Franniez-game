using UnityEngine;
using System.Collections;
using System.IO;

public class DataController : MonoBehaviour
{
    public static DataController instance;
    public Player player;
    private string fileName = "PlayerData.json";
    private string filePath;

    // Use this for initialization
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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
}
