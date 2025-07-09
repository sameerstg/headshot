using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField inputField;
    public Button joinButton, createButton;
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        joinButton.onClick.AddListener(() =>
        {
            if(inputField.text == string.Empty)
            {
                return;
            }
            if (!PhotonNetwork.IsConnectedAndReady) return;
            PhotonNetwork.JoinRoom(inputField.text);
            joinButton.gameObject.SetActive(false);
            createButton.gameObject.SetActive(false);
        });      
        createButton.onClick.AddListener(() =>
        {
            if(inputField.text == string.Empty)
            {
                return;
            }
            if (!PhotonNetwork.IsConnectedAndReady) return;
            PhotonNetwork.CreateRoom(inputField.text);
            createButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
        });      
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        //PhotonNetwork.CreateRoom("Nexskill");
    }
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("player created a room.");
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        inputField.transform.root.gameObject.SetActive(false);
        Debug.Log("player joined the room.");
        PhotonNetwork.Instantiate("Player", Vector3.up*2+Vector3.right*PhotonNetwork.PlayerList.Length+Vector3.forward* PhotonNetwork.PlayerList.Length, Quaternion.identity);
        
    }
   }
