using System;
using System.Globalization;
using System.Windows.Forms;

namespace Calculatrice
{
    public class Form1 : Form
    {
        // Components
        private TextBox textBoxDisplay;
        private Button[] buttons;
        private string input = string.Empty;  // User input
        private string operand1 = string.Empty; // First number
        private string operand2 = string.Empty; // Second number
        private char operation;  // Operator (+, -, *, /)
        private decimal result = 0.0m;  // Result of calculation

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
{
    // Initialize form properties
    this.Text = "Calculator";
    this.Size = new System.Drawing.Size(375, 520);  // Width, Height

    // Set the FormBorderStyle to FixedDialog to prevent resizing
    this.FormBorderStyle = FormBorderStyle.FixedDialog; 
    this.MaximizeBox = false; // Disable the maximize button

    // Create TextBox for display
    textBoxDisplay = new TextBox();
    textBoxDisplay.Size = new System.Drawing.Size(340, 50);  // Width, Height
    textBoxDisplay.Location = new System.Drawing.Point(10, 10);  // X, Y
    textBoxDisplay.ReadOnly = true;
    textBoxDisplay.TextAlign = HorizontalAlignment.Right;
    this.Controls.Add(textBoxDisplay);

    // Create buttons (digits and operators)
    buttons = new Button[24];
    string[] buttonLabels = { "%", "CE", "C", "DEL",
                               "1/x", "x²", "√", "/",
                               "7", "8", "9", "*",
                               "4", "5", "6", "-",
                               "1", "2", "3", "+",
                               "±", "0", ".", "=" };

    int posX = 10, posY = 60;  // Starting position for buttons

    for (int i = 0; i < buttons.Length; i++)
    {
        buttons[i] = new Button();
        buttons[i].Size = new System.Drawing.Size(80, 60);  // Button size
        buttons[i].Text = buttonLabels[i];
        buttons[i].Location = new System.Drawing.Point(posX, posY);
        buttons[i].Click += new EventHandler(OnButtonClick);  // Add click event

        this.Controls.Add(buttons[i]);

        posX += 85;  // Horizontal spacing

        if ((i + 1) % 4 == 0)
        {
            posX = 10;  // Reset X position after 4 buttons
            posY += 70; // Move to next row
        }
    }
}

        // Event handler for button clicks
       private void OnButtonClick(object sender, EventArgs e)
{
    Button button = (Button)sender;

    // Handle Clear button
    if (button.Text == "C" || button.Text == "CE")
    {
        textBoxDisplay.Clear();
        input = operand1 = operand2 = string.Empty;
        return;
    }

    // Handle ± button to toggle the sign of the current input
    if (button.Text == "±")
    {
        if (!string.IsNullOrEmpty(input))
        {
            // Toggle sign
            if (input.StartsWith("-"))
            {
                input = input.Substring(1);
            }
            else
            {
                input = "-" + input;
            }
        }
        else
        {
            // Start a negative input if input is empty
            input = "-";
        }
        textBoxDisplay.Text = input; // Update display
        return;
    }

    // Handle DEL button - delete the last character from input
    if (button.Text == "DEL")
    {
        if (input.Length > 0)
        {
            input = input.Substring(0, input.Length - 1); // Remove last character
            textBoxDisplay.Text = input; // Update display
        }
        return;
    }

    // Handle digit buttons
    if (button.Text.Length == 1 && char.IsDigit(button.Text[0]))
    {
        input += button.Text; // Append the button text to the input string
        textBoxDisplay.Text = input; // Update the display text box with the new input
    }
    // Handle the decimal point button
    else if (button.Text == ".")
    {
        // Allow adding a decimal point only if there isn't already one in the input
        if (!input.Contains("."))
        {
            input += ".";
            textBoxDisplay.Text = input; // Update display
        }
        return;
    }

    // Handle operator buttons
    else if ("+-*/".Contains(button.Text))
    {
        // If the input is not empty, set operand1 and the operation
        if (!string.IsNullOrEmpty(input))
        {
            operand1 = input;
            operation = button.Text[0];
            input = string.Empty; // Clear input for the next number
        }
        else if (button.Text == "-")
        {
            // If input is empty, we are starting a negative number
            input = "-"; // Start negative input
        }
        return;
    }

    // Handle equals button
    else if (button.Text == "=")
    {
        // Only calculate if both operands are present
        if (!string.IsNullOrEmpty(operand1) && !string.IsNullOrEmpty(input))
        {
            operand2 = input; // Set the second operand

            try
            {
                // Convert operands to decimal and calculate
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

                // Display the result
                textBoxDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                input = result.ToString(CultureInfo.InvariantCulture); // Update input for further calculations
                operand1 = operand2 = string.Empty; // Clear operands after calculation
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please check your numbers.");
                input = string.Empty; // Clear input if there's a format error
            }
        }
        return;
    }

    // Handle percentage button
    else if (button.Text == "%")
    {
        // Check if there's any input to calculate the percentage
        if (!string.IsNullOrEmpty(input))
        {
            decimal number = decimal.Parse(input, CultureInfo.InvariantCulture); // Handle decimal input
            result = number / 100; // Calculate percentage
            textBoxDisplay.Text = result.ToString(CultureInfo.InvariantCulture); // Display the result
            input = result.ToString(CultureInfo.InvariantCulture); // Update input for further calculations
        }
        return;
    }

    // Handle 1/x button
    else if (button.Text == "1/x")
    {
        if (!string.IsNullOrEmpty(input))
        {
            decimal number = decimal.Parse(input, CultureInfo.InvariantCulture); // Handle decimal input
            if (number != 0)
            {
                result = 1 / number; // Calculate reciprocal
                textBoxDisplay.Text = result.ToString(CultureInfo.InvariantCulture); // Display the result
                input = result.ToString(CultureInfo.InvariantCulture); // Update input for further calculations
            }
            else
            {
                MessageBox.Show("Cannot calculate the reciprocal of zero!");
            }
        }
        return;
    }

    // Handle x² button
    else if (button.Text == "x²")
    {
        if (!string.IsNullOrEmpty(input))
        {
            decimal number = decimal.Parse(input, CultureInfo.InvariantCulture); // Handle decimal input
            result = number * number; // Calculate square
            textBoxDisplay.Text = result.ToString(CultureInfo.InvariantCulture); // Display the result
            input = result.ToString(CultureInfo.InvariantCulture); // Update input for further calculations
        }
        return;
    }

    // Handle √ button
    else if (button.Text == "√")
    {
        if (!string.IsNullOrEmpty(input))
        {
            decimal number = decimal.Parse(input, CultureInfo.InvariantCulture); // Handle decimal input
            if (number >= 0)
            {
                result = (decimal)Math.Sqrt((double)number); // Calculate square root
                textBoxDisplay.Text = result.ToString(CultureInfo.InvariantCulture); // Display the result
                input = result.ToString(CultureInfo.InvariantCulture); // Update input for further calculations
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
