using System.Windows;
using CSharpPractice2.ViewModels;

namespace CSharpPractice2.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        } 
    }
}
