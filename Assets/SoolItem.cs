using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoolItem : MonoBehaviour
{
    public enum BonusType
    {
        Max_HP,
        HP_Regen,
        Dmg,
        Speed,
        FireRate,
        Respawn
    }

    public BonusType type;
    public int minValue;
    public int maxValue;
    private int value;

    private ItemSpawner spawner;

    private void Awake()
    {
        value = Random.Range(minValue, maxValue);
    }

    public void Init(ItemSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnTriggerEnter(Collider other)
    {
        Controller c = other.GetComponent<Controller>();
        if (c != null && !c.isBad && c.HasWeapon)
        {
            switch (type)
            {
                case BonusType.Max_HP:
                    c.max_healthPoint += value+(5*c.max_healthPoint/100);
                    c.regen(value + (5 * c.max_healthPoint / 100));
                    break;
                case BonusType.HP_Regen:
                    c.regen(value * c.max_healthPoint / 100);
                    break;
                case BonusType.Dmg:
                    c.damageDone += value;
                    break;
                case BonusType.Speed:
                    c.movementSpeed += value;
                    if (c.movementSpeed > Controller.maxSpeed) c.movementSpeed = Controller.maxSpeed;
                    break;
                case BonusType.FireRate:
                    c.reloadDelay *= (value / 100.0f);
                    break;
                case BonusType.Respawn:
                    c.soulCounter += value;
                    break;
            }
            if (spawner != null)
            {
                spawner.IsItemPresent = false;
                spawner = null;
            }
            Destroy(gameObject);
        }
    }
}
