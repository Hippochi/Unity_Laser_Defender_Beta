using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            UpdateScore();
        }
        else if (SceneManager.GetActiveScene().name == "Game Over")
        {
            FinalScore();
        }
        
    }

    public void UpdateScore()
    {
        GetComponent<TextMeshProUGUI>().text = FindObjectOfType<GameSession>().GetScore().ToString();
    }

    private void FinalScore()
    {
        GetComponent<TextMeshProUGUI>().text = "Score: " + FindObjectOfType<GameSession>().GetScore().ToString();
    }

}