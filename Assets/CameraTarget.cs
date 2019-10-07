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

    private Vector3 lastMousePos = Vector3.forward;
    
    // Update is called once per frame
    void LateUpdate()
    {
        camHolder.transform.position = transform.position + (getMousePosInWorld() - transform.position) * distanceRatio;
    }

    public Vector3 getMousePosInWorld()
    {
        if (Input.GetJoystickNames().Length > 0 && !string.IsNullOrEmpty(Input.GetJoystickNames()[0]))
        {
            if (Input.GetAxisRaw("Dir X") == 0 && Input.GetAxis("Dir Y") == 0)
                return lastMousePos;
            lastMousePos = transform.position + new Vector3(Input.GetAxisRaw("Dir X"), 0, Input.GetAxis("Dir Y")) * 10;
            return lastMousePos;
        }
        else
        {
            float dist;
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(r, out dist))
                lastMousePos = r.GetPoint(dist);
            return Vector3.zero;
        }
    }
}
