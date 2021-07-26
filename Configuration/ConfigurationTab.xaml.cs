﻿using System;
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

namespace Configuration
{
    /// <summary>
    /// Interaction logic for ConfigurationTab.xaml
    /// </summary>
    public partial class ConfigurationTab : Window
    {
        public ConfigurationTab()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }
    }
}
