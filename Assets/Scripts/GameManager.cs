using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject firstPlayer;
    public GameObject secondPlayer;
    public GameObject pauseMenu;
    public GameObject endScreen;
    public Text textTime;
    bool isPause = false;
    bool endGame = false;
    float gameDuration = 60;

    void Update()
    {
        gameDuration -= Time.deltaTime;
        textTime.text = gameDuration.ToString();

        if (gameDuration <= 0)
        {
            // code
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeGame();
            else PauseGame();
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

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

}