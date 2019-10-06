using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Spawner
{
    public bool IsItemPresent;

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
            }
        }
    }
}
