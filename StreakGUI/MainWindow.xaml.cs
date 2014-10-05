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

namespace Tarzenda.Streak.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int n = Convert.ToInt32(tbxN.Text);
                int k = Convert.ToInt32(tbxK.Text);
                StreakAlgorithm algo = new StreakAlgorithm();
                double result = algo.CalculatePercentageBySample(n, k);
                lblResults.Content = string.Format("Result: {0:N3}%", result * 100);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

    }
}
