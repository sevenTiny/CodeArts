using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demo.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public IList<DemoItem> DemoItems { get; set; }

        private void Window_Initialized(object sender, EventArgs e)
        {
            DemoItems = typeof(MainWindow).Assembly.GetTypes()
                .Where(t => t.IsClass && t.IsAssignableTo(typeof(ISamples)))
                .Select(t =>
                {
                    var instance = Activator.CreateInstance(t) as ISamples;

                    return new DemoItem
                    {
                        Title = instance?.SampleTitle ?? String.Empty,
                        Description = instance?.SampleDescription ?? String.Empty,
                        //这里没有直接存实例是因为点击按钮打开窗口，再关闭后，实例被销毁了，再展示会报错
                        SampleType = t
                    };
                })
                .ToList();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (sender as Button)?.DataContext as DemoItem;

            if (vm != null && vm.SampleType != null)
                (Activator.CreateInstance(vm.SampleType) as ISamples)?.SampleStart();
        }
    }

    public class DemoItem
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        // <summary>
        // Demo类型
        // </summary>
        public Type SampleType { get; set; }
    }
}
