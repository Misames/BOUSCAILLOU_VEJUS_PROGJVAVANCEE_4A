using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerIA : MonoBehaviour
{
    [SerializeField] PlayerVsIA firstPlayer;
    [SerializeField] IAPlayer secondPlayer;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject txtEnd;
    [SerializeField] Text textTime;
    [SerializeField] float gameDuration = 60;
    bool isPause;

    void Start()
    {
        Time.timeScale = 1f;
        isPause = false;
    }

    void Update()
    {
        gameDuration -= Time.deltaTime;
        textTime.text = Mathf.RoundToInt(gameDuration).ToString();
        if (gameDuration <= 0)
        {
            if (firstPlayer.myHealth > secondPlayer.myHealth) endGame("Joueur 1");
            else if (firstPlayer.myHealth < secondPlayer.myHealth) endGame("Joueur 2");
            else endGame("");
        }
        if (firstPlayer.myHealth <= 0) endGame("Joueur 2");
        if (secondPlayer.myHealth <= 0) endGame("Joueur 1");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeGame();
            else PauseGame();
        }
    }

    public void endGame(string namePlayer)
    {
        Time.timeScale = 0f;
        endScreen.SetActive(true);
        Text monText = txtEnd.GetComponent<Text>();
        if (namePlayer != "") monText.text = $"Le gagant est {namePlayer}";
        else monText.text = "Match nul !";
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPause = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPause = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

}