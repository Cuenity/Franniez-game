using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class PlayerDataController : MonoBehaviour
{
    public static PlayerDataController instance;

    public PlayerData player { get; set; }
    private string fileName = "PlayerData.json";
    private string filePath;
    public int previousScene { get; set; }
    public int previousSceneCoinCount { get; set; }

    public Material ballMaterial;

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
            PlayerData playerData = (PlayerData)bf.Deserialize(file);
            file.Close();

            player = playerData;
            SetMaterial();
        }
        return fileExist;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.dat");

        bf.Serialize(file, player);
        file.Close();
        Debug.Log("Opgeslagen");
    }

    public void SetPlayer(PlayerData setPlayer)
    {
        player = setPlayer;
    }

    internal void SetLanguage(int languageNumber)
    {
        player.language = languageNumber;
    }

    public void AddShopCoins(int amount)
    {
        player.ShopCoins = player.ShopCoins + amount;
        Save();
    }

    public bool RemoveShopCoins(int amount)
    {
        bool enoughCoins = false;

        if(amount <= player.ShopCoins)
        {
            enoughCoins = true;
            player.ShopCoins = player.ShopCoins - amount;
            return enoughCoins;
        }

        return enoughCoins;
        
    }

    public int ReturnCoins()
    {
        return player.ShopCoins;
    }

    public void AddMaterial(SkinObject skin)
    {
        player.materialsByName.Add(skin.skinName);
        Save();
    }

    public void SetActiveMaterial(Material skinMaterial)
    {
        ballMaterial = skinMaterial;
        player.activeMaterial = skinMaterial.name;
        Save();
    }

    public void SetMaterial()
    {
        if(player.activeMaterial == null)
        {
            // Load Async
            ballMaterial = Resources.Load("Skins/Franniez", typeof(Material)) as Material;
        }
        else
        {
            string path = "Skins/" + player.activeMaterial;
            ballMaterial = Resources.Load(path, typeof(Material)) as Material;
        }

    }
}
