using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GameData
{
    public static GameObject gun_have;
    
    public static float gun_fire_delay;

    public static int[] shield_health;
    public static int enemy_health;
    public static int enemy_attack;
    public static int weapone_level;
    public static int enemy_spawn;
    public static int enemy_many = 20;
    public static int bullet_have;
    public static int bullet_damage;
    public static int game_stage;
    public static int money;
    public static int kill_count;
    public static int shield_level;

    public static int uzi_price = 10000;
    public static int ak_price = 50000;
    public static int spase_price = 25000;
    public static int repair_price = 5000;
    public static int upgrade_price = 10000;
    public static int maximum_shield_hp = 100;
    public static int upgrade_value = 100;
    public static int shotgun_bullet_cnt = 0;

    public static float enem_spawn_delay = 5f;

    public static bool is_fire = true; // 발사할 수 있는지 확인
    public static bool is_reloading = true;
    public static bool is_next_stage = false;
    public static bool store_running = false;
    public static bool is_first_start = false;
    public static bool buy_order_uzi = false;
    public static bool buy_order_ak = false;
    public static bool buy_order_spase = false;
    public static bool buy_order_repair = false;
    public static bool buy_order_upgrade = false;
    public static bool buying_upgrade = false;
    public static bool playing_sound = false;
    public static bool enemy_is_death = false;
    public static bool genetic_is_run = false;
    public static bool lock_update = false;
    public static bool player_is_death = false;
    public static bool enemy_is_move = true;
    public static bool shotgun_effect_run = false;

    public static AudioSource fire_sound;

    public static void init()
    {
        Debug.Log("설정값 초기화!");
        enem_spawn_delay = 5f;

        for (int i = 0; i < 4; i++)
            shield_health[i] = 100;

        bullet_have = 15;
        bullet_damage = 5;
        uzi_price = 10000;
        ak_price = 50000;
        spase_price = 25000;
        repair_price = 5000;
        upgrade_price = 10000;
        maximum_shield_hp = 100;
        upgrade_value = 100;
        weapone_level = 1;
        game_stage = 1;
        enemy_health = 10;
        enemy_attack = 2;
        enemy_spawn = 2;
        enemy_many = 10;
        money = 0;
        kill_count = 0;

        is_fire = true; // 발사할 수 있는지 확인
        is_reloading = true;
        is_next_stage = false;
        store_running = false;
        is_first_start = false;
        buy_order_uzi = false;
        buy_order_ak = false;
        buy_order_spase = false;
        buy_order_repair = false;
        buy_order_upgrade = false;
        buying_upgrade = false;
        enemy_is_death = false;
        genetic_is_run = false;
        lock_update = false;
        player_is_death = false;
        enemy_is_move = true;
        shotgun_effect_run = false;
    }
}
