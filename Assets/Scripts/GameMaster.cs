using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private Slider playerHpBar;

    public bool isBattle;
    private int playerHp = 100;
    private int playerMaxHp;


    private void Start()
    {
        playerMaxHp = playerHp;
    }

    public void ChangeBattle(bool isBattle = false)
    {
        this.isBattle = isBattle;
    }

    public void UpDatePlayerHP(int damage)
    {
        playerHp -= damage;
        playerHpBar.DOValue((float)playerHp / (float)playerMaxHp, 0.25f);
    }
}
