using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created'
    public TextMeshProUGUI highscoreText;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Score")> PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", PlayerPrefs.GetInt("Score"));
        }
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
    }

    public void StartGame(int i)
    {
        PlayerPrefs.SetInt("Weapon", i);
        SceneManager.LoadScene("MainGame");
    }
}
