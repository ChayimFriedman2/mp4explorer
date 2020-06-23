//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace Mp4Explorer
{
    public partial class AboutBoxWindow : Window
    {
        public string AssemblyTitle =>
            GetAssemblyAttribute<AssemblyTitleAttribute>()?.Title ??
                Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public string AssemblyDescription => GetAssemblyAttribute<AssemblyDescriptionAttribute>()?.Description ?? "";
        public string AssemblyProduct => GetAssemblyAttribute<AssemblyProductAttribute>()?.Product ?? "";
        public string AssemblyCopyright => GetAssemblyAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? "";
        public string AssemblyCompany => GetAssemblyAttribute<AssemblyCompanyAttribute>()?.Company ?? "";

        public AboutBoxWindow()
        {
            InitializeComponent();
            Title = string.Format("About {0}", AssemblyTitle);
            labelProductName.Content = AssemblyProduct;
            labelVersion.Content = string.Format("Version {0}", AssemblyVersion);
            labelCopyright.Content = AssemblyCopyright;
            labelCompanyName.Content = AssemblyCompany;
            labelDescription.Content = AssemblyDescription;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private T GetAssemblyAttribute<T>()
            where T : Attribute
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : default;
        }
    }
}
