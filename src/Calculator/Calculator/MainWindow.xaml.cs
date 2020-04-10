
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
        /// In certain cases if result.Text contains only "0" character/"Error" string delete this character/string.
        /// </summary>
        /// <param name="text">Text that will be added to result.Text to specific position</param>
        private void AddToResult(string text)
        {
            if (result.SelectionLength > 0)
            {
                result.SelectedText = string.Empty;
            }
            if ((result.Text == "0" && ! ".+/*^!".Contains(text)) || result.Text == "Error")
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            result.Focus();

            AddToResult(button.Content.ToString());
        }

        /// <summary>
        /// Actions to take when key is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enter(object sender, KeyEventArgs e)
        {
            if ((result.Text == "0" && !".+-/*^!".Contains(e.Key.ToString())) || result.Text == "Error")
            {
                result.Text = string.Empty;
            }
            else if (e.Key == Key.Enter)
            {
                Dispatcher.Invoke(() => Get_Equation(sender, new RoutedEventArgs()));
            }
        }

        /// <summary>
        /// Modifies the behavior of the backspace button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private new void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                e.Handled = true;
                Dispatcher.Invoke(() => Delete_Last(sender, new RoutedEventArgs()));
            }
        }

        /// <summary>
        /// Actions to take when "C" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clean(object sender, RoutedEventArgs e)
        {
            result.Focus();
            result.Text = string.Empty;
            AddToResult("0");
        }

        /// <summary>
        /// Actions to take when "Del" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Last(object sender, RoutedEventArgs e)
        {
            result.Focus();

            if (result.Text == "" || result.Text == "Error")
                result.Text = "0";

            if (result.Text.Length != 0)
            {
                result.Text = result.Text.ToString().Remove(result.Text.Length - 1);
            }

            result.Select(result.Text.Length, 0);
        }

        /// <summary>
        /// Actions to take when "=" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Get_Equation(object sender, RoutedEventArgs e)
        {
            result.Focus();
            string res = Process(result.Text);
            if (Double.TryParse(res, out double res_double)) {
                string rounded = Math.Round(res_double, 9).ToString(); ;
                result.Text = rounded;
                result.Select(result.CaretIndex + result.Text.Length, 0);
            }
            else
            {
                result.Text = "Error";
                result.Select(result.CaretIndex + result.Text.Length, 0);
            }
        }

        /// <summary>
        /// Actions to take when "√" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Root(object sender, RoutedEventArgs e)
        {
            result.Focus();
            AddToResult("root(x,y)");
            result.CaretIndex -= 4; //Set caret index after left bracket (4 characters)
            result.Select(result.CaretIndex, 3); //Selection ends before right bracket (3 characters)
        }

        /// <summary>
        /// Actions to take when "Abs" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Abs(object sender, RoutedEventArgs e)
        {
            result.Focus();
            AddToResult("abs(x)");
        }

        /// <summary>
        /// Actions to take when "Rnd" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rnd_Gen(object sender, RoutedEventArgs e)
        {
            result.Focus();
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
            AddToResult(Math.Round(MathLib.Rnd(), 9).ToString("G", culture));
        }

        /// <summary>
        /// Change certain characters in the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void result_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int caretIndex = textBox.CaretIndex;
            textBox.Text = textBox.Text.Replace(" ", "");
            textBox.Text = textBox.Text.Replace("-", "−");
            textBox.Text = textBox.Text.Replace("*", "×");
            textBox.Text = textBox.Text.Replace("/", "÷");
            textBox.CaretIndex = caretIndex;
        }
    }
}        
