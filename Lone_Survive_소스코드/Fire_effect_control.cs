using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_effect_control : MonoBehaviour
{
    public GameObject[] muzzel_effect = new GameObject[5];
    public Transform effect_pos;
    Animation fire_ani;
    // Use this for initialization
    void Start ()
    {
        fire_ani = GetComponent<Animation>();
        GameData.fire_sound = GetComponent<AudioSource>();
        GameData.is_fire = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //rint("이펙트:총알발사->" + GameData.is_fire);
        //print("is_fire? : " + GameData.is_fire);
        //print("is_reloading? : " + GameData.is_reloading);
        if (GameData.weapone_level == 4)
        {
            if (GameData.shotgun_effect_run && !GameData.is_fire && GameData.is_reloading && GameData.shotgun_bullet_cnt <= 1)
            {
                print("이펙트 생성");
                //fire_ani.Play();
                Muzzle_effect();
                GameData.shotgun_effect_run = false;
            }
        }
        else
        {
            if (!GameData.is_fire && GameData.is_reloading && GameData.shotgun_bullet_cnt <= 1)
            {
                print("이펙트 생성");
                //fire_ani.Play();
                Muzzle_effect();
            }
        }
	}

    void Muzzle_effect()
    {
        print("이펙트 돌입");
        int muzzle_number = Random.Range(0, 5);
        GameObject select_effect = Instantiate(muzzel_effect[muzzle_number], effect_pos.position, effect_pos.rotation);
        select_effect.transform.parent = transform;
        fire_ani.Play();
        /*
        if(!GameData.playing_sound)
        {
            
            GameData.playing_sound = true;
            fire_ani.Play();
            GameData.playing_sound = false;
            
            StartCoroutine(Fire_sound_delay());
        }
        */
        //GameData.is_fire = true;
    }

    IEnumerator Fire_sound_delay()
    {
        GameData.playing_sound = true;
        fire_ani.Play();
        yield return new WaitForSeconds(1.0f);
        GameData.playing_sound = false;
    }
}
