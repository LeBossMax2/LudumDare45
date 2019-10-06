using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy_Controller : Character
{
    public int current_healthPoint = 0;
    // Time value
    public float movementSpeed = 10;
    public int damageDone = 1;

    private float nextActionTime = 0.0f;
    public float period = 1.0f;

    public void die()
    {
        Controller.killCount++;
        Destroy(gameObject);
    }

    public override void damage(int value)
    {
        current_healthPoint -= value;
        if (current_healthPoint <= 0)
        {
            die();
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (Time.time > nextActionTime)
        {    
            Character ch = col.gameObject.GetComponent<Character>();
            if (ch != null && ch.isBad != isBad)
            {
                ch.damage(damageDone);
                nextActionTime = Time.time + period;
            }
        }
    }
}
