using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    public GameObject weapon;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(weapon, new Vector3(10, 0, 10), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
