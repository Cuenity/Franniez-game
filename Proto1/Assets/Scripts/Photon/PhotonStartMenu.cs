using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonStartMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Canvas StartMenu, CustomRoom,  HostWait, ClientWait, JoinRoom, CreateRoom;
    [SerializeField]
    InputField RoomToJoin, RoomToCreate;
    [SerializeField]
    Button StartGame;
    [SerializeField]
    Text RoomNameHost,RoomNameClient, ConnectedStatus,  WaitingForPlayers;

    private int selectedLevel=1;

    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {

        StartMenu.gameObject.GetComponent<Canvas>().enabled = true ;
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            connectToPhoton();
        }
        else
        {
            OnConnectedToMaster();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void connectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    //switch panel methodes
    public void onClickSwitchToCustomRoom()
    {
        StartMenu.gameObject.GetComponent<Canvas>().enabled = false;
        CustomRoom.gameObject.GetComponent<Canvas>().enabled = true;
    }
    public void onClickSwitchToRoomCreate()
    {
        StartMenu.gameObject.GetComponent<Canvas>().enabled = false;
        CreateRoom.gameObject.GetComponent<Canvas>().enabled = true;
    }
    public void onClickSwitchToRoomJoin()
    {
        StartMenu.gameObject.GetComponent<Canvas>().enabled = false;
        JoinRoom.gameObject.GetComponent<Canvas>().enabled = true;
    }
    

    //click actions methodes

    //click andere shizzle
    public void onClickSelectLevel(int levelnumber)
    {
        selectedLevel = levelnumber;
    }

    //start game
    public void onClickStartGame()
    {
        PhotonNetwork.LoadLevel(17);
    }

    //click joining and creating rooms
    public void onClickJoinRoom()
    {
        //TODO maak iets als room niet bestaat of vol zit
        if (RoomToJoin.text == "")
        {
            Debug.Log("wat een dum dum geen roomnaam gekozen");
            RoomToJoin.text = "-.- geen naam";
        }
        else
        {
            PhotonNetwork.JoinRoom(RoomToJoin.text);

            //view switch
            CustomRoom.gameObject.GetComponent<Canvas>().enabled = false;
            ClientWait.gameObject.GetComponent<Canvas>().enabled = true;
        }
    }

    public void onClickCreateRoom()
    {
        //TODO maak iets als room al bestaat
        PhotonNetwork.CreateRoom(RoomToCreate.text, new RoomOptions() { MaxPlayers = 2 }, null);

        //switch naar juiste view
        CustomRoom.gameObject.GetComponent<Canvas>().enabled = false;
        HostWait.gameObject.GetComponent<Canvas>().enabled = true;
    }
    

    public void CreateOrJoinRandomRoom()
    {
        if (PhotonNetwork.CountOfRooms > 0)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.CreateRoom("", new RoomOptions() { MaxPlayers = 2 }, null);
        }
        
    }

    //callbacks
    override public void OnJoinedRoom()
    {
        RoomNameHost.text = PhotonNetwork.CurrentRoom.Name;
        RoomNameClient.text = PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.IsMasterClient)
        {
            StartMenu.gameObject.GetComponent<Canvas>().enabled = false;
            HostWait.gameObject.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            StartMenu.gameObject.GetComponent<Canvas>().enabled = false;
            ClientWait.gameObject.GetComponent<Canvas>().enabled = true;
        }
    }

    override public void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected And Ready");
        ConnectedStatus.text = "Connected";
        ConnectedStatus.color = Color.green;
        Button[] startButtons =  StartMenu.GetComponentsInChildren<Button>();
        foreach (Button button in startButtons)
        {
            button.interactable = true;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        ConnectedStatus.text = "Disconnected";
        ConnectedStatus.color = Color.red;
        Button[] startButtons = StartMenu.GetComponentsInChildren<Button>();
        foreach (Button button in startButtons)
        {
            button.interactable = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        WaitingForPlayers.text = "Andere Speler Gevonden Start level:" + selectedLevel.ToString();
        StartGame.interactable = true;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        WaitingForPlayers.text = "Wachten op vrienden....";
    }
    //public override void room //OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //    //base.OnRoomListUpdate(roomList);
    //    //foreach (RoomInfo room in roomList)
    //    //{
    //    //    RoomList.text += room.Name + "/n";
    //    //}
    //
}
