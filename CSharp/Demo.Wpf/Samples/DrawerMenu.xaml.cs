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

namespace Demo.Wpf.Samples
{
    /// <summary>
    /// Interaction logic for DrawerMenu.xaml
    /// </summary>
    public partial class DrawerMenu : Window, ISamples
    {
        public DrawerMenu()
        {
            InitializeComponent();
        }

        public string SampleTitle => "抽屉菜单";

        public string SampleDescription => "实现抽屉菜单效果";

        public void SampleStart()
        {
            this.Show();
        }
    }
}
