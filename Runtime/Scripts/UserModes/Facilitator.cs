using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

namespace EqualReality.MetaverseClassrooms
{

	public class Facilitator : MonoBehaviour//, IOnEventCallback
	{
		public static bool mode = false;
		/*public static List<int> otherFaciltiators = new List<int>();
		
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
			
			if(eventCode == ClassroomSendEventManager.BecomeFacilitatorEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				bool isFacilitator = (bool)data[1];
				
				if(id != PhotonNetwork.LocalPlayer.ActorNumber)
				{
					if(isFacilitator)
						otherFaciltiators.Add(id);
					else 
					{
						while(otherFaciltiators.Contains(id))
							otherFaciltiators.Remove(id);
					}
				}
			}
		}
		
		public void SetFacilitatorFromToggle(Toggle toggle)
		{
			mode = toggle.isOn;
		}		
		
		public void SetToggleFromFacilitator(Toggle toggle)
		{
			toggle.isOn = mode;
		}
		
		public bool SwitchMode()
		{
			mode = !mode;
			
			return mode;
		}
	
		public void SetMode(bool newModeStatus)
		{
			mode = newModeStatus;
		}*/
	}

}