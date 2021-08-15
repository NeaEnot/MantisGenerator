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
        private Tree tree;

        public MainWindow()
        {
            InitializeComponent();

            tree = new Tree();
        }

        private void treeView_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            treeView.ItemsSource = null;
            treeView.Items.Clear();
            treeView.ItemsSource = tree.Root.Children;
        }

        private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoadData();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Node selected = (Node)treeView.SelectedItem ?? tree.Root;

            NodeWindow nodeWindow = new NodeWindow(null);
            if (nodeWindow.ShowDialog() == true)
            {
                selected.Children.Add(nodeWindow.Node);
                tree.Save();
                LoadData();
            }
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Node selected = (Node)treeView.SelectedItem ?? tree.Root;

            NodeWindow nodeWindow = new NodeWindow(selected);
            if (nodeWindow.ShowDialog() == true)
            {
                tree.Save();
                LoadData();
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Node selected = (Node)treeView.SelectedItem ?? tree.Root;
            if (tree.Delete(selected))
            {
                tree.Save();
                LoadData();
            }
        }

        private void buttonGenerate_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            string msg = "";

            Node currentNode = tree.Root;

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
