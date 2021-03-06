﻿using MyToolkits.Log.TraceLog;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                        try
                        {
                            foreach (string msg in _trace.Trace)
                            {
                                //TxtAll.AppendText($"{DateTime.Now.ToString(" HH:mm:ss fff")} | {msg}");
                                TxtAll.AppendText(msg);
                                TxtCurrrent.Text = msg.Replace("\r", "").Replace("\n", "");
                            }
                            _trace.Clear();
                        }
                        catch(Exception)
                        {

                        }
                }));

            });
        }

        private void TxtAll_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtAll.ScrollToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TxtAll.Clear();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TxtBox1.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void TxtBox2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TxtBox2.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
