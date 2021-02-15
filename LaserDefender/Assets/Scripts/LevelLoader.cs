using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelLoader : MonoBehaviour
{

    public void StartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        if (FindObjectsOfType<GameSession>().Length > 0)
        {
            Destroy(FindObjectOfType<GameSession>().gameObject);
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game Over");
    }
}
