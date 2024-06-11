using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    public class Field
    {
        public Cell[,] Cells { get; private set; } = new Cell[10, 10];

        public List<Ship> Ships { get; private set; } = new List<Ship>();

        public Field()
        { 
            for (int row = 0; row < 10; row++) 
            {
                for(int col = 0; col < 10; col++) 
                {
                    Cells[row, col] = new Cell(row, col);
                }
            }
        }

        private bool AvailableToPlace(Ship ship, List<Cell> cells, bool rotated)
        {
            foreach (Cell cell in cells)
            {
                foreach (Ship existingShip in Ships)
                {
                    foreach (Cell existingCell in existingShip.Cells)
                    {
                        int dx = Math.Abs(cell.Row - existingCell.Row);
                        int dy = Math.Abs(cell.Column - existingCell.Column);
                        if (dx <= 1 && dy <= 1)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool PlaceShip(Ship ship, int row, int column, bool rotated)
        {
            List<Cell> cells = new List<Cell>();

            for (int i = 0; i < ship.Length; i++)
            {
                if (rotated)
                {
                    if (row + i >= Cells.GetLength(0))
                    {
                        MessageBox.Show("Невозможно разместить корабль таким образом: выходит за границы поля");
                        return false;
                    }
                    cells.Add(Cells[row + i, column]);
                }
                else
                {
                    if (column + i >= Cells.GetLength(1))
                    {
                        MessageBox.Show("Невозможно разместить корабль таким образом: выходит за границы поля");
                        return false;
                    }
                    cells.Add(Cells[row, column + i]);
                }
            }

            if (!AvailableToPlace(ship, cells, rotated))
            {
                MessageBox.Show("Невозможно разместить корабль таким образом: конфликт с другими кораблями");
                return false;
            }

            ship.Place(cells);
            Ships.Add(ship);
            
            return true;
        }

        public Cell.StateTypes getAttacked(int row, int column) 
        {
            Cell cell = Cells[row, column];
            foreach(Ship ship in Ships) 
            {
                if (ship.Cells.Contains(cell))
                { 
                    ship.Attack(cell);
                    return cell.State;
                }
            }
            cell.GetAttacked();

            return Cell.StateTypes.Attacked;
        }
    }
}

