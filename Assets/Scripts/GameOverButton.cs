using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{

    
    /// <summary>
    /// �Q�[�����X�^�[�g
    /// </summary>
    public void ReStart()
    {
        SceneManager.LoadScene("Main");
    }
    /// <summary>
    /// �Q�[���I��
    /// </summary>
    public void Exit()
    {
        SceneManager.LoadScene("Start");
    }

    /// <summary>
    ///�Q�[�����x�����グ�ăQ�[�����X�^�[�g�A�X�e�[�^�X�A�b�v��ʌĂяo�� 
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
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// hpLevel���グ�ăQ�[���X�^�[�g
    /// </summary>
    public void HpUP()
    {
        GameLevel.instance.hpLevel++;
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// manaLevel���グ�ăQ�[���X�^�[�g
    /// </summary>
    public void ManaUp()
    {
        GameLevel.instance.manaLevel++;
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// attackLevel���グ�ăQ�[���X�^�[�g
    /// </summary>
    public void AttackUP()
    {
        GameLevel.instance.attackLevel++;
        SceneManager.LoadScene("Main");
    }
}
