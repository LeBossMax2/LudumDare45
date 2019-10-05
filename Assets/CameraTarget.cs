using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public static Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    public Camera cam;
    public GameObject camHolder;
    [Range(0, 1)]
    public float distanceRatio = 0.2f;
    
    // Update is called once per frame
    void LateUpdate()
    {
        camHolder.transform.position = transform.position + (getMousePosInWorld() - transform.position) * distanceRatio;
    }

    public Vector3 getMousePosInWorld()
    {
        float dist;
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(r, out dist))
            return r.GetPoint(dist);
        else return Vector3.zero;
    }
}
