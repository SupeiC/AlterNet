using Alternet.Scripter.Debugger;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            _vm = new ViewModel();
            this.DataContext = _vm;

            debugEditor.Debugger = _vm.Debugger;
            debugEditor.Lexer = _vm.Lexer;
            debugEditor.Text = File.ReadAllText("Code.py");
        }

        private async void btnRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var debugMode = cbDebugMode.IsChecked == true;
                var sb = new StringBuilder();

                await Task.Run(() =>
                {
                    _vm.Execute(debugMode, x => sb.Append(x), debugEditor.Text);
                });

                MessageBox.Show(sb.ToString(), "Output", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ex: " + ex.Message);
            }
        }

    }
}
