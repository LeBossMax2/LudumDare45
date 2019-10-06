using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float cd_spawn = 150;
    public int maxEnnemiesPerWave = 30;
    public int ennemiesPerWave = 10;
    private int ennemiesLeft = 10;
    private int counterEnnemiesGrowRate = 0;
    protected float cd = 0;
    public GameObject[] Prefabs_firstLevel;
    public GameObject[] Prefabs_secondLevel;

    // Update is called once per frame
    private void Update()
    {
        Spawn();
        cd += Time.deltaTime;

        if(ennemiesLeft <= 0)
        {
            if(ennemiesPerWave >= maxEnnemiesPerWave)
            {
                counterEnnemiesGrowRate++;
            } else
            {
                ennemiesPerWave += 2;
            }
            ennemiesLeft = ennemiesPerWave;
        }
    }

    protected virtual void Spawn()
    {
        if (cd >= cd_spawn/ennemiesPerWave)
        {
            if(ennemiesLeft < 10)
            {
                GameObject prefab = Prefabs_firstLevel[Random.Range(0, Prefabs_firstLevel.Length)];
                prefab.GetComponent<Ennemy_Controller>().current_healthPoint += counterEnnemiesGrowRate;
                Instantiate(prefab, this.transform.position, Quaternion.identity);
                ennemiesLeft--;
            } else
            {
                GameObject prefab = Prefabs_secondLevel[Random.Range(0, Prefabs_secondLevel.Length)];
                prefab.GetComponent<Ranged_enemy_controllers>().current_healthPoint += counterEnnemiesGrowRate;
                Instantiate(prefab, this.transform.position, Quaternion.identity);
                ennemiesLeft -= 10;
            }
            cd = 0;
        }
    }
}
