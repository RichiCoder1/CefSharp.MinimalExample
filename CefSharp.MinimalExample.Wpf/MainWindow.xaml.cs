using System;
using System.Windows;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var browser = new ChromiumWebBrowser("https://google.com/");
            browser.LoadingStateChanged += BrowserOnLoadingStateChanged;
            browser.TitleChanged += BrowserOnTitleChanged;
            BrowserHost.Child = browser;
        }

        private void BrowserOnTitleChanged(object sender, TitleChangedEventArgs titleChangedEventArgs)
        {
            InvokeOnUI(() => {
                Title = titleChangedEventArgs.Title;
            });
        }

        private void BrowserOnLoadingStateChanged(object sender, LoadingStateChangedEventArgs eventArgs)
        {
            InvokeOnUI(() => {
                Address.Text = eventArgs.Browser.MainFrame.Url;
                ProgressBar.IsIndeterminate = eventArgs.IsLoading;
            });
        }

        public void InvokeOnUI(Action action)
        {
            if (BrowserHost.Child.InvokeRequired)
            {
                BrowserHost.Child.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
