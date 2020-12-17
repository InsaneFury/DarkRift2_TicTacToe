using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Scripts.Networking
{
    public class BoardChangeEvent : UnityEvent<ushort, MatchModel.SlateStatus> { };

    public class MatchModel
    {
        public enum SlateStatus
        {
            NONE,
            MINE,
            HIS
        }

        public BoardChangeEvent OnBoardChange;
        public SlateStatus[] slates;

        public readonly ushort id;
        public static MatchModel currentMatch;
        public MatchModel(ushort _id)
        {
            id = _id;
            slates = new SlateStatus[9];
            OnBoardChange = new BoardChangeEvent();
        }       

        public void ReportSlateTaken(ushort slateIndex)
        {
            NetworkingManager.Instance.MessageSlateTaken(slateIndex, id);
        }
        public void ServerReportSlateTaken(ushort slateIndex, bool mine)
        {
            if(slateIndex >= slates.Length) return;

            if (slates[slateIndex] == SlateStatus.NONE)
            {
                slates[slateIndex] = mine ? SlateStatus.MINE : SlateStatus.HIS;
                OnBoardChange?.Invoke(slateIndex,slates[slateIndex]);
            }
            else
            {
                throw new Exception("trying to override a taken slate");
            }
                
        }
    }
}
