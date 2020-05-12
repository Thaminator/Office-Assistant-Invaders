using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{

    int score = 0;
    int life = 5;



    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }


    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;

    }

    public int GetLife()
    {
        return life;
    }

    public void LoseLife(int lifeValue)
    {
        life -= lifeValue;

    }

    public void AddToLife(int lifeValue)
    {
        life += lifeValue;
        Debug.Log(life);

    }


    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
