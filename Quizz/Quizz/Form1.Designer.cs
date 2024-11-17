namespace Quizz
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblQuestion;
        private ListBox listBoxChoix;
        private Button btnValider;
        private ListBox listBoxGains;
        private Button btn5050;
        private Button btnAppelAmi;
        private Button btnDemanderPublic;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblQuestion = new System.Windows.Forms.Label();
            this.listBoxChoix = new System.Windows.Forms.ListBox();
            this.btnValider = new System.Windows.Forms.Button();
            this.listBoxGains = new System.Windows.Forms.ListBox();
            this.btn5050 = new System.Windows.Forms.Button();
            this.btnAppelAmi = new System.Windows.Forms.Button();
            this.btnDemanderPublic = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblQuestion
            this.lblQuestion.Location = new System.Drawing.Point(50, 30);
            this.lblQuestion.Size = new System.Drawing.Size(700, 50);
            this.lblQuestion.Text = "Question will appear here";
            this.lblQuestion.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);

            // listBoxChoix
            this.listBoxChoix.Location = new System.Drawing.Point(50, 100);
            this.listBoxChoix.Size = new System.Drawing.Size(700, 150);
            
            
            // btnValider
            this.btnValider.Location = new System.Drawing.Point(200, 300);
            this.btnValider.Size = new System.Drawing.Size(100, 30);
            this.btnValider.Text = "Valider";
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            this.btnValider.Enabled = false; // Initially disabled

            // listBoxGains
            this.listBoxGains.Location = new System.Drawing.Point(760, 30);
            this.listBoxGains.Size = new System.Drawing.Size(150, 400);
            this.listBoxGains.Items.AddRange(new object[]
            {
                "$ 1,000,000",
                "$ 500,000",
                "$ 250,000",
                "$ 125,000",
                "$ 64,000",
                "$ 32,000",
                "$ 16,000",
                "$ 8,000",
                "$ 4,000",
                "$ 2,000",
                "$ 1,000",
                "$ 500",
                "$ 300",
                "$ 200",
                "$ 100"
            });

            // btn5050
            this.btn5050.Location = new System.Drawing.Point(350, 300);
            this.btn5050.Size = new System.Drawing.Size(100, 30);
            this.btn5050.Text = "50/50";
            this.btn5050.Click += new System.EventHandler(this.btn5050_Click);
            this.btn5050.Enabled = true;

            // btnAppelAmi
            this.btnAppelAmi.Location = new System.Drawing.Point(470, 300);
            this.btnAppelAmi.Size = new System.Drawing.Size(100, 30);
            this.btnAppelAmi.Text = "Appel à un ami";
            this.btnAppelAmi.Click += new System.EventHandler(this.btnAppelAmi_Click);
            this.btnAppelAmi.Enabled = true;

            // btnDemanderPublic
            this.btnDemanderPublic.Location = new System.Drawing.Point(590, 300);
            this.btnDemanderPublic.Size = new System.Drawing.Size(130, 30);
            this.btnDemanderPublic.Text = "Demander au public";
            this.btnDemanderPublic.Click += new System.EventHandler(this.btnDemanderPublic_Click);
            this.btnDemanderPublic.Enabled = true;

            // Form1
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 450);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.listBoxChoix);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.listBoxGains);
            this.Controls.Add(this.btn5050);
            this.Controls.Add(this.btnAppelAmi);
            this.Controls.Add(this.btnDemanderPublic);
            this.Text = "Quizz";
            this.ResumeLayout(false);
        }
    }
}
