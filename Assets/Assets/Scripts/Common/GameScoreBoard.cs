using System.Text;
using Common.Interfaces;
using Match3.App;
using Match3.App.Interfaces;
using UnityEngine;

namespace Common
{
    public class GameScoreBoard : ISolvedSequencesConsumer<IUnityGridSlot>
    {
        private Unit[] units;
        private Enemies enemies;
        public GameScoreBoard()
        {
            units = GameObject.FindObjectsOfType<Unit>();
            enemies = GameObject.FindObjectOfType<Enemies>();
        }

        public void OnSequencesSolved(SolvedData<IUnityGridSlot> solvedData)
        {
            foreach (var sequence in solvedData.SolvedSequences)
            {
                RegisterSequenceScore(sequence);
            }
        }

        private void RegisterSequenceScore(ItemSequence<IUnityGridSlot> sequence)
        {
            Debug.Log(GetSequenceDescription(sequence));
            switch (sequence.SolvedGridSlots[0].Item.ContentId)
            {
                case 0: addDelay(sequence.SolvedGridSlots.Count); return;
                case 1: enemies.incDamage(-sequence.SolvedGridSlots.Count); return;
                case 2: addUnit(0, sequence.SolvedGridSlots.Count); return;                 
                case 4: addUnit(2, sequence.SolvedGridSlots.Count); return;                    
                case 5: addUnit(1, sequence.SolvedGridSlots.Count); return;                    
                default: return;
            }
        }

        private string GetSequenceDescription(ItemSequence<IUnityGridSlot> sequence)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("ContentId <color=yellow>");
            stringBuilder.Append(sequence.SolvedGridSlots[0].Item.ContentId);
            stringBuilder.Append("</color> | <color=yellow>");
            stringBuilder.Append(sequence.SequenceDetectorType.Name);
            stringBuilder.Append("</color> sequence of <color=yellow>");
            stringBuilder.Append(sequence.SolvedGridSlots.Count);
            stringBuilder.Append("</color> elements");

            return stringBuilder.ToString();
        }

        private void addUnit(int unit_type, int count)
        {
            for (int i = 0; i < units.Length; i++)
            {
                if (units[i].getType() == unit_type)
                {
                    units[i].addHpCount(count, unit_type);
                }
            }
        }

        private void addDelay(int delay)
        {
            enemies.addDelay(delay);
        }
    }
}