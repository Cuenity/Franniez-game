using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonStartMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Canvas StartMenu, CustomRoom,  HostWait, ClientWait, JoinRoom, CreateRoom,DisconnectedCanvas;
    [SerializeField]
    InputField RoomToJoin, RoomToCreate;
    [SerializeField]
    Button StartGame, CreateJoinButton,ManualConnect,DisconnectReturnButton;
    [SerializeField]
    Text RoomNameHost,RoomNameClient, ConnectedStatus,  WaitingForPlayers, TitleText,DisconnectedText, DisconnectReturnButtonText;
    [SerializeField]
    GameObject photonMenuView;

    private int selectedLevel=1;
    private bool isCreator;

    private static PhotonStartMenu photonStartMenu;

    public bool ComesFromLevel;
    
    public static PhotonStartMenu Instance
    {
        get { return photonStartMenu; }
    }
    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically

        PhotonNetwork.AutomaticallySyncScene = true;
        //zorgt ervoor dat photon startmenu maar 1 keer bestaat(singeleton)
        if (photonStartMenu == null)
        {
            photonStartMenu = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(photonStartMenu.gameObject);
            photonStartMenu = this;
            DontDestroyOnLoad(this);
        }
        //laat het juiste menu zien als we van een level komen
        if (ComesFromLevel)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                DisconnectedCanvas.gameObject.SetActive(false);
                HostWait.gameObject.SetActive(true);
            }
            else
            {
                DisconnectedCanvas.gameObject.SetActive(false);
                ClientWait.gameObject.SetActive(true);
            }
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        //als er disconnect wordt door de speler zelf of de vriend waar mee gespeeld wordt zet de juiste tekst en canvas
        //kan dit niet in de callback zelf?
        //breakpoint dit nog ff
        if (PlayerDataController.instance.PreviousScene > 9000)
        {
            if(PlayerDataController.instance.PreviousScene == 9998)
            {
                DisconnectedCanvas.gameObject.SetActive(true);
                DisconnectedText.text = LocalizationManager.instance.GetLocalizedValue("multi_disconnected");
                DisconnectReturnButton.GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_backtomenu");
            }
            else if(PlayerDataController.instance.PreviousScene == 9999)
            {
                DisconnectedCanvas.gameObject.SetActive(true);
                DisconnectedText.text = LocalizationManager.instance.GetLocalizedValue("multi_friendleft");
                DisconnectReturnButton.GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_backtomenu");
            }
        }
        //nog niet connected connect dan
        else if (!PhotonNetwork.IsConnected)
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
        //mochten we uit iets anders komen dan zorgen dat we weer in start game wacht menu komen
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                HostWait.gameObject.SetActive(true);
                StartGame.interactable = true;
                ConnectedStatus.text = LocalizationManager.instance.GetLocalizedValue("multi_status_connected");
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
        //zeker maken dat het een singleton is
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            Destroy(this.gameObject);
        }
        //zorgen dat we reconnecten automatisch bij een disconnect
        if (!PhotonNetwork.IsConnected)
        {
            connectToPhoton();
        }
    }

    //aparte methode voor connecten zodat in de toekomst eventueel met serverselection gewerkt kan worden
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
    //disconnect nodig want anders kan je niet een nieuwe kamer joinen
    public void onClickSwitchToStartMenu()
    {
        DisconnectedCanvas.gameObject.SetActive(false);
        PhotonNetwork.Disconnect();
        StartMenu.gameObject.SetActive(true);
    }
    //
    public void onClickConnect()
    {
        connectToPhoton();
    }
    

    //click actions methodes
    //gebruikt om een level te selecteren(eventuele level gekozen indicatie hier doen met een rpc)
    public void onClickSelectLevel(int levelnumber)
    {
        selectedLevel = levelnumber;
    }

    //disconnect en ga terug naar mainmenu
    public void onClickDisconnectAndReturn()
    {
        PhotonNetwork.Disconnect();
        DisconnectedCanvas.gameObject.SetActive(false);
        StartMenu.gameObject.SetActive(true);
        SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("StartMenu");
        Destroy(this.gameObject);
    }

    //start game
    //functioneel zelfde als de start game in levelmanager
    //gaat daar ook naartoe door het laden van een specifieke scene via de gebruikelijke route(gamestate->levelmanager->initscene)
    public void onClickStartGame()
    {
        PhotonView view = photonMenuView.GetComponent<PhotonView>();
        if (selectedLevel == 1)
        {
            view.RPC("StartLevel", RpcTarget.All, 101);
            PlayerDataController.instance.PreviousScene = 101;
            PhotonNetwork.LoadLevel(8);
        }
        else if (selectedLevel == 2)
        {
            view.RPC("StartLevel", RpcTarget.All, 102);
            PlayerDataController.instance.PreviousScene = 102;
            PhotonNetwork.LoadLevel(9);
        }
        else if (selectedLevel == 3)
        {
            view.RPC("StartLevel", RpcTarget.All, 103);
            PlayerDataController.instance.PreviousScene = 103;
            PhotonNetwork.LoadLevel(11);
        }
        else if (selectedLevel == 4)
        {
            view.RPC("StartLevel", RpcTarget.All, 104);
            PlayerDataController.instance.PreviousScene = 104;
            PhotonNetwork.LoadLevel(12);
        }
        else if (selectedLevel == 5)
        {
            view.RPC("StartLevel", RpcTarget.All, 105);
            PlayerDataController.instance.PreviousScene = 105;
            PhotonNetwork.LoadLevel(13);
        }
        else if (selectedLevel == 6)
        {
            view.RPC("StartLevel", RpcTarget.All, 106);
            PlayerDataController.instance.PreviousScene = 106;
            PhotonNetwork.LoadLevel(14);
        }
        else if (selectedLevel == 7)
        {
            view.RPC("StartLevel", RpcTarget.All, 107);
            PlayerDataController.instance.PreviousScene = 107;
            PhotonNetwork.LoadLevel(15);
        }
        else if (selectedLevel == 8)
        {
            view.RPC("StartLevel", RpcTarget.All, 108);
            PlayerDataController.instance.PreviousScene = 108;
            PhotonNetwork.LoadLevel(16);
        }
        else if (selectedLevel == 9)
        {
            view.RPC("StartLevel", RpcTarget.All, 109);
            PlayerDataController.instance.PreviousScene = 109;
            PhotonNetwork.LoadLevel(17);
        }
        else if (selectedLevel == 10)
        {
            view.RPC("StartLevel", RpcTarget.All, 110);
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
        RoomToCreate.text = "";
        RoomToCreate.placeholder.GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("multi_roomjoinfailed");
    }

    override public void OnJoinedRoom()
    {
        //laat juiste menu zien
        RoomNameHost.text = PhotonNetwork.CurrentRoom.Name;
        RoomNameClient.text = PhotonNetwork.CurrentRoom.Name;
        TitleText.text = "Room Name:"+PhotonNetwork.CurrentRoom.Name;
        //menu specifiek voor hosts en clients
        //Zou hetzelfde kunnen zijn als je werkt met RPC en dan kunnen clients ook starten
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
        //zorgt ervoor dat knoppen aangaan als je connected bent
        base.OnConnectedToMaster();
        ConnectedStatus.text = "Connected";
        ConnectedStatus.color = Color.green;
        //behalve de manual connect natuurlijk
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
        //zet de knoppen weer uit
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
        //dit is als de speler terug gaat naar main menu
        else if (cause != DisconnectCause.DisconnectByClientLogic)
        {
            PlayerDataController.instance.PreviousScene = 9998;
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
        //niet de meest zuivere oplossing maar voorkomt dat spelers vast blijven staan in speel menu als ze alleen zijn
        // voorkomt multiplayer in je eentje
        base.OnPlayerLeftRoom(otherPlayer);
        
        PlayerDataController.instance.PreviousScene = 9999;
        PhotonNetwork.LoadLevel(10);
        Destroy(this.gameObject);
    }
}
