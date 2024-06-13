using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RestartScreen : BaseScreen
{
    [SerializeField] Button _restartButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
    }

    public override void ActivateScreen()
    {
        base.ActivateScreen();
    }

    public override void DeactivateScreen()
    {
        base.DeactivateScreen();
    }

    void Restart()
    {
       LevelManager.instance.ResetLevel();
       UiManager.instance.SwitchScreen(GameScreens.GamePlay);
    }

    void OnExit()
    {
        Application.Quit();
    }
}
