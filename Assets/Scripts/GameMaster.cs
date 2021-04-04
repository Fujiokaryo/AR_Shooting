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

    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private GameObject gameOverSet;

    [SerializeField]
    private Image gameOverFilter;

    [SerializeField]
    private GameObject imgGameOver;

    private int playerHp = 100;
    private int playerMaxHp;
    private int playerMaxMp = 3;
    private int highMagicCount;  
    public int highMagicCost = 1;
    public float playerMp;
    public bool bossClear;
    public bool isGameStart;
    public bool isBattle;
    public bool isStageClear;
    public bool isGameOver;

    private IEnumerator Start()
    {
        playerMaxHp = playerHp;
        enemyGenerator.SetUPEnemyGenerator();

        yield return new WaitForSeconds(2f);

        isGameStart = true;
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

        if(playerHp <= 0)
        {
            playerHp = 0;
            GameOverEffect();
        }
        
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

    /// <summary>
    /// �{�X�����ς݂��o�����Ă���G��0�̏ꍇ�ɃX�e�[�W�N���A���������
    /// </summary>
    public void CheckStageClear()
    {
        if(bossClear == true && enemyGenerator.enemyCount == 0)
        {
            isStageClear = true;
        }
           
    }

    /// <summary>
    /// �Q�[���I�[�o�[���o
    /// </summary>
    private void GameOverEffect()
    {
        isGameOver = true;
        gameOverSet.SetActive(true);

        gameOverFilter.DOFade(0.6f, 1.5f).SetEase(Ease.Linear)
            .OnComplete(() => imgGameOver.SetActive(true));
     
    }

}
