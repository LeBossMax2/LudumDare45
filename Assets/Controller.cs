using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int max_healthPoint = 100;
    public int current_healthPoint = 0;
    // Time value
    public float reloadDelay = 10;
    public float movementSpeed = 10;
    public int damageDone = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void die()
    {
        this.Start();
    }
}
