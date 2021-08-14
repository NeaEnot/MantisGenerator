using MantisGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MantisGenerator
{
    public partial class MainWindow : Window
    {
        private Node root;

        public MainWindow()
        {
            InitializeComponent();

            root = new Node();
            root.Children.Add(new Node { Name = "Первыф", Probability = 3, IsActive = true });
            root.Children.Add(new Node { Name = "Фторой", Probability = 1, IsActive = true });
            root.Children[0].Children.Add(new Node { Name = "Трефий", Probability = 1, IsActive = true });
            root.Children[0].Children.Add(new Node { Name = "Четфёрфый", Probability = 1, IsActive = true });
            root.Children[1].Children.Add(new Node { Name = "Пяфый", Probability = 1, IsActive = true });
            root.Children[0].Children[0].Children.Add(new Node { Name = "Фестой", Probability = 1, IsActive = true });
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

        private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoadData();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Node selected = (Node)treeView.SelectedItem ?? root;

            NodeWindow nodeWindow = new NodeWindow(null);
            if (nodeWindow.ShowDialog() == true)
            {
                selected.Children.Add(nodeWindow.Node);
                LoadData();
            }
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Node selected = (Node)treeView.SelectedItem ?? root;

            NodeWindow nodeWindow = new NodeWindow(selected);
            if (nodeWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Node selected = (Node)treeView.SelectedItem ?? root;
            if (root.Delete(selected))
            {
                LoadData();
            }
        }

        private void buttonGenerate_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            string msg = "";

            Node currentNode = root;

            while (currentNode.Children.Count > 0)
            {
                List<Node> list = new List<Node>();

                foreach (Node node in currentNode.Children.Where(req => req.IsActive))
                {
                    for (int i = 0; i < node.Probability; i++)
                    {
                        list.Add(node);
                    }
                }

                currentNode = list[rnd.Next(0, list.Count)];
                msg += currentNode.Name + '\n';
            }

            MessageBox.Show(msg, "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
