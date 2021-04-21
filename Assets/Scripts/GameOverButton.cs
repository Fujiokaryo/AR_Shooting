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
    /// ゲームリスタート
    /// </summary>
    public void ReStart()
    {
        startPos = GameObject.Find("StartPosAnker").GetComponent<Transform>();
        player = GameObject.Find("AR Session Origin");
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        

        player.transform.position = startPos.position;

        gameMaster.GameReady();
        GameObject.Find("GameOverSet").SetActive(false);

        //SceneManager.LoadScene("Main");
    }
    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void Exit()
    {
        SceneManager.LoadScene("Start");
    }

    /// <summary>
    ///ゲームレベルUP時のステータスアップ画面呼び出し 
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
    /// ゲームレベルを下げてリスタート（詰み防止）
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
    /// hpLevelを上げてゲームスタート
    /// </summary>
    public void HpUP()
    {
        GameLevel.instance.hpLevel++;
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// manaLevelを上げてゲームスタート
    /// </summary>
    public void ManaUp()
    {
        GameLevel.instance.manaLevel++;
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// attackLevelを上げてゲームスタート
    /// </summary>
    public void AttackUP()
    {
        GameLevel.instance.attackLevel++;
        SceneManager.LoadScene("Main");
    }
}
