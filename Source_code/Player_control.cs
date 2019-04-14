using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_control : MonoBehaviour
{
    public Camera main_camera;
    public GameObject HUD;

	// Use this for initialization
	void Start ()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            GameData.money = 10000000; // 테스트용
        else
            GameData.money = 0; // 실전용
	}
	
	// Update is called once per frame
	void Update ()
    {
        HUD_control();
        Movement_control();
        Player_Flip();
	}

    void HUD_control()
    {
        string HUD_message;
        //print(SceneManager.GetActiveScene().buildIndex);
        //Transform fire_pos = GameObject.Find("fire_position").GetComponentInChildren<Transform>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
            HUD_message = "Bullet : " + GameData.bullet_have.ToString();
        else
            HUD_message = "Bullet : " + GameData.bullet_have.ToString()
                                    + "\nStage : " + GameData.game_stage.ToString()
                                    + "\nEnemy : " + GameData.enemy_many.ToString();

        HUD.GetComponent<TextMesh>().text = HUD_message;
        /*
        HUD.GetComponent<TextMesh>().text = "총알 : " + GameData.bullet_have.ToString()
                                          + "\n스테이지 : " + GameData.game_stage.ToString()
                                          + "\n남은 적 : " + GameData.enemy_many.ToString();
        */
    }

    /*
     * "\n카메라 : " + main_camera.transform.rotation.ToString()
     * "오브젝트 : " + this.transform.rotation.ToString() 
     */

    void Movement_control()
    {
        this.transform.LookAt(main_camera.transform);
    }

    void Player_Flip()
    {
        if (Input.GetKeyDown(KeyCode.Joystick3Button0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            print("뒤집기");
            //Quaternion flip = Quaternion.identity;
            //flip.eulerAngles = new Vector3(0, 90, 0);
            transform.rotation = Quaternion.Euler(0, 90f, 0);
        }
    }
}
