﻿#pragma checksum "..\..\..\View\MainP.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8F85CC08730D14815413D1B0B79F17728BCD7F9412338D9DE06E9B7A6CA06872"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation.Peers;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Converters;
using MahApps.Metro.Markup;
using MahApps.Metro.Theming;
using MahApps.Metro.ValueBoxes;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ElectricPro.View {
    
    
    /// <summary>
    /// MainP
    /// </summary>
    public partial class MainP : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 137 "..\..\..\View\MainP.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.AutoCompleteBox FilterByText;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\View\MainP.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FilterByCombo;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\..\View\MainP.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listView;
        
        #line default
        #line hidden
        
        
        #line 201 "..\..\..\View\MainP.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.ProgressRing loading;
        
        #line default
        #line hidden
        
        
        #line 202 "..\..\..\View\MainP.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Snackbar SnackbarFive;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ElectricPro;component/view/mainp.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\MainP.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.FilterByText = ((System.Windows.Controls.AutoCompleteBox)(target));
            
            #line 137 "..\..\..\View\MainP.xaml"
            this.FilterByText.TextChanged += new System.Windows.RoutedEventHandler(this.FilterByText_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.FilterByCombo = ((System.Windows.Controls.ComboBox)(target));
            
            #line 140 "..\..\..\View\MainP.xaml"
            this.FilterByCombo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FilterByCombo_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.listView = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            this.loading = ((MahApps.Metro.Controls.ProgressRing)(target));
            return;
            case 5:
            this.SnackbarFive = ((MaterialDesignThemes.Wpf.Snackbar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

