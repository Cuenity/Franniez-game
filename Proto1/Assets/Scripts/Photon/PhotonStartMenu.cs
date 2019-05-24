﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonStartMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Canvas StartMenu, CustomRoom,  HostWait, ClientWait, JoinRoom, CreateRoom;
    [SerializeField]
    InputField RoomToJoin, RoomToCreate;
    [SerializeField]
    Button StartGame, CreateJoinButton,ManualConnect;
    [SerializeField]
    Text RoomNameHost,RoomNameClient, ConnectedStatus,  WaitingForPlayers, TitleText;

    private int selectedLevel=1;
    private bool isCreator;

    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically

        PhotonNetwork.AutomaticallySyncScene = true;
        DontDestroyOnLoad(this);
        
    }

    void LocalizedTextLoader()
    {

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
                ConnectedStatus.text = LocalizationManager.instance.GetLocalizedValue("multi_status_disconnected");
                ConnectedStatus.color = Color.green;
            }
            else
            {
                ClientWait.gameObject.SetActive(true);
                ConnectedStatus.text = LocalizationManager.instance.GetLocalizedValue("multi_status_connected");
                ConnectedStatus.color = Color.green;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            Destroy(this.gameObject);
        }
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
        CreateJoinButton.GetComponentInChildren<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_joinroom");
        RoomToCreate.placeholder.GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_joinroominput");
    }
    public void onClickSwitchToCustomRoomCreate()
    {
        isCreator = true;
        StartMenu.gameObject.SetActive(false);
        CustomRoom.gameObject.SetActive(true);
        CreateJoinButton.GetComponentInChildren<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_createroom");
        RoomToCreate.placeholder.GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_createroominput");
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
    public void onClickConnect()
    {
        connectToPhoton();
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
        if (selectedLevel == 1)
        {
            PlayerDataController.instance.PreviousScene = 101;
            PhotonNetwork.LoadLevel(8);
        }
        if (selectedLevel == 2)
        {
            PlayerDataController.instance.PreviousScene = 102;
            PhotonNetwork.LoadLevel(9);
        }
        if (selectedLevel == 3)
        {
            PlayerDataController.instance.PreviousScene = 103;
            PhotonNetwork.LoadLevel(11);
        }
        if (selectedLevel == 4)
        {
            PlayerDataController.instance.PreviousScene = 104;
            PhotonNetwork.LoadLevel(12);
        }
        if (selectedLevel == 5)
        {
            PlayerDataController.instance.PreviousScene = 105;
            PhotonNetwork.LoadLevel(13);
        }
        if (selectedLevel == 6)
        {
            PlayerDataController.instance.PreviousScene = 106;
            PhotonNetwork.LoadLevel(14);
        }
        if (selectedLevel == 7)
        {
            PlayerDataController.instance.PreviousScene = 107;
            PhotonNetwork.LoadLevel(15);
        }
        if (selectedLevel == 8)
        {
            PlayerDataController.instance.PreviousScene = 108;
            PhotonNetwork.LoadLevel(16);
        }
        if (selectedLevel == 9)
        {
            PlayerDataController.instance.PreviousScene = 109;
            PhotonNetwork.LoadLevel(17);
        }
        if (selectedLevel == 10)
        {
            PlayerDataController.instance.PreviousScene = 110;
            PhotonNetwork.LoadLevel(18);
        }
    }

    //click joining and creating rooms
    public void onClickJoinRoom()
    {
        //TODO maak iets als room niet bestaat of vol zit
        if (RoomToJoin.text == "")
        {
            RoomToCreate.placeholder.GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_roomjoinfailednoinput");
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
                RoomToCreate.placeholder.GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_roomjoinfailednoinput"); 
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
        RoomToCreate.placeholder.GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_roomjoinfailed");

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
        ConnectedStatus.text = "Connected";
        ConnectedStatus.color = Color.green;
        Button[] startButtons =  StartMenu.GetComponentsInChildren<Button>();
        foreach (Button button in startButtons)
        {
            button.interactable = true;
            if (button.name == "ManualConnect")
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        if (SceneManager.GetActiveScene().name == "MultiplayerStart")
        {
            ConnectedStatus.text = "Disconnected";
            ConnectedStatus.color = Color.red;
            Button[] startButtons = StartMenu.GetComponentsInChildren<Button>(true);
            foreach (Button button in startButtons)
            {
                button.interactable = false;
                if (button.name == "ManualConnect") 
                {
                    button.interactable = true;
                    button.gameObject.SetActive(true);
                }
            }
        }
        else if (cause != DisconnectCause.DisconnectByClientLogic)
        {
            PhotonNetwork.LoadLevel(10);
            Destroy(this.gameObject);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        //WaitingForPlayers.text = "Other Player Found Start Level" + selectedLevel.ToString();
        StartGame.interactable = true;
        StartGame.GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_hoststartgame");
        StartGame.GetComponent<Text>().color = Color.green;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        WaitingForPlayers.text = LocalizationManager.instance.GetLocalizedValue("multi_hostwaittext");
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
