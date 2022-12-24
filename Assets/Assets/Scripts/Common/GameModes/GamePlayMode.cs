using System;
using Common.Interfaces;
using Cysharp.Threading.Tasks;
using Match3.App;
using Match3.App.Interfaces;
using UnityEngine;
using Match3.Infrastructure.Interfaces;

namespace Common.GameModes
{
    public class GamePlayMode : IGameMode, IDeactivatable
    {
        private readonly UnityGame _unityGame;
        private readonly IGameUiCanvas _gameUiCanvas;
        private readonly IBoardFillStrategy<IUnityGridSlot> _boardFillStrategy;

        public GamePlayMode(IAppContext appContext)
        {
            _unityGame = appContext.Resolve<UnityGame>();
            _gameUiCanvas = appContext.Resolve<IGameUiCanvas>();
            _boardFillStrategy = appContext.Resolve<IBoardFillStrategy<IUnityGridSlot>>();
        }

        public event EventHandler Finished
        {
            add => _unityGame.Finished += value;
            remove => _unityGame.Finished -= value;
        }

        public void Activate()
        {
            UnityGameBoardRenderer _board = (UnityGameBoardRenderer)UnityEngine.Object.FindObjectOfType<AppContext>().Resolve<IUnityGameBoardRenderer>();
                        for (int i = 0; i < UnityEngine.Random.Range(5, 11); i++) {
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0: _board.SetTile(UnityEngine.Random.Range(3, 7), UnityEngine.Random.Range(3, 7), Enums.TileGroup.Ice); break;
                    case 1: _board.SetTile(UnityEngine.Random.Range(3, 7), UnityEngine.Random.Range(3, 7), Enums.TileGroup.Stone); break;
                    case 2: _board.SetTile(UnityEngine.Random.Range(3, 7), UnityEngine.Random.Range(3, 7), Enums.TileGroup.Unavailable); break;
                }
            }
            _unityGame.LevelGoalAchieved += OnLevelGoalAchieved;

            _unityGame.SetGameBoardFillStrategy(GetSelectedFillStrategy());
            _unityGame.StartAsync().Forget();

            _gameUiCanvas.ShowMessage("Game started.");

        }


        public void Deactivate()
        {
            _unityGame.LevelGoalAchieved -= OnLevelGoalAchieved;

            _unityGame.StopAsync().Forget();
            _gameUiCanvas.ShowMessage("Game finished.");

        }

        private void OnLevelGoalAchieved(object sender, LevelGoal<IUnityGridSlot> levelGoal)
        {
            _gameUiCanvas.RegisterAchievedGoal(levelGoal);
        }

        private IBoardFillStrategy<IUnityGridSlot> GetSelectedFillStrategy()
        {
            return GetFillStrategy();
        }

        private IBoardFillStrategy<IUnityGridSlot> GetFillStrategy()
        {
            return _boardFillStrategy;
        }
    }
}