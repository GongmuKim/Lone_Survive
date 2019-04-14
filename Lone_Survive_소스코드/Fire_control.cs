using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_control : MonoBehaviour
{
    public Transform fire_pos;
    public GameObject bullet;
    //public GameObject[] muzzel_effect = new GameObject[5];
    public float bullet_speed = 3000f;
    public float shotgun_bullet_degree;
    float fire_delay;
    //bool is_fire;
    Vector3 start_pos;
    Quaternion start_rot;
    Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        gun_fire();
        //print("발사 가능? : " + is_fire);
    }

    void gun_fire()
    {
        switch(GameData.weapone_level)
        {
            case 1:
                fire_delay = 0.15f;
                //GameData.bullet_have = 15;
                single_fire_trigger();
                break;
            case 2:
                fire_delay = 0.1f;
                //GameData.bullet_have = 15;
                auto_fire_trigger();
                break;
            case 3:
                fire_delay = 0.08f;
                //GameData.fire_delay = 3f;
                //GameData.bullet_have = 35f;
                auto_fire_trigger();
                break;
            case 4:
                fire_delay = 0.45f;
                //shotgun_fire_trigger();
                single_fire_trigger();
                break;
        }

    }

    void single_fire_trigger()
    {
        if (GameData.is_fire && Input.GetButtonDown("Fire1") && GameData.bullet_have > 0 && GameData.is_reloading)
        {
            if(GameData.weapone_level == 1)
                StartCoroutine("Fire_delay");
            else
                StartCoroutine("Fire_shotgun_delay");
        }   
            /*
            else if(GameData.bullet_have <= 0)
            {
                Weapone_control temp = new Weapone_control();
                temp.Reload_bullet();
            }
            */
    }

    void auto_fire_trigger()
    {
       if (GameData.is_fire && Input.GetButton("Fire1") && GameData.bullet_have > 0 && GameData.is_reloading)     
       {
            StartCoroutine("Fire_delay");
       }     
            /*
            else if (GameData.bullet_have <= 0)
            {
                Weapone_control temp = new Weapone_control();
                temp.Reload_bullet();
            }
            */
    }

    /*
    void shotgun_fire_trigger()
    {
       if (GameData.is_fire && Input.GetButtonDown("Fire1") && GameData.bullet_have > 0 && GameData.is_reloading)     
       {
            StartCoroutine("Fire_shotgun_delay");
       }     
    }
    */

    IEnumerator Fire_delay()
    {
        GameData.is_fire = false;
        GameObject bullet_clone = Instantiate(bullet, fire_pos.position, fire_pos.rotation);
        GameData.bullet_have -= 1;
        /*
        start_pos = fire_pos.position;
        start_rot = fire_pos.rotation;
        transform.position = start_pos;
        transform.rotation = start_rot;
        */
        transform.position = fire_pos.position;
        transform.rotation = fire_pos.rotation;

        GameData.fire_sound.Play();
        rb = bullet_clone.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bullet_speed);
        yield return new WaitForSeconds(fire_delay);
        GameData.is_fire = true;
    }

    IEnumerator Fire_shotgun_delay()
    {
        GameData.is_fire = false;
        GameObject[] bullet_clone = new GameObject[5];
        Vector3 shotgun_bullet_pos;
        float rand_bullet_x;
        float rand_bullet_y;

        for (int i = 0; i < 5; i++)
        {
            if (MathLibrary.Random.NextBoolean())
                rand_bullet_x = (float)(MathLibrary.Random.NextDouble() * shotgun_bullet_degree);
            else
                rand_bullet_x = (float)((MathLibrary.Random.NextDouble() * shotgun_bullet_degree) * -1.0);

            if (MathLibrary.Random.NextBoolean())
                rand_bullet_y = (float)(MathLibrary.Random.NextDouble() * shotgun_bullet_degree);
            else
                rand_bullet_y = (float)((MathLibrary.Random.NextDouble() * shotgun_bullet_degree) * -1.0);

            shotgun_bullet_pos = new Vector3(fire_pos.position.x + rand_bullet_x, fire_pos.position.y + rand_bullet_y, fire_pos.position.z);

            bullet_clone[i] = Instantiate(bullet, shotgun_bullet_pos, fire_pos.rotation);
        }

        GameData.bullet_have -= 1;

        transform.position = fire_pos.position;
        transform.rotation = fire_pos.rotation;

        GameData.fire_sound.Play();

        for (int j = 0; j < 5; j++)
        {
            ++GameData.shotgun_bullet_cnt;
            rb = bullet_clone[j].GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * bullet_speed);
            GameData.shotgun_effect_run = true;
        }

        GameData.shotgun_bullet_cnt = 0;
        yield return new WaitForSeconds(fire_delay);
        GameData.is_fire = true;
    }
    
}
