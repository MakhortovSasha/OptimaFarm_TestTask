using OptimaFarm_TestTask.Commands.Base;
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

namespace OptimaFarm_TestTask.Views
{
    /// <summary>
    /// Логика взаимодействия для CellWithEmploeeData.xaml
    /// </summary>
    public partial class CellWithEmploeeData : UserControl
    {

        public static readonly DependencyProperty isActive = DependencyProperty.Register("IsActive", typeof(bool),typeof(CellWithEmploeeData) );

        public bool IsActive
        {
            get => (bool)GetValue(isActive);
            set
            {
               SetValue(isActive, value);
            }
        }


        public CellWithEmploeeData()
        {
            
            InitializeComponent();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Rec1.Stroke = new SolidColorBrush(Colors.Transparent);
            Opacity = 1;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Rec1.Stroke = new SolidColorBrush(Colors.Red);
            Opacity = 0.8;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Rec1.Stroke = new SolidColorBrush(Colors.Black);
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rec1.Stroke = new SolidColorBrush(Colors.Red);
        }
    }
}
