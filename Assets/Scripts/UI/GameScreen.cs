using UnityEngine;
using UnityEngine.UI;
using System;
public class GameScreen : BaseScreen
{
    [SerializeField] Button _restartButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
    }
    
    public override void DeactivateScreen()
    {
        base.DeactivateScreen();
    }
    public override void ActivateScreen()
    {
        base.ActivateScreen();
    }
    void Restart()
    {
        LevelManager.instance.ResetLevel1();
    }
}
