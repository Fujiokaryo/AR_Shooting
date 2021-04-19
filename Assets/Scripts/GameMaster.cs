using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

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
    private GameObject canvas;

    [SerializeField]
    private Text hpText;

    [SerializeField]
    private GameObject gameStartSet;

    [SerializeField]
    private Image imgLevelNum;

    [SerializeField]
    private PlayGameLevelSO playGameLevel;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private FieldAutoScroller fieldAutoScroller;

    [SerializeField]
    private PathDataSO pathDataSO;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private List<GameObject> enemiesList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> gimmicksList = new List<GameObject>();

    [System.Serializable]
    public class RootEventData
    {
        public int[] rootEventNos;
        public BranchDirectionType[] branchDirectionTypes;
        public RootType rootType;
    }

    [SerializeField]
    private List<RootEventData> rootDatasList = new List<RootEventData>();

    private int currentRailCount;


    private int playerHp;
    public int playerMaxHp;
    private int playerMaxMp;
    private int highMagicCount;  
    public int highMagicCost = 1;
    public float playerMp;

    public bool bossClear;
    public bool isGameStart;
    public bool isBattle;
    public bool isGameOver;
    public bool isStageClear;

    public IEnumerator Start()
    {
        BGMmanager.instance.PlayBGM(SoundDataSO.BgmType.Main);
        imgLevelNum.sprite = SelectGameLevelNum();

        //��{HP��100�AHPLevel���オ�閈��50���㏸
        playerHp = 50 + GameLevel.instance.hpLevel * 50;
        playerMaxHp = playerHp;

        //�����@�̍ő�X�g�b�N���A��{2��ManaLevel��1�オ�閈��1������
        playerMaxMp = GameLevel.instance.manaLevel;
        enemyGenerator.SetUPEnemyGenerator();
        hpText.text = playerHp + " / " + playerMaxHp.ToString();

        yield return new WaitForSeconds(2.0f);
        gameStartSet.SetActive(false);
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
        if(isGameOver == true)
        {
            return;
        }

        playerHp -= damage;

        if(playerHp > playerMaxHp)
        {
            playerHp = playerMaxHp;
        }

        playerHpBar.DOValue((float)playerHp / (float)playerMaxHp, 0.25f);
        

        if (playerHp <= 0)
        {
            playerHp = 0;
            BGMmanager.instance.PlayBGM(SoundDataSO.BgmType.GameOver);
            GameOverEffect("GameOver");
            isGameOver = true;
        }
        hpText.text = playerHp + " / " + playerMaxHp.ToString();

    }

    /// <summary>
    /// �v���C���[��MP�ʂ̍X�V
    /// </summary>
    public void UpDatePlayerMP(float getMp)
    {
        playerMp += getMp;

        if(playerMp >= playerMaxMp)
        {
            playerMp = playerMaxMp;
        }

        CheckHighMagicCount();

        if (playerMp < 1)
        { 

            playerMpBar.DOValue(playerMp * 1, 0.2f);

        }
        else if (playerMp >= highMagicCount)
        {

            if(playerMp == playerMaxMp)
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
        if(bossClear == true && enemyGenerator.enemyCount == 0�@&& isGameOver == false)
        {
            BGMmanager.instance.PlayBGM(SoundDataSO.BgmType.GameClear);
            GameOverEffect("GameClear");
            isStageClear = true;
        }
           
    }

    /// <summary>
    /// �Q�[���I�[�o�[���o
    /// </summary>
    private void GameOverEffect(string GameOver)
    {
        GameObject gameOverSet = canvas.transform.Find(GameOver + "Set").gameObject;
        Image gameOverFilter = gameOverSet.transform.Find(GameOver + "Filter").gameObject.GetComponent<Image>();
        GameObject imgGameOver = gameOverSet.transform.Find("img" + GameOver).gameObject;
        GameObject nextLevelBtn = null;

        if (GameOver == "GameClear")
        {
            nextLevelBtn = imgGameOver.transform.Find("NextLevelButton").gameObject;
        }
       
        gameOverSet.SetActive(true);

        gameOverFilter.DOFade(0.6f, 2.0f).SetEase(Ease.Linear)
            .OnComplete(() => imgGameOver.SetActive(true));
        if (GameLevel.instance.gameLevel == 10)
        {
            if(nextLevelBtn.GetComponent<Button>().interactable == true)
            {
                nextLevelBtn.SetActive(false);
            }
            
        }
    }
    
    /// <summary>
    /// �X�e�[�W���x���\���p��Sprite�̑I��
    /// </summary>
    /// <returns></returns>
    private Sprite SelectGameLevelNum()
    {
        PlayGameLevelSO.GameLevelImage newgameLevelImage = null;
        foreach (PlayGameLevelSO.GameLevelImage gameLevelImage in playGameLevel.gameLevelImageList.Where(x => x.no == GameLevel.instance.gameLevel))
        {
            newgameLevelImage = gameLevelImage;
            break;
        }

        return newgameLevelImage.levelSprite;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="checkRootNo"></param>
    /// <returns></returns>
    private List<PathData>GetPathDatasList(int checkRootNo)
    {
        return pathDataSO.rootDatasList.Find(x => x.rootNo == checkRootNo).pathDatasList;
    }

    /// <summary>
    /// ���[�g�̊m�F
    /// ���򂪂���ꍇ�ɂ͕���̖��{�^���𐶐�
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckNextRootBranch()
    {
        if(currentRailCount >= rootDatasList.Count)
        {
            Debug.Log("�N���A");
            yield break;
        }

        //
        switch (rootDatasList[currentRailCount].rootType)
        {
            case RootType.Normal_Battle:

                //���̃��[�g����Ȃ�
                if (rootDatasList[currentRailCount].rootEventNos.Length == 1)
                {
                    //�����I�Ƀ��[���ړ����J�n
                    fieldAutoScroller.SetNextField(GetPathDatasList(rootDatasList[currentRailCount].rootEventNos[0]));
                }
                else
                {
                    //
                    yield return StartCoroutine(uiManager.GenerateBranchButtons(rootDatasList[currentRailCount].rootEventNos, rootDatasList[currentRailCount].branchDirectionTypes));

                    //
                    yield return new WaitUntil(() => uiManager.GetSubmitBranch().Item1 == true);

                    //
                    fieldAutoScroller.SetNextField(GetPathDatasList(uiManager.GetSubmitBranch().Item2));
                }
                break;

            case RootType.Boss_Battle:

                break;

            case RootType.Event:

                break;

        }

        currentRailCount++;
    }

    /// <summary>
    /// �G�l�~�[�̐����C�x���g
    /// </summary>
    /// <param name="enemyEventData"></param>
    /// <param name="enemyEventTran"></param>
    public void GenerateEnemy(EventDataSO.EventData enemyEventData, Transform enemyEventTran)
    {
        //GameObject enemy = Instantiate(enemyEventData.eventPrefab, enemyEventTran);
        //enemy.GetComponent<EnemyController>().SetUpEnemy();
        //enemiesList.Add(enemy);
    }

    public void GenerateGimmick(EventDataSO.EventData enemyEventData, Transform enemyEventTran)
    {
        GameObject enemy = Instantiate(enemyEventData.eventPrefab, enemyEventTran);
        gimmicksList.Add(enemy);

        
    }
}
