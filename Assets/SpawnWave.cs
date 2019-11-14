using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnWave : MonoBehaviour
{
    public float durationWave = 60;
    protected float cd = 0;

    public int maxEnnemiesPerWave = 10;
    public int ennemiesPerWave = 1;
    public GameObject[] ennemies;

    public bool active = false;
    public int localWaveNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cd += Time.deltaTime;
        if (active)
        {
            Spawn();
        } else if(cd >= durationWave / 10)
        {
            active = HudController.maxNumberEnnemies > (HudController.ennemiesCount);
            cd = 0;
        }
    }

    protected virtual void Spawn()
    {
        if (cd >= durationWave / ennemiesPerWave)
        {
            GameObject badGuy = Instantiate(ennemies[Random.Range(0, ennemies.Length)], this.transform.position, Quaternion.identity);
            Ennemy_Controller ec = badGuy.GetComponent<Ennemy_Controller>();
            if (ec != null)
            {
                ec.current_healthPoint = (int)(ec.current_healthPoint * Mathf.Max(1, Mathf.Pow(1.1F, HudController.currentWaveNumber-9)));
                ec.damageDone += HudController.currentWaveNumber;
                DirectedAgent da = badGuy.GetComponent<DirectedAgent>();
                da.period *= Mathf.Min(2, Mathf.Max(0.5F, da.period * Mathf.Pow(0.8F, HudController.currentWaveNumber - 9)));
            } else
            {
                Ranged_enemy_controllers rec = badGuy.GetComponent<Ranged_enemy_controllers>();
                if(rec != null)
                {
                    rec.current_healthPoint = rec.current_healthPoint * Mathf.Max(1, (int)(Mathf.Pow(1.1F, HudController.currentWaveNumber - 9)));
                    Ranged_enemy_directed_agent dirAgent = rec.GetComponent<Ranged_enemy_directed_agent>();
                    dirAgent.cd_fire = dirAgent.cd_fire * Mathf.Min(1, (Mathf.Pow(0.86F, HudController.currentWaveNumber - 9)));
                    dirAgent.bulletSpeed = dirAgent.bulletSpeed * Mathf.Min(Mathf.Max(1, (int)(Mathf.Pow(1.05F, HudController.currentWaveNumber - 9))), Controller.maxSpeed * 1.3F);
                }
                Ranged_enemy_directed_agent reda = badGuy.GetComponent<Ranged_enemy_directed_agent>();
                reda.period *= Mathf.Min(2, Mathf.Max(0.5F, reda.period * Mathf.Pow(0.8F, HudController.currentWaveNumber - 9)));
            }

            NavMeshAgent nva = badGuy.GetComponent<NavMeshAgent>();
            nva.speed = Mathf.Min(13, nva.speed * Mathf.Pow(1.08F, HudController.currentWaveNumber - 9));

            cd = 0;
            HudController.ennemiesCount++;
        }
    }

    public void restart()
    {
        cd = 0;
        active = false;
    }
}
