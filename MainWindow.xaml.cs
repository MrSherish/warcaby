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

namespace Warcaby
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void newGame(object sender, RoutedEventArgs e)
        {
            MinMax minMax = new MinMax();
            minMax.Depth = ProjectSettings.DEPTH;
            minMax.computerStarts = true;
            wyniki.Content = "MinMax:";
            wyniki.Content += minMax.run();

            MinMax alfaBeta = new AlfaBeta();
            alfaBeta.Depth = ProjectSettings.DEPTH;
            alfaBeta.computerStarts = true;
            wyniki.Content += "\nAlfaBeta:";
            this.wyniki.Content += alfaBeta.run();
        }
    }
}
