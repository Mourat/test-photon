using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        ConnectedToServer();
    }

    private void ConnectedToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        print("Try Connect to server.");
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server.");
        base.OnConnectedToMaster();
        var roomOptions = new RoomOptions()
        {
            MaxPlayers = 5,
            IsVisible = true,
            IsOpen = true
        };
        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        print("Joined a room.");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("New player joined a room.");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
