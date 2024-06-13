using UnityEngine;
using UnityEngine.UI;
using System;
public class GameScreen : BaseScreen
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Slider inkSlider;
    [SerializeField] private int inkMaxCount;


    private void OnEnable()
    {
        LinesDrawer.updateInkSlider += UpdateInkSlider;
        //LinesDrawer.resetInkSlider += ResetSliderValue;
    }

    private void OnDisable()
    {
        LinesDrawer.updateInkSlider -= UpdateInkSlider;
       // LinesDrawer.resetInkSlider -= ResetSliderValue;
    }

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart); 
        inkSlider.maxValue = inkMaxCount;
        inkSlider.value = 0;
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
        LevelManager.instance.ResetLevel();
    }

    private void UpdateInkSlider(int pointsToAdd)
    {
        inkSlider.value += pointsToAdd;
        if (inkSlider.value >= inkSlider.maxValue)
        {
            UiManager.instance.SwitchScreen(GameScreens.Restart);
            inkSlider.value = 0;
            LevelManager.instance.ResetLevel();
        }
    }

    //private void ResetSliderValue()
    //{
    //    inkSlider.value = 0;
    //}
}
