using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int score;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score + "";
    }

    public void UpdateScoreBy(int i)
    {
        score += i;
        PlayerPrefs.SetInt("Score", score);
    }
}
