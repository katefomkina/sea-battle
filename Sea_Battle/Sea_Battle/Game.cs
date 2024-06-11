using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle
{
    public class Game
    {
        private class PlayerCycle
        {
            private List<Tuple<Player, FormGame>> _playerFormPairs;
            private int _currentIndex;
 
            public PlayerCycle(List<Player> players, List<FormGame> forms)
            {
                _currentIndex = 0;
                _playerFormPairs = new List<Tuple<Player, FormGame>>();
                for (int i = 0; i < players.Count; i++)
                {
                    _playerFormPairs.Add(Tuple.Create(players[i], forms[i]));
                }
            }

            public Tuple<Player, FormGame> GetNextPlayerFormPair()
            {
                Tuple<Player, FormGame> nextPair = _playerFormPairs[_currentIndex];
                _currentIndex = (_currentIndex + 1) % _playerFormPairs.Count;
                return nextPair;
            }

            public Tuple<Player, FormGame> PeekNextPlayerFormPair()
            {
                return _playerFormPairs[_currentIndex];
            }
        }

        public enum GameMode { Place, Attack }

        public List<Player> PlayerList = new List<Player>();
        private PlayerCycle _playerCycle;
        private bool _isGameContinue = false;
        public GameMode Mode { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public FormGame CurrentForm { get; private set; }

        private FormGame formGamePlayer1;
        private FormGame formGamePlayer2;

        private Form _formregister;

        public Game()
        {
            _formregister = new FormRegistration(this);
            Application.Run(_formregister);
        }

        public void StartGame()
        {
            formGamePlayer1 = new FormGame(this);
            formGamePlayer2 = new FormGame(this);
            _playerCycle = new PlayerCycle(PlayerList, new List<FormGame> { formGamePlayer1, formGamePlayer2 });
            _isGameContinue = true;
            StartPlacing();
            StartAttacking();
        }

        public void StartPlacing()
        {
            Mode = GameMode.Place;
            Move();
            Move();
        }

        private void StartAttacking()
        {
            Mode = GameMode.Attack;
            
            while (_isGameContinue)
            {
                Move();
            }
        }

        public void Move()
        {
            if (!_isGameContinue)
            {
                return;
            }
            Tuple<Player, FormGame> nextPair = _playerCycle.GetNextPlayerFormPair();
            CurrentPlayer = nextPair.Item1;
            CurrentForm = nextPair.Item2;
            MessageBox.Show($"Ход игрока {CurrentPlayer.Name}");
            if (!CurrentForm.Created)
            {
                CurrentForm.ShowDialog();
            }
            else
            {
                CurrentForm.Show();
            }
        }

        public Tuple<Player, FormGame> GetNextPlayerFormPairWithoutChangingOrder()
        {
            return _playerCycle.PeekNextPlayerFormPair();
        }

        public void CreatePlayer(string name)
        {
            Player player = new Player(name);
            PlayerList.Add(player);
        }

        public bool EndPlacement()
        {
            return CurrentPlayer.Ships.Count == 0;
        }

        public List<Cell> GetLastShipCells()
        {
            return CurrentPlayer.Field.Ships.Last().Cells;
        }

        public bool PlaceShip(int row, int column, bool rotated = false)
        {
            return CurrentPlayer.PlaceShip(row, column, rotated);
        }

        public Cell.StateTypes Attack(int row, int column)
        {
            Tuple<Player, FormGame> nextPair = GetNextPlayerFormPairWithoutChangingOrder();
            Player nextPlayer = nextPair.Item1;
            FormGame nextForm = nextPair.Item2;

            Cell.StateTypes type = nextPlayer.Field.getAttacked(row, column);
            switch (type)
            {
                case Cell.StateTypes.Attacked:
                    CurrentForm.SetAttacked(row, column);
                    nextForm.GetAttacked(row, column);
                    MessageBox.Show("Мимо!");
                    break;
                case Cell.StateTypes.Harmed:
                    CurrentForm.SetHarmed(row, column);
                    nextForm.GetHarmed(row, column);
                    MessageBox.Show("Ранил!");
                    break;
                case Cell.StateTypes.Killed:
                    CheckEnd(nextPlayer);
                    CurrentForm.SetKilled(row, column);
                    nextForm.GetKilled(row, column);
                    MessageBox.Show("Убил!");
                    break;
            }
            return type;
        }

        public void CheckEnd(Player player) 
        {
            if (player.Field.Ships.Count == GetKilledShipsList(player).Count)
            {
                MessageBox.Show("Победа игрока " + CurrentPlayer + "!");
                EndGame();
            }
        }

        public List<Ship> GetKilledShipsList(Player player)
        {
            List<Ship> ships = player.Field.Ships;
            List<Ship> result = new List<Ship>();
            foreach (Ship ship in ships)
            {
                if (ship.Lives == 0)
                {
                    result.Add(ship);
                }
            }
            return result;
        }

        public List<Cell> GetKilledShipsCellsList(Player player) 
        {
            List<Ship> ships = player.Field.Ships;
            List<Cell> result = new List<Cell>();
            foreach (Ship ship in ships) 
            {
                if (ship.Lives == 0)
                {
                    result.AddRange(ship.Cells);
                }
            }
            return result;       
        }

        public void EndGame() 
        {
            _isGameContinue = false;
            _formregister.Close();
        }
    }
}
