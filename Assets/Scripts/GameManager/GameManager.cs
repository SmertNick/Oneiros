using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject[] systemPrefabs;
    private readonly List<GameObject> instancedSystemPrefabs = new List<GameObject>();
    private readonly List<AsyncOperation> loadOperations = new List<AsyncOperation>();
    private string currentLevelName = string.Empty;
    public Theme theme = Theme.Happy;
    public GameState CurrentGameState { get; private set; } = GameState.Pregame;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        InstantiateSystemPrefabs();
        Events.OnThemeChange += ThemeChangeHandler;
        Events.ChangeTheme(theme);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeTheme();
        }
    }

    #region ThemeControl
    private void ThemeChangeHandler(Theme newTheme)
    {
        theme = newTheme;
    }

    private void ChangeTheme(Theme newTheme)
    {
        Events.ChangeTheme(theme);
    }

    private void ChangeTheme(int themeID)
    {
        Events.ChangeTheme((Theme) (themeID % 2));
    }

    private void ChangeTheme()
    {
        ChangeTheme((int)theme + 1);
    }
    
    #endregion

    #region SceneControl
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }
        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);
        currentLevelName = levelName;
        //add transition animation
        //make a progress bar from ao.progress (float 0-1)
    }

    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
            ao.completed -= OnLoadOperationComplete;
        }
        Events.ChangeGameState(GameState.Running, GameState.Pregame);
        //End transition animation
        Debug.Log("Load Complete");
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + levelName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        ao.completed -= OnUnloadOperationComplete;
        Debug.Log("Unload Complete");
    }
    #endregion

    #region ManagersControl

    private void InstantiateSystemPrefabs()
    {
        foreach (GameObject obj in systemPrefabs)
            instancedSystemPrefabs.Add(Instantiate(obj));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Events.OnThemeChange -= ThemeChangeHandler;
        foreach (GameObject obj in instancedSystemPrefabs)
            Destroy(obj);
        instancedSystemPrefabs.Clear();
    }
    #endregion

    #region StateControl

    private void UpdateState(GameState state)
    {
        GameState previousGameState = CurrentGameState;
        CurrentGameState = state;
        switch (CurrentGameState)
        {
            case GameState.Pregame:
                Time.timeScale = 1f;
                break;
            case GameState.Running:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Events.ChangeGameState(CurrentGameState, previousGameState);
    }

    public void StartGame()
    {
        LoadLevel("Main");
    }

    public void TogglePause()
    {
        if (CurrentGameState == GameState.Pregame) return;
        UpdateState(CurrentGameState == GameState.Running ? GameState.Paused : GameState.Running);
    }

    public void RestartGame()
    {
        throw new NotImplementedException();
    }

    public void QuitGame()
    {
        throw new NotImplementedException();
    }
    #endregion
}

public enum Theme
{
    Happy = 0,
    Brutal = 1
}
public enum GameState
{
    Pregame = 0,
    Running = 1,
    Paused = 2
}