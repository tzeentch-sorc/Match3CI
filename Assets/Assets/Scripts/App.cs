using System;
using Match3.Infrastructure.Extensions;
using Match3.Infrastructure.Interfaces;
using UnityEngine;
using Common.GameModes;

public class App : MonoBehaviour
{
    [SerializeField] private AppContext _appContext;

    private int _currentModeIndex;

    private IGameMode _activeMode;
    private IGameMode[] _gameModes;

    private void Awake()
    {
        _appContext.Construct();
       _gameModes = new IGameMode[]
        {
            new DrawGameBoardMode(_appContext),
            new GameInitMode(_appContext),
            new GamePlayMode(_appContext),
            new GameResetMode(_appContext)
        };
    }

    private void Start()
    {
        ActivateGameMode(0);
    }

    private void OnEnable()
    {
        foreach (var gameMode in _gameModes)
        {
            gameMode.Finished += OnGameModeFinished;
        }
    }

    private void OnDisable()
    {
        foreach (var gameMode in _gameModes)
        {
            gameMode.Finished -= OnGameModeFinished;
        }
    }

    private void OnDestroy()
    {
        foreach (var gameMode in _gameModes)
        {
            gameMode.Dispose();
        }
    }

    private void OnGameModeFinished(object sender, EventArgs e)
    {
        _currentModeIndex++;

        if (_currentModeIndex >= _gameModes.Length)
        {
            _currentModeIndex = 0;
        }

        ActivateGameMode(_currentModeIndex);
    }

    public void ActivateGameMode(int gameModeIndex)
    {
        _activeMode?.Deactivate();
        _activeMode = _gameModes[gameModeIndex];
        _activeMode.Activate();
    }

    public IGameMode getActiveMode()
    {
        return _activeMode;
    }
}