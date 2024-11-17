namespace Morpion;

partial class Form1
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
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(350, 300);
        this.Text = "TicTacToe";
    }
    

    private TextBox textLog;
    private void InitTextLog()
    {
        textLog = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Bottom,
                Height = 50,
                Text = "Bienvenue dans le TicTacToe !\r\nTour de X"
            };
         this.Controls.Add(textLog);
    }
    
    private Button[] buttons;
    private void InitButtons()
    {
        buttons = new Button[9];
           
        int posX = 50, posY = 50;  // Starting position for buttons
           
        for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Size = new System.Drawing.Size(80, 60);  // Button size
                buttons[i].Text = "";
                buttons[i].Location = new System.Drawing.Point(posX, posY);
                buttons[i].Click += new EventHandler(OnButtonClick);  // Add click event
                this.Controls.Add(buttons[i]);
                posX += 85;  // Horizontal spacing
                if ((i + 1) % 3 == 0) 
                    {
                        posX = 50;  // Reset X position after 4 buttons
                        posY += 70; // Move to next row
                   }
            }
    }
    
    private Button restartButton;

    private void InitRestartButton()
    {
        restartButton = new Button
        {
            Text = "Restart",
            Dock = DockStyle.Top,
            Size = new System.Drawing.Size(1, 30)
        };
        restartButton.Click += new EventHandler(RestartGame);
        this.Controls.Add(restartButton);
    }

    private int round = 0;
    private void OnButtonClick(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (button.Text == "")
        {
            if (round % 2 == 0)
            {
                button.Text = "X";
            }
            else
            {
                button.Text = "O";
            }
            round++;
			CheckRound();
            CheckWin();
        }
    }

	private void CheckRound()
	{
		string tour = round % 2 == 0 ? "Tour de X" : "Tour de O";
		textLog.Clear();
		textLog.AppendText(tour);
	}

    private void CheckWin()
    {

        // check des lignes
        for (int i = 0; i < 3; i++)
        {
            if (buttons[i * 3].Text != "" && buttons[i * 3].Text == buttons[i * 3 + 1].Text && buttons[i * 3].Text == buttons[i * 3 + 2].Text)
            {
                AnnounceWinner(buttons[i * 3].Text);
				EndGame();                
				return;
            }
        }

        // check des colonnes
        for (int i = 0; i < 3; i++)
        {
            if (buttons[i].Text != "" && buttons[i].Text == buttons[i + 3].Text && buttons[i].Text == buttons[i + 6].Text)
            {
                AnnounceWinner(buttons[i].Text);
				EndGame();
                return;
            }
        }

        // check des diagonales
        if (buttons[0].Text != "" && buttons[0].Text == buttons[4].Text && buttons[0].Text == buttons[8].Text)
        {
            AnnounceWinner(buttons[0].Text);
			EndGame();
            return;
        }
        if (buttons[2].Text != "" && buttons[2].Text == buttons[4].Text && buttons[2].Text == buttons[6].Text)
        {
            AnnounceWinner(buttons[2].Text);
			EndGame();
            return;
        }
        
		// check si plein et pas de gagnant
        if (round == 9)
        {
            textLog.Clear();
            textLog.AppendText("Match nul !");
			EndGame();
        }
    }

    private void AnnounceWinner(string winner)
    {
        textLog.Clear();
        textLog.AppendText($"Le joueur {winner} a gagné !");	
    }
	
	private void EndGame()
	{
		foreach (var button in buttons)
		{
			button.Enabled = false;
		}
	}
    
    private void RestartGame(object sender, EventArgs e)
    {
        foreach (var button in buttons)
        {
            button.Text = "";
			button.Enabled = true;
        }
        round = 0;
        textLog.Clear();
        textLog.AppendText("Le jeu a été redémarré !");
    }

    #endregion
}