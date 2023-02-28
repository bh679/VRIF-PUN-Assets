using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace BrennanHatton.Networking
{
		
	public class NetworkSyncScenes : MonoBehaviour
	{
		public void SyncScenes(Toggle toggle)
		{
			PhotonNetwork.AutomaticallySyncScene = toggle.isOn;
		}
	}
}