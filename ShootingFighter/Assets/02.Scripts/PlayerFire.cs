using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject tmpBullet = Instantiate(bullet, firePoint);
        }

    }
}
