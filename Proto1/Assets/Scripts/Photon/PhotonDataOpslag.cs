using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonDataOpslag : MonoBehaviourPunCallbacks,  IPunObservable
{
    public bool FlagHitPlayer1;
    public bool FlagHitPlayer2;

    private static PhotonDataOpslag instance;
    public static PhotonDataOpslag Instance
    {
        get { return instance; }
    }
    private void Start()
    {
        instance = this;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //vrij zeker dat hij nu alleen synct met de client
        if (stream.IsWriting)
        {
            if(PhotonNetwork.IsMasterClient)
                stream.SendNext(FlagHitPlayer1);
            else
                stream.SendNext(FlagHitPlayer2);
            Debug.Log("SendHitEvent");
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
                FlagHitPlayer2 = (bool)stream.ReceiveNext();
            else
                FlagHitPlayer1 = (bool)stream.ReceiveNext();
            Debug.Log("ReceiveHitEvent");
        }
    }
}
