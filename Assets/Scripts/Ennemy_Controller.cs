﻿using System.Collections;
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

    // Sound Effects
    public AudioClip soundEffect_death;

    GameObject PHolder;
    AudioSource audioSource_death;

    public void Awake()
    {
        PHolder = GameObject.Find("PumpkinAudioHolder");
        audioSource_death = PHolder.GetComponent<AudioSource>();
    }


    public void die()
    {
        Controller.killCount++;
        HudController.ennemiesCount--;
        Destroy(gameObject);
    }

    public override void damage(int value)
    {
        current_healthPoint -= value;
        if (current_healthPoint <= 0)
        {
            audioSource_death.PlayOneShot(soundEffect_death, 0.1f);
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
