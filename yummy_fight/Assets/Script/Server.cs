using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Server : MonoBehaviourPunCallbacks,IInRoomCallbacks
{
    public TMP_Text statusText;
    public GameObject LoginPanel;
    public TMP_InputField playerNameInput;

    public GameObject ConnectingPanel;
    public GameObject LobbyPanel;
    public GameObject Coin;
    public GameObject Canvas;
    public GameObject StartPanel;

    private const int MaxPlayerPerRoom = 2;
    public bool OnServer;

    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

    }
    void Start()
    {
        statusText.enabled = false;
        StartPanel.SetActive(true);
        LoginPanel.SetActive(false);
        ConnectingPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        Coin.SetActive(false);
        OnServer = false;
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConnectToPhotonServer() //LoginButton�ŌĂ�
    {
        if (!PhotonNetwork.IsConnected) //�T�[�o�[�ɐڑ����Ă�����
        {
            string playerName = playerNameInput.text;
            if (!string.IsNullOrEmpty(playerName))
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
                ConnectingPanel.SetActive(true);
                LoginPanel.SetActive(false);
            }
        }
        else { }
    }

    public void JoinRandomRoom() //StartButton�ŌĂ�
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("�}�X�^�[�ɐڑ����܂���");
        LobbyPanel.SetActive(true);
        ConnectingPanel.SetActive(false);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���[�����쐬���܂��B");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayerPerRoom });
    }

    public override void OnJoinedRoom() //���[���ɓ�������Ă΂��
    {
        Debug.Log(PhotonNetwork.NickName + "joined to" + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("���[���ɎQ�����܂���");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount != MaxPlayerPerRoom)
        {
            statusText.enabled = true;
            statusText.text = "waiting player...";
        }
        else
        {
            statusText.text = "�ΐ푊�肪�����܂����B�o�g���V�[���Ɉړ����܂��B";
            Coin.SetActive(true);
            Canvas.SetActive(false);
        }
        //PhotonNetwork.LoadLevel("SampleScene"); //�V�[�������[�h
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayerPerRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;

                statusText.text = "�ΐ푊�肪�����܂����B�o�g���V�[���Ɉړ����܂��B";
                Canvas.SetActive(false);

                Coin.SetActive(true);
                //PhotonNetwork.LoadLevel("SampleScene");
            }
        }
    }
    public void CPUButton()
    {
        StartPanel.SetActive(false);
        Coin.SetActive(true);
        OnServer = false;
    }

    public void PvPButton()
    {
        StartPanel.SetActive(false);
        LoginPanel.SetActive(true);
        OnServer = true;
    }
}
