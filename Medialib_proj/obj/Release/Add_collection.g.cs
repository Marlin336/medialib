#pragma checksum "..\..\Add_collection.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D0B5832D7EEC44EF9961A00A97251F971B68AE8B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Medialib_proj;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace Medialib_proj {
    
    
    /// <summary>
    /// Add_collection
    /// </summary>
    public partial class Add_collection : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\Add_collection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_name;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Add_collection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_movies;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Add_collection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_movie_add;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Add_collection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_music;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Add_collection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_music_add;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Add_collection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_pictures;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Add_collection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_picture_add;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Add_collection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_texts;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\Add_collection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_text_add;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\Add_collection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_add;
        
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
            System.Uri resourceLocater = new System.Uri("/Medialib_proj;component/add_collection.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Add_collection.xaml"
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
            this.tb_name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.cb_movies = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.b_movie_add = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\Add_collection.xaml"
            this.b_movie_add.Click += new System.Windows.RoutedEventHandler(this.B_movie_add_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cb_music = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.b_music_add = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\Add_collection.xaml"
            this.b_music_add.Click += new System.Windows.RoutedEventHandler(this.B_music_add_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cb_pictures = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.b_picture_add = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\Add_collection.xaml"
            this.b_picture_add.Click += new System.Windows.RoutedEventHandler(this.B_picture_add_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cb_texts = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.b_text_add = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\Add_collection.xaml"
            this.b_text_add.Click += new System.Windows.RoutedEventHandler(this.B_text_add_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.b_add = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\Add_collection.xaml"
            this.b_add.Click += new System.Windows.RoutedEventHandler(this.B_add_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

