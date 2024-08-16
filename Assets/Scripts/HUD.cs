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
    [SerializeField] private string mainMenuScene;
    [Header("References")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image zaznaczenie;
    [SerializeField] private Transform[] tools;
    [SerializeField] private Image[] toolsIcons;

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

    public void ChangeTool(int toolNumber)
    {
        if (toolNumber > 0)
            zaznaczenie.transform.position = tools[toolNumber - 1].transform.position;

        foreach (Image image in toolsIcons)
            image.enabled = false;

        toolsIcons[toolNumber - 1].enabled = true;
    }

    // ---------- public methods (for buttons)

    public void OnBackToMenuBtn()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void OnRestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ---------- private methods

    private void UpdateHUD()
    {
        scoreText.text = score.ToString();
    }
}
