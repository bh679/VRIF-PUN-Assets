using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

namespace BrennanHatton.Networking
{

	public class NickNameGenerator : MonoBehaviour
	{
		
		public string[] noun, adjective;
		public UnityEvent onSetFromInput, onChange;
		public bool setOnEnable = true;
		
		
		public void OnEnable()
		{
			if(setOnEnable)
			{
				SetRandomNickName();
			}
		}
		
		public void SetInputFieldToNickName(InputField input)
		{
			input.text = PhotonNetwork.NickName;
		}
		
		public void SetNickNameFromInputField(InputField input)
		{
			PhotonNetwork.NickName = input.text;
			onSetFromInput.Invoke();
			onChange.Invoke();
		}
	    
		public void SetRandomNickName()
		{
			PhotonNetwork.NickName = GetRandomNickName();
			onChange.Invoke();
		}
	    
		public string GetRandomNickName()
		{	
			return adjective[Random.Range(0,adjective.Length-1)] + " " + noun[Random.Range(0,noun.Length-1)];
		}
	}

}
