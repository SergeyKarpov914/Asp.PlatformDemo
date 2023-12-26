using System;
using System.Windows;

namespace BlazorWebViewTutorial.WpfApp
{
    // add usings here
    using BlazorWebView;

    public partial class MainWindow : Window
    {
        private IDisposable disposable;
        private bool initialized = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (!this.initialized)
            {
                this.initialized = true;
                // run blazor.
                this.disposable = BlazorWebViewHost.Run<Startup>(this.BlazorWebView, "wwwroot/index.html");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.disposable != null)
            {
                this.disposable.Dispose();
                this.disposable = null;
            }
        }
    }
}

