using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulcan : MonoBehaviour
{

    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject beamPrefab, beamCollider;
    [SerializeField] private float beamSpeed;

    private GameObject fireball;
    private int fireCount = 0;
    private bool fire = false;

    private GameObject beam, col;
    private int beamCount = 0;
    private bool fireBeam = false;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            fire = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            fireBeam = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Destroy(beam);
            beamCount = 0;
            fireBeam = false;
        }

        if (fireBeam)
        {
            flameBlast();
        }

        if (fire)
        {
            Fireballs();
            if(fireCount == 11)
            {
                fire = false;
                fireCount= 0;
            }
        }
    }

    private void flameBlast()
    {
        if(beamCount == 0)
        {
            beam = Instantiate(beamPrefab);
            beam.transform.SetPositionAndRotation(transform.position, transform.rotation);
            beamCount++;
        }
        if(Time.frameCount % 20 == 0)
        {
            col = Instantiate(beamCollider);
            col.transform.SetPositionAndRotation(transform.position, transform.rotation);
            col.GetComponent<Rigidbody>().AddForce(Vector3.right * beamSpeed, ForceMode.Impulse);
            Destroy(col, 2f);
        }
    }

    private void Fireballs()
    {
        if(fireCount == 0)
        {
            fireball = Instantiate(fireballPrefab);
            fireCount++;
        }
        if (fireCount < 11 && fireball.transform.position.y > fireball.GetComponent<fireball>().getMaxHeight())
        {
            fireball.transform.SetPositionAndRotation(new Vector3(player.transform.position.x, fireball.transform.position.y, player.transform.position.z), Quaternion.Euler(new Vector3(180, 0, 0)));
            fireCount++;
            if(fireCount < 11)
            {
                fireball = Instantiate(fireballPrefab);
            }
        }
    }
}
