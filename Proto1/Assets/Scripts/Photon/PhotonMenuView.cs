using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PhotonMenuView : MonoBehaviourPun
{
    // Start is called before the first frame update
    [PunRPC]
    void StartLevel(int levelnumber)
    {
        PlayerDataController.instance.PreviousScene = levelnumber;
    }
}
