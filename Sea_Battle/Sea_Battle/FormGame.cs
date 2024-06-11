using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle
{
    public partial class FormGame : Form
    {
        private Button[,] playerButtons;
        private Button[,] enemyButtons;

        private bool rotated = false;
        private Game game;

        public FormGame(Game game)
        {
            this.game = game;
            InitializeComponent();
            PlaceButtons();
        }

        private void ShowPlayer()
        {
            labelPlayer.Text = game.CurrentPlayer.Name;
        }

        private void ShowStage()
        {
            labelStage.Text = game.Mode==Game.GameMode.Place ? "Расстановка" : "Атака";
        }

        private void ShowRotated()
        {
            labelRotated.Text = rotated ? "Вертикально" : "Горизонтально";
        }

        private void PaintKilledOwnShips(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                playerButtons[cell.Row, cell.Column].BackColor = Color.Black;
            }
        }

        private void PaintKilledEnemyShips(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                enemyButtons[cell.Row, cell.Column].BackColor = Color.Black;
            }
        }

        private void DisableButton(Button button, System.EventHandler fun)
        {
            button.Click -= fun;
            button.Cursor = Cursors.Default;
            button.FlatAppearance.BorderSize = 0;
            button.TabStop = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.MouseOverBackColor = button.BackColor;
            button.FlatAppearance.MouseDownBackColor = button.BackColor;
        }

        private void EndPlacing()
        {
            groupBoxPlacement.Visible = false;
            foreach (Button button in playerButtons)
            {
                DisableButton(button, PlacingButton_Click);
            }
            foreach (Button button in enemyButtons)
            {
                button.Enabled = true;
            }
        }

        private void GetCurrentShip()
        {
            Ship ship = game.CurrentPlayer.Ships.First();
            int length = ship.Length;
            labelShipType.Text = $"{length}-х палубный";
        }

        private void PlaceButtons()
        {
            playerButtons = new Button[10, 10];
            enemyButtons = new Button[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    playerButtons[i, j] = new Button
                    {
                        Width = 44,
                        Height = 44,
                        Dock = DockStyle.Fill,
                        BackColor = Color.LightBlue,
                        ForeColor = Color.Red,
                        Enabled = game.Mode == Game.GameMode.Place ? true : false,
                        Tag = new Point(i, j),
                    };
                    playerButtons[i, j].Click += new EventHandler(PlacingButton_Click);
                    playerGrid.Controls.Add(playerButtons[i, j], j, i);

                    enemyButtons[i, j] = new Button
                    {
                        Width = 44,
                        Height = 44,
                        Dock = DockStyle.Fill,
                        BackColor = Color.LightGray,
                        ForeColor = Color.Red,
                        Enabled = game.Mode == Game.GameMode.Attack ? true : false,
                        Tag = new Point(i, j),
                    };
                    enemyButtons[i, j].Click += new EventHandler(AttackingButton_Click);
                    enemyGrid.Controls.Add(enemyButtons[i, j], j, i);
                }
            }
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            if (game.Mode == Game.GameMode.Place)
            {
                GetCurrentShip();
                ShowRotated();
            }
            ShowPlayer();
            ShowStage();
        }

        private void PlacingButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Point coordinates = (Point)clickedButton.Tag;
            if (game.PlaceShip(coordinates.X, coordinates.Y, rotated))
            {
                List<Cell> cells = game.GetLastShipCells();
                foreach (Cell cell in cells)
                {
                    Button button = playerButtons[cell.Row, cell.Column];
                    button.BackColor = Color.DarkBlue;
                }

                if (game.EndPlacement())
                {
                    EndPlacing();
                    this.Hide();
                }
                else
                {
                    GetCurrentShip();
                }
            }
        }


        private void AttackingButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Point coordinates = (Point)clickedButton.Tag;
            Cell.StateTypes type = game.Attack(coordinates.X, coordinates.Y);
            DisableButton(clickedButton, AttackingButton_Click);
            if (type == Cell.StateTypes.Attacked)
            {
                this.Hide();
            }
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            rotated = !rotated;
            ShowRotated();
        }

        public void GetAttacked(int row, int column)
        {
            Button button = playerButtons[row, column];
            button.Text = "●";
        }

        public void GetHarmed(int row, int column)
        {
            Button button = playerButtons[row, column];
            button.Text = "X";
        }
        public void GetKilled(int row, int column)
        {
            PaintKilledOwnShips(game.GetKilledShipsCellsList(game.GetNextPlayerFormPairWithoutChangingOrder().Item1));
            Button button = playerButtons[row, column];
            button.Text = "X";
        }

        public void SetAttacked(int row, int column)
        {
            Button button = enemyButtons[row, column];
            button.Text = "●";
        }

        public void SetHarmed(int row, int column)
        {
            Button button = enemyButtons[row, column];
            button.Text = "X";
        }
        public void SetKilled(int row, int column)
        {
            PaintKilledEnemyShips(game.GetKilledShipsCellsList(game.GetNextPlayerFormPairWithoutChangingOrder().Item1));
            Button button = enemyButtons[row, column];
            button.Text = "X";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            game.EndGame();
            this.Close();
        }
    }
}
