using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPoint;

    PlayerFollow playerFollow;
    PlayerData playerData;

    private void Awake()
    {
        playerFollow = FindAnyObjectByType<PlayerFollow>();
        playerData = FindAnyObjectByType<PlayerData>();
    }

    void Start()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        GameObject playerSpawned = PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation);
        //AvatarSetUp playerAvatar = playerSpawned.GetComponent<AvatarSetUp>();
        //playerAvatar.CurrentHairIndex = playerData.data.playerHair;
        playerFollow.SetCameraTarget(playerSpawned.transform);
    }
}
