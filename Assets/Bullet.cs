﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float rotationSpeed = 100;

    public bool isBad;

    private Rigidbody body;

    // Sound
    public AudioClip BulletImpact;
    GameObject BHolder;
    AudioSource audioBulletImpact;


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        BHolder = GameObject.Find("BulletAudioHolder");
        audioBulletImpact = BHolder.GetComponent<AudioSource>();
    }

    public void shoot(float speed, Vector3 dir)
    {
        body.velocity = dir.normalized * speed;
    }

    private void Update()
    {
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, Time.deltaTime * rotationSpeed, 0);
        if (transform.position.sqrMagnitude > 20000)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character ch = other.GetComponent<Character>();
        Bullet bu = other.GetComponent<Bullet>();
        if (ch != null && ch.isBad != isBad)
        {
            audioBulletImpact.PlayOneShot(BulletImpact, 0.3f);
            ch.damage(damage);
        }
        if ((ch == null || ch.isBad != isBad) && (bu == null || bu.isBad != isBad))
        {
            Destroy(gameObject);
        }
    }
}
