using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [SerializeField] private int staringScore = 5;
    [SerializeField] private int plantingCost = 1;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private string mainMenuScene;

    private int score = 0;

    //singleton
    private static HUD _instance;
    public static HUD Instance { get { return _instance; } }

    // ---------- Unity methods

    private void Awake()
    {
        _instance = this;

        score = staringScore;
        UpdateHUD();
    }

    // ---------- public methods

    public void AddScore(int addedScore)
    {
        score += addedScore;
        UpdateHUD();
    }

    public bool RemoveScore()
    {
        if (score - plantingCost < 0)
            return false;
        else
        {
            score -= plantingCost;
            UpdateHUD();
            return true;
        }
    }

    // ---------- public methods (for buttons)

    public void OnBackToMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    // ---------- private methods

    private void UpdateHUD()
    {
        scoreText.text = score.ToString();
    }
}
