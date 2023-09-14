using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;
    [SerializeField] ShieldManager shieldManager;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI shieldText;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start() 
    {
        healthText.text = "Hull: " + playerHealth.GetHealth();
        if(shieldManager.shieldEnabled)
            shieldText.text = "Shield: " + shieldManager.shieldHealth;
        else
            shieldText.text = "";
       //healthSlider.maxValue = playerHealth.GetHealth(); 
    }

    void Update()
    {
        healthText.text = "Hull: " + playerHealth.GetHealth();
        if(shieldManager.shieldEnabled)
            shieldText.text = "Shield: " + shieldManager.shieldHealth;
        else
            shieldText.text = "";
        scoreText.text = scoreKeeper.GetScore().ToString("0000000");
        //healthSlider.value = playerHealth.GetHealth();
    }
}
