﻿using UnityEngine;
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

    //public void MakeNewPlayer()
    //{
    //    Player playerTest = new Player();
    //    playerTest.coins = 0;
    //    playerTest.ShopCoins = 0;
    //    playerTest.language = 2;
    //    playerTest.levels = new Level[1];
    //    playerTest.stickers = new Sticker[1];
    //    playerTest.name = "Joris";

    //    player = playerTest;
    //    Save();
    //}

    //public Player GetPlayer()
    //{
    //    return player;
    //}

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
        Debug.Log("Parameter amound: " + amount);
        Debug.Log("Player coins van te voren: " + player.ShopCoins);
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

    public void GetBallMaterial()
    {
        // Alleen voor Debug
        if(player.materialsByName.Count > 1)
        {
            foreach(string name in player.materialsByName)
            {
                Debug.Log(name);
            }
        }

    }


    public void AddMaterial(SkinObject skin)
    {
        //PlayerBallMaterial newSkin = new PlayerBallMaterial
        //{
        //    Material = skin.material,
        //    Name = skin.skinName
        //};
        //player.materials.Add(newSkin);
        player.materialsByName.Add(skin.skinName);
        Save();
    }

    private void setMaterials()
    {
        foreach (string skinName in player.materialsByName)
        {

        }
    }
}
