   M S B A M L   `   `   `  ���� H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Medialib_proj.PagesMedialib_proj.Pages  *    $Medialib_proj.Pages.Collections_view   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:Medialib_proj.Pages       N� Title$  Collections_view��.��� .+��� $
��Auto=�.���� .+���  �� Orientation$ 
Horizontal=�.+��� $
��Bold"�.��Collection name:�� -    ��Name$ l_name���� -    ��Name$ 
movie_part��.+��� $
��Bold"�$��Center=�.��Movies&   System.Windows.Controls.DataGrid  -     Name$ 
grid_movie��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ id��   
IsReadOnly .    
Visibility$ Hidden=�1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *id+  $ Original name�� . 	 �� *	orig_name+  $ Localized name�� . 	 �� *
local_name+  $
 Year�� . 	 �� *year+  $ Duration�� . 	 �� *
duration+  $ Rating�� . 	 �� *rating+�� -   $ 
music_part��.+��� $
��Bold"�$��Center=�.��Music  -   $ 
grid_music��   $ id�� . $ Hidden=�	 �� *id+  $ Musician/Band�� . 	 �� *
musician+  $ Album�� . 	 �� *album+  $
 Year�� . 	 �� *year+  $ Number�� . 	 �� *number+  $
 Name�� . 	 �� *name+  $ Duration�� . 	 �� *
duration+  $ Rating�� . 	 �� *rating+�� -   $ picture_part��.+��� $
��Bold"�$��Center=�.��
Pictures  -   $ grid_picture��   $ id�� . $ Hidden=�	 �� *id+  $
 Name�� . 	 �� *name+  $
 Year�� . 	 �� *year+  $ Rating�� . 	 �� *rating+�� -   $ 	text_part��.+��� $
��Bold"�$��Center=�.��Texts  -	   $ 	grid_text��   $ id�� . $ Hidden=�	 �� *id+  $
 Name�� . 	 �� *name+  $
 Year�� . 	 �� *year+  $ Rating�� . 	 �� *rating+