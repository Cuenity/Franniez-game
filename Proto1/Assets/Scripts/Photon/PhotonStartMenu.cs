﻿using Photon.Pun;
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
    Button StartGame, CreateJoinButton;
    [SerializeField]
    Text RoomNameHost,RoomNameClient, ConnectedStatus,  WaitingForPlayers, TitleText;

    private int selectedLevel=1;
    private bool isCreator;

    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            StartMenu.gameObject.SetActive(true);
            if (!PhotonNetwork.IsConnectedAndReady)
            {
                connectToPhoton();
            }
            else
            {
                OnConnectedToMaster();
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                HostWait.gameObject.SetActive(true);
                StartGame.interactable = true;
            }
            else
            {
                ClientWait.gameObject.SetActive(true);
            }
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
    public void onClickSwitchToCustomRoomJoin()
    {
        isCreator = false;
        StartMenu.gameObject.SetActive(false);
        CustomRoom.gameObject.SetActive(true);
        CreateJoinButton.GetComponentInChildren<Text>().text = "Join Room";
        RoomToCreate.placeholder.GetComponent<Text>().text = "Enter room name to join....";
    }
    public void onClickSwitchToCustomRoomCreate()
    {
        isCreator = true;
        StartMenu.gameObject.SetActive(false);
        CustomRoom.gameObject.SetActive(true);
        CreateJoinButton.GetComponentInChildren<Text>().text = "Create Room";
        RoomToCreate.placeholder.GetComponent<Text>().text = "Enter room name to create....";
    }
    public void onClickSwitchToRoomCreate()
    {
        StartMenu.gameObject.SetActive(false);
        CreateRoom.gameObject.SetActive(true);
    }
    public void onClickSwitchToRoomJoin()
    {
        StartMenu.gameObject.SetActive(false);
        JoinRoom.gameObject.SetActive(true);
    }
    

    //click actions methodes

    //click andere shizzle
    public void onClickSelectLevel(int levelnumber)
    {
        selectedLevel = levelnumber;
    }

    //disconnect en ga terug naar mainmenu
    public void onClickDisconnectAndReturn()
    {
        PhotonNetwork.Disconnect();
        SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("StartMenu");
    }

    //start game
    public void onClickStartGame()
    {
        if(selectedLevel ==1)
            PhotonNetwork.LoadLevel(27);
        if (selectedLevel == 2)
            PhotonNetwork.LoadLevel(28);
    }

    //click joining and creating rooms
    public void onClickJoinRoom()
    {
        //TODO maak iets als room niet bestaat of vol zit
        if (RoomToJoin.text == "")
        {
            Debug.Log("wat een dum dum geen roomnaam gekozen");
            RoomToCreate.placeholder.GetComponent<Text>().text = "No Room Chosen";
        }
        else
        {
            PhotonNetwork.JoinRoom(RoomToJoin.text);
        }
    }

    public void onClickCreateRoom()
    {
        if (isCreator)
        {
            //TODO maak iets als room al bestaat
            PhotonNetwork.CreateRoom(RoomToCreate.text, new RoomOptions() { MaxPlayers = 2 }, null);

            //switch naar juiste view
            //CustomRoom.gameObject.SetActive(false);
            //HostWait.gameObject.SetActive(true);
        }
        else
        {
            if (RoomToCreate.text == "")
            {
                Debug.Log("wat een dum dum geen roomnaam gekozen");
                RoomToCreate.placeholder.GetComponent<Text>().text = "No Room Chosen";
            }
            else
            {
                PhotonNetwork.JoinRoom(RoomToCreate.text);
            }
        }
    }
    

    public void CreateOrJoinRandomRoom()
    {
        if (PhotonNetwork.CountOfRooms > 0)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.CreateRoom(PhotonNetwork.CountOfRooms.ToString(), new RoomOptions() { MaxPlayers = 2 }, null);
        }
        
    }

    //callbacks
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        //zou placeholder.text moeten zijns
        RoomToCreate.text = "";
        RoomToCreate.placeholder.GetComponent<Text>().text = "Room bestaat niet";

    }

    override public void OnJoinedRoom()
    {
        RoomNameHost.text = PhotonNetwork.CurrentRoom.Name;
        RoomNameClient.text = PhotonNetwork.CurrentRoom.Name;
        TitleText.text = "Room Name:"+PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.IsMasterClient)
        {
            StartMenu.gameObject.SetActive(false);
            CustomRoom.gameObject.SetActive(false);
            HostWait.gameObject.SetActive(true);
            
        }
        else
        {
            StartMenu.gameObject.SetActive(false);
            CustomRoom.gameObject.SetActive(false);
            ClientWait.gameObject.SetActive(true);
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
        WaitingForPlayers.text = "Other Player Found Start Level" + selectedLevel.ToString();
        StartGame.interactable = true;
        StartGame.GetComponent<Text>().text = "Start Game";
        StartGame.GetComponent<Text>().color = Color.green;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        WaitingForPlayers.text = "Waiting On Friends....";
        StartGame.interactable = false;
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