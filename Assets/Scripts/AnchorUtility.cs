using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR.iOS;

public class AnchorUtility
{
    private MeshCollider meshCollider; //declared to avoid code stripping of class
    private MeshFilter meshFilter; //declared to avoid code stripping of class
    private static GameObject planePrefab = null;

    public static GameObject InitializePlanePrefab(GameObject go)
    {
        planePrefab = go;
        return planePrefab;
    }
    
    public static GameObject CreatePlaneInScene(ARPlaneAnchor arPlaneAnchor)
    {
        GameObject plane;
        if (planePrefab != null) {
            plane = GameObject.Instantiate(planePrefab);
        } else {
            plane = new GameObject (); //put in a blank gameObject to get at least a transform to manipulate
        }

        plane.name = arPlaneAnchor.identifier;

        return UpdateGameObjectWithAnchorTransform(plane, arPlaneAnchor);

    }

    public static GameObject UpdateGameObjectWithAnchorTransform(GameObject go, ARPlaneAnchor arPlaneAnchor)
    {
        
        //do coordinate conversion from ARKit to Unity
        go.transform.localPosition = GetPosition (arPlaneAnchor.transform);
        go.transform.rotation = GetRotation (arPlaneAnchor.transform);
        //Debug.Log(string.Format("NEW UTILITY gameobject position={0} localposition={1} rotation={2}", plane.transform.position, plane.transform.localPosition, plane.transform.rotation));
        return go;
    }

    public static Vector3 GetPosition(Matrix4x4 matrix)
    {
        // Convert from ARKit's right-handed coordinate
        // system to Unity's left-handed
        Vector3 position = matrix.GetColumn(3);
        position.z = -position.z;

        return position;
    }

    public static Quaternion GetRotation(Matrix4x4 matrix)
    {
        // Convert from ARKit's right-handed coordinate
        // system to Unity's left-handed
        Quaternion rotation = QuaternionFromMatrix(matrix);
        rotation.z = -rotation.z;
        rotation.w = -rotation.w;

        return rotation;
    }

    static Quaternion QuaternionFromMatrix(Matrix4x4 m)
    {
        // Adapted from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
        Quaternion q = new Quaternion();
        q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
        q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
        q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
        q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
        q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
        q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
        q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
        return q;
    }

}

