using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TankController tank;

    private GameObject restartDisplay;
    private Text treasureDisplay;
    private Text weaponDisplay;

    private void Awake()
    {
        restartDisplay = GameObject.Find("Restart_txt");
    }
    private void Start()
    {
        treasureDisplay = GameObject.Find("Treasure_txt").GetComponent<Text>();
        weaponDisplay = GameObject.Find("WeaponChosen_txt").GetComponent<Text>();


        restartDisplay.SetActive(false);
        if (player != null)
        {
            tank = player.GetComponent<TankController>();
        }
        
    }

    protected void Update()
    {
       if (player != null)
        {
            treasureDisplay.text = "Treasure: " + player.CurrentTreasure.ToString();

            if (tank.onMissile)
            {
                weaponDisplay.text = "R - Switch Weapon: Missile";
            }

            if (tank.onRegular)
            {
                weaponDisplay.text = "R - Switch Weapon: Bullet";
            }

            if (player.GetComponent<Health>()._currentHealth <= 0)
            {
                restartDisplay.SetActive(true);
            }
        } 
        



    }
}
