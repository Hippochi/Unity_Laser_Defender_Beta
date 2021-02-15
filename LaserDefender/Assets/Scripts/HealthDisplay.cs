using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = FindObjectOfType<Player>().GetHealth();
        ShowHealth();  
    }

    public void ShowHealth()
    {
        float health = FindObjectOfType<Player>().GetHealth();
        if (health <= 0)
        {
            health = 0;
        }
        GetComponent<TextMeshProUGUI>().text = health.ToString();

        if (health/maxHealth >= 0.70)
        {
            GetComponent<TextMeshProUGUI>().color = new Color32(59, 241, 31, 255);
        }
        else if(health/maxHealth >= 0.3)
        {
            GetComponent<TextMeshProUGUI>().color = new Color32(241, 231, 31, 255);
        }
        else
        {
            GetComponent<TextMeshProUGUI>().color = new Color32(241, 31, 53, 255);
        }
    }
}
