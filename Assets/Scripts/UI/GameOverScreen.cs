using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameOverScreen : BaseScreen
{
    [SerializeField] Button _restartButton;
    [SerializeField] Button _nextButton;
    [SerializeField] private GameScreen gameScreen;
    [SerializeField] private HomeScreen homeScreen;
    //[SerializeField] Button _homeButton;


    private void Start()
    {
        // Add listeners to buttons
        _restartButton.onClick.AddListener(Restart);
        _nextButton.onClick.AddListener(Next);
       // _homeButton.onClick.AddListener(DisplayHomeScreen);
    }

    public override void ActivateScreen()
    {
        base.ActivateScreen();
    }

    public override void DeactivateScreen()
    {
        base.DeactivateScreen();
    }

    public void Restart()
    {
        LevelManager.instance.ResetLevel1();
        Debug.Log("restart Press");
    }

    public void Next()
    {
        //if (levelManager != null)
        //{
        //    levelManager.OnLevelCleared();
        //    Debug.Log("Pressed");
        //}
    }

    //void DisplayHomeScreen()
    //{
    //    homeScreen.enabled = true;
    //    Debug.Log("Homebtn");
    //}
    
}
