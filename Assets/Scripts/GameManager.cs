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
        // Update l'UI
        gameDuration -= Time.deltaTime;
        textTime.text = Mathf.RoundToInt(gameDuration).ToString();

        // Check le temps écoulé
        if (gameDuration <= 0)
        {
            // Détermine un Gagant à la fin du tempss
            if (firstPlayer.myHealth > secondPlayer.myHealth) EndGame("Joueur 1");
            else if (firstPlayer.myHealth < secondPlayer.myHealth) EndGame("Joueur 2");
            else EndGame("");
        }

        // Choisi un gagnant quand un des deux n'a plus de point de vie
        if (firstPlayer.myHealth <= 0) EndGame("Joueur 2");
        if (secondPlayer.myHealth <= 0) EndGame("Joueur 1");

        // Action qui met en pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeGame();
            else PauseGame();
        }
    }

    // Affiche l'écran de fin de partie en fonction du nom du joueur gagnant
    public void EndGame(string namePlayer)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        endScreen.SetActive(true);
        if (namePlayer != "") textEnd.text = $"Le gagant est {namePlayer}";
        else textEnd.text = "Match nul !";
    }

    // Fait reprend la partie
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPause = false;
    }

    // Met en pause la partie encours
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPause = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Recharge la scène
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Retourne au menu pricncipale
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}