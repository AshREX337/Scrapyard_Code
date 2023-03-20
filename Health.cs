using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private GameObject hitEffect, damagePopup;
    private bool lava = false;
    private int lavaCount = 0, stun = 1, frame = 0;

    

    // Update is called once per frame
    void Update()
    {
        if(health < 0 && health != -1)
        {
            if (gameObject.tag.Equals("Enemy"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().setCollide(false);
            }
            Destroy(gameObject);
        }

        if(lava && Time.frameCount % 210 == 0)
        {
            lavaCount++;
            if(lavaCount == 7)
            {
                lava = false;
            }
        }

        if(lava && Time.frameCount % 50 == 0)
        {
            removeHealth(1, false);
        }
        if(GameObject.FindGameObjectWithTag("Lava") == null)
        {
            lava = false;
        }

        if (Camera.main.GetComponent<Animator>().GetBool("Shake") && Time.frameCount % 20 == 0)
        {
            Camera.main.GetComponent<Animator>().SetBool("Shake", false);
        }

        if(stun == -1)
        {
            frame++;
            if(frame-500 == 0)
            {
                gameObject.GetComponent<Movement>().setStun(1);
                stun = 1;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Weapon") && gameObject.tag.Equals("Enemy") && GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().GetBool("Attack"))
        {
            //just a comment 2
            var hit = Instantiate(hitEffect);
            hit.transform.SetPositionAndRotation(collision.GetContact(0).point, Quaternion.identity);
            Destroy(hit, 0.2f);
            removeHealth(collision.gameObject.GetComponent<Health>().getDamage(), true);
        }
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Lava") && gameObject.tag.Equals("Player"))
        {
            lava = true;
            lavaCount = 0;
        }
        if(collision.gameObject.tag.Equals("Fireball") && gameObject.tag.Equals("Player"))
        {
            removeHealth(5, false);
        }
        if (collision.gameObject.tag.Equals("Stun") && gameObject.tag.Equals("Player"))
        {
            removeHealth(10, false);
            gameObject.GetComponent<Movement>().setStun(-1);
            stun = gameObject.GetComponent<Movement>().getStun();
        }
        if (collision.gameObject.tag.Equals("Steel") && gameObject.tag.Equals("Player"))
        {
            removeHealth(10, false);
        }
        if (collision.gameObject.tag.Equals("Smash") && gameObject.tag.Equals("Player"))
        {
            removeHealth(50, false);
        }
    }


    public int getDamage()
    {
        return damage;
    }

    public int getHealth()
    {
        return health;
    }

    void removeHealth(int removed, bool p)
    {
        if(health > -2)
        {
            health -= removed;
        }

        var constant = 1;
        Debug.Log(constant);
        if(p)
        {
            damagePopup.GetComponent<TextMeshPro>().color = new Color(255, 92, 0);
        }
        else
        {
            damagePopup.GetComponent<TextMeshPro>().color = Color.red;
            Camera.main.GetComponent<Animator>().SetBool("Shake", true);
            

            constant *= -1;
        }
        
        var damage = Instantiate(damagePopup);
        damage.transform.SetPositionAndRotation(transform.position + new Vector3(0, 10, 0), Quaternion.Euler(new Vector3(30, -135, 0)));
        damage.GetComponent<DamagePopup>().Setup(constant * removed);
    }

    
}
