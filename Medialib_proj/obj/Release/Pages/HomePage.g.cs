﻿#pragma checksum "..\..\..\Pages\HomePage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21E79430EAF8ABA166DE68092A6AFC795D7C321B"
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


namespace Medialib_proj {
    
    
    /// <summary>
    /// HomePage
    /// </summary>
    public partial class HomePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Pages\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_collections;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Pages\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_celebrities;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Pages\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_movies;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Pages\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_books;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Pages\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_music;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Pages\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b_pictures;
        
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
            System.Uri resourceLocater = new System.Uri("/Medialib_proj;component/pages/homepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\HomePage.xaml"
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
            this.b_collections = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\Pages\HomePage.xaml"
            this.b_collections.Click += new System.Windows.RoutedEventHandler(this.B_collections_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.b_celebrities = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\Pages\HomePage.xaml"
            this.b_celebrities.Click += new System.Windows.RoutedEventHandler(this.B_celebrities_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.b_movies = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\Pages\HomePage.xaml"
            this.b_movies.Click += new System.Windows.RoutedEventHandler(this.B_movies_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.b_books = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\Pages\HomePage.xaml"
            this.b_books.Click += new System.Windows.RoutedEventHandler(this.B_books_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.b_music = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\..\Pages\HomePage.xaml"
            this.b_music.Click += new System.Windows.RoutedEventHandler(this.B_music_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.b_pictures = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\Pages\HomePage.xaml"
            this.b_pictures.Click += new System.Windows.RoutedEventHandler(this.B_pictures_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

