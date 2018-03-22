using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ARSurfaceTracker : MonoBehaviour 
{
	public event Action OnTracking;
	[SerializeField]
	private bool lockOnFirst = true;
	
	public Transform Ground
	{
		get; private set;
	}
	
	private void Start () 
	{
		UnityARSessionNativeInterface.ARAnchorUpdatedEvent += SetNewGroundAnchor;
		Ground = new GameObject ("Ground Reference").transform;
	}

    private void SetNewGroundAnchor(ARPlaneAnchor anchorData)
    {
		if (lockOnFirst)
			UnityARSessionNativeInterface.ARAnchorUpdatedEvent -= SetNewGroundAnchor;
		AnchorUtility.UpdateGameObjectWithAnchorTransform (Ground.gameObject, anchorData);
		if (OnTracking != null)
			OnTracking ();
    }
}
