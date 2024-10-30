namespace Weather_app;

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
    private System.Windows.Forms.TextBox txtCity;
    private System.Windows.Forms.Button btnGetWeather;
    private System.Windows.Forms.Label lblTemperature;
    private System.Windows.Forms.Label lblHumidity;
    private System.Windows.Forms.Label lblConditions;
    private System.Windows.Forms.PictureBox GeneralIcon;
    
    private void InitializeComponent()
    {
        this.txtCity = new System.Windows.Forms.TextBox();
        this.btnGetWeather = new System.Windows.Forms.Button();
        this.lblTemperature = new System.Windows.Forms.Label();
        this.lblHumidity = new System.Windows.Forms.Label();
        this.lblConditions = new System.Windows.Forms.Label();
        this.GeneralIcon = new System.Windows.Forms.PictureBox();
        
        // txtCity
        this.txtCity.Location = new System.Drawing.Point(13, 13);
        this.txtCity.Name = "txtCity";
        this.txtCity.Size = new System.Drawing.Size(200, 22);
        
        // btnGetWeather
        this.btnGetWeather.Location = new System.Drawing.Point(13, 50);
        this.btnGetWeather.Name = "btnGetWeather";
        this.btnGetWeather.Size = new System.Drawing.Size(200, 23);
        this.btnGetWeather.Text = "Get Weather";
        this.btnGetWeather.Click += new System.EventHandler(this.btnGetWeather_Click);
        
        // lblTemperature
        this.lblTemperature.Location = new System.Drawing.Point(13, 90);
        this.lblTemperature.Name = "lblTemperature";
        this.lblTemperature.Size = new System.Drawing.Size(100, 23);
        
        // lblHumidity
        this.lblHumidity.Location = new System.Drawing.Point(13, 120);
        this.lblHumidity.Name = "lblHumidity";
        this.lblHumidity.Size = new System.Drawing.Size(100, 23);
        
        // lblConditions
        this.lblConditions.Location = new System.Drawing.Point(13, 150);
        this.lblConditions.Name = "lblConditions";
        this.lblConditions.Size = new System.Drawing.Size(200, 23);
        
        // pictureBoxIcon
        this.GeneralIcon.Location = new System.Drawing.Point(13, 180);
        this.GeneralIcon.Name = "pictureBoxIcon";
        this.GeneralIcon.Size = new System.Drawing.Size(100, 100);
        this.GeneralIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

        // Add controls to the form
        this.Controls.Add(this.txtCity);
        this.Controls.Add(this.btnGetWeather);
        this.Controls.Add(this.lblTemperature);
        this.Controls.Add(this.lblHumidity);
        this.Controls.Add(this.lblConditions);
        this.Controls.Add(this.GeneralIcon);

        this.Text = "Weather App";
    }

    #endregion
}