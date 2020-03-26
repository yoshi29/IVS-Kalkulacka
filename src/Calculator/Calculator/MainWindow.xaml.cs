using System;
using System.Collections.Generic;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            result.Focus();

            if (result.Text == "0")
            {
                result.Text = String.Empty;


            }

            result.Text += button.Content;
            result.Select(result.Text.Length, 0);
        }

        private void Clean(object sender, RoutedEventArgs e)
        {
            result.Focus();
            result.Select(0, 0);
            result.Text = String.Empty + "0";

        }

        private void Delete_Last(object sender, RoutedEventArgs e)
        {
            result.Focus();

            result.Text = result.Text.ToString().Remove(result.Text.Length - 1);
            if (result.Text == "")
                result.Text = "0";

            result.Select(result.Text.Length, 0);

        }

        private void square_root(object sender, RoutedEventArgs e)
        {
            result.Focus();
            if (result.Text == "0")
                result.Text = $"sqr(x,y)";
            else
                result.Text += $"sqr(x,y)";

            result.Select(result.Text.Length - 4, 3); ///caret is set into sqr and text to overwrite is selected
        }


     

    }


}        
