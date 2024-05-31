using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<GameObject> levelPrefabs;
    private int currentLevelIndex = 0;
    private GameObject currentLevel;
    public GameObject environmentParent;
    public GameOverScreen gameOverScreen;
    public GameScreen gameScreen;
    [SerializeField] private LinesDrawer linesdrawer;

    [SerializeField] Animator animator;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        LoadNextLevel();
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

    public void OnLevelCleared()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        linesdrawer.ResetLine();
        LoadNextLevel(); // Load the next level after the current one is cleared
        linesdrawer.enabled = false;

        gameOverScreen.DeactivateScreen();
        gameScreen.ActivateScreen();
        linesdrawer.enabled = true;
    }

    private void LoadNextLevel()
    {
        if (currentLevelIndex < levelPrefabs.Count)
        {
            Debug.Log("Loading level at index: " + currentLevelIndex);
            currentLevel = Instantiate(levelPrefabs[currentLevelIndex], Vector3.zero, Quaternion.identity, environmentParent.transform);
            currentLevelIndex++;
            InstantiateBalls();
        }
        else
        {
            Debug.Log("Level End");
        }
    }
    //public void ResetLevel()
    //{
    //    StartCoroutine(ResetLevel1());
    //    if (currentLevel != null)
    //    {
    //        Destroy(currentLevel);
    //    }
    //    // Reload the current level
    //    linesdrawer.ResetLine();
    //    currentLevel = Instantiate(levelPrefabs[currentLevelIndex ], Vector3.zero, Quaternion.identity, environmentParent.transform);
    //   // currentLevel = Instantiate(levelPrefabs[currentLevelIndex -1], Vector3.zero, Quaternion.identity, environmentParent.transform);  // without Level
    //    InstantiateBalls();
    //    linesdrawer.enabled = true;
    //    gameOverScreen.DeactivateScreen();
    //    gameScreen.ActivateScreen();
    //    Debug.Log("Reset");
    //}

    public IEnumerator ResetLevel1()
    {
        yield return new WaitForSeconds(0.1f);   
    }

    public void InstantiateBalls()
    {
        Transform ball1Transform = currentLevel.transform.Find("Ball1");
        Transform ball2Transform = currentLevel.transform.Find("Ball2");

        Debug.Log("InstantiateBalls called");
        if (ball1Transform != null && ball2Transform != null)
        {
            Rigidbody2D ball1Rigidbody = ball1Transform.GetComponent<Rigidbody2D>();
            Rigidbody2D ball2Rigidbody = ball2Transform.GetComponent<Rigidbody2D>();

            if (ball1Rigidbody != null && ball2Rigidbody != null)
            {
                linesdrawer.SetBalls(ball1Rigidbody, ball2Rigidbody);
                // Set balls to kinematic
                ball1Rigidbody.isKinematic = true;
                ball2Rigidbody.isKinematic = true;

            }
        }
    }

    public void GameOverScreenFunction()
    {
        linesdrawer.enabled = false;
        Debug.Log("Active-" + linesdrawer.isActiveAndEnabled);
        gameScreen.DeactivateScreen();
        gameOverScreen.ActivateScreen();
    }

    public void GameOverAnimation()
    {
        animator.SetTrigger("IsGameOver");
        Debug.Log("Circle Animation");
    }


    // New method to load a specific level
    public void LoadSpecificLevel(int levelIndex)
    {
        Debug.Log("LoadSpecificLevel called with levelIndex: " + levelIndex);

        if (levelIndex >= 0 && levelIndex < levelPrefabs.Count)
        {
            if (currentLevel != null)
            {
                Destroy(currentLevel);
            }

            currentLevelIndex = levelIndex; // Correctly set the currentLevelIndex
            currentLevel = Instantiate(levelPrefabs[currentLevelIndex], Vector3.zero, Quaternion.identity, environmentParent.transform);
            InstantiateBalls();

            gameOverScreen.DeactivateScreen();
            gameScreen.ActivateScreen();
            linesdrawer.ResetLine();
            linesdrawer.enabled = true;

            Debug.Log("Loaded specific level: " + levelIndex);
        }
        else
        {
            Debug.LogWarning("Invalid level index: " + levelIndex);
        }
    }

}

