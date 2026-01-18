
using System.Windows;
using TestGenerics.ViewModels;

namespace TestGenerics;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new AppViewModel();
    }
}
