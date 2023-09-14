using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] GameObject gameOverDisplay;
    [SerializeField] bool isTransition;
    [SerializeField] bool advertTransition;
    [SerializeField] float sceneLoadDelay = 3f;

    ScoreKeeper scoreKeeper;

    void Awake() 
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        if (isTransition && !advertTransition) 
        {
            Invoke((nameof(LoadNextScene)), sceneLoadDelay);
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadInfoMenu()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Info Menu");
    }

    public void LoadMainMenu() 
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadGameOver() 
    {
        StartCoroutine(WaitAndLoad("Game Over", sceneLoadDelay));
    }

    public void RestartGame()
    {
        StartCoroutine(WaitAndLoad("Level 1-1 Transition", sceneLoadDelay));
    }

    public void QuitGame() 
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void ContinueButton()
    {
        AdMaker.Instance.ShowAd(this);
        continueButton.interactable = false;
    }

    public void ContinueGame()
    {
        advertTransition = false;
    }
    IEnumerator WaitAndLoad(string sceneName, float delay) 
    {
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(delay);
    }
}
