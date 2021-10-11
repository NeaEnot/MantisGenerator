using MantisGenerator.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            Dictionary<Node, bool> dict = new Dictionary<Node, bool>();

            foreach (Node child in tree.Root.Children)
            {
                DependencyObject container = treeView.ItemContainerGenerator.ContainerFromItem(child);
                if (container != null && container is TreeViewItem)
                    SaveItemState(dict, container as TreeViewItem);
            }

            tree.Sort();

            treeView.ItemsSource = null;
            treeView.Items.Clear();
            treeView.ItemsSource = tree.Root.Children;

            foreach (Node child in tree.Root.Children)
            {
                DependencyObject container = treeView.ItemContainerGenerator.ContainerFromItem(child);
                if (container != null && container is TreeViewItem)
                    SetItemState(dict, container as TreeViewItem);
            }
        }

        private void SaveItemState(Dictionary<Node, bool> dict, TreeViewItem item)
        {
            dict.Add(item.DataContext as Node, item.IsExpanded);

            foreach (Node child in (item.DataContext as Node).Children)
            {
                DependencyObject container = item.ItemContainerGenerator.ContainerFromItem(child);
                if (container != null && container is TreeViewItem)
                    SaveItemState(dict, container as TreeViewItem);
            }
        }

        private void SetItemState(Dictionary<Node, bool> dict, TreeViewItem item)
        {
            if (dict.ContainsKey(item.DataContext as Node))
            {
                item.IsExpanded = dict[item.DataContext as Node];
                item.UpdateLayout();

                foreach (Node child in (item.DataContext as Node).Children)
                {
                    DependencyObject container = item.ItemContainerGenerator.ContainerFromItem(child);
                    if (container != null && container is TreeViewItem)
                        SetItemState(dict, container as TreeViewItem);
                }
            }
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

            if (selected == tree.Root)
            {
                MessageBoxResult questionResult = 
                    MessageBox.Show("Вы собираетесь удалить всё дерево?", 
                                    "Результат", 
                                    MessageBoxButton.OKCancel, 
                                    MessageBoxImage.Question);

                if (questionResult == MessageBoxResult.Yes)
                    return;

                questionResult =
                    MessageBox.Show("Вы действительно хотите удалить всё дерево? Отмена не предусмотрена!",
                                    "Результат",
                                    MessageBoxButton.OKCancel,
                                    MessageBoxImage.Question);

                if (questionResult == MessageBoxResult.Yes)
                    return;
            }

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

            Node currentNode = (Node)treeView.SelectedItem ?? tree.Root;

            while (currentNode.Children.Count > 0)
            {
                List<Node> list = new List<Node>();

                foreach (Node node in currentNode.Children)
                    for (int i = 0; i < node.Probability; i++)
                        list.Add(node);

                if (list.Count == 0)
                    break;

                currentNode = list[rnd.Next(0, list.Count)];
                msg += currentNode.Name + '\n';
            }

            MessageBox.Show(msg, "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Node node = (sender as TextBlock).DataContext as Node;

            DragDrop.DoDragDrop(sender as TextBlock, node, DragDropEffects.Copy);
        }

        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            Node target = (sender as TextBlock).DataContext as Node;
            Node source = e.Data.GetData(typeof(Node)) as Node;

            if (target != source && !source.Contains(target))
            {
                tree.Delete(source);

                target.Children.Add(source);

                LoadData();
            }
        }

        private void treeView_Drop(object sender, DragEventArgs e)
        {
            Node source = e.Data.GetData(typeof(Node)) as Node;

            tree.Delete(source);
            tree.Root.Children.Add(source);
            LoadData();
        }
    }
}
