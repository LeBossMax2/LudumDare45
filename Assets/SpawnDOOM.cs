using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnDOOM : MonoBehaviour
{
    protected float cd = 0;

    public int ennemiesPerWave = 2;
    public float timeToSpawnOneEnnemy = 10;
    private int entGeneratedThisWave = 0;

    public GameObject[] minions;
    public int[] minionsProbas;
    public int healPerMinion = 0;

    public bool active = false;
    public int localWaveNumber = 1;
    // Update is called once per frame
    void Update()
    {
        cd += Time.deltaTime;
        if (cd >= timeToSpawnOneEnnemy)
        {
            active = HudController.maxNumberEnnemies > (HudController.ennemiesCount);
            if (active)
            {
                Spawn();
            }
            cd = 0;
        }
    }

    void Spawn()
    {
        GameObject badGuy = Instantiate(minions[Random.Range(0, minions.Length)], this.transform.position, Quaternion.identity);
        Ranged_enemy_controllers hp = this.GetComponent<Ranged_enemy_controllers>();
        if (hp != null)
        {
            badGuy.GetComponent<NavMeshAgent>().speed = this.GetComponent<NavMeshAgent>().speed * 1.25F;
            badGuy.GetComponent<DirectedAgent>().period *= this.GetComponent<Ranged_enemy_directed_agent>().period * 0.8F;
            badGuy.GetComponent<Ennemy_Controller>().damageDone += (int)Mathf.Max((hp.current_healthPoint*0.3F), localWaveNumber * 1.3F);
            entGeneratedThisWave++;
            HudController.ennemiesCount++;
            hp.current_healthPoint+=healPerMinion*localWaveNumber;
        }
        if (entGeneratedThisWave >= ennemiesPerWave)
        {
            localWaveNumber++;
            entGeneratedThisWave = 0;
        }
    }
}
