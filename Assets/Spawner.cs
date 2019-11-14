using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private void Awake()
    {
        ennemiesLeft = ennemiesPerWave;
    }

    // Update is called once per frame
    private void Update()
    {
        if (inUse)
        {
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
            } else
            {
                Spawn();
            }
            cd += Time.deltaTime;
        }
        inUse = HudController.maxNumberEnnemies > (HudController.ennemiesCount);
    }

    protected virtual void Spawn()
    {
        if (cd >= cd_spawn/ennemiesPerWave)
        {
            if(ennemiesLeft < levelTwoWeight)
            {
                GameObject badGuy = Instantiate(Prefabs_firstLevel[Random.Range(0, Prefabs_firstLevel.Length)], this.transform.position, Quaternion.identity);
                Ennemy_Controller ec = badGuy.GetComponent<Ennemy_Controller>();
                if (ec != null) {
                    ec.current_healthPoint = (int)(ec.current_healthPoint * Mathf.Max(1, Mathf.Pow(1.08F, counterEnnemiesGrowRate)));
                    ec.damageDone += HudController.currentWaveNumber;
                }

                NavMeshAgent nva = badGuy.GetComponent<NavMeshAgent>();
                nva.speed = Mathf.Min(11,Mathf.Max(3.5F, nva.speed * Mathf.Pow(1.08F, HudController.currentWaveNumber - 9)));
                DirectedAgent da = badGuy.GetComponent<DirectedAgent>();
                da.period *= Mathf.Min(2, Mathf.Max(0.5F, da.period * Mathf.Pow(0.8F, HudController.currentWaveNumber - 9)));

                ennemiesLeft--;
            } else
            {
                GameObject badGuy = Instantiate(Prefabs_secondLevel[Random.Range(0, Prefabs_secondLevel.Length)], this.transform.position, Quaternion.identity);
                Ranged_enemy_controllers ec = badGuy.GetComponent<Ranged_enemy_controllers>();

                ec.current_healthPoint = ec.current_healthPoint * Mathf.Max(1, (int)(Mathf.Pow(1.08F, counterEnnemiesGrowRate)));

                Ranged_enemy_directed_agent dirAgent = ec.GetComponent<Ranged_enemy_directed_agent>();
                dirAgent.cd_fire = dirAgent.cd_fire * Mathf.Min(1, (Mathf.Pow(0.9F, counterEnnemiesGrowRate)));
                dirAgent.bulletSpeed = dirAgent.bulletSpeed * Mathf.Min(Mathf.Max(1, (int)(Mathf.Pow(1.05F, counterEnnemiesGrowRate))),Controller.maxSpeed*1.3F);

                NavMeshAgent nva = badGuy.GetComponent<NavMeshAgent>();
                nva.speed = Mathf.Min(11, Mathf.Max(3.5F, nva.speed * Mathf.Pow(1.08F, HudController.currentWaveNumber - 9)));
                Ranged_enemy_directed_agent reda = badGuy.GetComponent<Ranged_enemy_directed_agent>();
                reda.period *= Mathf.Min(2, Mathf.Max(0.5F, reda.period * Mathf.Pow(0.9F, HudController.currentWaveNumber - 9)));

                ennemiesLeft -= levelTwoWeight;
            }

            HudController.ennemiesCount++;
            cd = 0;
        }
    }
}
