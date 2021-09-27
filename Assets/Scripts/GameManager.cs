using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject firstPlayer;
    public GameObject secondPlayer;
    public GameObject pauseMenu;
    bool isPause = false;
    int gameDuration = 60;
    float currentTime;

    void Update()
    {
        // check condition de victoire

        // mettre à jour la scène

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeGame();
            else PauseGame();
        }
    }

    void Start()
    {
        Debug.Log("Init game !");
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
        SceneManager.LoadScene("Menu");
    }

}