using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_enemies : MonoBehaviour
{
    public float cd_spawn = 15;
    private float cd = 0;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
        cd++;
    }

    void Spawn()
    {
        if (cd*Time.deltaTime >= cd_spawn)
        {
            Instantiate(Enemy, this.transform.position, Quaternion.identity);
            cd = 0;
        }
    }
}
