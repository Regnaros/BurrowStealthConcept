using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0f, 2.0f);
    }

    // Update is called once per frame
    void Shoot()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.x += 90;
        rotation.z = 0;
        rotation.y = 0;
        
        GameObject myBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        
        myBullet.transform.parent = transform;

        myBullet.transform.localRotation = Quaternion.Euler(rotation);
    }
}
