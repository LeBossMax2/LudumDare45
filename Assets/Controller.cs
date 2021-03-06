﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : Character
{
    public static int killCount = 0;

    public CameraTarget cam;

    public int max_healthPoint = 100;
    public int current_healthPoint = 1;
    // Time value
    public float movementSpeed = 350;
    public static float maxSpeed = 700;

    public float reloadDelay = 10;
    public int damageDone = 1;
    public int levelOfWeapon = 1;
    
    public GameObject character;

    public float bulletSpeed;
    public Bullet bullet;

    public GameObject skull;
    public GameObject soul;

    public Transform skullPos;
    public Transform skullArrowIndicator;

    public int soulCounter = 1;

    private bool hasWeapon = false;

    private float reloadTimer = 0;

    private Vector2 movement;

    // Player Sounds
    public AudioClip BulletShoot;
    public AudioSource PlayerAudio;

    private Vector3 spawnPos;


    // Start is called before the first frame update
    void Start()
    {
        killCount = 0;
        current_healthPoint = this.max_healthPoint;
        cam = GetComponent<CameraTarget>();
        PlayerAudio = GetComponent<AudioSource>();
        spawnPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if(0 != Time.timeScale)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                Time.timeScale = 0;
                foreach (AudioSource audioSource in (cam.camHolder.GetComponentsInChildren<AudioSource>()))
                {
                    audioSource.Pause();
                }
                PlayerPrefs.SetInt("SPOOKTOBERSURVIVAL_dmg", (int)(damageDone * Mathf.Pow(2.3F, levelOfWeapon)));
                SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
            }
            movement.x = 0;
            movement.y = 0;
            if (reloadTimer > 0) reloadTimer -= Time.deltaTime;

            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            if (inputX > 0)
            {
                movement.x += movementSpeed;
            }
            else if (inputX < 0)
            {
                movement.x -= movementSpeed;
            }

            if (inputY > 0)
            {
                movement.y += movementSpeed;
            }
            else if (inputY < 0)
            {
                movement.y -= movementSpeed;
            }

            Vector3 mouseDir = cam.getMousePosInWorld() - transform.position;
            mouseDir.y = 0;

            if (hasWeapon && Input.GetAxis("Fire1") > 0 && reloadTimer <= 0)
            {
                fire(mouseDir);
            }

            if (hasWeapon)
                transform.localRotation = Quaternion.LookRotation(-mouseDir, Vector3.up);
            else if (skullArrowIndicator != null && skullPos != null)
                skullArrowIndicator.localRotation = Quaternion.LookRotation(skullPos.position - transform.position, Vector3.up);

            //If the player is leaving the map or doesn't have any hp left or if he doesn't want to kill anyone
            if (Math.Abs(this.transform.position.y) >= 10 || this.current_healthPoint <= 0 || HudController.ennemiesCount>HudController.maxNumberEnnemies)
            {
                RestartGame();
            }
        }
    }

    private void fire(Vector3 mouseDir)
    {
        if(damageDone >=5 || reloadDelay <= 0.2F)
        {
            levelOfWeapon++;
            damageDone = Math.Max(1,damageDone/2);
            reloadDelay = Mathf.Min(0.5F,reloadDelay+0.15F);
        }

        Bullet b = Instantiate(bullet);
        b.transform.position = transform.position;
        b.damage = (int)(damageDone * Mathf.Pow(2.3F, levelOfWeapon));
        b.shoot(bulletSpeed, mouseDir);

        /*int centralShot = levelOfWeapon % 2;
        int pairLevelOfWeapon = levelOfWeapon-centralShot;
        if (1 == centralShot)
        {
            Bullet b = Instantiate(bullet);
            b.transform.position = transform.position;
            b.damage = (int)(damageDone * Mathf.Pow(2.3F, levelOfWeapon));
            b.shoot(bulletSpeed, mouseDir);
        }*/
        // Generate a collision bug where entities are killed twice or more instead of just one...
        /*
        for (int i = 1; i <= pairLevelOfWeapon/2; i++)
        {
            Bullet b = Instantiate(bullet);
            b.transform.position = transform.position;
            b.damage = (int)(damageDone * Mathf.Pow(2.3F, levelOfWeapon));
            Vector3 dirrectionBullet = mouseDir;
            if ((mouseDir.x > 0 && mouseDir.z > 0) || (mouseDir.x <= 0 && mouseDir.z <= 0))
            {
                dirrectionBullet.x += -i;
                dirrectionBullet.z += i;
            }
            else
            {
                dirrectionBullet.x += i;
                dirrectionBullet.z += i;
            }
            b.shoot(bulletSpeed, dirrectionBullet);

            Bullet b2 = Instantiate(bullet);
            b2.transform.position = transform.position;
            b2.damage = (int)(damageDone * Mathf.Pow(2.3F, levelOfWeapon));
            dirrectionBullet = mouseDir;
            if ((mouseDir.x > 0 && mouseDir.z > 0) || (mouseDir.x <= 0 && mouseDir.z <= 0))
            {
                dirrectionBullet.x += i;
                dirrectionBullet.z += -i;
            }
            else
            {
                dirrectionBullet.x += -i;
                dirrectionBullet.z += -i;
            }
            b2.shoot(bulletSpeed, dirrectionBullet);
        }
        */
        GetComponent<AudioSource>().PlayOneShot(BulletShoot, 0.5f);
        reloadTimer = reloadDelay;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(movement.x * Time.fixedDeltaTime, 0, movement.y * Time.fixedDeltaTime);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        PlayerPrefs.SetInt("SPOOKTOBERSURVIVAL_score", killCount);
        PlayerPrefs.SetInt("SPOOKTOBERSURVIVAL_dmg", (int)(damageDone * Mathf.Pow(2.3F, levelOfWeapon)));
    }

    public void die()
    {
        soulCounter--;
        if (soulCounter > 0)
        {
            if (hasWeapon) skullPos.position = transform.position;
            setHasWeapon(false);
            current_healthPoint = max_healthPoint;
            transform.position = spawnPos;
            transform.rotation = Quaternion.identity;
            skullPos.gameObject.SetActive(true);
        }
        else
            RestartGame();
    }

    public void regen(int value)
    {
        current_healthPoint += value;
        if (current_healthPoint > max_healthPoint) current_healthPoint = max_healthPoint;
    }

    public override void damage(int value)
    {
        current_healthPoint -= value;
        if (current_healthPoint <= 0)
            die();
    }

    public void setHasWeapon()
    {
        setHasWeapon(true);
    }

    private void setHasWeapon(bool hasWeapon)
    {
        skull.SetActive(hasWeapon);
        soul.SetActive(!hasWeapon);
        this.hasWeapon = hasWeapon;
    }

    public bool HasWeapon { get => hasWeapon; }
}
