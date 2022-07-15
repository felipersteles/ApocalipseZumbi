using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject[] spawns;

    private void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Respawn");
        PhotonNetwork.Instantiate(playerPrefab.name, spawns[Random.Range(0,spawns.Length)].transform.position, Quaternion.identity);
    }
}
