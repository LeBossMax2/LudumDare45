using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : Character
{
    private CameraTarget cam;

    public int max_healthPoint = 100;
    public int current_healthPoint = 1;
    // Time value
    public float reloadDelay = 10;
    public float movementSpeed = 69.0f;
    public int damageDone = 1;
    
    public GameObject character;

    public float bulletSpeed;
    public Bullet bullet;

    private float reloadTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        current_healthPoint = this.max_healthPoint;
        cam = GetComponent<CameraTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        if (reloadTimer > 0) reloadTimer -= Time.deltaTime;
        float a = 1;
        if (Input.GetKey(KeyCode.Space))
        {
            a = 30;
        }

        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().velocity = Vector3.right * movementSpeed * Time.deltaTime * a;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            GetComponent<Rigidbody>().velocity = Vector3.left * movementSpeed * Time.deltaTime * a;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            GetComponent<Rigidbody>().velocity = Vector3.forward * movementSpeed * Time.deltaTime * a;
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().velocity = Vector3.back * movementSpeed * Time.deltaTime * a;
        }
        if (Input.GetKey(KeyCode.Mouse0) && reloadTimer <= 0)
        {
            Bullet b = Instantiate(bullet);
            b.transform.position = transform.position;
            b.damage = damageDone;
            Vector3 dir = cam.getMousePosInWorld() - transform.position;
            dir.y = 0;
            b.shoot(bulletSpeed, dir);
            reloadTimer = reloadDelay;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            //Button pause
        }

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

    public override void damage(int value)
    {
        current_healthPoint -= value;
        if (current_healthPoint <= 0)
            die();
    }
}
