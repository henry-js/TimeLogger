using System.Windows;
using Wpf.Controls;

namespace Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        content.Content = new ClientControl();
    }
}

