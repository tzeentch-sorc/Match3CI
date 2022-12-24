using Common;
using Common.Interfaces;
using Match3.App;
using Match3.App.Interfaces;
using Match3.Infrastructure.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : LevelGoal<IUnityGridSlot>
{
    private readonly int _contentId;
    private readonly int _itemsCount;
    private IGameBoard<IUnityGridSlot> _gameBoard;
    private GameUiCanvas _gameUiCanvas;

    private int _collectedItemsCount;

    public CollectItems(int contentId, int itemsCount, IGameBoard<IUnityGridSlot> gameBoard, GameUiCanvas gameUiCanvas)
    {
        _contentId = contentId;
        _itemsCount = itemsCount;
        _gameBoard = gameBoard;
        _gameUiCanvas = (GameUiCanvas) gameUiCanvas;
        _gameUiCanvas.setGoalCount(_itemsCount);
    }

    public override void OnSequencesSolved(SolvedData<IUnityGridSlot> solvedData)
    {
        // Get unique and only movable items.
        foreach (var solvedGridSlot in solvedData.GetUniqueSolvedGridSlots(true))
        {
            if (solvedGridSlot.Item.ContentId == _contentId)
            {
                _collectedItemsCount++;
                _gameUiCanvas.setGoalCount(Mathf.Clamp(_itemsCount - _collectedItemsCount, 0, _itemsCount));
            }
        }

        if (_collectedItemsCount >= _itemsCount)
        {
            MarkAchieved();
            _gameUiCanvas.setWinText();
        }
    }
}
