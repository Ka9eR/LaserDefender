using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    void Update()
    {
        HealthUpdate();
        ScoreUpdate();
    }

    void ScoreUpdate()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("Score: 00000000");
    }

    void HealthUpdate()
    {
        healthSlider.value = playerHealth.GetHealth();
    }
}
