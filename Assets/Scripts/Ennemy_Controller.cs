using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy_Controller : MonoBehaviour
{
    public int current_healthPoint = 0;
    // Time value
    public float reloadDelay = 10;
    public float movementSpeed = 10;
    public int damageDone = 1;

    public GameObject player;

    private float nextActionTime = 0.0f;
    public float period = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            this.transform.position += player.transform.position;
        }
   
    }

    public void die()
    {
        this.Start();
    }
}
