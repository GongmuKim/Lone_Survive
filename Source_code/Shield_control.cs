using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_control : MonoBehaviour
{
    public GameObject[] fence = new GameObject[4];
    public GameObject[] fence_HUD = new GameObject[4];
    GameObject[] shield_forward = new GameObject[4]; // 상하좌우 쉴드 오브젝트
    // Use this for initialization
    void Start ()
    {
        GameData.shield_health = new int[4];
        for (int i = 0; i < 4; i++)
        {
            GameData.shield_health[i] = GameData.maximum_shield_hp;
            shield_forward[i] = transform.GetChild(i).gameObject;
            shield_forward[i].transform.GetChild(1).gameObject.SetActive(true); // 쉴드 오브젝트 자식으로 있는 레벨별 쉴드 텍스쳐 지정
        }
        GameData.shield_level = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameData.buy_order_repair || GameData.buy_order_upgrade)
            Buy_repair_upgrade();
        Fence_manager();
	}

    void Fence_manager()
    {
        for (int i = 0; i < 4; i++)
            fence_HUD[i].GetComponent<TextMesh>().text = GameData.shield_health[i].ToString();

        if (GameData.shield_health[0] <= 0 && fence[0].activeSelf == true)
            fence[0].SetActive(false);
        else if(GameData.shield_health[1] <= 0 && fence[1].activeSelf == true)
            fence[1].SetActive(false);
        else if(GameData.shield_health[2] <= 0 && fence[2].activeSelf == true)
            fence[2].SetActive(false);
        else if(GameData.shield_health[3] <= 0 && fence[3].activeSelf == true)
            fence[3].SetActive(false);
    }

    void Buy_repair_upgrade()
    {
        if (GameData.buy_order_repair && GameData.money >= GameData.repair_price)
        {
            GameData.buy_order_repair = false;
            if (GameData.shield_health[0] != GameData.maximum_shield_hp || GameData.shield_health[1] != GameData.maximum_shield_hp || GameData.shield_health[2] != GameData.maximum_shield_hp || GameData.shield_health[3] != GameData.maximum_shield_hp)
            {
                GameData.money -= GameData.repair_price;
                for (int i = 0; i < 4; i++)
                {
                    if (fence[i].activeSelf == false)
                        fence[i].SetActive(true);

                    GameData.shield_health[i] = GameData.maximum_shield_hp;
                }
            }
        }

        if(GameData.buy_order_upgrade && GameData.money >= GameData.upgrade_price)
        {
            GameData.shield_level++;
            GameData.buy_order_upgrade = false;
            GameData.money -= GameData.upgrade_price;
            GameData.maximum_shield_hp += GameData.upgrade_value;
            GameData.upgrade_price += 10000;
            GameData.upgrade_value += 100;
            
            switch(GameData.shield_level)
            {
                case 2:
                    Setting_shield_texture(2);
                    break;
                case 3:
                    Setting_shield_texture(3);
                    break;
                case 4:
                    Setting_shield_texture(4);
                    break;
                case 5:
                    Setting_shield_texture(5);
                    break;
            }
        }
    }

    void Setting_shield_texture(int shield_level)
    {
        for (int i = 0; i < 4; i++)
        {
            shield_forward[i] = transform.GetChild(i).gameObject;
            shield_forward[i].transform.GetChild(shield_level-1).gameObject.SetActive(false);
            shield_forward[i].transform.GetChild(shield_level).gameObject.SetActive(true); // 쉴드 오브젝트 자식으로 있는 레벨별 쉴드 텍스쳐 지정
        }
    }
}
