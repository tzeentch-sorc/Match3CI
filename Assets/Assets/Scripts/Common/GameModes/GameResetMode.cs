using System;
using Common.Extensions;
using Common.Interfaces;
using Match3.Infrastructure.Interfaces;
using UnityEngine;
using System.Collections.Generic;


namespace Common.GameModes
{
    public class GameResetMode : IGameMode
    {
        private readonly UnityGame _unityGame;
        private readonly IItemsPool<IUnityItem> _itemsPool;
        private readonly IUnityGameBoardRenderer _gameBoardRenderer;

        public GameResetMode(IAppContext appContext)
        {
            _unityGame = appContext.Resolve<UnityGame>();
            _itemsPool = appContext.Resolve<IItemsPool<IUnityItem>>();
            _gameBoardRenderer = appContext.Resolve<IUnityGameBoardRenderer>();
        }

        public event EventHandler Finished;

        public void Activate()
        {
            _itemsPool.ReturnAllItems(_unityGame.GetGridSlots());
            _gameBoardRenderer.ResetGridTiles();
            _unityGame.ResetGameBoard();
            foreach (Unit u in GameObject.FindObjectsOfType<Unit>()) {
                u.hpCount = 10;
            }
            Enemies e = GameObject.FindObjectOfType<Enemies>();
            e.damage = 2;
            e.delay = 7;
            Finished?.Invoke(this, EventArgs.Empty);
        }
    }
}