using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using Photon.Pun;

namespace BrennanHatton.Networking
{

	public class EventOnConnection : MonoBehaviour
	{
		public bool onStart = false;
		public string url = "http://google.com";
		
		public UnityEvent onConnect, onError;
		
		// Start is called before the first frame update
		void Start(){
		
			if(onStart)
			{
				if(PhotonNetwork.IsConnected)
					onConnect.Invoke();
				else
					StartCoroutine(checkInternetConnection());
			}
		}
		
		IEnumerator checkInternetConnection(){
			Debug.Log("checkInternetConnection");
			UnityWebRequest request = new UnityWebRequest(url);
			yield return request .SendWebRequest();
			
			if (request.error != null) {
			
				Debug.Log("onConnect");
				onConnect.Invoke();
			
			} else {
				Debug.Log("onError");
				onError.Invoke();
			}
		} 
	}

}