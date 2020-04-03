using static ExpressionProcessor.ExpressionProcessor;
using MathLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          
            result.Focus();
            result.Select(result.Text.Length, 0);
        }

        /// <summary>
        /// Add <paramref name="text"/> to result.Text to specific position. 
        /// If result.Text contains only "0" character, delete this character.
        /// </summary>
        /// <param name="text">Text that will be added to result.Text to specific position</param>
        private void AddToResult(string text)
        {
            if ((result.Text == "0" && ! ".+-/*^!".Contains(text)) || result.Text == "Error")
            {
                result.Text = string.Empty;
            }

            if (result.CaretIndex == result.Text.Length)
            {
                result.AppendText(text);
                result.Select(result.Text.Length, 0);
            }
            else
            {
                int caretIndex = result.CaretIndex;
                result.Text = result.Text.Insert(result.CaretIndex, text);
                result.CaretIndex = caretIndex + text.Length;
                result.Select(result.CaretIndex, 0);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            result.Focus();

            AddToResult(button.Content.ToString());
        }

        private void Enter(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((result.Text == "0" && !".+-/*^!".Contains(e.Key.ToString())) || result.Text == "Error")
            {
                result.Text = string.Empty;
            }
            if (e.Key == Key.Enter)
            {
                Dispatcher.Invoke(() => Get_Equation(sender, new RoutedEventArgs()));
            }
        }

        private void Clean(object sender, RoutedEventArgs e)
        {
            result.Focus();
            result.Text = string.Empty;
            AddToResult("0");
        }

        private void Delete_Last(object sender, RoutedEventArgs e)
        {
            result.Focus();

            if (result.Text.Length != 0)
            {
                result.Text = result.Text.ToString().Remove(result.Text.Length - 1);
            }
            if (result.Text == "")
                result.Text = "0";

            result.Select(result.Text.Length, 0);
        }

        public void Get_Equation(object sender, RoutedEventArgs e)
        {
            result.Focus();
            result.Text = Process(result.Text); //TODO: Round to 9 decimal places
            result.Select(result.CaretIndex + result.Text.Length, 0);
        }

        private void Root(object sender, RoutedEventArgs e)
        {
            result.Focus();
            AddToResult("root(x,y)");    
        }

        public void Abs(object sender, RoutedEventArgs e)
        {
            result.Focus();
            AddToResult("abs(x)");
        }

        private void Rnd_Gen(object sender, RoutedEventArgs e)
        {
            result.Focus();
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
            AddToResult(MathLib.Rnd().ToString("G", culture));
        }
    }
}        
