using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player firstPlayer;
    [SerializeField] SecondPlayer secondPlayer;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject endScreen;
    [SerializeField] Text textTime;
    [SerializeField] Text textEnd;
    [SerializeField] float gameDuration = 60;
    bool isPause;

    void Start()
    {
        Time.timeScale = 1f;
        isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        gameDuration -= Time.deltaTime;
        textTime.text = Mathf.RoundToInt(gameDuration).ToString();
        if (gameDuration <= 0)
        {
            if (firstPlayer.myHealth > secondPlayer.myHealth) EndGame("Joueur 1");
            else if (firstPlayer.myHealth < secondPlayer.myHealth) EndGame("Joueur 2");
            else EndGame("");
        }
        if (firstPlayer.myHealth <= 0) EndGame("Joueur 2");
        if (secondPlayer.myHealth <= 0) EndGame("Joueur 1");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeGame();
            else PauseGame();
        }
    }

    public void EndGame(string namePlayer)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        endScreen.SetActive(true);
        if (namePlayer != "") textEnd.text = $"Le gagant est {namePlayer}";
        else textEnd.text = "Match nul !";
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