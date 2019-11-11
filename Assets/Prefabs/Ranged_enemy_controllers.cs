using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_enemy_controllers : Character
{
    public int current_healthPoint = 0;
    // Time value
    public float movementSpeed = 10;
    public int damageDone = 1;

    // Sound Effects
    public AudioClip GhostDeath;

    GameObject PHolder;
    AudioSource audioGhostDeath;

    public void Awake()
    {
        PHolder = GameObject.Find("GhostAudioHolder");
        audioGhostDeath = PHolder.GetComponent<AudioSource>();
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
            audioGhostDeath.PlayOneShot(GhostDeath, 0.2f);
            die();
        }
    }
}