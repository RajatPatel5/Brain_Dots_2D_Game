using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : BaseScreen
{ 
    [SerializeField] Button _restartButton;
    [SerializeField] Button _nextButton;
    //[SerializeField] Button _exitButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
         _nextButton.onClick.AddListener(Next);
       // _exitButton.onClick.AddListener(OnExit);
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
    public void Restart()
    {
        // AudioManager.instance.Play(SoundName.ButtonSound);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log("Restart");
    }
    public void Next()
    {
        //// Get the index of the current scene
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //// Calculate the index of the next scene
        //int nextSceneIndex = currentSceneIndex + 1;

        //// Load the next scene
        //SceneManager.LoadScene(nextSceneIndex);

        //Debug.Log("Next Screen index:" + nextSceneIndex);

        //Debug.Log("Next Level ");
        SceneControllerScript.instance.NextLevel();
    }

    void OnExit()
    {
        //  UiManager.instance.OpenPopUp(GamePopUp.SoundSetting);
        Application.Quit();
    }
}
