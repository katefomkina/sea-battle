namespace Sea_Battle
{
    public partial class FormRegistration : Form
    {

        private Game game;

        public FormRegistration(Game game)
        {
            this.game = game;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string playerName = textBoxPlayerName.Text;
            if (playerName.Length > 0) 
            {
                game.CreatePlayer(playerName);
                listBoxPlayers.Items.Add(playerName);
                textBoxPlayerName.Text = string.Empty;
                textBoxPlayerName.Focus();
                if (game.PlayerList.Count == 2)
                {
                    buttonAdd.Visible = false;
                    buttonStart.Visible = true;
                }
            } 
            else 
            {
                MessageBox.Show("¬ведите им€ игрока");
            }

        }

        private void buttonStart_Click(object sender, EventArgs e)
        { 
            this.Hide();
            game.StartGame();          
            this.Close();
        }
    }
}
