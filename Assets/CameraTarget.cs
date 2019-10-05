using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Camera cam;
    public GameObject camHolder;
    public LayerMask groundMask;
    [Range(0, 1)]
    public float distanceRatio = 0.2f;
    
    // Update is called once per frame
    void Update()
    {
        RaycastHit rayRes;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out rayRes, 1000f, groundMask))
            camHolder.transform.position = transform.position + (rayRes.point - transform.position) * distanceRatio;
        else camHolder.transform.position = transform.position;
    }
}
