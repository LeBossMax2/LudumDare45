using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullItem : MonoBehaviour
{
    private void Update()
    {
        transform.localRotation *= Quaternion.Euler(0, Time.deltaTime * 100, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Controller c = other.GetComponent<Controller>();
        if (c != null && !c.isBad && !c.HasWeapon)
        {
            c.setHasWeapon();
            Destroy(gameObject);
        }
    }
}