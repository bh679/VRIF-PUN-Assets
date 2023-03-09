using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using BNG;
using BrennanHatton.Networking;

namespace BrennanHatton.Networking.Events
{
	
	public class ReceivePlayerResetEventForScene : MonoBehaviour, IOnEventCallback
	{
		public UnityEvent onReceiveLocal, onReceiveGlobal;
		public PlayerSpawnPosition spawner;
		
		void Reset()
		{
			spawner = GameObject.FindObjectOfType<PlayerSpawnPosition>();
		}
		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
	
		public void OnEvent(EventData photonEvent)
		{
			byte eventCode = photonEvent.Code;
			
			if(eventCode == SendPVPEventManager.PlayerResetEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				
				if(id == PhotonNetwork.LocalPlayer.ActorNumber)
				{
					if(spawner != null) spawner.SetPosition();
					onReceiveLocal.Invoke();
				}
				
				onReceiveGlobal.Invoke();
				
			}
		}
	}

}