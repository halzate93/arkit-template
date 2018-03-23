using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleARPlacer : MonoBehaviour 
{
	[SerializeField]
	private Transform root;
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
		root.rotation = Quaternion.Euler (0f, camera.rotation.eulerAngles.y, 0f);
		root.position = plane.ClosestPointOnPlane (camera.position) + (root.rotation * Vector3.forward) * forwardOffset;
    }
}
