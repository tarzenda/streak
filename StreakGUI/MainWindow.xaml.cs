﻿using System;
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
            cbxAlgo.ItemsSource = IStreakAlgoExtensions.ListAlgos();
            cbxVariant.ItemsSource = Enum.GetValues(typeof(StreakVariant));

            cbxAlgo.SelectedIndex = 0;
            cbxVariant.SelectedValue = StreakVariant.HeadsAndTails;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Type algoType = cbxAlgo.SelectedItem as Type;
            if (algoType == null)
            {
                MessageBox.Show("Please select an algorithm.");
                return;
            }
            StreakVariant? variant = cbxVariant.SelectedItem as StreakVariant?;
            if (variant == null)
            {
                MessageBox.Show("Please select a problem variant.");
            }

            this.Cursor = Cursors.Wait;
            try
            {
                IStreakAlgo algo = (IStreakAlgo)Activator.CreateInstance(algoType);
                int n = Convert.ToInt32(tbxN.Text);
                int k = Convert.ToInt32(tbxK.Text);
                StreakResults result = algo.Calculate(variant.Value, n, k);
                lblResults.Content = result.ToString(); ;
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

    }
}
