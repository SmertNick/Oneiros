using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject[] systemPrefabs;
//    public Events.EventGameState OnGameStateChanged;
    private readonly List<GameObject> instancedSystemPrefabs = new List<GameObject>();
    private readonly List<AsyncOperation> loadOperations = new List<AsyncOperation>();
    private string currentLevelName = string.Empty;
    public GameState CurrentGameState { get; private set; } = GameState.Pregame;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        InstantiateSystemPrefabs();
    }

    #region SceneControl
    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
        }
        Debug.Log("Load Complete");
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
    }
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
    #endregion

    #region ManagersControl
    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        foreach (GameObject obj in systemPrefabs)
        {
            prefabInstance = Instantiate(obj);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (int i = 0; i < instancedSystemPrefabs.Count; i++)
        {
            Destroy(instancedSystemPrefabs[i]);
        }
        instancedSystemPrefabs.Clear();
    }
    #endregion

    #region StateControl
    void UpdateState(GameState state)
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
                break;
        }

        Events.ChangeGameState(CurrentGameState, previousGameState);
    }

    public void StartGame()
    {
        LoadLevel("Main");
    }

    public void TogglePause()
    {
        //if called from pregame might cause bugs
        //UpdateState(currentGameState == GameState.Running ? GameState.Paused : GameState.Running);
        if (CurrentGameState == GameState.Running)
        {
            UpdateState(GameState.Paused);
        }
        else if (CurrentGameState == GameState.Paused)
        {
            UpdateState(GameState.Running);
        }
    }

    public void RestartGame()
    {

    }

    public void QuitGame()
    {

    }
    #endregion
}
