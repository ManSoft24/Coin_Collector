using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoretxt; 

    public int score = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore();
    }

    public void TambahScore(int nilai)
    {
        score += nilai;
        UpdateScore();
    }

    // Update is called once per frame
    void UpdateScore()
    {
        scoretxt.text = score.ToString();
    }
}
