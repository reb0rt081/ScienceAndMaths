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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScienceAndMaths.Client.Modules.Picking.ViewModels;
using Prism.Regions;

using Unity;

namespace ScienceAndMaths.Client.Modules.Picking.Views
{
    /// <summary>
    /// Interaction logic for StockCountView.xaml
    /// </summary>
    public partial class LocationView : UserControl
    {
        [Dependency]
        public IPickingViewModel PickingViewModel
        {
            get { return (IPickingViewModel)DataContext; }
            set
            {
                DataContext = value;
            }
        }

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }
        public LocationView()
        {
            InitializeComponent();
        }

        private void ScienceAndMathsScannerControl_OnScanSubmitted(object sender, string e)
        {
            PickingViewModel.LocationEnteredCommand.Execute(e);
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ScienceAndMathsScannerControl != null && !string.IsNullOrEmpty(ScienceAndMathsScannerControl.LastSuccessfulScan))
            {
                PickingViewModel.LocationEnteredCommand.Execute(ScienceAndMathsScannerControl.LastSuccessfulScan);
            }
        }
    }
}
