using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnNumberClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Text;
            if (Display.Text == "Press Any" || Display.Text == "0")
            {
                Display.Text = number;
            }
            else
            {
                Display.Text += number;
            }
        }

        void OnOperatorClicked(object sender, EventArgs e)
        {
            if(Display.Text == "Press Any")
            {
                Console.WriteLine("Error, press AC");
                return;
            }
            Button button = (Button)sender;
            Display.Text += button.Text;
        }

        void OnEquaClicked(object sender, EventArgs e)
        {
            if (Display.Text =="Press Any") 
            {
                return;
            } 
            string expression = Display.Text;
            List<string> tokens = new List<string>(Regex.Split(expression, @"([+\-*/])"));
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == "*" || tokens[i] == "/")
                {
                    double left = Convert.ToDouble(tokens[i - 1]);
                    double right = Convert.ToDouble(tokens[i + 1]);
                    double result = tokens[i] == "*" ? left * right : left / right;
                    tokens[i - 1] = result.ToString();
                    tokens.RemoveAt(i); 
                    tokens.RemoveAt(i); 
                    i--; 
                }
            }

            double total = Convert.ToDouble(tokens[0]);
            for (int i = 1; i < tokens.Count; i += 2)
            {
                string operation = tokens[i];
                double nextNumber = Convert.ToDouble(tokens[i + 1]);

                if (operation == "+")
                {
                    total += nextNumber;
                }
                else if (operation == "-")
                {
                    total -= nextNumber;
                }
            }
            Display.Text = total.ToString() + "  Press AC";
        }

        void OnACClicked(object sender, EventArgs e)
        {
            Display.Text = "0";
        }
    }

}
