using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Ranged_enemy_directed_agent : MonoBehaviour
{

    private NavMeshAgent agent;

    private GameObject player = null;

    private float nextActionTime = 0.0f;
    public float period = 2.0f;
    public float distance = 4;
    public float cd_fire = 4;
    private float cd_fire_counter = 0;
    public Bullet bullet;
    public int damageDone = 1;
    public float bulletSpeed;

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
        float x = this.transform.position.x - player.transform.position.x;
        float z = this.transform.position.z - player.transform.position.z;
        if (x < 0)
            x = player.transform.position.x + distance;
        else
        {
            x = player.transform.position.x - distance;
        }
        if (z < 0)
            z = player.transform.position.x + distance;
        else
        {
            z = player.transform.position.x - distance;
        }

        agent.destination = new Vector3(x, 0, z);
        agent.isStopped = false;
    }


    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            MoveToLocation();
        }
        if (cd_fire_counter > cd_fire)
        {
            Fire();
            cd_fire_counter = 0;
        }
        else
        {
            cd_fire_counter += Time.deltaTime;
        }

    }
    private void Fire()
    {
        Bullet b = Instantiate(bullet, this.transform.position, Quaternion.identity);
        b.damage = damageDone;
        Vector3 dir = player.transform.position - transform.position;
        dir.y = 0;
        b.shoot(bulletSpeed, dir);
    }
}