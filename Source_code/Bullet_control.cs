using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class Bullet_control : MonoBehaviour
{
    public float max_distance = 1000000;
    RaycastHit hit;
    public GameObject hit_wall_decal;
    public float infront_wall;
    public GameObject blood_effect;
    public LayerMask ignore_layer;
    public float bullet_speed = 3000f;
    float bullet_time = 0;
    int ray_num = 0;
    //Enemy_control exturn_enemy_hp;
 
    // Use this for initialization
    void Start ()
    {
        /*
        if(SceneManager.GetActiveScene().buildIndex == 1)
            exturn_enemy_hp = GameObject.Find("Enemy_list").GetComponent<Enemy_control>();
        */
	}
	
	// Update is called once per frame
	void Update ()
    {       
        bullet_time += Time.deltaTime;
        //print("총알 체류시간 : " + bullet_time);
        Bullet_effect_tag();
        Destroy_bullet();
	}

    void Destroy_bullet()
    {
        if (this.transform.position.y < -5)
            Destroy(this.gameObject);
        else if (bullet_time > 20)
        {
            bullet_time = 0;
            Destroy(this.gameObject);
        }
    }

    void Bullet_effect_tag()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, max_distance, ~ignore_layer))
        {
            ray_num++;
            if (hit_wall_decal)
            {
                print("레이캐스트 : "+hit.transform.tag);
                //print("레이캐스트 갯수 : " + ray_num);
                if (hit.transform.tag == "Enemy")
                {
                    Instantiate(blood_effect, hit.point, Quaternion.LookRotation(hit.normal));
                    hit.transform.gameObject.GetComponent<Enemy_control>().enemy_hp -= GameData.bullet_damage;
                    Destroy(gameObject);
                }
                else if (hit.transform.tag == "Goto_market")
                    GameData.store_running = true;
                else if (hit.transform.tag == "Next_stage")
                    GameData.is_next_stage = true;
                else if (hit.transform.tag == "Uzi")
                {
                    GameData.is_fire = false;
                    GameData.buy_order_uzi = true;
                }
                else if (hit.transform.tag == "AK-47")
                {
                    GameData.is_fire = false;
                    GameData.buy_order_ak = true;
                }
                else if (hit.transform.tag == "Spase-12")
                {
                    GameData.is_fire = false;
                    GameData.buy_order_spase = true;
                }
                else if (hit.transform.tag == "Repair")
                    GameData.buy_order_repair = true;
                else if (hit.transform.tag == "Upgrade")
                {
                    if (ray_num < 2)
                        GameData.buy_order_upgrade = true;
                }
                else if (hit.transform.tag == "Goto_main")
                {
                    GameData.init();
                    Enemy_manager.ResetCreatures();
                    GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Grayscale>().enabled = false;
                    SceneManager.LoadScene("Start_Scene");
                }
                else if (hit.transform.tag == "Retry")
                {
                    GameData.init();
                    Enemy_manager.ResetCreatures();
                    GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Grayscale>().enabled = false;
                    SceneManager.LoadScene("Game_Scene");
                }
                else if (hit.transform.tag == "Start")
                    //SceneManager.LoadScene("Game_Scene");
                    SceneManager.LoadScene("Game_Scene");
                else if (hit.transform.tag == "Exit")
                {
                    print("종료");
                    Application.Quit();
                }
                else if (hit.transform.tag == "Invirment" || hit.transform.tag == "Fence_up" || hit.transform.tag == "Fence_down" || hit.transform.tag == "Fence_right" || hit.transform.tag == "Fence_left")
                {
                    Instantiate(hit_wall_decal, hit.point + hit.normal * infront_wall, Quaternion.LookRotation(hit.normal));
                    Destroy(gameObject);
                }
            }
            Destroy_bullet();
        }
        Destroy_bullet();
    }
}
