using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Player player;

    private Text treasureDisplay;

    private void Start()
    {
        treasureDisplay = GetComponent<Text>();
        
    }

    protected void Update()
    {
        
        treasureDisplay.text = "Treasure: " + player.CurrentTreasure.ToString();
        
    }
}
