using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using static Events;

public class GameManager : Singleton<GameManager>
{
    private List<GameObject> instancedSystemPrefabs;
    private List<AsyncOperation> loadOperations;
    private string currentLevelName = string.Empty;
    private GameState currentGameState = GameState.PREGAME;

    public GameObject[] SystemPrefabs;

    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    public GameState CurrentGameState
    {
        get { return this.currentGameState; }
        private set { this.currentGameState = value; }
    }

    public EventGameState OnGameStateChanged;

    public void LoadLevel(string levelName)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if (asyncOperation == null)
        {
            Debug.LogError($"[{nameof(GameManager)}] Unable to load level {levelName}");
            return;
        }

        asyncOperation.completed += OnLoadOperationComplete;
        this.loadOperations.Add(asyncOperation);

        this.currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        var asyncOperation = SceneManager.UnloadSceneAsync(levelName);
        asyncOperation.completed += OnUnloadOperationComplete;
    }

    public void StartGame()
    {
        LoadLevel("Main");
    }

    public void TogglePause()
    {
        if (this.currentGameState == GameState.RUNNING)
        {
            UpdateState(GameState.PAUSED);
        }
        else
        {
            UpdateState(GameState.RUNNING);
        }
    }

    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
    }

    public void QuitGame()
    {
        // implement features for quitting

        Application.Quit();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < this.instancedSystemPrefabs.Count; i++)
        {
            Destroy(this.instancedSystemPrefabs[i]);
        }

        this.instancedSystemPrefabs.Clear();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        this.loadOperations = new List<AsyncOperation>();
        this.instancedSystemPrefabs = new List<GameObject>();

        InstantiateSystemPrefabs();

        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    private void Update()
    {
        if (this.currentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void OnLoadOperationComplete(AsyncOperation asyncOperation)
    {
        if (this.loadOperations.Contains(asyncOperation))
        {
            this.loadOperations.Remove(asyncOperation);

            if (this.loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }

        Debug.Log("Load Complete.");
    }

    private void OnUnloadOperationComplete(AsyncOperation asyncOperation)
    {
        Debug.Log("Unload Complete.");
    }

    private void UpdateState(GameState state)
    {
        if (!Enum.TryParse(state.ToString(), out GameState gameState))
        {
            Debug.LogError($"Invalid provided game state.");
        }

        GameState previousGameState = this.currentGameState;
        this.currentGameState = gameState;

        switch (this.currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(this.currentGameState, previousGameState);
    }

    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;

        for (int i = 0; i < SystemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            this.instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    private void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
        {
            UnloadLevel(this.currentLevelName);
        }
    }
}
