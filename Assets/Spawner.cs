using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float cd_spawn = 15;
    protected float cd = 0;
    public GameObject[] Prefabs;

    // Update is called once per frame
    private void Update()
    {
        Spawn();
        cd += Time.deltaTime;
    }

    protected virtual void Spawn()
    {
        if (cd >= cd_spawn)
        {
            GameObject prefab = Prefabs[Random.Range(0, Prefabs.Length)];
            Instantiate(prefab, this.transform.position, Quaternion.identity);
            cd = 0;
        }
    }
}
