using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<GameObject> levelPrefabs;
    private int currentLevelIndex = 0;
    private GameObject currentLevel;
    public GameObject environmentParent;
    [SerializeField] private LinesDrawer linesdrawer;
    public LevelMenu levelmenu;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        BallCollisionDetection.gameOver += GameOverScreenFunction;
    }

    private void OnDisable()
    {
        BallCollisionDetection.gameOver -= GameOverScreenFunction;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            linesdrawer.enabled = false;
        }
        else
        {
            linesdrawer.enabled = true;
        }
    }

    public void InstantiateBalls()
    {
        Level levelPrefab = currentLevel.GetComponent<Level>();
        Rigidbody2D ball1Rigidbody = levelPrefab.ball1.GetComponent<Rigidbody2D>();
        Rigidbody2D ball2Rigidbody = levelPrefab.ball2.GetComponent<Rigidbody2D>();
        linesdrawer.SetBalls(ball1Rigidbody, ball2Rigidbody);
    }

    private void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelPrefabs.Count)
        {
            currentLevel = Instantiate(levelPrefabs[levelIndex], Vector3.zero, Quaternion.identity, environmentParent.transform);
            InstantiateBalls();
        }
    }

    public void OnLevelCleared()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        linesdrawer.ResetLine();
        PlayerPrefs.SetInt("CurrentLevelIndex at save time:", currentLevelIndex);
        PlayerPrefs.Save();
        levelmenu.UnlockNextLevel();
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        LoadLevel(currentLevelIndex);
    }

    public void ResetLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        LoadLevel(currentLevelIndex);
        linesdrawer.ResetLine();
        Debug.Log("Reset");
    }

    public void LoadSpecificLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelPrefabs.Count)
        {
            if (currentLevel != null)
            {
                Destroy(currentLevel);
            }
            currentLevelIndex = levelIndex;
            LoadLevel(currentLevelIndex);
            linesdrawer.ResetLine();
            linesdrawer.enabled = true;
            Debug.Log("Loaded specific level: " + levelIndex);
        }
    }

    public void GameOverScreenFunction()
    {
        linesdrawer.enabled = false;
        linesdrawer.BallsKinematic();
        UiManager.instance.SwitchScreen(GameScreens.GameOver);
    }
}
