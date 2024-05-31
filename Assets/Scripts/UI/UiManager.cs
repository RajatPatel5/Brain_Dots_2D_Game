using System.Collections.Generic;
using UnityEngine;
public enum GameScreens
{
    Home,
    Game,
    Pause,
    GameOver
}
public enum GamePopUp
{
    SoundSetting,
}

public class UiManager : MonoBehaviour
{
    [SerializeField] BaseScreen _currentScreen;
    public static UiManager instance;
    [SerializeField] List<BaseScreen> _screens;
    //[SerializeField] List<BasePopUp> _popUps;
    //Stack<BasePopUp> PopUp = new Stack<BasePopUp>();

    private void Start()
    {
        instance = this;
        _currentScreen.ActivateScreen();
    }

    private void Update()
    {
        
        _currentScreen.TackInput();

    }
    public void SwitchScreen(GameScreens screen)
    {
        foreach (BaseScreen baseScreen in _screens)
        {
            if (baseScreen.screen == screen)
            {
                baseScreen.ActivateScreen();
                _currentScreen.DeactivateScreen();
                _currentScreen = baseScreen;
            }

        }
    }
}