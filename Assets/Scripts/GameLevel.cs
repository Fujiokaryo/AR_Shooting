using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public static GameLevel instance;

    public int gameLevel = 1;
    public int manaLevel = 2;
    public int hpLevel = 1;
    public int attackLevel = 1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
