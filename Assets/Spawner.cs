using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float cd_spawn = 150;
    public int maxEnnemiesPerWave = 30;
    public int ennemiesPerWave = 10;
    public int levelTwoWeight = 12;
    private int ennemiesLeft = 10;
    private int counterEnnemiesGrowRate = 0;
    protected float cd = 0;
    public GameObject[] Prefabs_firstLevel;
    public GameObject[] Prefabs_secondLevel;

    public bool inUse = true;
    public HudController hudController;

    // Update is called once per frame
    private void Update()
    {
        if (inUse)
        {
            Spawn();
            cd += Time.deltaTime;

            if (ennemiesLeft <= 0)
            {
                if (ennemiesPerWave >= maxEnnemiesPerWave)
                {
                    counterEnnemiesGrowRate++;
                }
                else
                {
                    ennemiesPerWave += 2;
                }
                ennemiesLeft = ennemiesPerWave;
            }
        }
        inUse = 40 > (hudController.ennemiesCount - Controller.killCount);
    }

    protected virtual void Spawn()
    {
        if (cd >= cd_spawn/ennemiesPerWave)
        {
            if(ennemiesLeft < levelTwoWeight)
            {
                GameObject prefab = Prefabs_firstLevel[Random.Range(0, Prefabs_firstLevel.Length)];
                Instantiate(prefab, this.transform.position, Quaternion.identity).GetComponent<Ennemy_Controller>().current_healthPoint += counterEnnemiesGrowRate;
                ennemiesLeft--;
            } else
            {
                GameObject prefab = Prefabs_secondLevel[Random.Range(0, Prefabs_secondLevel.Length)];
                Instantiate(prefab, this.transform.position, Quaternion.identity).GetComponent<Ranged_enemy_controllers>().current_healthPoint += counterEnnemiesGrowRate;
                ennemiesLeft -= levelTwoWeight;
            }

            hudController.ennemiesCount++;
            cd = 0;
        }
    }
}
