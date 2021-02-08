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
using System.Configuration;

namespace WPFPractices
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		public List<TodoItem> Todos { get; set; }
		public MainWindow()
        {
			Todos = new List<TodoItem>();
			Todos.Add(new TodoItem() { Title = "Example 1", Completion = 0.1f });
			Todos.Add(new TodoItem() { Title = "Learn C#", Completion = 0.8f });
			Todos.Add(new TodoItem() { Title = "Learn Unity", Completion = 1 });

            InitializeComponent();
		}
	}

	public class TodoItem
	{
		public string Title { get; set; }
		public float Completion { get; set; }
	}
}

