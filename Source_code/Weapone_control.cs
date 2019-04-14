using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapone_control : MonoBehaviour
{
    //public int gun_list;
    public GameObject[] gunpref = new GameObject[4];
    public GameObject[] gun_panel_store = new GameObject[3];
    public GameObject reload_HUD;
    public GameObject upgrade_cost;
    public Transform set_gun;
    //public GameObject reload_sound;
    public AudioSource[] reload_sound_list;
    //AudioClip reload_sound_clip;
    int weapone_level; // 처음 시작은 1부터
    //AudioSource get_sound;
    //bool is_reloading;
    float full_mag;

    // Use this for initialization
    void Start ()
    {
        weapone_level = 1;
        Setting_gun();
        GameData.is_reloading = true;
        GameData.is_fire = true;
        full_mag = GameData.bullet_have;
        //print("재장전소리 갯수 : " + reload_sound_list.Length);
        //reload_sound_list = GetComponent<AudioSource>();
        //reload_sound_clip = new AudioClip();
        //reload_sound.clip = reload_sound_clip;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //print("총 발사상태 ; " + GameData.fire_state);
        upgrade_cost.GetComponent<TextMesh>().text = "Upgrade\n$" + GameData.upgrade_price.ToString();
        if (GameData.buy_order_uzi || GameData.buy_order_ak || GameData.buy_order_spase)
            Buy_gun();
        Reload_bullet();
    }

    void Setting_gun()
    {
        GameObject set_gun_child;
        GameData.weapone_level = weapone_level;

        if (GameData.gun_have != null)
        {
            /*
            Transform temp = GetComponentInChildren<Transform>(true);
            temp.gameObject.SetActive(false);
            */
            int cnt = transform.childCount;
            for(int i=0; i < cnt; i++)
            {
                Transform child_gun = transform.GetChild(i);
                //child_gun.gameObject.SetActive(false);
                Destroy(child_gun.gameObject);
            }
        }

        GameData.gun_have = gunpref[weapone_level-1];

        switch (weapone_level)
        {
            case 1:
                GameData.bullet_have = 15;
                GameData.bullet_damage = 5;
                GameData.gun_fire_delay = 1f;
                break;
            case 2:
                GameData.bullet_have = 25;
                GameData.bullet_damage = 5;
                GameData.gun_fire_delay = 1.2f;
                break;
            case 3:
                GameData.bullet_have = 35;
                GameData.bullet_damage = 10;
                GameData.gun_fire_delay = 1.4f;
                break;
            case 4:
                GameData.bullet_have = 8;
                GameData.bullet_damage = 5;
                GameData.gun_fire_delay = 1.6f;
                break;
        }
        set_gun_child = Instantiate(GameData.gun_have, set_gun.transform.position, set_gun.transform.rotation);
        set_gun_child.transform.parent = set_gun;
    }

    public void Reload_bullet()
    {
        if ((!Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire2") && GameData.bullet_have < full_mag && GameData.is_reloading) || (!Input.GetButtonDown("Fire1") && GameData.bullet_have == 0))
        {
            //print("장전 돌입");
            GameData.is_reloading = false;
            GameData.is_fire = false;
            reload_HUD.GetComponent<TextMesh>().text = "RELOAD...";
            StartCoroutine(Reload_delay(GameData.gun_fire_delay));
        }
    }

    void Buy_gun()
    {
        if(GameData.buy_order_uzi && GameData.money >= GameData.uzi_price && GameData.gun_have.transform.tag != "Uzi")
        {
            GameData.buy_order_uzi = false;
            GameData.money -= GameData.uzi_price;
            weapone_level = 2;
            Setting_gun();
            GameData.is_reloading = true;
            full_mag = GameData.bullet_have;
        }
        else if(GameData.buy_order_ak && GameData.money >= GameData.ak_price && GameData.gun_have.transform.tag != "AK-47")
        {
            GameData.buy_order_ak = false;
            GameData.money -= GameData.ak_price;
            weapone_level = 3;
            Setting_gun();
            GameData.is_reloading = true;
            full_mag = GameData.bullet_have;
        }
        else if (GameData.buy_order_spase && GameData.money >= GameData.spase_price && GameData.gun_have.transform.tag != "Spase-12")
        {
            GameData.buy_order_spase = false;
            GameData.money -= GameData.spase_price;
            weapone_level = 4;
            Setting_gun();
            GameData.is_reloading = true;
            full_mag = GameData.bullet_have;
        }
    }

    IEnumerator Reload_delay(float delay_cnt)
    {        
        switch(GameData.weapone_level)
        {
            case 1:
                reload_sound_list[0].Play();
                //print("권총 재장전 소리");
                break;
            case 2:
                reload_sound_list[1].Play();
                //print("기관단총 재장전 소리");
                break;
            case 3:
                reload_sound_list[3].Play();
                //print("소총 재장전 소리");
                break;
            case 4:
                reload_sound_list[2].Play();
                //print("샷건 재장전 소리");
                break;
        }
        //get_sound.Play();
        //reload_sound_list.Play();
        //GameData.is_reloading = false;
        //GameData.is_fire = false;
        yield return new WaitForSeconds(delay_cnt);
        switch (weapone_level)
        {
            case 1:
                GameData.bullet_have = 15;
                break;
            case 2:
                GameData.bullet_have = 25;
                break;
            case 3:
                GameData.bullet_have = 35;
                break;
            case 4:
                GameData.bullet_have = 8;
                break;
        }
        reload_HUD.GetComponent<TextMesh>().text = "";
        GameData.is_reloading = true;
        GameData.is_fire = true;
    }
}
