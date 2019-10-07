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
    public int counterEnnemiesGrowRate = 0;
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
                    counterEnnemiesGrowRate+= (int)Mathf.Max(2,counterEnnemiesGrowRate*1.3F);
                }
                else
                {
                    ennemiesPerWave ++;
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
                Ennemy_Controller ec = Instantiate(prefab, this.transform.position, Quaternion.identity).GetComponent<Ennemy_Controller>();
                if(ec != null) {
                    ec.current_healthPoint = (int)(ec.current_healthPoint * Mathf.Max(1, Mathf.Pow(1.1F, counterEnnemiesGrowRate)));
                    ec.movementSpeed = Mathf.Min(Controller.maxSpeed * 1.1F, ec.movementSpeed * Mathf.Pow(1.1F, counterEnnemiesGrowRate));
                }
                ennemiesLeft--;
            } else
            {
                GameObject prefab = Prefabs_secondLevel[Random.Range(0, Prefabs_secondLevel.Length)];
                Ranged_enemy_controllers ec = Instantiate(prefab, this.transform.position, Quaternion.identity).GetComponent<Ranged_enemy_controllers>();
                ec.current_healthPoint = ec.current_healthPoint * Mathf.Max(1, (int)(Mathf.Pow(1.1F, counterEnnemiesGrowRate)));
                ec.movementSpeed = Mathf.Min(Controller.maxSpeed * 1.3F, ec.movementSpeed * Mathf.Pow(1.1F, counterEnnemiesGrowRate));
                ennemiesLeft -= levelTwoWeight;
            }

            hudController.ennemiesCount++;
            cd = 0;
        }
    }
}
