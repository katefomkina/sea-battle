namespace Sea_Battle
{
    partial class FormRegistration
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonAdd = new Button();
            textBoxPlayerName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            listBoxPlayers = new ListBox();
            buttonStart = new Button();
            SuspendLayout();
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(592, 312);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(109, 29);
            buttonAdd.TabIndex = 0;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // textBoxPlayerName
            // 
            textBoxPlayerName.Location = new Point(71, 179);
            textBoxPlayerName.Name = "textBoxPlayerName";
            textBoxPlayerName.Size = new Size(460, 27);
            textBoxPlayerName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(71, 140);
            label1.Name = "label1";
            label1.Size = new Size(97, 20);
            label1.TabIndex = 2;
            label1.Text = "Введите имя";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(592, 38);
            label2.Name = "label2";
            label2.Size = new Size(177, 20);
            label2.TabIndex = 3;
            label2.Text = "Игроки (необходимо 2):";
            // 
            // listBoxPlayers
            // 
            listBoxPlayers.FormattingEnabled = true;
            listBoxPlayers.Location = new Point(592, 102);
            listBoxPlayers.Name = "listBoxPlayers";
            listBoxPlayers.Size = new Size(150, 104);
            listBoxPlayers.TabIndex = 4;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(592, 367);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(109, 29);
            buttonStart.TabIndex = 5;
            buttonStart.Text = "Начать игру";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Visible = false;
            buttonStart.Click += buttonStart_Click;
            // 
            // FormRegistration
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonStart);
            Controls.Add(listBoxPlayers);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxPlayerName);
            Controls.Add(buttonAdd);
            Name = "FormRegistration";
            Text = "Регистрация";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAdd;
        private TextBox textBoxPlayerName;
        private Label label1;
        private Label label2;
        private ListBox listBoxPlayers;
        private Button buttonStart;
    }
}
