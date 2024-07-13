using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Lobby : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput, joinInput;
    public byte maxPlayers;

    public void CreateBTN()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }
    
    public void JoinBTN()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Level");
    }
}
