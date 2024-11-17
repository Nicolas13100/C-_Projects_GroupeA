namespace Weather_app
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Button btnGetWeather;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.Label lblHumidity;
        private System.Windows.Forms.Label lblConditions;
        private System.Windows.Forms.PictureBox GeneralIcon;
        private System.Windows.Forms.GroupBox weatherGroupBox;

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

        private void InitializeComponent()
        {
            this.txtCity = new System.Windows.Forms.TextBox();
            this.btnGetWeather = new System.Windows.Forms.Button();
            this.lblTemperature = new System.Windows.Forms.Label();
            this.lblHumidity = new System.Windows.Forms.Label();
            this.lblConditions = new System.Windows.Forms.Label();
            this.GeneralIcon = new System.Windows.Forms.PictureBox();
            this.weatherGroupBox = new System.Windows.Forms.GroupBox();

            // WeatherGroupBox
            this.weatherGroupBox.Controls.Add(this.lblTemperature);
            this.weatherGroupBox.Controls.Add(this.lblHumidity);
            this.weatherGroupBox.Controls.Add(this.lblConditions);
            this.weatherGroupBox.Controls.Add(this.GeneralIcon);
            this.weatherGroupBox.Location = new System.Drawing.Point(12, 90);
            this.weatherGroupBox.Name = "weatherGroupBox";
            this.weatherGroupBox.Size = new System.Drawing.Size(260, 220);
            this.weatherGroupBox.TabIndex = 5;
            this.weatherGroupBox.TabStop = false;
            this.weatherGroupBox.Text = "Weather Details";

            // txtCity
            this.txtCity.Location = new System.Drawing.Point(12, 12);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(260, 22);
            this.txtCity.TabIndex = 0;
            this.txtCity.PlaceholderText = "Enter city name...";

            // btnGetWeather
            this.btnGetWeather.Location = new System.Drawing.Point(12, 50);
            this.btnGetWeather.Name = "btnGetWeather";
            this.btnGetWeather.Size = new System.Drawing.Size(260, 30);
            this.btnGetWeather.TabIndex = 1;
            this.btnGetWeather.Text = "Get Weather";
            this.btnGetWeather.UseVisualStyleBackColor = true;
            this.btnGetWeather.Click += new System.EventHandler(this.btnGetWeather_Click);

            // lblTemperature
            this.lblTemperature.Location = new System.Drawing.Point(20, 30);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new System.Drawing.Size(220, 23);
            this.lblTemperature.TabIndex = 2;
            this.lblTemperature.Text = "Temperature: --";

            // lblHumidity
            this.lblHumidity.Location = new System.Drawing.Point(20, 60);
            this.lblHumidity.Name = "lblHumidity";
            this.lblHumidity.Size = new System.Drawing.Size(220, 23);
            this.lblHumidity.TabIndex = 3;
            this.lblHumidity.Text = "Humidity: --";

            // lblConditions
            this.lblConditions.Location = new System.Drawing.Point(20, 90);
            this.lblConditions.Name = "lblConditions";
            this.lblConditions.Size = new System.Drawing.Size(220, 23);
            this.lblConditions.TabIndex = 4;
            this.lblConditions.Text = "Conditions: --";

            // GeneralIcon
            this.GeneralIcon.Location = new System.Drawing.Point(80, 130);
            this.GeneralIcon.Name = "GeneralIcon";
            this.GeneralIcon.Size = new System.Drawing.Size(100, 100);
            this.GeneralIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GeneralIcon.TabIndex = 5;
			this.GeneralIcon.BackColor = System.Drawing.Color.Black;

            // MainForm
            this.ClientSize = new System.Drawing.Size(284, 331);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.btnGetWeather);
            this.Controls.Add(this.weatherGroupBox);
            this.Name = "Form1";
            this.Text = "Weather App";

            ((System.ComponentModel.ISupportInitialize)(this.GeneralIcon)).EndInit();
        }

        #endregion
    }
}
