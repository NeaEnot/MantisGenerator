using MantisGenerator.Models;
using System;
using System.Windows;

namespace MantisGenerator
{
    public partial class NodeWindow : Window
    {
        public Node Node { get; private set; }

        public NodeWindow(Node node)
        {
            InitializeComponent();

            Node = node;
            if (Node != null)
            {
                textName.Text = Node.Name;
                textProbability.Text = Node.Probability.ToString();
                checkActive.IsChecked = Node.IsActive;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a;

                if (string.IsNullOrWhiteSpace(textName.Text))
                    throw new Exception("Имя узла пустое");
                if (string.IsNullOrWhiteSpace(textProbability.Text))
                    throw new Exception("Вероятность не задана");
                if (!int.TryParse(textProbability.Text, out a))
                    throw new Exception("Неверная вероятность");
                if (a < 0)
                    throw new Exception("Вероятность не может быть меньше 0");

                Node = Node ?? new Node();

                Node.Name = textName.Text;
                Node.Probability = int.Parse(textProbability.Text);
                Node.IsActive = checkActive.IsChecked.Value;

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
