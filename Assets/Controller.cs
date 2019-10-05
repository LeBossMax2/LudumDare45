using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : Character
{
    public static int killCount = 0;

    private CameraTarget cam;

    public int max_healthPoint = 100;
    public int current_healthPoint = 1;
    // Time value
    public float reloadDelay = 10;
    public float movementSpeed = 300;
    public int damageDone = 1;
    
    public GameObject character;

    public float bulletSpeed;
    public Bullet bullet;
    
    public bool hasWeapon = false;

    private float reloadTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        killCount = 0;
        current_healthPoint = this.max_healthPoint;
        cam = GetComponent<CameraTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = 0;
        float movementZ = 0;
        if (reloadTimer > 0) reloadTimer -= Time.deltaTime;
        float a = 1;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            a = 10;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementX += movementSpeed * Time.deltaTime * a;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            movementX -= movementSpeed * Time.deltaTime * a;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            movementZ += movementSpeed * Time.deltaTime * a;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementZ -= movementSpeed * Time.deltaTime * a;
        }

        GetComponent<Rigidbody>().velocity = new Vector3(movementX, 0, movementZ);
        Vector3 mouseDir = cam.getMousePosInWorld() - transform.position;
        mouseDir.y = 0;

        if (hasWeapon && Input.GetKey(KeyCode.Mouse0) && reloadTimer <= 0)
        {
            Bullet b = Instantiate(bullet);
            b.transform.position = transform.position;
            b.damage = damageDone;
            b.shoot(bulletSpeed, mouseDir);
            reloadTimer = reloadDelay;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            //Button pause
        }

        transform.localRotation = Quaternion.LookRotation(-mouseDir, Vector3.up);

        if (this.transform.position.y <= -10 || this.current_healthPoint <= 0)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void die()
    {
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
}
