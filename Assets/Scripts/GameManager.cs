using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player firstPlayer;
    [SerializeField] Player secondPlayer;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject endScreen;
    [SerializeField] Text textTime;
    [SerializeField] float gameDuration = 60;
    [SerializeField] GameObject txtEnd;
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
            if (firstPlayer.health > secondPlayer.health) endGame("Joueur 1");
            else if (firstPlayer.health < secondPlayer.health) endGame("Joueur 2");
            else endGame("");
        }
        if (firstPlayer.health <= 0) endGame("Joueur 2");
        if (secondPlayer.health <= 0) endGame("Joueur 1");
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
        if (namePlayer != "")
        {
            var monText = txtEnd.GetComponent<Text>();
            monText.text = $"Le gagant est {namePlayer}";
        }
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