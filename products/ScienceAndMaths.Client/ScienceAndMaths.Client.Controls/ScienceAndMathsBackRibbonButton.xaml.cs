﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScienceAndMaths.Client.Shared;

namespace ScienceAndMaths.Client.Controls
{
    /// <summary>
    /// Interaction logic for ScienceAndMathsBackRibbonButton.xaml
    /// </summary>
    public partial class ScienceAndMathsBackRibbonButton : RibbonButton
    {
        public ScienceAndMathsBackRibbonButton()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (!(DataContext is ScienceAndMathsModule) && !(DataContext is ScienceAndMathsViewModel))
            {
                throw new InvalidCastException($"{nameof(ScienceAndMathsBackRibbonButton)} needs a {nameof(DataContext)} of type {typeof(ScienceAndMathsModule)} or {typeof(ScienceAndMathsViewModel)}. Actual type is {DataContext.GetType()}");
            }
        }
    }
}
