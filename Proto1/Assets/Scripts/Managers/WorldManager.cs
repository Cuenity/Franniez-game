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

    private readonly int NUMBERSTARTWORLD2 = 10;
    private readonly int NUMBERSTARTWORLD3 = 20;
    private readonly int LEVELNUMBER = PlayerDataController.instance.PreviousScene;

    public Material[] GetMaterials()
    {
        Material[] materials = new Material[2];
        if (LEVELNUMBER <= NUMBERSTARTWORLD2)
        {
            materials[0] = Resources.Load("PlatformMaterial/RechthoekText", typeof(Material)) as Material;
            materials[1] = Resources.Load("PlatformMaterial/RampText", typeof(Material)) as Material;
        }

        else if (LEVELNUMBER > NUMBERSTARTWORLD2 && LEVELNUMBER < NUMBERSTARTWORLD3)
        {
            materials[0] = Resources.Load("PlatformMaterial/RechthoekSpace", typeof(Material)) as Material;
            materials[1] = Resources.Load("PlatformMaterial/RampSpace", typeof(Material)) as Material;
        }

        else
        {
            materials[0] = Resources.Load("PlatformMaterial/RechthoekWinter", typeof(Material)) as Material;
            materials[1] = Resources.Load("PlatformMaterial/RampWinter", typeof(Material)) as Material;
        }

        return materials;
    }

    public Material GetSkyBox()
    {
        Material skyBox = Resources.Load("SkyBox/World1", typeof(Material)) as Material;

        if (LEVELNUMBER > NUMBERSTARTWORLD2 && LEVELNUMBER < NUMBERSTARTWORLD3)
        {
            skyBox = Resources.Load("SkyBox/Space", typeof(Material)) as Material;
        }

        if (LEVELNUMBER > NUMBERSTARTWORLD3)
        {
            skyBox = Resources.Load("SkyBox/Snow", typeof(Material)) as Material;
        }
        return skyBox;
    }

    public bool SetSnow()
    {
        if(LEVELNUMBER > NUMBERSTARTWORLD3)
        {
            return true;
        }
        return false;
    }

    public Sprite GetRampIcon()
    {
        Sprite rampIcon;

        if (LEVELNUMBER <= NUMBERSTARTWORLD2)
        {
            rampIcon = Resources.Load("Icons/RampWorld1Icon", typeof(Sprite)) as Sprite;
        }

        else if (LEVELNUMBER > NUMBERSTARTWORLD2 && LEVELNUMBER < NUMBERSTARTWORLD3)
        {
            rampIcon = Resources.Load("Icons/RampWorld2Icon", typeof(Sprite)) as Sprite;
        }

        else
        {
            rampIcon = Resources.Load("Icons/RampWorld3Icon", typeof(Sprite)) as Sprite;
        }
        return rampIcon;
    }

    public Sprite GetRechthoekIcon()
    {
        Sprite rechthoekIcon;

        if (LEVELNUMBER <= NUMBERSTARTWORLD2)
        {
            rechthoekIcon = Resources.Load("Icons/RechthoekWorld1Icon", typeof(Sprite)) as Sprite;
        }

        else if (LEVELNUMBER > NUMBERSTARTWORLD2 && LEVELNUMBER < NUMBERSTARTWORLD3)
        {
            rechthoekIcon = Resources.Load("Icons/RechthoekWorld2Icon", typeof(Sprite)) as Sprite;
        }

        else
        {
            rechthoekIcon = Resources.Load("Icons/RechthoekWorld3Icon", typeof(Sprite)) as Sprite;
        }
        return rechthoekIcon;
    }
}

