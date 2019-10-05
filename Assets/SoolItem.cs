using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoolItem : MonoBehaviour
{
    public enum BonusType
    {
        HP,
        Dmg,
        Speed,
        FireRate
    }

    public BonusType type;
    public int value;

    private void OnTriggerEnter(Collider other)
    {
        Controller c = other.GetComponent<Controller>();
        if (c != null && !c.isBad)
        {
            switch (type)
            {
                case BonusType.HP:
                    c.max_healthPoint += value;
                    break;
                case BonusType.Dmg:
                    c.damageDone += value;
                    break;
                case BonusType.Speed:
                    c.movementSpeed += value;
                    break;
                case BonusType.FireRate:
                    c.reloadDelay *= (value / 100.0f);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
