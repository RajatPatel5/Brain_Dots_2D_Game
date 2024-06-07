using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : BaseScreen
{
    [SerializeField] Button _restartButton;
    [SerializeField] Button _nextButton;
    [SerializeField] private GameScreen gameScreen;
    [SerializeField] private HomeScreen homeScreen;

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
        _nextButton.onClick.AddListener(Next);
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
        LevelManager.instance.ResetLevel();
        UiManager.instance.SwitchScreen(GameScreens.GamePlay);
        Debug.Log("restart Press");
    }

    public void Next()
    {
        LevelManager.instance.OnLevelCleared();
        UiManager.instance.SwitchScreen(GameScreens.GamePlay);
        Debug.Log("Pressed");
    }
}
