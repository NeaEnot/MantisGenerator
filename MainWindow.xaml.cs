using MantisGenerator.Models;
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

namespace MantisGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Node root;

        public MainWindow()
        {
            InitializeComponent();

            root = new Node();
            root.Children.Add(new Node { Name = "Первыф" });
            root.Children.Add(new Node { Name = "Фторой" });
            root.Children[0].Children.Add(new Node { Name = "Трефий" });
            root.Children[0].Children.Add(new Node { Name = "Четфёрфый" });
            root.Children[1].Children.Add(new Node { Name = "Пяфый" });
            root.Children[0].Children[0].Children.Add(new Node { Name = "Фестой" });
        }

        private void treeView_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            treeView.ItemsSource = null;
            treeView.Items.Clear();
            treeView.ItemsSource = root.Children;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Node selected = (Node)treeView.SelectedItem ?? root;

            NodeWindow nodeWindow = new NodeWindow();
            if (nodeWindow.ShowDialog() == true)
            {
                selected.Children.Add(nodeWindow.Node);
                LoadData();
            }
        }

        private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoadData();
        }
    }
}
