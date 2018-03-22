using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleARPlacer : MonoBehaviour 
{
	[SerializeField]
	private GameObject prefab;
	[SerializeField]
	private Transform camera;
	[SerializeField]
	private ARSurfaceTracker surfaceTracker;
	[SerializeField]
	private float forwardOffset = 1f;

	private void Start ()
	{
		surfaceTracker.OnTracking += OnTracking;
	}

    private void OnTracking ()
    {
		Plane plane = new Plane (surfaceTracker.Ground.up, surfaceTracker.Ground.position);
		Quaternion rotation = Quaternion.Euler (0f, camera.rotation.eulerAngles.y, 0f);
		Vector3 origin = plane.ClosestPointOnPlane (camera.position) + (rotation * Vector3.forward) * forwardOffset;
		Instantiate (prefab, origin, rotation);
    }
}
