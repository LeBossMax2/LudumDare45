using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnWave : MonoBehaviour
{
    protected float cd = 0;

    public int ennemiesPerWave = 5;
    public float timeToSpawnOneEnnemy = 10;
    private int entGeneratedThisWave = 0;

    public GameObject[] ennemies;
    public int[] ennemiesProbas;

    public bool active = false;
    public int localWaveNumber = 1;
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
        } else if(cd >= timeToSpawnOneEnnemy)
        {
            active = HudController.maxNumberEnnemies > (HudController.ennemiesCount);
            cd = 0;
        }
    }

    protected virtual void Spawn()
    {
        if (cd >= timeToSpawnOneEnnemy)
        {
            int[] probas = new int[ennemies.Length];
            probas[0] = ennemiesProbas[0];
            for (int i = 1; i < ennemies.Length; i++)
            {
                probas[i] = ennemiesProbas[i]+probas[i-1];
            }
            int randomValueThatDetermineTheEnnemyChoosenToSpawn = Random.Range(0, probas[probas.Length-1]);
            int finalNumOfEnnemyToSpawn = 0;
            bool isFinalNumOfEnnemyToSpawnFound = false;
            int rangeInProbas = 0;
            while (!isFinalNumOfEnnemyToSpawnFound && rangeInProbas < probas.Length)
            {
                if(randomValueThatDetermineTheEnnemyChoosenToSpawn <= probas[rangeInProbas])
                {
                    finalNumOfEnnemyToSpawn = rangeInProbas;
                    isFinalNumOfEnnemyToSpawnFound = true;
                }
                rangeInProbas++;
            }

            GameObject badGuy = Instantiate(ennemies[finalNumOfEnnemyToSpawn], this.transform.position, Quaternion.identity);

            Ennemy_Controller ec = badGuy.GetComponent<Ennemy_Controller>();
            if (ec != null)
            {
                // Because fuck the player ! I want the ennemies to have hp
                ec.current_healthPoint = (int)(Mathf.Max(ec.current_healthPoint, ec.current_healthPoint * localWaveNumber * 1.05F));
                ec.damageDone += localWaveNumber*2;
                DirectedAgent da = badGuy.GetComponent<DirectedAgent>();
                da.period *= Mathf.Min(2, Mathf.Max(0.5F, da.period * Mathf.Pow(0.8F, localWaveNumber)));
            } else
            {
                Ranged_enemy_controllers rec = badGuy.GetComponent<Ranged_enemy_controllers>();
                if(rec != null)
                {
                    rec.current_healthPoint = (int)(Mathf.Max(rec.current_healthPoint, rec.current_healthPoint * localWaveNumber * 1.03F));
                    Ranged_enemy_directed_agent dirAgent = rec.GetComponent<Ranged_enemy_directed_agent>();
                    dirAgent.cd_fire = dirAgent.cd_fire * Mathf.Min(1, (Mathf.Pow(0.9F, localWaveNumber)));
                    dirAgent.bulletSpeed = dirAgent.bulletSpeed * Mathf.Min(Mathf.Max(1, (int)(Mathf.Pow(1.05F, localWaveNumber))), Controller.maxSpeed * 1.3F);
                }
                Ranged_enemy_directed_agent reda = badGuy.GetComponent<Ranged_enemy_directed_agent>();
                reda.period *= Mathf.Min(2, Mathf.Max(0.5F, reda.period * Mathf.Pow(0.8F, localWaveNumber)));
            }

            NavMeshAgent nva = badGuy.GetComponent<NavMeshAgent>();
            nva.speed = Mathf.Min(13, nva.speed * Mathf.Pow(1.08F, localWaveNumber));

            cd = 0;
            HudController.ennemiesCount++;

            entGeneratedThisWave++;
            if (entGeneratedThisWave >= ennemiesPerWave)
            {
                entGeneratedThisWave = 0;
                localWaveNumber++;
            }
        }
    }

    public void restart()
    {
        cd = 0;
        active = false;
    }
}
