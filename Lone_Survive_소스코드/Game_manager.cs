using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class Game_manager : MonoBehaviour
{
    public GameObject clear_UI;
    public GameObject[] money_UI = new GameObject[2];
    public GameObject gameover_UI;
    //public GameObject allclear_UI;
    public GameObject player;
    public GameObject store_UI;
    public GameObject[] kill_count_textmesh = new GameObject[4];
    public GameObject[] stage_textmesh = new GameObject[4];
    public GameObject death_blackout;
    public Text wave_text;

    public UnityEngine.UI.Image fade;

    float fades = 1.0f;
    float time = 0;

    int enemy_spawn_cnt;

    
    Game_manager()
    {
        print("첫 스테이지");
        GameData.game_stage = 1;
        //GameData.stage_01 = true;
        GameData.kill_count = 0;
        GameData.is_first_start = true;
        //첫번째 스테이지 셋팅
        //GameData.enemy_spawn = enemy_spawn_cnt = 2;
        GameData.enemy_spawn = enemy_spawn_cnt = 5;
        GameData.enemy_attack = 2;
        GameData.enemy_health = 10;
        //GameData.enemy_many = 4 * enemy_spawn_cnt;
        GameData.enemy_many = 20;
        print("첫 스테이지 설정 끝");
    }
    

    // Use this for initialization
    void Start ()
    {
        /*
        if (!GameData.is_first_start)
        {
            print("첫 스테이지");
            GameData.game_stage = 1;
            //GameData.stage_01 = true;
            GameData.kill_count = 0;
            GameData.is_first_start = true;
            //첫번째 스테이지 셋팅
            GameData.enemy_spawn = enemy_spawn_cnt = 2;
            GameData.enemy_attack = 2;
            GameData.enemy_health = 10;
            GameData.enemy_many = 4 * enemy_spawn_cnt;
            print("첫 스테이지 설정 끝");
            //
        }
        */
        //Stage_setting();
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;

        print("fade : " + fades);
        if (fades > 0.0f && time >= 0.5f)
        {
            fades -= 0.1f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0;
            wave_text.text = "Wave " + GameData.game_stage.ToString();
        }
        else if (fades <= 0.0f)
        {
            wave_text.text = "";
            wave_text.enabled = false;
            //print("적군 스폰 수 : " + enemy_spawn_cnt);
            Popup_UI(); // UI 컨트롤

            Player_death(); // 플레이어 사망확인

            Money_UI_setting(); // 플레이어가 벌은 돈 출력

            time = 0;
        }
    }

    void Stage_setting()
    {
        //print("현재 스테이지 적군값 설정");
        //테스트용 스테이지(4스테이지)
        /*
        switch (GameData.game_stage)
        {
            case 1:
                GameData.enemy_spawn = 2;
                GameData.enemy_attack = 2;
                GameData.enemy_health = 10;
                break;
            case 2:
                GameData.enemy_spawn = 3;
                GameData.enemy_attack = 2;
                GameData.enemy_health = 15;
                break;
            case 3:
                GameData.enemy_spawn = 4;
                GameData.enemy_attack = 3;
                GameData.enemy_health = 15;
                break;
            case 4:
                GameData.enemy_spawn = 5;
                GameData.enemy_attack = 3;
                GameData.enemy_health = 20;
                break;
        }

        GameData.enemy_many = 4 * GameData.enemy_spawn;
        */
    }

    void Next_stage_setting()
    {
        //print("다음 스테이지 세팅!");
        if (GameData.enemy_many <= 0)
        {
            //테스트 스테이지
            /*
            if (GameData.stage_01 == true)
            {
                GameData.stage_01 = false;
                GameData.stage_02 = true;
                GameData.game_stage = 2;
            }
            else if (GameData.stage_02 == true)
            {
                GameData.stage_02 = false;
                GameData.stage_03 = true;
                GameData.game_stage = 3;
            }
            else if (GameData.stage_03 == true)
            {
                GameData.stage_03 = false;
                GameData.stage_04 = true;
                GameData.game_stage = 4;
            }
            else
            {
                GameData.stage_04 = false;
                GameData.stage_01 = true;
                GameData.game_stage = 1;
            }

            GameData.game_stage++;
            Stage_setting();
            */
            //스테이지(무한)
            GameData.game_stage++;
            // 스테이지에 소환되는 적군 수를 16명으로 고정
            //enemy_spawn_cnt++;
            GameData.enemy_spawn = enemy_spawn_cnt;
            
            //print("적군 스폰 수 : " + GameData.enemy_spawn);
            if (GameData.game_stage % 2 == 0)
                GameData.enemy_health += 5;
            else
                GameData.enemy_attack++;

            GameData.enemy_many = 4 * enemy_spawn_cnt;
            Enemy_manager.enemy_spawn_num = GameData.enemy_many;
            Enemy_manager.max_spawn_random = 4;
            //Enemy_manager.GC.AddRandomPopulation(GameData.enemy_many);
            //print("현재 스테이지 : " + GameData.game_stage);
            //print("스테이지 적군 수 : " + GameData.enemy_many);
        }
    }

    void Money_UI_setting()
    {
        foreach(GameObject gold_ui in money_UI)
            gold_ui.GetComponent<TextMesh>().text = "$"+GameData.money.ToString();
    }

    void Popup_UI()
    {
        //print("게임 스테이지 : "+GameData.game_stage);
        if (GameData.enemy_many == 0 && player.activeSelf == true)
        {
            //테스트 4개 스테이지
            /*
            if (GameData.game_stage < 4)
            {
                clear_UI.SetActive(true);
                store_UI.SetActive(true);
                if (GameData.is_next_stage)
                {
                    GameData.is_next_stage = false;
                    GameData.store_running = false;
                    Next_stage_setting();
                    clear_UI.SetActive(false);
                    store_UI.SetActive(false);
                }
                if (GameData.store_running)
                    store_UI.SetActive(true);
            }
            else
            {
                allclear_UI.SetActive(true);
            }
            */
            clear_UI.SetActive(true);
            store_UI.SetActive(true);
            Enemy_manager.enemy_child_cnt = 0;
            GameData.genetic_is_run = true;

            if (GameData.is_next_stage)
            {
                GameData.is_next_stage = false;
                GameData.store_running = false;
                GameData.genetic_is_run = false;
                GameData.lock_update = false;
                Next_stage_setting();
                clear_UI.SetActive(false);
                store_UI.SetActive(false);
                fades = 1;
                fade.color = new Color(0, 0, 0, fades);
                wave_text.enabled = true;
                // 첫 스테이지에 생성한 오브젝트 재사용할 것
                /*
                GameObject death_enemy = GameObject.Find("Enemy_spawn");

                int des = death_enemy.transform.childCount;

                for (int i = 0; i < des; i++)
                {
                    Transform child_enemy = death_enemy.transform.GetChild(i);
                    Destroy(child_enemy.gameObject);                    
                }
                */
            }
        }
    }

    void Player_death()
    {
        if (player.activeSelf == false)
        {
            GameData.player_is_death = true;
            GameData.genetic_is_run = false;
            death_blackout.GetComponent<Grayscale>().enabled = true;
            //print("캐릭터 죽음");
            //GameObject.FindWithTag("Enemy").GetComponent<Enemy_control>().enemy_hp = 0;
            //int spawn_enemy_now = GameData.enemy_many - Enemy_manager.enemy_spawn_num;
            int spawn_enemy_now = Enemy_manager.Receive_enemy_spawn_num();
            //print("현재 적 NPC 스폰 수:" + spawn_enemy_now);
            for (int cnt = 0; cnt < spawn_enemy_now; cnt++)
            {
                //print(cnt + "번째 적 NPC 사망처리");
                GameObject enemy_list = GameObject.Find("Enemy_spawn_list").transform.GetChild(cnt).gameObject;
                enemy_list.GetComponent<Enemy_control>().enemy_hp = 0;
                enemy_list.SetActive(false);
            }

            foreach(GameObject kill in kill_count_textmesh)
                kill.GetComponent<TextMesh>().text = "Kill : "+GameData.kill_count.ToString();

            foreach(GameObject coin in stage_textmesh)
                coin.GetComponent<TextMesh>().text = "Clear Stage : "+GameData.game_stage.ToString();
            gameover_UI.SetActive(true);
        }
    }
}
