using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace BrennanHatton.Networking
{
	public class NetworkStatusTMP : MonoBehaviourPunCallbacks
	{
		public TMP_Text statusText, subtitle;
		
		public string 
			onJoinedRoom = "You joined a classroom in &.",
			yourNameIs = "Your name is #",
			tryingToConnect = "Trying to connect to server",
			connectedToServer = "Connected to server",
			onPlayerLeftRoom = "% left the party. Probably for the best...",
			onDisconnected = "You have been disconnected. Reason: %",
			tryingToReconnect = "\nTrying to reconnect...",
			onJoinRandomFailed = "Join Random Failed %";
			
			
		public string nameChar = "#", numberOfPlayersChar = "$", serverChar = "&", localChar = "%";
			
		[Tooltip("# for number of players")]
		public string[] onNumberOfPlayers = {"There are $ classmates already here."};
		
		void Reset()
		{
			statusText = this.gameObject.GetComponent<TMP_Text>();
			subtitle = this.GetComponentInChildren<TMP_Text>();
		}
		
		void Start()
		{
			Debug.Log(PhotonNetwork.CloudRegion);
			statusText.text = tryingToConnect;//.Replace(serverChar, PhotonNetwork.PhotonServerSettings.AppSettings.Server);
			subtitle.text = "";
		}
		
		public override void OnJoinedRoom()
		{
			
			if(statusText != null)
				statusText.text = onJoinedRoom.Replace(serverChar, PhotonNetwork.CloudRegion);
				
			if(subtitle != null)
				subtitle.text = yourNameIs.Replace(nameChar, PhotonNetwork.NickName) + "\n" + NumberOfPlayersMessage();
				
			StartCoroutine(getName());
				
			base.OnJoinedRoom();
			
		}
		
		IEnumerator getName()
		{
			yield return new WaitForSeconds(0.1f);
				
			if(subtitle != null)
				subtitle.text = yourNameIs.Replace(nameChar, PhotonNetwork.NickName) + NumberOfPlayersMessage();
			
		}
		
		public override void OnPlayerEnteredRoom(Player newPlayer)
		{
			
			if(statusText != null)
				statusText.text = NumberOfPlayersMessage();
				
			if(subtitle != null)
				subtitle.text = yourNameIs.Replace(nameChar, PhotonNetwork.NickName);
			
			base.OnPlayerLeftRoom(newPlayer);
		}
		
		string NumberOfPlayersMessage()
		{
			int i = onNumberOfPlayers.Length-1;
			
			if(onNumberOfPlayers.Length > PhotonNetwork.PlayerList.Length)
				i = PhotonNetwork.PlayerList.Length;
				
			return onNumberOfPlayers[i].Replace(numberOfPlayersChar,i.ToString());
		}
	
		void ConnectToServer()
		{
			PhotonNetwork.ConnectUsingSettings();
			
			if(statusText != null)
				statusText.text = tryingToConnect.Replace(serverChar, PhotonNetwork.CloudRegion);
		}
		public override void OnConnectedToMaster()
		{
			if(statusText != null)
				statusText.text = connectedToServer.Replace(serverChar, PhotonNetwork.CloudRegion);
				
			base.OnConnectedToMaster();
			
		}
		
		public override void OnDisconnected (DisconnectCause cause)
		{
			if(statusText != null)
				statusText.text = onDisconnected.Replace(localChar,cause.ToString());
				
			if(subtitle != null)
				subtitle.text = yourNameIs.Replace(nameChar, tryingToReconnect);
			
		}
		
		
		public override void OnPlayerLeftRoom (Player otherPlayer)
		{
			if(statusText != null)
				statusText.text = onPlayerLeftRoom.Replace(localChar, otherPlayer.NickName);
		
			base.OnPlayerLeftRoom(otherPlayer);
		}
		//Called when a remote player left the room or became inactive. Check otherPlayer.IsInactive. More...
 
		public override void 	OnJoinRandomFailed (short returnCode, string message)
		{
		
			base.OnJoinRandomFailed(returnCode, message);
			
			if(statusText != null)
				statusText.text = onJoinRandomFailed.Replace(localChar,returnCode.ToString());
				
			if(subtitle != null)
				subtitle.text = message;
		}
		
	}
}
