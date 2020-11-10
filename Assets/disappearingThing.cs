using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappearingThing : MonoBehaviour
{
    public MeshRenderer[] meshRenderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Controller c = other.GetComponent<Controller>();
        foreach (MeshRenderer mr in this.meshRenderer) {
            mr.enabled = (false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Controller c = other.GetComponent<Controller>();
        foreach (MeshRenderer mr in this.meshRenderer)
        {
            mr.enabled = true;
        }
    }
}
