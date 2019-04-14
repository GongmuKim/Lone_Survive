using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_control : MonoBehaviour
{
    GameObject enemy_child;
    Transform player;
    NavMeshAgent agent;
    Vector3 first_pos;
    CharacterController enemy_control;
    NavMeshAgent enemy_nav;
    Animator enemy_ani;
    public AudioSource[] enemy_zombie_sfx;
    float count = 0;
    int enemy_child_number;
    float sfx_timer = 0;
    public int nav_dist = 1000;
    public float enemy_hp;
    public bool enemy_is_death = false;

    // Use this for initialization
    void Start ()
    {
        first_pos = this.transform.position; // 내 객체의 최초의 객체
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        enemy_hp = GameData.enemy_health;
        enemy_control = GetComponent<CharacterController>();
        enemy_nav = GetComponent<NavMeshAgent>();
        Enemy_list_control();
        StartCoroutine(SFX_delay());
    }
	
	// Update is called once per frame
	void Update ()
    {
        sfx_timer = Time.deltaTime;

        if (enemy_nav.isActiveAndEnabled)
        {
            //print("NavMeshAgent 기능 실행중");
            Nav_system();
        }
        else
            print("NavMeshAgent 기능 중지");

        Death_control();
    }

    void Enemy_list_control()
    {
        print("자식 갯수 : " + transform.childCount);
        enemy_child_number = MathLibrary.Random.Range(0, transform.childCount-6);
        print("선택한 적NPC 텍스쳐 : " + enemy_child_number);
        enemy_child = transform.GetChild(enemy_child_number).gameObject;
        enemy_child.SetActive(true);
    }

    void Nav_system()
    {
        float char_dist = Vector3.Distance(this.transform.position, player.position);

        ///무조건 플레이어를 향해 따라가도록 설정
        if (char_dist < nav_dist)
        {
            agent.stoppingDistance = 1.5f;
            agent.destination = player.position;
        }
    }

    void Death_control()
    {
        if(enemy_hp <= 0)
        {
            print("적군 죽음!");
            enemy_is_death = true;
            /*
            if (count <= 0)
            {
                print("적군 death 실행");
                count++;             
                //좀비 텍스쳐를 넣으면 게임이 멈춰서 보류
                /*
                StartCoroutine(Animation_delay());

                print("네비게이션 끄기");
                gameObject.GetComponent<NavMeshAgent>().enabled = false;

                print("죽는 애니메이션 실행");
                enemy_ani.SetTrigger("death_trig");
                
                //print("정보세팅");
                GameData.money += 1000;
                GameData.enemy_many -= 1;
                if (GameObject.Find("Game_manager").GetComponent<Game_manager>().player.activeSelf == true)
                    GameData.kill_count += 1;
                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
            }
            */
            GameData.money += 1000;
            GameData.enemy_many -= 1;
            if (GameObject.Find("Game_manager").GetComponent<Game_manager>().player.activeSelf == true)
                GameData.kill_count += 1;
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        //print("부딫힌 대상 : " + other.transform.tag);
        if (other.transform.tag == "Fence_up")
        { GameData.shield_health[0] -= GameData.enemy_attack; }
        else if (other.transform.tag == "Fence_down")
        { GameData.shield_health[1] -= GameData.enemy_attack;}
        else if (other.transform.tag == "Fence_right")
        { GameData.shield_health[2] -= GameData.enemy_attack;}
        else if (other.transform.tag == "Fence_left")
        { GameData.shield_health[3] -= GameData.enemy_attack;}

        if (other.transform.tag == "Player")
            other.gameObject.SetActive(false);
    }

    IEnumerator Animation_delay()
    {
        print("지연중...");
        yield return new WaitForSeconds(1.5f);
        print("지연완료!");
        /*
        GameData.money += 1000;
        GameData.enemy_many -= 1;
        if (GameObject.Find("Game_manager").GetComponent<Game_manager>().player.activeSelf == true)
            GameData.kill_count += 1;
        Destroy(this.gameObject);
        */
    }

    
    IEnumerator SFX_delay()
    {
        while (this.isActiveAndEnabled)
        {
            int rand_sfx = MathLibrary.Random.Range(0, 4);
            enemy_zombie_sfx[rand_sfx].Play();
            yield return new WaitForSeconds(10f);
        }
    }
    
}
