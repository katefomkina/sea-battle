using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    public class Cell
    {
        public enum StateTypes {Attacked, NotAttacked, Killed, Harmed};

        public int Row { get; private set; }
        public int Column { get; private set; }
        public StateTypes State { get; private set; }

        public Cell(int row, int column) 
        {
            Row = row;
            Column = column;    
            State = StateTypes.NotAttacked;
        }

        public void GetAttacked() 
        {
            State = StateTypes.Attacked;
        }

        public void GetKilled()
        { 
            State = StateTypes.Killed;
        }

        public void GetHarmed() 
        {
            State = StateTypes.Harmed;
        }
    }
}
