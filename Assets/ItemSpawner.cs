using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Spawner
{
    public bool IsItemPresent;
    public bool isAffected_timerModif;
    public int nbSecondsAdded_timerModif = 5;

    // Update is called once per frame
    private void Update()
    {
        Spawn();
        if (!IsItemPresent) cd += Time.deltaTime;
    }

    protected override void Spawn()
    {
        if (cd >= cd_spawn)
        {
            if (!IsItemPresent)
            {
                GameObject prefab = Prefabs_firstLevel[Random.Range(0, Prefabs_firstLevel.Length)];
                SoolItem item = Instantiate(prefab, this.transform.position, Quaternion.identity).GetComponent<SoolItem>();
                item.Init(this);
                IsItemPresent = true;
                cd = 0;
                if (this.isAffected_timerModif)
                {
                    this.cd_spawn += this.nbSecondsAdded_timerModif;
                }
            }
        }
    }
}
