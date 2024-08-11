using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private int score = 0;

    //singleton
    private static HUD _instance;
    public static HUD Instance { get { return _instance; } }

    // ---------- Unity methods

    private void Awake()
    {
        _instance = this;
    }

    // ---------- public methods

    public void AddScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = score.ToString();
    }
}
