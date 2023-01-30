using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace BrennanHatton.Networking
{

	public class PlayerCustomProperties : MonoBehaviour
	{
		public const string  ActiveScene = "ActiveScene", AvatarId= "AvatarId", Facilitator = "Facilitator", ClassSceneId = "ClassSceneId";
		
		public static void SetCustomProp<T>(string id, T value)
		{
			var hash = PhotonNetwork.LocalPlayer.CustomProperties;
			
			if(hash.ContainsKey(id))
			{
				hash[id] = value;
			} else
				hash.Add(id, value);
				
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
		}
		
		public T GetCustomProp<T>(Player player, string id)
		{
			return (T)player.CustomProperties[id];
		}
	    
		public static void SetFacilitator(bool active)
		{
			
			var hash = PhotonNetwork.LocalPlayer.CustomProperties;
			
			if(hash.ContainsKey(Facilitator))
			{
				hash[Facilitator] = active;
			} else
				hash.Add(Facilitator, active);
				
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
		}
	    
		public static void SetAvatarId(int id)
		{
			
			var hash = PhotonNetwork.LocalPlayer.CustomProperties;
			
			if(hash.ContainsKey(AvatarId))
			{
				hash[AvatarId] = id;
			} else
				hash.Add(AvatarId, id);
				
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
		}
		
		public static void SetActiveScene()
		{
			var hash = PhotonNetwork.LocalPlayer.CustomProperties;
			
			if(hash.ContainsKey(ActiveScene))
			{
				hash[ActiveScene] = SceneManager.GetActiveScene().buildIndex;
			} else
				hash.Add(ActiveScene, SceneManager.GetActiveScene().buildIndex);
				
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
		}
		
		public static void SetClassSceneId(int classSceneID)
		{
			var hash = PhotonNetwork.LocalPlayer.CustomProperties;
			
			if(hash.ContainsKey(ClassSceneId))
			{
				hash[ClassSceneId] = classSceneID;
			} else
				hash.Add(ClassSceneId, classSceneID);
				
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
		}
		
		public int GetSceneBuildIndex(Player player)
		{
			return (int)player.CustomProperties[ActiveScene];
		}
	}
}