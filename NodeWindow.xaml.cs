using MantisGenerator.Models;
using System.Windows;

namespace MantisGenerator
{
    /// <summary>
    /// Логика взаимодействия для NodeWindow.xaml
    /// </summary>
    public partial class NodeWindow : Window
    {
        public Node Node { get; private set; }

        public NodeWindow()
        {
            InitializeComponent();
            Node = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textTitle.Text))
            {
                Node = new Node { Name = textTitle.Text };
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Имя узла пустое", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
