using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    public class Player
    {
        public string Name { get; private set; }
        public List<Ship> Ships { get; private set; } = new List<Ship>();  //player hand
        public Field Field { get; private set; }

        public Player(string name) 
        {
            Name = name;
            Field = new Field();
            this.AddShip(4);
            this.AddShip(3, 2);
            this.AddShip(2, 3);
            this.AddShip(1, 4);
        }

        public void AddShip(int length, int count = 1)
        { 
            for(int i = 0; i < count; i++) 
            {
                Ship ship = new Ship(length);
                Ships.Add(ship);
            }
        }


        public bool PlaceShip(int row, int column, bool rotated = false)
        {
            Ship ship = Ships.First();          
            bool check = Field.PlaceShip(ship, row, column, rotated);
            if (check) Ships.Remove(ship);
            return check;
        }

        public override string ToString() { return Name; }
    }
}
