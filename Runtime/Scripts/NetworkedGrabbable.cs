using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using BNG;

namespace BrennanHatton.Networking{
	
	[RequireComponent(typeof(PhotonView))]
	[RequireComponent(typeof(PhotonRigidbodyView))]
	public class NetworkedGrabbable : MonoBehaviourPunCallbacks
	{
		bool joined, owner;
		public GrabbableUnityEvents grabbableEvents;
		Grabbable grabbable;
		public Rigidbody rb;
		
		//bool isKinematic = false;
		//bool useGravity = true;
		
		void Reset()
		{
			grabbableEvents = this.GetComponent<GrabbableUnityEvents>();
			if(!grabbableEvents)
				grabbableEvents = this.gameObject.AddComponent<GrabbableUnityEvents>();
				
			rb = this.GetComponent<Rigidbody>();
				
			if(this.GetComponent<PhotonView>() != null)
				this.GetComponent<PhotonView>().OwnershipTransfer = OwnershipOption.Takeover;
			
				
		}
		
		void Start()
		{
			grabbableEvents.onGrab.AddListener((Grabber grabber)=>{TakeOver();});
			//grabbableEvents.onRelease.AddListener(()=>{
				//if(owner)
					//rb.useGravity = useGravity;
			//});
			
			grabbable = this.GetComponent<Grabbable>();
		}
		
		public override void OnJoinedRoom()
		{
			joined = true;
			if(this.photonView.Owner == null && PhotonNetwork.LocalPlayer.IsMasterClient)
				TakeOver();
		}
	
	    // Update is called once per frame
	    void Update()
	    {
		    if(!joined)
			    return;
			    
	    }
	    
		void LateUpdate()
		{
			if(!joined)
				return;
				
			owner = this.photonView.Owner != null && this.photonView.Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber;
			
			    
			if(!owner)
			{
				//rb.isKinematic = false;
				//rb.useGravity = false;
				if(grabbable.BeingHeld)
				{
					
					Vector3 velocity = grabbable.HeldByGrabbers[0].GetGrabberAveragedVelocity() + grabbable.HeldByGrabbers[0].GetComponent<Rigidbody>().velocity;
					Vector3 angularVelocity = grabbable.HeldByGrabbers[0].GetGrabberAveragedAngularVelocity() + grabbable.HeldByGrabbers[0].GetComponent<Rigidbody>().angularVelocity;
					grabbable.Release(velocity,angularVelocity);
				}
			    
				if(this.photonView.Owner == null && PhotonNetwork.LocalPlayer.IsMasterClient)
					TakeOver();
			}
	
		}
	    
		public void TakeOver()
		{
			this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
			//rb.isKinematic = isKinematic;
			//rb.useGravity = useGravity;
		}
	}

}
