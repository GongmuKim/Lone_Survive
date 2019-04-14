using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature_enemy : MonoBehaviour
{
    public Rigidbody bodies;
    public int force_power;
    static int _count = 0;
    int index = 0;
    int geneIndex = 0;

    float max_Score_Time = 0.0f; // 제일 오래 살아남은 기록
    float sumScore = 0.0f; // 기록 합산
    float averageScore = 0.0f; // 기록 평균
    float live_time = 0.0f; // 적이 살아있는 시간

	// Use this for initialization
	void Start ()
    {
        SetSelfIndex();
	}

    void SetSelfIndex() // 지렁이 Creature form을 추가
    {
        index = _count;
        _count++;
        Enemy_manager.AddCreature(this);
    }

    float time = 0;
	// Update is called once per frame
	void FixedUpdate ()
    {
        //시간당 적군 회전
        time += Time.fixedDeltaTime;

        //적군 생존시간
        live_time += Time.deltaTime;
        //print("생존시간 : " + live_time);
        //print("동작시간 : " + time);

        
        if (time > 1.0f)
        {
            time = 0;
            if (GameData.enemy_is_move)
            {
                GetComponent<NavMeshAgent>().enabled = false;
                move();
                GetComponent<NavMeshAgent>().enabled = true;
            }
        }
        


        float hp = this.gameObject.GetComponent<Enemy_control>().enemy_hp;
        //print("적 체력 : " + hp);
        if (hp == 0)
        {
            print("적이 살아있었던 시간 : " + live_time);
            max_Score_Time = Mathf.Max(live_time, max_Score_Time);
            live_time = 0;
            gameObject.GetComponent<Enemy_control>().enemy_is_death = false;
            this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            print("적이 최대 살아있었던 시간 : " + max_Score_Time);
        }
    }

    void move()
    {
        print(index+" 번 적NPC의"+geneIndex+" 번 유전자");
        int gene = Enemy_manager.GetGene(index, geneIndex);

        Quaternion spin_enemy = Quaternion.identity;
        //print("각도? : " + gene);
        switch (gene)
        {
            case 0: // 위쪽으로 60도
                //spin_enemy.eulerAngles = new Vector3(0, 60, 0);
                transform.rotation = Quaternion.Euler(0f, 60f, 0f);
                StartCoroutine(Force_delay());
                //GetComponent<Rigidbody>().AddForce(Vector3.forward * force_power, ForceMode.Impulse);
                //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                break;
            case 1: // 위쪽으로 40도
                transform.rotation = Quaternion.Euler(0f, 40f, 0f);
                StartCoroutine(Force_delay());
                //GetComponent<Rigidbody>().AddForce(Vector3.forward * force_power, ForceMode.Impulse);
                //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                break;
            case 2: // 위쪽으로 20도
                transform.rotation = Quaternion.Euler(0f, 20f, 0f);
                StartCoroutine(Force_delay());
                //GetComponent<Rigidbody>().AddForce(Vector3.forward * force_power, ForceMode.Impulse);
                //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                break;
            case 3: // 아래쪽으로 20도
                transform.rotation = Quaternion.Euler(0f, -20f, 0f);
                StartCoroutine(Force_delay());
                //GetComponent<Rigidbody>().AddForce(Vector3.forward * force_power, ForceMode.Impulse);
                //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                break;
            case 4: // 아래쪽으로 40도
                transform.rotation = Quaternion.Euler(0f, -40f, 0f);
                StartCoroutine(Force_delay());
                //GetComponent<Rigidbody>().AddForce(Vector3.forward * force_power, ForceMode.Impulse);
                //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                break;
            case 5: // 아래쪽으로 60도
                transform.rotation = Quaternion.Euler(0f, -60f, 0f);
                StartCoroutine(Force_delay());
                //GetComponent<Rigidbody>().AddForce(Vector3.forward * force_power, ForceMode.Impulse);
                //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                break;
            default: // 직진
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
        }

        //GetComponent<Rigidbody>().AddForce(Vector3.forward * force_power, ForceMode.Impulse);
        //GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
        //GetComponent<Rigidbody>().AddForce(Vector3.forward * 20, ForceMode.Force);
        //StartCoroutine(Force_move());
        //this.transform.rotation = Quaternion.Slerp(transform.rotation, spin_enemy, Time.deltaTime * 0.1f);
        //this.transform.rotation = Quaternion.Euler(0,spin_enemy.eulerAngles.y, 0);
        //Rigidbody enemy_force = GetComponent<Rigidbody>();
        //bodies.AddForce(Vector3.forward * 300, ForceMode.Impulse);
        //StartCoroutine(Force_move(spin_enemy));
        
        geneIndex++;
        if (geneIndex > Enemy_manager.geneCount - 1)
        {
            geneIndex = 0;
        }
    }

    public void Reset()
    {
        max_Score_Time = 0;
        averageScore = 0;
        sumScore = 0;
        live_time = 0.0f;
        _count = 0;
        index = 0;
        geneIndex = 0;

        /*
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.Euler(Vector3.zero);
        rb.transform.localRotation = Quaternion.Euler(Vector3.zero);
        */

        time = 0;
        //GetComponent<NavMeshAgent>().enabled = true;
    }

    public double GetFitness()
    {
        return Mathf.Max(max_Score_Time, 0.0f);
    }

    
    IEnumerator Force_delay()
    {     
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody>().AddForce(Vector3.forward * force_power, ForceMode.Impulse);
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }
    
}
