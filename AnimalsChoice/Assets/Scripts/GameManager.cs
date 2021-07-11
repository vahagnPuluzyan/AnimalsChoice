using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public Text levelText;
    public GameObject startButton;
    public GameObject gameWinUi;
    public GameObject gameOverUI;

    [HideInInspector] public bool gameIsOver;
    GameObject joystick;

    void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4203769", false);
        }

        joystick = FindObjectOfType<FloatingJoystick>().gameObject;
        _instance = this;
        Time.timeScale = 0;
        joystick.SetActive(false);
        if (LevelManager._instance != null)
        {
            levelText.text = "Level " + LevelManager._instance.currentLevel;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startButton.SetActive(false);
        joystick.SetActive(true);
        AnimalTransformation._instance.playerAnimator.SetBool("Run", true);
    }

    public void CharacterDead()
    {
        gameOverUI.SetActive(true);
    }

    public void SkipGame()
    {
        if (Advertisement.isInitialized)
        {
            var showOptions = new ShowOptions();
            showOptions.resultCallback += ResultCallback;
            Advertisement.Show("Rewarded_Android", showOptions);
        }
    }

    public void GameOver()
    {
        LevelManager._instance.ReplayScene();
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
        AnimalTransformation._instance.playerAnimator.SetBool("Run", false);
    }

    public void NextLevel()
    {
        LevelManager._instance.NextLevel();
        LevelManager._instance.RandomScene();
    }

    private void ResultCallback(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            NextLevel();
        }
        else
        {
            Debug.Log("No award given. Result was :: " + result);
        }
    }
}
