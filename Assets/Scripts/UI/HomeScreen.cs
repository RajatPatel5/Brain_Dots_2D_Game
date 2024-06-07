using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : BaseScreen
{
    public LinesDrawer linesDrawer;
    [SerializeField] Button _playButton;

    private void Start()
    {
        _playButton.onClick.AddListener(OnPlay);
        linesDrawer.enabled = false;
    }
     public override void ActivateScreen()
    {
      //  AudioManager.instance.PlayInBackGround(SoundName.HomeScreenSound);
        base.ActivateScreen();
    }

    public override void DeactivateScreen()
    {
        base.DeactivateScreen();
    }
    void OnPlay()
    {
        UiManager.instance.SwitchScreen(GameScreens.GamePlay);
        linesDrawer.enabled = true;
    }
    void OnExit()
    {
        Application.Quit();
    }
}
