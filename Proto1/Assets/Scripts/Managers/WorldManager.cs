using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


class WorldManager
{
    private readonly int startWorld2 = 10;
    private readonly int startWorld3 = 20;
    private readonly int currentLevel = PlayerDataController.instance.PreviousScene;


    // Returned a list of materials for platforms
    public Material[] GetMaterials()
    {
        Material[] materials = new Material[2];

        // World 1 or Multiplayer
        if (currentLevel <= startWorld2 || PhotonNetwork.InRoom)
        {
            materials[0] = Resources.Load("PlatformMaterial/RechthoekText", typeof(Material)) as Material;
            materials[1] = Resources.Load("PlatformMaterial/RampText", typeof(Material)) as Material;
        }

        // World 2
        else if (currentLevel > startWorld2 && currentLevel <= startWorld3)
        {
            materials[0] = Resources.Load("PlatformMaterial/RechthoekWinter", typeof(Material)) as Material;
            materials[1] = Resources.Load("PlatformMaterial/RampWinter", typeof(Material)) as Material;
        }

        // World 3
        else
        {
            materials[0] = Resources.Load("PlatformMaterial/RechthoekSpace", typeof(Material)) as Material;
            materials[1] = Resources.Load("PlatformMaterial/RampSpace", typeof(Material)) as Material;
        }

        return materials;
    }

    // Returned skybox
    public Material GetSkyBox()
    {
        // World 1
        Material skyBox = Resources.Load("SkyBox/World1", typeof(Material)) as Material;

        // World 2
        if (currentLevel > startWorld2 && currentLevel <= startWorld3)
        {
            skyBox = Resources.Load("SkyBox/Snow", typeof(Material)) as Material;
        }

        // World 3
        else if (currentLevel > startWorld3)
        {
            skyBox = Resources.Load("SkyBox/Space", typeof(Material)) as Material;
        }

        return skyBox;
    }

    // Returned true when it's world 3
    public bool SetSnow()
    {
        if (currentLevel > startWorld2 && currentLevel <= startWorld3)
        {
            return true;
        }
        return false;
    }

    // Return a ramp icon
    public Sprite GetRampIcon()
    {
        Sprite rampIcon;

        // World 1 or Multiplayer
        if (currentLevel <= startWorld2 || PhotonNetwork.InRoom)
        {
            rampIcon = Resources.Load("Icons/RampWorld1Icon", typeof(Sprite)) as Sprite;
        }

        // World 2
        else if (currentLevel > startWorld2 && currentLevel <= startWorld3)
        {
            rampIcon = Resources.Load("Icons/RampWorld3Icon", typeof(Sprite)) as Sprite;
        }

        // World 3
        else
        {
            rampIcon = Resources.Load("Icons/RampWorld2Icon", typeof(Sprite)) as Sprite;
        }

        return rampIcon;
    }

    public Sprite GetRechthoekIcon()
    {
        Sprite rechthoekIcon;

        // World 1 or Multiplayer
        if (currentLevel <= startWorld2 || PhotonNetwork.InRoom)
        {
            rechthoekIcon = Resources.Load("Icons/RechthoekWorld1Icon", typeof(Sprite)) as Sprite;
        }

        // World 2
        else if (currentLevel > startWorld2 && currentLevel <= startWorld3)
        {
            rechthoekIcon = Resources.Load("Icons/RechthoekWorld3Icon", typeof(Sprite)) as Sprite;
        }

        // World 3
        else
        {
            rechthoekIcon = Resources.Load("Icons/RechthoekWorld2Icon", typeof(Sprite)) as Sprite;
        }
        return rechthoekIcon;
    }

    /* Return world number
     * 
     * Wanneer er nog tijd over is moet dit veranderd worden. Het beste is om dit globaal bij te houden in welke
     * wereld de speler is.
    */
    public int SetWorldMusic()
    {
        // World 1 
        int world = 1;

        // World 2
        if (currentLevel > startWorld2 && currentLevel <= startWorld3)
        {
            world = 2;
        }

        // World 3
        else if (currentLevel > startWorld3)
        {
            world = 3;
        }
        return world;
    }
}

