using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform gunTip;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;

    private GameObject gun;

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % 50 == 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("here");
        gun = Instantiate(bullet);
        gun.GetComponent<Bullet>().SetPos(gunTip.transform);
        gun.transform.LookAt(gameObject.transform);
        gun.GetComponent<Rigidbody>().AddForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
    }
}
