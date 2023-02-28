using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BNG;

namespace BrennanHatton.Networking
{

	public class UIHandSwapper : MonoBehaviour
	{
	    [SerializeField] InputActionAsset controls;
	    InputAction leftTrigger, rightTrigger;
	
		void Reset()
		{
			//auto populate controls
			if(InputBridge.Instance != null)
				controls = InputBridge.Instance.actionSet;
		}
	
	    // Start is called before the first frame update
	    void Start()
	    {
	        controls.Enable();
	        InputActionMap XRLeftActionMap = controls.FindActionMap("XR LeftHand");
	        InputActionMap XRRightActionMap = controls.FindActionMap("XR RightHand");
	
	        leftTrigger = XRLeftActionMap.FindAction("Activate");
	        leftTrigger.Enable();
	
	        rightTrigger = XRRightActionMap.FindAction("Activate");
	        rightTrigger.Enable();
	
	        leftTrigger.started += SwapToLeftHand;
	        rightTrigger.started += SwapToRightHand;
	    }
	    
	    void SwapToLeftHand(InputAction.CallbackContext context)
	    {
	       VRUISystem.Instance.UpdateControllerHand(BNG.ControllerHand.Left);
	    }
	
	    void SwapToRightHand(InputAction.CallbackContext context)
	    {
	        VRUISystem.Instance.UpdateControllerHand(BNG.ControllerHand.Right);
	    }
	}

}