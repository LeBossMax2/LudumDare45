using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDOOM : MonoBehaviour
{
    public float cd_spawn = 150;
    public int maxEnnemiesPerWave = 30;
    public int ennemiesPerWave = 10;
    private int ennemiesLeft = 10;

    protected float cd = 0;
    public GameObject[] minions;

    bool inUse = true;
    

    // Update is called once per frame
    void Update()
    {
        if (inUse)
        {
            if (ennemiesLeft <= 0)
            {
                if (ennemiesPerWave < maxEnnemiesPerWave)
                {
                    ennemiesPerWave++;
                }
                ennemiesLeft = ennemiesPerWave;
            } else
            {
                Spawn();
            }
            cd += Time.deltaTime;
        }
        inUse = 40 > (HudController.ennemiesCount);
    }

    private void Spawn()
    {
        if (cd >= cd_spawn / ennemiesPerWave)
        {
            GameObject prefab = minions[Random.Range(0, minions.Length)];
            Ennemy_Controller ec = Instantiate(prefab, this.transform.position, Quaternion.identity).GetComponent<Ennemy_Controller>();

            ennemiesLeft--;
            HudController.ennemiesCount++;
            Ranged_enemy_controllers hp = this.GetComponent<Ranged_enemy_controllers>();
            if(hp != null)
            {
                hp.current_healthPoint++;
            }
            cd = 0;
        }
    }
}
