using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject _spawnedPlayerPrefab;
    [SerializeField] private GameObject networkPlayer;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        _spawnedPlayerPrefab = PhotonNetwork.Instantiate(
            networkPlayer.name,
            new Vector3(transform.position.x + Random.Range(0, 4), 0, transform.position.z + Random.Range(0, 4)),
            transform.rotation
        );
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(_spawnedPlayerPrefab);
    }
}