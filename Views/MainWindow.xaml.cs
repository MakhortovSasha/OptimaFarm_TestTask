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
using OptimaFarm_TestTask.ViewModels;
using OptimaFarm_TestTask.Views;

namespace OptimaFarm_TestTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainVM;
        public MainWindow()
        {

            InitializeComponent();
            mainVM = new MainWindowViewModel();
            DataContext = mainVM;
            
        }
    }
}
