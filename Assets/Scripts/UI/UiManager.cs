using System.Collections.Generic;
using UnityEngine;

public enum GameScreens
{
    Home,
    GamePlay,
    Restart,
    GameOver
}

public class UiManager : MonoBehaviour
{
    [SerializeField] BaseScreen currentScreen;
    public static UiManager instance;
    [SerializeField] List<BaseScreen> screens;

    private void Start()
    {
        instance = this;
        currentScreen.ActivateScreen();
    }

    public void SwitchScreen(GameScreens screen)
    {
        foreach (BaseScreen baseScreen in screens)
        {
            if (baseScreen.screen == screen)
            {
                baseScreen.ActivateScreen();
                currentScreen.DeactivateScreen();
                currentScreen = baseScreen;
            }
        }
    }
}