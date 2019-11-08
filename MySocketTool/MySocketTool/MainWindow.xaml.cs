using MyToolkits.Log.TraceLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace MySocketTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _trace = new MyTraceListener();
            Trace.Listeners.Add(_trace);
            _trace.PropertyChanged += traceOnPropertyChanged;
        }
        MyTraceListener _trace;
        void traceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    if (e.PropertyName == "Trace")
                        TxtAll.AppendText($"{DateTime.Now.ToString(" HH:mm:ss fff")} | {_trace.Trace}");
                    TxtCurrrent.Text = _trace.Trace.Replace("\r", "").Replace("\n", "");
                }));

            });
        }

        private void TxtAll_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtAll.ScrollToEnd();
        }
    }
}
