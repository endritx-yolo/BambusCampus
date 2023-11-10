using Example;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class PhotonChatScript : MonoBehaviour, IChatClientListener
{
    Gameplay gameplay;

    [SerializeField] GameObject joinChatButton;
    [SerializeField] GameObject chatPanel;
    [SerializeField] InputField chatField;
    [SerializeField] Text chatDisplay;
    string currentChat;
    string privateReceiver = "";
    ChatClient chatClient;
    bool isConnected;
    [SerializeField] Button sendButton;

    PlayerInput playerInput;


    [SerializeField] string username;

    private string roomName;

    public GameObject targetGameObject;

    public void UsernameOnValueChange(string valueIn)
    {
        username = valueIn;
    }


    public void ChatConnectOnClick()
    {
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        Debug.Log("Connecting");

    }

    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }

    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }

    public void SubmitPrivateChatOnClick()
    {
        if (privateReceiver != "")
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameplay = GetComponent<Gameplay>();
        sendButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
        }

        if (chatField.text != "")
        {
            sendButton.interactable = true;
        }

        if (privateReceiver != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
        }

    }


    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
    }

    public void OnConnected()
    {
        Debug.Log("Connected");
        NetworkDebugStart networkDebugStart = targetGameObject.GetComponent<NetworkDebugStart>();
        roomName = networkDebugStart.DefaultRoomName;
        isConnected = true;
        joinChatButton.SetActive(false);
        chatClient.Subscribe(new string[] { roomName });

        Debug.Log("room name: " + roomName);

    }


    public void SubmitPublicChatOnClick()
    {
        if (privateReceiver == "")
        {
            chatClient.PublishMessage(roomName, currentChat);
            chatField.text = "";
            currentChat = "";
            sendButton.interactable = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameObject playerInputScript = GameObject.FindWithTag("Player");
            if (playerInputScript != null)
            {
                playerInput = playerInputScript.GetComponent<PlayerInput>();

                if (playerInput != null)
                {
                    bool myBoolValue = playerInput.GetMyBool();

                    playerInput.SetMyBool(true);

                    myBoolValue = playerInput.GetMyBool();

                }
            }
        }
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);

            chatDisplay.text += "\n" + msgs;

        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string msgs = "";

        msgs = string.Format("(Private) {0}: {1}", sender, message);

        chatDisplay.text += "\n" + msgs;

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
}
