#pragma checksum "..\..\..\Pages\Celebrities_start.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "43F5AB1E1311126A987FB98457E1897D7FB5E936C3D80D6165ED990139BC39C2"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Medialib_proj.Pages;
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


namespace Medialib_proj.Pages {
    
    
    /// <summary>
    /// Celebrities_start
    /// </summary>
    public partial class Celebrities_start : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 16 "..\..\..\Pages\Celebrities_start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_search;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Pages\Celebrities_start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_search;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Pages\Celebrities_start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid manager_panel;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Pages\Celebrities_start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_add;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Pages\Celebrities_start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_del;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Pages\Celebrities_start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_edit;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Pages\Celebrities_start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid grid_celebrities;
        
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
            System.Uri resourceLocater = new System.Uri("/Medialib_proj;component/pages/celebrities_start.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Celebrities_start.xaml"
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
            this.tb_search = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\..\Pages\Celebrities_start.xaml"
            this.tb_search.GotFocus += new System.Windows.RoutedEventHandler(this.Tb_search_GotFocus);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\Pages\Celebrities_start.xaml"
            this.tb_search.LostFocus += new System.Windows.RoutedEventHandler(this.Tb_search_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.b_search = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Pages\Celebrities_start.xaml"
            this.b_search.Click += new System.Windows.RoutedEventHandler(this.B_search_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.manager_panel = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.b_add = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\Pages\Celebrities_start.xaml"
            this.b_add.Click += new System.Windows.RoutedEventHandler(this.B_add_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.b_del = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\Pages\Celebrities_start.xaml"
            this.b_del.Click += new System.Windows.RoutedEventHandler(this.B_del_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.b_edit = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\Pages\Celebrities_start.xaml"
            this.b_edit.Click += new System.Windows.RoutedEventHandler(this.B_edit_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.grid_celebrities = ((System.Windows.Controls.DataGrid)(target));
            
            #line 41 "..\..\..\Pages\Celebrities_start.xaml"
            this.grid_celebrities.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Grid_collection_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 8:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 44 "..\..\..\Pages\Celebrities_start.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.DataGridRow_MouseDoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

