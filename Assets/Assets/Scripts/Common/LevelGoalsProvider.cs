using Common.Interfaces;
using Common.LevelGoals;
using Match3.App;
using Match3.App.Interfaces;

namespace Common
{
    public class LevelGoalsProvider : ILevelGoalsProvider<IUnityGridSlot>
    {

        private GameUiCanvas ui;
        public LevelGoalsProvider(GameUiCanvas ui) { this.ui = ui; } 


        public LevelGoal<IUnityGridSlot>[] GetLevelGoals(int level, IGameBoard<IUnityGridSlot> gameBoard)
        {
            return new LevelGoal<IUnityGridSlot>[] {
                new CollectItems(3, 30, gameBoard, ui)
            };
        }
    }
}