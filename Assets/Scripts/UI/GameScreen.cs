using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BaseScreen
{
    //[SerializeField] Button _pauseButton;


    private void Start()
    {

       // _pauseButton.onClick.AddListener(OnPause);
    }

    void OnPause()
    {
        //AudioManager.instance.Play(SoundName.ButtonSound);

        UiManager.instance.SwitchScreen(GameScreens.Pause);
    }

    public override void DeactivateScreen()
    {
        base.DeactivateScreen();
    }

    public override void ActivateScreen()
    {
        //AudioManager.instance.PlayInBackGround(SoundName.PlayScreenSound);
        base.ActivateScreen();
    }
}
