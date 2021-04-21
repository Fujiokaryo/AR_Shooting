using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    private Transform startPos;

    private GameObject player;

    private GameMaster gameMaster;
    /// <summary>
    /// �Q�[�����X�^�[�g
    /// </summary>
    public void GameOverReStart()
    {
        startPos = GameObject.Find("StartPosAnker").GetComponent<Transform>();
        player = GameObject.Find("AR Session Origin");
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    
        player.transform.position = startPos.position;

        gameMaster.GameReady();
        GameObject.Find("GameOverSet").SetActive(false);

        //SceneManager.LoadScene("Main");
    }

    public void GameClearReStart()
    {
        startPos = GameObject.Find("StartPosAnker").GetComponent<Transform>();
        player = GameObject.Find("AR Session Origin");
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        player.transform.position = startPos.position;
        player.transform.rotation = startPos.rotation;

        gameMaster.GameReady();
        GameObject.Find("GameClearSet").SetActive(false);
    }

    /// <summary>
    /// �Q�[���I��
    /// </summary>
    public void Exit()
    {
        SceneManager.LoadScene("Start");
    }

    /// <summary>
    ///�Q�[�����x��UP���̃X�e�[�^�X�A�b�v��ʌĂяo�� 
    /// 
    /// </summary>
    public void NextLevel()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject clearSet = GameObject.Find("GameClearSet");
        canvas.transform.Find("LevelUpSet").gameObject.SetActive(true);
        GameLevel.instance.gameLevel++;
        clearSet.SetActive(false);     
    }

    /// <summary>
    /// �Q�[�����x���������ă��X�^�[�g�i�l�ݖh�~�j
    /// </summary>
    public void LevelDownReStart()
    {
        GameLevel.instance.gameLevel--;
        if(GameLevel.instance.gameLevel < 1)
        {
            GameLevel.instance.gameLevel = 1;
        }
        startPos = GameObject.Find("StartPosAnker").GetComponent<Transform>();
        player = GameObject.Find("AR Session Origin");
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        player.transform.position = startPos.position;
        player.transform.rotation = startPos.rotation;

        gameMaster.GameReady();
        GameObject.Find("GameOverSet").SetActive(false);
    }

    /// <summary>
    /// hpLevel���グ�ăQ�[���X�^�[�g
    /// </summary>
    public void HpUP()
    {
        GameLevel.instance.hpLevel++;
        startPos = GameObject.Find("StartPosAnker").GetComponent<Transform>();
        player = GameObject.Find("AR Session Origin");
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        player.transform.position = startPos.position;
        player.transform.rotation = startPos.rotation;

        gameMaster.GameReady();
        GameObject.Find("LevelUpSet").SetActive(false);
    }

    /// <summary>
    /// manaLevel���グ�ăQ�[���X�^�[�g
    /// </summary>
    public void ManaUp()
    {
        GameLevel.instance.manaLevel++;
        startPos = GameObject.Find("StartPosAnker").GetComponent<Transform>();
        player = GameObject.Find("AR Session Origin");
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        player.transform.position = startPos.position;
        player.transform.rotation = startPos.rotation;

        gameMaster.GameReady();
        GameObject.Find("LevelUpSet").SetActive(false);
    }

    /// <summary>
    /// attackLevel���グ�ăQ�[���X�^�[�g
    /// </summary>
    public void AttackUP()
    {
        GameLevel.instance.attackLevel++;
        startPos = GameObject.Find("StartPosAnker").GetComponent<Transform>();
        player = GameObject.Find("AR Session Origin");
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        player.transform.position = startPos.position;
        player.transform.rotation = startPos.rotation;

        gameMaster.GameReady();
        GameObject.Find("LevelUpSet").SetActive(false);
    }
}
