﻿#pragma checksum "..\..\..\Windows\MigrationB2B.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1B3E6D076D7FDE0EED2A2A12487539F9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace CookingBook.Windows {
    
    
    /// <summary>
    /// MigrationB2B
    /// </summary>
    public partial class MigrationB2B : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Windows\MigrationB2B.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Windows\MigrationB2B.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MainRecipeFilterText;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Windows\MigrationB2B.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ForeignRecipeFilterText;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Windows\MigrationB2B.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ForeignDbListViev;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Windows\MigrationB2B.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView MainDb;
        
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
            System.Uri resourceLocater = new System.Uri("/CookingBook;component/windows/migrationb2b.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\MigrationB2B.xaml"
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
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 20 "..\..\..\Windows\MigrationB2B.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenForeignDB);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MainRecipeFilterText = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\Windows\MigrationB2B.xaml"
            this.MainRecipeFilterText.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ForeignRecipeFilterText_Changed);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ForeignRecipeFilterText = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\Windows\MigrationB2B.xaml"
            this.ForeignRecipeFilterText.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.MainRecipeFilterText_Changed);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ForeignDbListViev = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.MainDb = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
