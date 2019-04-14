using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class Enemy_manager : MonoBehaviour
{
    public Transform[] spawn_point = new Transform[4];
    /*
     * 0 -> 위
     * 1 -> 아래
     * 2 -> 오른쪽
     * 3 -> 왼쪽
     */
    public GameObject enemy;
    public GameObject player_is_active;
    public const int geneCount = 10; // 적 유전자 길이

    bool is_enemy = true;
    float time = 0;
    const float cooltime = 10;

    StringBuilder stringBuilder; // 지렁이의 블록 상태를 표시할 텍스트
    public TextMesh statusText; // 진행상황을 담는 텍스트 변수

    public static Genetic_control GC; // 유전 알고리즘
    static List<Creature_enemy> creatures; // 적군

    public static int enemy_spawn_num;
    public static int max_spawn_random;

    int spawn_count = 0;
    public static int enemy_child_cnt = 0;
    bool start_onece_function = false;

    GameObject enemy_unit;

    static GameObject enemy_spawn_parent;

    
    static Enemy_manager()
    {
        print("초기화!");
        creatures = new List<Creature_enemy>();
        GC = new Genetic_control(geneCount); // 적군 캐릭터는 하나의 오브젝트로 구성됨
        print("초기 적군 수:" + GameData.enemy_many);
        GC.AddRandomPopulation(GameData.enemy_many);
    }
    

    // Use this for initialization
    void Start ()
    {
        print("Enemy Manager 시작");
        stringBuilder = new StringBuilder();
        enemy_spawn_num = GameData.enemy_many;
        max_spawn_random = 4;
        GameData.genetic_is_run = false;
        /*
        if (GameData.game_stage == 1)
        {
            creatures = new List<Creature_enemy>();
            GC = new Genetic_control(geneCount); // 적군 캐릭터는 하나의 오브젝트로 구성됨
            print("초기 적군 수:" + GameData.enemy_many);
            GC.AddRandomPopulation(GameData.enemy_many);
        }
        */
    }

    // Update is called once per frame
    void Update ()
    {
        enemy_spawn_system();

        //print("유전 알고리즘 실행가능? : " + GameData.genetic_is_run);
        //print("update 반복시 락기능 활성화? : " + GameData.lock_update);
        if (GameData.genetic_is_run && !GameData.lock_update)
        {
            GameData.genetic_is_run = false;
            GameData.lock_update = true;
            //print("유전 알고리즘 실행");
            DoGenetic();
        }

        /*
        if (GameData.genetic_is_run)
        {
            GameData.genetic_is_run = false;
            print("유전 알고리즘 실행");
            DoGenetic();
        }
        */
        /*
        time += Time.deltaTime;
        if (time > cooltime)
        {
            time = 0;
            DoGenetic();
        }
        
        if(GameData.genetic_is_run)
        {
            GameData.genetic_is_run = false;
            print("유전 알고리즘 실행");
            DoGenetic();
        }
         
        if (GameData.enemy_many == 0)
            DoGenetic();
        */
    }

    void enemy_spawn_system()
    {
        player_is_active = GameObject.FindWithTag("Player");

        if (is_enemy == true && enemy_spawn_num > 0 && !GameData.player_is_death)
        {
            if (max_spawn_random > enemy_spawn_num)
            {
                max_spawn_random = enemy_spawn_num;
            }

            print("최대 스폰 횟수 : " + max_spawn_random);

            spawn_count = MathLibrary.Random.Range(1, max_spawn_random);

            for (int _ = 0; _ < spawn_count; _++)
            {
                int set_point = MathLibrary.Random.Range(0, 3);
                Transform set = spawn_point[set_point];

                if (set_point <= 1)
                {
                    set.position = new Vector3(Random.Range(-8f, 8f), set.position.y, set.position.z);
                }
                else
                {
                    set.position = new Vector3(set.position.x, set.position.y, Random.Range(-8f, 8f));
                }

                //Instantiate(enemy, set.position, set.rotation);
                // GameData.game_stage == 1
                // GameObject.Find("Enemy_spawn").transform.childCount == 0
                enemy_spawn_parent = GameObject.Find("Enemy_spawn_list");
                print("Enemy_spawn에 있는 적군 수 : " + enemy_spawn_parent.transform.childCount);

                /// && enemy_spawn_parent.transform.childCount < GameData.enemy_many

                if (GameData.game_stage == 1)
                {
                    print("적군 오브젝트 생성");
                    enemy_unit = Instantiate(enemy, set.position, set.rotation);
                    //enemy_spawn_parent = GameObject.Find("Enemy_spawn");
                    enemy_unit.transform.parent = enemy_spawn_parent.transform;
                }
                else
                {
                    print("적군 오브젝트 재사용");
                    enemy_unit = enemy_spawn_parent.transform.GetChild(enemy_child_cnt).gameObject;
                    enemy_unit.GetComponent<Enemy_control>().enemy_hp = GameData.enemy_health;
                    enemy_unit.transform.position = set.position;
                    enemy_unit.transform.rotation = set.rotation;
                    enemy_unit.SetActive(true);
                    enemy_child_cnt++;
                }
                enemy_spawn_num--;
                StartCoroutine("Spawn_delay");
            }
            /*
            if(enemy_spawn_num == 0)
                GameData.genetic_is_run = true;
            */
        }
        
        /*
        if(GameData.genetic_is_run)
        {
            print("유전 알고리즘 실행");
            GameData.genetic_is_run = false;
            DoGenetic();
        }
         */  
        
    }

    void DoGenetic() // 유전알고리즘 실행
    {
        double bestFitness = 0; // 최적의 형태
        Move_Chromosome bestChromo = null; // 최적의 초기값 상태

        int enemy_populations = 20;
        //print("총 적 인원 : " + enemy_populations);

        for (int i = 0; i < enemy_populations; i++) // 소환하는 적 수만큼 반복
        {
            //print("적 번호 : " + i);
            //print("creatures 배열 개수:" + creatures.Count);
            Creature_enemy creature = creatures[i]; // i번 적군의 이동 폼 지정
            Move_Chromosome chromo = GC.Populations[i]; // i번째의 적 이동 폼 유전알고리즘 폼 지정
            //print("chromo test : " + chromo.Genes[0]);

            chromo.Fitness = creature.GetFitness(); // i번째의 얼만큼 살아남았는지 지정
            //print(i + "번째 적군 생존시간 : " + chromo.Fitness);
            //print("최고기록 : " + bestFitness);

            if (chromo.Fitness > bestFitness) // 지렁이의 제일 최적에 초기값을 구함
            {
                //print("Chromo Fitness Debug : " + chromo.Fitness);
                bestFitness = chromo.Fitness; // 최적의 블록구조 움직임으로 나간 거리
                bestChromo = chromo; // 최적의 블럭구조 움직임 초기 값
                //print("best Chromo test : " + bestChromo.Fitness);
            }
        }

        stringBuilder.Length = 0; // 지렁이 몸통블록이 움직이는 순서값 초기화

        if (bestChromo.Genes.Length > 0 && bestChromo.Fitness != 0)
        {
            for (int i = 0; i < geneCount; i++) // 지렁이 몸통블록이 움직이는 순서값 입력
            {
                //print("best Chromo : " + bestChromo[i]);
                stringBuilder.Append(bestChromo[i].ToString() + ',');
            }
        }
        
        string text = string.Format("Generation: {0} - Best: {1:F2}\r\nGene: {2}", GC.Generation, bestFitness, stringBuilder.ToString()); // 현재 세대 수, 최적의 블록 움직임으로 나간 거리, 최적의 블록 움직임 순서
        statusText.text = text; // 상태 텍스트에 설정

        Debug.Log(text); // 상태 출력

        GC.Select(6); // 적합도 선정
        GC.CrossOver(enemy_populations, 0.5); // 교차
        GC.Mutation(0.01f, -2, 2, 0, 5); // 변이
        GC.Generation++; // 다음 세대로 증가
        ResetCreatures(); // 이전 데이터값 초기화
        //print("유전 알고리즘 실행 끝");
    }

    public static void ResetCreatures() // 이전 적 데이터값 초기화
    {
        foreach (Creature_enemy creature in creatures)
        {
            creature.Reset();
        }
    }

    public static int GetGene(int popIndex, int geneIndex)  // 적군 움직임 반환
    {
        //print("Debug : " + GC.Populations.Count);
        return GC.Populations[popIndex].Genes[geneIndex];
    }

    public static void AddCreature(Creature_enemy creature) // 적군 폼 추가
    {
        creatures.Add(creature);
    }

    public static int Receive_enemy_spawn_num()
    {
        return enemy_spawn_parent.transform.childCount;
    }

    IEnumerator Spawn_delay()
    {
        is_enemy = false;
        yield return new WaitForSeconds(GameData.enem_spawn_delay);
        is_enemy = true;
    }

}
