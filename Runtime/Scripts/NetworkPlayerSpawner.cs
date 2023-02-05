using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BrennanHatton.Networking
{

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
	
	public string PlayerPrebName = "Network Player";
	public static List<PhotonView> spawnedPlayerPrefabs = new List<PhotonView>();
	public static PhotonView localPlayer;
	
	public override void OnJoinedRoom()
	{
		
		base.OnJoinedRoom();
		localPlayer = PhotonNetwork.Instantiate(PlayerPrebName, transform.position, transform.rotation).GetComponent<PhotonView>();
		spawnedPlayerPrefabs.Add(localPlayer);
	}
	
	public override void OnLeftRoom()
	{
		base.OnLeftRoom();
		
		for (int i = 0; i < spawnedPlayerPrefabs.Count; i++)
		{
			if(spawnedPlayerPrefabs[i] != null)
				PhotonNetwork.Destroy(spawnedPlayerPrefabs[i].gameObject);
		}
	}
	
	public override void OnLeftRoom()
	{
		base.OnLeftRoom();
		
		for (int i = 0; i < spawnedPlayerPrefabs.Count; i++)
		{
			if(spawnedPlayerPrefabs[i] != null)
				PhotonNetwork.Destroy(spawnedPlayerPrefabs[i].gameObject);
		}
	}
	
}

}
