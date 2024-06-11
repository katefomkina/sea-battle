using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    public class Ship
    {
        public List<Cell> Cells { get; private set; }
        public int Lives { get; private set; }
        public int Length { get; private set; }

        public Ship(int length)
        {
            Length = length;
            Lives = length;
        }

        public void Place(List<Cell> cells)
        { 
            if(cells.Count != Length) { throw new Exception(); }
            Cells=cells;
        }

        public void Attack(Cell cell)
        {
            Lives--;
            if(Lives == 0) cell.GetKilled();
            else cell.GetHarmed();
        }
    }
}
