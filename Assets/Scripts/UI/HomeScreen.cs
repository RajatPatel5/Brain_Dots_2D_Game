using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : BaseScreen
{
    public LinesDrawer lines;
    [SerializeField] Button _playButton;
   // [SerializeField] Button _exitButton;
   // [SerializeField] Button _settingButton;

    private void Start()
    {
        _playButton.onClick.AddListener(OnPlay);
        //  _exitButton.onClick.AddListener(OnExit);
        //_settingButton.onClick.AddListener(OnSetting);

        lines.enabled = false;
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
        // AudioManager.instance.Play(SoundName.ButtonSound);
        //lines.Ball1.isKinematic = true;
        //lines.Ball2.isKinematic = true;
        UiManager.instance.SwitchScreen(GameScreens.Game);
        lines.enabled = true;
    }
    void OnExit()
    {
        //AudioManager.instance.Play(SousndName.ButtonSound);

        Application.Quit();
    }
    //private IEnumerator StartGameplay()
    //{
    //    yield return new WaitForSeconds(1f);
    //    lines.enabled = true;
    //}
}
