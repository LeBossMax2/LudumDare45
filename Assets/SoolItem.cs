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
        Respawn,
        ItemTimer,
        ScorePoints,
        Nothing
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
                    // We heal a bit more than we augment the player hp
                    c.regen(value + (10 * c.max_healthPoint / 100));
                    break;
                case BonusType.HP_Regen:
                    // The + 10 is a little hidden bonus from me because stay strong will ya ?
                    c.regen((value * c.max_healthPoint / 100) + 10);
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
                case BonusType.ItemTimer:
                    // 20 sec is the minimum interval between 2 spawn
                    if (spawner != null)
                    {
                        this.spawner.cd_spawn = Mathf.Max(this.spawner.cd_spawn - value, 20);
                    }
                    break;
                case BonusType.ScorePoints:
                    Controller.killCount += this.value;
                    break;
                case BonusType.Nothing:
                default:
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
