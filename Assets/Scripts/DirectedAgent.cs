using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DirectedAgent : MonoBehaviour
{

    private NavMeshAgent agent;

    private GameObject player = null;

    private float nextActionTime = 0.0f;
    public float period = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.Find("Player");
    }


            // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToLocation()
    {
        agent.destination = player.transform.position;
        agent.isStopped = false;
    }


    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            MoveToLocation();
        }

    }
}