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
using System.Windows.Shapes;

namespace WPFPractices
{
    /// <summary>
    /// Interaction logic for ConfigEditor.xaml
    /// </summary>
    public partial class ConfigEditor : Window
    {
        Configuration config;

        public ConfigEditor(Configuration config)
        {

            InitializeComponent();
            PositionTextBox.Text = config.PositionX.ToString();

            this.config = config;
        }

        private void ApplyConfig(object sender, RoutedEventArgs e)
        {
            if (float.TryParse(PositionTextBox.Text, out float value))
            {
                config.PositionX = value;
            }
        }
    }
}
