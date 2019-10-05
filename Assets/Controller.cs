using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public int max_healthPoint = 100;
    public int current_healthPoint = 1;
    // Time value
    public float reloadDelay = 10;
    public float movementSpeed = 69.0f;
    public int damageDone = 1;

    // Start is called before the first frame update
    void Start()
    {
        current_healthPoint = this.max_healthPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += Vector3.back * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // Add fire machin.
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
}
