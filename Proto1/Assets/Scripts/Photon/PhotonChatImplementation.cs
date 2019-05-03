using ExitGames.Client.Photon;
using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonChatImplementation :MonoBehaviour, IChatClientListener
{
    [SerializeField]
    InputField userName,chatInput;
    [SerializeField]
    Text chatOutput;


    ChatClient chatClient;

    
    void Awake()
    {
        //magic
        chatClient = new ChatClient(this);

        chatClient.ChatRegion = "EU";
        chatClient.Connect("efbbd03c-c388-44d2-9eb3-f42069131af4", "0.1", new AuthenticationValues(userName.text));

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        chatClient.Service();
        if (Input.GetKeyDown(KeyCode.Break))
        {
            sendMessage();
        }
    }

    public void StartChat()
    {
        if (userName.text!="")
        {
            chatOutput.text = "KIES NAAM DUM DUM ";
        }
        ConnectChat();
    }

    void ConnectChat()
    {
        // Set your favourite region. "EU", "US", and "ASIA" are currently supported.
    }

    void ConnectToChannel(string channel)
    {
        chatClient.Subscribe(new string[] { channel });
    }

    public void sendMessage()
    {
        chatClient.PublishMessage("Global", userName.text +":"+chatInput.text);
    }

    void IChatClientListener.DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(level + message);
    }

    void IChatClientListener.OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnConnected()
    {
        ConnectToChannel("Global");
    }

    void IChatClientListener.OnChatStateChange(ChatState state)
    {
        Debug.Log(state);
    }

    void IChatClientListener.OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName == "Global")
        {
            foreach (var message in messages)
            {
                chatOutput.text += message.ToString();
            }
        }
    }

    void IChatClientListener.OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("Subscribed");
    }

    void IChatClientListener.OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
}
