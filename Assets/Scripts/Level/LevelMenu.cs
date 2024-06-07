using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    private LevelData levelData;
    [SerializeField] Button backButton;

    private void Awake()
    {
        levelData = LevelDataHandler.LoadLevelData();
        int unlockLevel = levelData.unlockedLevel;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < unlockLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    private void Start()
    {
        backButton.onClick.AddListener(DisplayHomeScreen);
    }
    public void OpenLevel(int levelId)
    {
        LevelManager.instance.LoadSpecificLevel(levelId);
    }

    public void UnlockNextLevel()
    {
        levelData.unlockedLevel++;
        LevelDataHandler.SaveLevelData(levelData);

        for (int i = 0; i < levelData.unlockedLevel && i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    void DisplayHomeScreen()
    {
        UiManager.instance.SwitchScreen(GameScreens.Home);
    }
}
