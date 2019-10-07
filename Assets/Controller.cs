using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : Character
{
    private bool stopTime = false;
    public static int killCount = 0;

    private CameraTarget cam;

    public int max_healthPoint = 100;
    public int current_healthPoint = 1;
    // Time value
    public float reloadDelay = 10;
    public float movementSpeed = 300;
    public static float maxSpeed = 800;
    public int damageDone = 1;
    
    public GameObject character;

    public float bulletSpeed;
    public Bullet bullet;

    public GameObject skull;
    public GameObject soul;

    private bool hasWeapon = false;

    private float reloadTimer = 0;

    private Vector2 movement;

    // Player Sounds
    public AudioClip BulletShoot;
    public AudioSource PlayerAudio;


    // Start is called before the first frame update
    void Start()
    {
        killCount = 0;
        current_healthPoint = this.max_healthPoint;
        cam = GetComponent<CameraTarget>();
        PlayerAudio = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            stopTime = !stopTime;
            Time.timeScale = stopTime ? 0 : 1;
        }
        if(0 != Time.timeScale)
        {
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
                Bullet b = Instantiate(bullet);
                b.transform.position = transform.position;
                b.damage = damageDone;
                b.shoot(bulletSpeed, mouseDir);
                reloadTimer = reloadDelay;
                GetComponent<AudioSource>().PlayOneShot(BulletShoot, 0.5f);
            }

            if (hasWeapon) transform.localRotation = Quaternion.LookRotation(-mouseDir, Vector3.up);

            if (this.transform.position.y <= -10 || this.current_healthPoint <= 0)
            {
                RestartGame();
            }
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(movement.x * Time.fixedDeltaTime, 0, movement.y * Time.fixedDeltaTime);
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
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

    public void setHasWeapon()
    {
        skull.SetActive(true);
        soul.SetActive(false);
        hasWeapon = true;
    }

    public bool HasWeapon { get => hasWeapon; }
}
