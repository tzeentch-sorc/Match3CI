using System;
using System.Linq;
using Common.Interfaces;
using Common.UiElements;
using Match3.App;
using Match3.App.Interfaces;
using TMPro;
using UnityEngine;

namespace Common
{
    public class GameUiCanvas : MonoBehaviour, IGameUiCanvas
    {
        [SerializeField] private AppContext _appContext;
        [SerializeField] private InteractableButton _startGameButton;
        [SerializeField] private TextMeshProUGUI _text;

        private CollectItems _goal;

        public int SelectedFillStrategyIndex => 0;

        public event EventHandler StartGameClick;

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            _startGameButton.Click += OnStartGameButtonClick;
        }

        private void OnDisable()
        {
            _startGameButton.Click -= OnStartGameButtonClick;
        }

        public void ShowMessage(string message)
        {
            Debug.Log(message);
        }

        public void RegisterAchievedGoal(LevelGoal<IUnityGridSlot> achievedGoal)
        {
            ShowMessage($"The goal {achievedGoal.GetType().Name} achieved.");
        }

        private void OnStartGameButtonClick()
        {
            StartGameClick?.Invoke(this, EventArgs.Empty);
        }

        public void setGoalCount(int remain)
        {
            _text.text = "Gold to collect: " + remain;
        }

        public void setWinText()
        {
            _text.text = "Game won!";
        }

        public void setLoseText()
        {
            _text.text = "Game lost!";
        }
    }
}