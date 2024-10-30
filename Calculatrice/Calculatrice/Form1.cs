using System;
using System.Globalization;
using System.Windows.Forms;

namespace Calculatrice
{
    public class Form1 : Form
    {
        private TextBox textBoxDisplay;
        private Button[] buttons;
        private string input = string.Empty;
        private string operand1 = string.Empty;
        private string operand2 = string.Empty;
        private char operation;
        private decimal result = 0.0m;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // Enable key preview to capture key events
            this.KeyDown += new KeyEventHandler(OnKeyDown); // Handle key down event
        }

        private void InitializeComponent()
        {
            this.Text = "Calculator";
            this.Size = new System.Drawing.Size(375, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            textBoxDisplay = new TextBox();
            textBoxDisplay.Size = new System.Drawing.Size(340, 50);
            textBoxDisplay.Location = new System.Drawing.Point(10, 10);
            textBoxDisplay.ReadOnly = true;
            textBoxDisplay.TextAlign = HorizontalAlignment.Right;
            this.Controls.Add(textBoxDisplay);

            buttons = new Button[24];
            string[] buttonLabels = { "%", "CE", "C", "DEL",
                                       "1/x", "x²", "√", "/",
                                       "7", "8", "9", "*",
                                       "4", "5", "6", "-",
                                       "1", "2", "3", "+",
                                       "±", "0", ".", "=" };

            int posX = 10, posY = 60;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Size = new System.Drawing.Size(80, 60);
                buttons[i].Text = buttonLabels[i];
                buttons[i].Location = new System.Drawing.Point(posX, posY);
                buttons[i].Click += new EventHandler(OnButtonClick);

                this.Controls.Add(buttons[i]);

                posX += 85;

                if ((i + 1) % 4 == 0)
                {
                    posX = 10;
                    posY += 70;
                }
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            HandleButtonInput(button.Text);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.NumPad0:
                    HandleButtonInput("0");
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    HandleButtonInput("1");
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    HandleButtonInput("2");
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    HandleButtonInput("3");
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    HandleButtonInput("4");
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    HandleButtonInput("5");
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    HandleButtonInput("6");
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    HandleButtonInput("7");
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    HandleButtonInput("8");
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    HandleButtonInput("9");
                    break;
                case Keys.Add:
                    HandleButtonInput("+");
                    break;
                case Keys.Subtract:
                    HandleButtonInput("-");
                    break;
                case Keys.Multiply:
                    HandleButtonInput("*");
                    break;
                case Keys.Divide:
                    HandleButtonInput("/");
                    break;
                case Keys.Decimal:
                    HandleButtonInput(".");
                    break;
                case Keys.Enter:
                    HandleButtonInput("=");
                    break;
                case Keys.Back:
                    HandleButtonInput("DEL");
                    break;
                case Keys.Escape:
                    HandleButtonInput("C");
                    break;
                default:
                    break;
            }
        }

        private void HandleButtonInput(string buttonText)
        {
            // Handle Clear button
            if (buttonText == "C" || buttonText == "CE")
            {
                textBoxDisplay.Clear();
                input = operand1 = operand2 = string.Empty;
                return;
            }

            // Handle ± button to toggle the sign of the current input
            if (buttonText == "±")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    if (input.StartsWith("-"))
                        input = input.Substring(1);
                    else
                        input = "-" + input;
                }
                else
                {
                    input = "-";
                }
                textBoxDisplay.Text = input;
                return;
            }

            // Handle DEL button - delete the last character from input
            if (buttonText == "DEL")
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    textBoxDisplay.Text = input;
                }
                return;
            }

            // Handle digit buttons
            if (buttonText.Length == 1 && char.IsDigit(buttonText[0]))
            {
                input += buttonText;
                textBoxDisplay.Text = input;
            }
            // Handle decimal point button
            else if (buttonText == ".")
            {
                if (!input.Contains("."))
                {
                    input += ".";
                    textBoxDisplay.Text = input;
                }
                return;
            }

            // Handle operator buttons
            else if ("+-*/".Contains(buttonText))
            {
                if (!string.IsNullOrEmpty(input))
                {
                    operand1 = input;
                    operation = buttonText[0];
                    input = string.Empty;
                }
                else if (buttonText == "-")
                {
                    input = "-";
                }
                return;
            }

            // Handle equals button
            else if (buttonText == "=")
            {
                if (!string.IsNullOrEmpty(operand1) && !string.IsNullOrEmpty(input))
                {
                    operand2 = input;

                    try
                    {
                        decimal num1 = decimal.Parse(operand1, CultureInfo.InvariantCulture);
                        decimal num2 = decimal.Parse(operand2, CultureInfo.InvariantCulture);

                        switch (operation)
                        {
                            case '+':
                                result = num1 + num2;
                                break;
                            case '-':
                                result = num1 - num2;
                                break;
                            case '*':
                                result = num1 * num2;
                                break;
                            case '/':
                                if (num2 != 0)
                                    result = num1 / num2;
                                else
                                    MessageBox.Show("Cannot divide by zero!");
                                break;
                        }

                        textBoxDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                        input = result.ToString(CultureInfo.InvariantCulture);
                        operand1 = operand2 = string.Empty;
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Invalid input format. Please check your numbers.");
                        input = string.Empty;
                    }
                }
                return;
            }

            // Handle percentage button
            else if (buttonText == "%")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    decimal number = decimal.Parse(input, CultureInfo.InvariantCulture);
                    result = number / 100;
                    textBoxDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                    input = result.ToString(CultureInfo.InvariantCulture);
                }
                return;
            }

            // Handle 1/x button
            else if (buttonText == "1/x")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    decimal number = decimal.Parse(input, CultureInfo.InvariantCulture);
                    if (number != 0)
                    {
                        result = 1 / number;
                        textBoxDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                        input = result.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        MessageBox.Show("Cannot calculate the reciprocal of zero!");
                    }
                }
                return;
            }

            // Handle x² button
            else if (buttonText == "x²")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    decimal number = decimal.Parse(input, CultureInfo.InvariantCulture);
                    result = number * number;
                    textBoxDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                    input = result.ToString(CultureInfo.InvariantCulture);
                }
                return;
            }

            // Handle √ button
            else if (buttonText == "√")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    decimal number = decimal.Parse(input, CultureInfo.InvariantCulture);
                    if (number >= 0)
                    {
                        result = (decimal)Math.Sqrt((double)number);
                        textBoxDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                        input = result.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        MessageBox.Show("Cannot calculate the square root of a negative number!");
                    }
                }
                return;
            }
        }
    }
}
