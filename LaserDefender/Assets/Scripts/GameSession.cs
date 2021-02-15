using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameSession : MonoBehaviour
{
    int score = 0;

    private void Awake()
    {
        int sessionCount = FindObjectsOfType<GameSession>().Length;
        if (sessionCount > 1)
        {
            gameObject.SetActive(false);
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
        score = 0;
    }

    public void AddtoScore(int points)
    {
        score += points;
        FindObjectOfType<ScoreDisplay>().UpdateScore();
    }

    public int GetScore() { return score; }

}
