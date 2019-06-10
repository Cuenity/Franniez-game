using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class PlayerDataController : MonoBehaviour
{
    // Public static
    public static PlayerDataController instance;

    // Properties
    public PlayerData Player { get; set; }
    public int PreviousScene { get; set; }
    public int PreviousSceneCoinCount { get; set; }

    public Material ballMaterial { get; set; }

    // Localization
    private readonly string fileName = "PlayerData.json";


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


    #region Saving and Loading

    public void MakeNewPlayer()
    {
        PlayerData player = new PlayerData();
        player.language = (int)LocalizationManager.instance.LanguageChoice;
        player.materialsByName.Add("Franniez");
        player.activeMaterial = "Franniez";
        Player = player;
        SetMaterial();
        Save();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.dat");

        bf.Serialize(file, Player);
        file.Close();
        Debug.Log("Opgeslagen");
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

            Player = playerData;
            SetMaterial();
        }
        return fileExist;
    }
    #endregion

    public void SetPlayer(PlayerData setPlayer)
    {
        Player = setPlayer;
        Save();
    }

    internal void SetLanguage(int languageNumber)
    {
        Player.language = languageNumber;
        Save();
    }

    #region Shop Methods
    public void AddShopCoins(int amount)
    {
        Player.ShopCoins = Player.ShopCoins + amount;
        Save();
    }

    public bool RemoveShopCoins(int amount)
    {
        bool enoughCoins = false;

        if (amount <= Player.ShopCoins)
        {
            enoughCoins = true;
            Player.ShopCoins = Player.ShopCoins - amount;
            return enoughCoins;
        }
        return enoughCoins;
    }

    public int ReturnCoins()
    {
        return Player.ShopCoins;
    }

    public void AddMaterial(SkinObject skin)
    {
        Player.materialsByName.Add(skin.skinName);
        Save();
    }

    public void AddBundle(ShopCategory category)
    {
        Player.categoriesByName.Add(category.Name);
        Save();
    }

    public void SetActiveMaterial(Material skinMaterial)
    {
        ballMaterial = skinMaterial;
        Player.activeMaterial = skinMaterial.name;
        Save();
    }

    public void SetMaterial()
    {
        if (Player.activeMaterial == null)
        {
            ballMaterial = Resources.Load("Skins/Franniez", typeof(Material)) as Material;
        }
        else
        {
            string path = "Skins/" + Player.activeMaterial;
            ballMaterial = Resources.Load(path, typeof(Material)) as Material;
        }
    }
    #endregion
}
