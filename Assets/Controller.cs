using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int max_healthPoint = 100;
    public int current_healthPoint = 0;
    // Time value
    public float reloadDelay = 10;
    public float movementSpeed = 169.0f;
    public int damageDone = 1;

    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            character.transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            character.transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            character.transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            character.transform.position += Vector3.back * movementSpeed * Time.deltaTime;
        }
    }

    public void die()
    {
        this.Start();
    }
}
