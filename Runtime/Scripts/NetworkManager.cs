using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using Photon.Voice.Unity;
//using Photon.Voice.Unity.UtilityScripts;
namespace BrennanHatton.Networking
{
	public class NetworkManager : MonoBehaviourPunCallbacks
	{
		public UnityEvent onConnectedToMaster = new UnityEvent(),onJoinedRoom = new UnityEvent(), onPlayerEnteredRoom = new UnityEvent();
		
		static RoomOptions roomOptions = null;
		public static int roomSize = 10;
		public static bool visible = false, open = true;
		public bool autoConnect = false,
			autoReconnect = true;
		public static string roomName = "Public";
		public float reconnectDelay = 0;
		
		
		static string roomPassword;
		public static string RoomPassword
		{	
			set{
				Debug.Log("set " + roomPassword);
				
				//if the password isnt changing
				if(string.Compare(roomPassword,value) == 0)
					//exit
					return;
					
				roomPassword = value;
				Debug.Log("set " + roomPassword);
					
				if(usePassword)
				{
					PhotonNetwork.LeaveRoom();
					
					
				}
			}
		}
		
		public static bool usePassword;
		
		
		public Text statusText;
		
		void Reset()
		{
			roomName = SceneManager.GetActiveScene().name;
		}
		
		public void Start()
		{
			
			if(!PhotonNetwork.IsConnectedAndReady)
			{
				GetRoomOptions();
				ConnectToServer();
			}
			
		}
		
		public static RoomOptions GetRoomOptions()
		{
			if(roomOptions == null)	
			{
				roomOptions = new RoomOptions();
			}
			
			roomOptions.MaxPlayers = (byte)roomSize;
			roomOptions.IsVisible = visible;
			roomOptions.IsOpen = open;
			
			return roomOptions;
		}
		
		public void ConnectToRoom()
		{
			
			
			PhotonNetwork.JoinOrCreateRoom(roomName+roomPassword, GetRoomOptions(), TypedLobby.Default);
		}
		
		public override void OnJoinedRoom()
		{
			
			if(statusText != null)
				statusText.text = "You joined a classroom.\nYour name is " + PhotonNetwork.NickName + ".\nThere are " + PhotonNetwork.PlayerList.Length + " classmates already here.";
				
			base.OnJoinedRoom();
			
			onJoinedRoom.Invoke();
		}
		
		public override void OnPlayerEnteredRoom(Player newPlayer)
		{
			
			if(statusText != null)
				statusText.text = "You are live with " + PhotonNetwork.PlayerList.Length + " classmates in the room.\nYour name is " + PhotonNetwork.NickName + ".";
			Debug.Log("A new player joined the room");
			base.OnPlayerLeftRoom(newPlayer);
			
			onPlayerEnteredRoom.Invoke();
		}
		
		// called second
		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			
			Debug.Log("OnSceneLoaded: " + scene.name);
			Debug.Log(mode);
			
			if(PhotonNetwork.IsConnectedAndReady)
			{
				PhotonNetwork.LeaveRoom();
			}
			
			
			if(autoConnect)
				ConnectToRoom();
	
		}
	
		void ConnectToServer()
		{
			PhotonNetwork.ConnectUsingSettings();
			
			if(statusText != null)
				statusText.text = "Trying to connect to server";
		}
		public override void OnConnectedToMaster()
		{
			if(statusText != null)
				statusText.text = "Connected to server";
				
			base.OnConnectedToMaster();
			
			ConnectToRoom();
			
			onConnectedToMaster.Invoke();
		}
		
		public override void OnDisconnected (DisconnectCause cause)
		{
			if(statusText != null)
				statusText.text = "You have been disconnected. Reason: " + cause.ToString() + "\nTrying to reconnect...";
				
			if(autoReconnect)
			{
				if(reconnectDelay == 0)
					ConnectToServer();
				else
				{
					StartCoroutine(reconnectAfterTime(reconnectDelay));
				}
			}
		}
		
		IEnumerator reconnectAfterTime(float time)
		{
			yield return new WaitForSeconds(time);
			
			ConnectToServer();
		}
		
		public static Player GetActorPlayer(int actor)
		{
			for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
			{
				if(PhotonNetwork.PlayerList[i].ActorNumber == actor)
					return PhotonNetwork.PlayerList[i];
			}
			
			return null;
		}
		
	}
}
