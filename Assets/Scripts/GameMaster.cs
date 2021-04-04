using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private Slider playerHpBar;

    [SerializeField]
    private Slider playerMpBar;

    [SerializeField]
    private Text magicCountText;

    [SerializeField]
    private Button magicButton;

    public bool isBattle;
    private int playerHp = 100;
    private int playerMaxHp;
    public float playerMp;
    private int playerMaxMp = 3;
    private int highMagicCount;  
    public int highMagicCost = 1;
    public bool isBossBattle;
    public bool bossClear;

    private void Start()
    {
        playerMaxHp = playerHp;
    }

    /// <summary>
    /// �퓬�t���O�̍X�V
    /// </summary>
    /// <param name="isBattle"></param>
    public void ChangeBattle(bool isBattle = false)
    {
        this.isBattle = isBattle;
    }

    /// <summary>
    /// �v���C���[��HP�ʂ̍X�V
    /// </summary>
    /// <param name="damage"></param>
    public void UpDatePlayerHP(int damage)
    {
        playerHp -= damage;

        if(playerHp > playerMaxHp)
        {
            playerHp = playerMaxHp;
        }

        playerHpBar.DOValue((float)playerHp / (float)playerMaxHp, 0.25f);
    }

    /// <summary>
    /// �v���C���[��MP�ʂ̍X�V
    /// </summary>
    public void UpDatePlayerMP(float getMp)
    {
        playerMp += getMp;

        if(playerMp >= 3)
        {
            playerMp = 3;
        }

        CheckHighMagicCount();

        if (playerMp < 1)
        { 

            playerMpBar.DOValue(playerMp * 1, 0.2f);

        }
        else if (playerMp >= highMagicCount)
        {

            if(playerMp == 3)
            {
                playerMpBar.DOValue(1, 0.25f);
                return;
            }

            playerMpBar.DOValue(playerMp - highMagicCount * 1, 0.2f);

        }

        
    }


    /// <summary>
    /// �����@���g���邩�̔���A�g����ꍇ�ɂ͎g����񐔂̍X�V
    /// </summary>
    private void CheckHighMagicCount()
    {
        highMagicCount = Mathf.FloorToInt(playerMp / highMagicCost);
        magicCountText.text = highMagicCount.ToString();

        if (playerMp >= highMagicCost)
        {
            magicButton.interactable = true;
        }
        else if (highMagicCost > playerMp)
        {
            magicButton.interactable = false;
        }
    }


    /// <summary>
    /// �{�X��|���Ă��邩�̔���
    /// </summary>
    public void ChengeBossClear()
    {
        bossClear = true;
    }

}
