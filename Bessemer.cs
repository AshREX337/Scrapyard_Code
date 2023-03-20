using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bessemer : MonoBehaviour
{
    [SerializeField] private GameObject steelBar;
    [SerializeField] private float upSpeed, smashSpeed;

    [SerializeField] private GameObject hitIndicator;

    private GameObject bar, hit;
    private int spikeCount, cageCount = 0, frame = 0;
    private bool spike, cage, side, down;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            steelBar.tag = "Steel";
            spike = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            steelBar.tag = "Steel";
            if (hit != null)
            {
                Destroy(hit);
            }
            cageCount = 0;
            cage = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            side = true;
        }

        if (side)
        {
            side = false;
            sideSmash();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            down = true;
        }

        if (down)
        {
            down = false;
            downSmash();
        }

        if (cageCount == 0 && cage)
        {
            hit = Instantiate(hitIndicator);
            hit.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0,-4.7f,0), Quaternion.identity);
            hit.transform.localScale = Vector3.one * 0.5f;
            steelBar.tag = "Stun";
            cageCount++;
        }
        else if(cageCount == 1)
        {
            BeamCage();
        }

        if (spike && Time.frameCount % 10 == 0)
        {
            if(spikeCount < 50)
            {
                Spikes();
                spikeCount++;
            }
            else
            {
                spikeCount = 0;
                spike = false;
            }
        }
    }

    private void sideSmash()
    {
        steelBar.tag = "Smash";
        bar = Instantiate(steelBar);
        bar.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, -4.2f, -1000), Quaternion.identity);
        bar.transform.localScale = Vector3.one * 20;
        bar.GetComponent<Rigidbody>().AddForce(Vector3.forward * smashSpeed, ForceMode.Impulse);
        Destroy(bar, 3f);

        hit = Instantiate(hitIndicator);
        hit.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, -4.2f, -50), Quaternion.identity);
        hit.transform.localScale = new Vector3(2,2,30);
        Destroy(hit, 2f);

        bar = Instantiate(steelBar);
        bar.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, -4.2f, 1000), Quaternion.identity);
        bar.transform.localScale = Vector3.one * 20;
        bar.GetComponent<Rigidbody>().AddForce(-Vector3.forward * smashSpeed, ForceMode.Impulse);
        Destroy(bar, 3f);

        hit = Instantiate(hitIndicator);
        hit.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, -4.2f, 50), Quaternion.identity);
        hit.transform.localScale = new Vector3(2, 2, 30);
        Destroy(hit, 2f);



    }

    private void downSmash()
    {
        steelBar.tag = "Smash";
        bar = Instantiate(steelBar);
        bar.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 1000, 0), Quaternion.Euler(90,0,0));
        bar.transform.localScale = Vector3.one * 20;
        bar.GetComponent<Rigidbody>().AddForce(Vector3.down * smashSpeed, ForceMode.Impulse);
        Destroy(bar, 3f);

        hit = Instantiate(hitIndicator);
        hit.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, -4.2f, 0), Quaternion.identity);
        hit.transform.localScale = new Vector3(4, 4, 4);
        Destroy(hit, 2f);
    }

        private void BeamCage()
    {
        
        if(Time.frameCount % 10 == 0)
        {
            hit.transform.localScale += Vector3.one * 0.05f;
        }
        frame++;
        if (frame - 75 == 0)
        {
            Destroy(hit, 1f);
            bar = Instantiate(steelBar);
            bar.transform.SetPositionAndRotation(hit.transform.position + new Vector3(-3, -1f, -3), Quaternion.Euler(new Vector3(135, 35, 0)));
            bar.transform.localScale = Vector3.one * 1.5f;
            Destroy(bar, 4f);

            bar = Instantiate(steelBar);
            bar.transform.SetPositionAndRotation(hit.transform.position + new Vector3(-3, -1f, 0), Quaternion.Euler(new Vector3(135, 90, 0)));
            bar.transform.localScale = Vector3.one * 1.5f;
            Destroy(bar, 4f);

            bar = Instantiate(steelBar);
            bar.transform.SetPositionAndRotation(hit.transform.position + new Vector3(-3, -1f, 3), Quaternion.Euler(new Vector3(135, 135, 0)));
            bar.transform.localScale = Vector3.one * 1.5f;
            Destroy(bar, 4f);

            bar = Instantiate(steelBar);
            bar.transform.SetPositionAndRotation(hit.transform.position + new Vector3(3, -1f, -3), Quaternion.Euler(new Vector3(135, -35, 0)));
            bar.transform.localScale = Vector3.one * 1.5f;
            Destroy(bar, 4f);

            bar = Instantiate(steelBar);
            bar.transform.SetPositionAndRotation(hit.transform.position + new Vector3(0, -1f, -3), Quaternion.Euler(new Vector3(135, 0, 0)));
            bar.transform.localScale = Vector3.one * 1.5f;
            Destroy(bar, 4f);

            cageCount++;
            cage = false;
            frame = 0;
            steelBar.tag = "Steel";
        }


    }

    private void Spikes()
    {
        var rand = new Vector3(Random.Range(-30, 30), -100, Random.Range(-30, 30));

        bar = Instantiate(steelBar);
        bar.transform.SetPositionAndRotation(GameObject.FindGameObjectWithTag("Player").transform.position + rand, Quaternion.Euler(new Vector3(90, 0, 0)));
        bar.GetComponent<Rigidbody>().AddForce(Vector3.up * upSpeed, ForceMode.Impulse);
        Destroy(bar, 3f);

        hit = Instantiate(hitIndicator);
        hit.transform.SetPositionAndRotation(bar.transform.position + new Vector3(0, 95.8f, 0), Quaternion.identity);
        Destroy(hit, 1f);
    }
}
