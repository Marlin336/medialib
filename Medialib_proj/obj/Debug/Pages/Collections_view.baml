   M S B A M L   `   `   `  ����H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Medialib_proj.PagesMedialib_proj.Pages  5      *    $Medialib_proj.Pages.Collections_view   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:Medialib_proj.Pages     5        N� Title$  Collections_view��5	      .��5      � .+�5   
   �� $
��Auto=�6   .��5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $
��Bold"�6   .��6.   Collection name:6A   �� 5      -   5         ��Name$ l_name��5      5      �� 5      -   5         ��Name$ 
movie_part��5      .+�5      �� $
��Bold"�6   $��Center=�6.   .��6K   Movies6S   &   System.Windows.Controls.DataGrid  5      -   5          Name$ 
grid_movie��5      ��5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6$   .�5   "   -   5        5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ id��6H      
IsReadOnly . 	   
Visibility$	 Hidden=�6f   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 61   �� *id+6f     5      $ Original name��6O    . 
 61   �� *	orig_name+6f     5      $ Localized name��6P    . 
 61   �� *
local_name+6h     5      $
 Year��6J    . 
 61   �� *year+6X     5      $ Duration��6N    . 
 61   �� *
duration+6`     5       $ Rating��6L    . 
 61   �� *rating+6\   5"      5#      �� 5$      -   5        $ 
music_part��5$      .+�5%      �� $
��Bold"�6   $��Center=�6.   .��6K   Music6R     5&      -   5        $ 
grid_music��5&      ��5'      �� 5(      "  6$   .�5)   "   -   5        5*       5,        5-      $ id��6H    . $	 Hidden=�6f   
 61   �� *id+6f     5.      $ Musician/Band��6N    . 
 61   �� *
musician+6e     5/      $ Album��6K    . 
 61   �� *album+6Z     50      $
 Year��6J    . 
 61   �� *year+6X     51      $ Number��6L    . 
 61   �� *number+6\     52      $
 Name��6J    . 
 61   �� *name+6X     53      $ Duration��6N    . 
 61   �� *
duration+6`     54      $ Rating��6L    . 
 61   �� *rating+6\   56      57      �� 58      -   5        $ picture_part��58      .+�59      �� $
��Bold"�6   $��Center=�6.   .��6K   
Pictures6U     5:      -	   5        $ grid_picture��5:      ��5;      �� 5<      "  6$   .�5=   "   -
   5        5>       5@        5A      $ id��6H    . $	 Hidden=�6f   
 61   �� *id+6f     5B      $
 Name��6J    . 
 61   �� *name+6X     5C      $
 Year��6J    . 
 61   �� *year+6X     5D      $ Rating��6L    . 
 61   �� *rating+6\   5F      5G      �� 5H      -   5        $ 	text_part��5H      .+�5I      �� $
��Bold"�6   $��Center=�6.   .��6K   Texts6R     5J      -   5        $ 	grid_text��5J      ��5K      �� 5L      "  6$   .�5M   "   -   5        5N       5P        5Q      $ id��6H    . $	 Hidden=�6f   
 61   �� *id+6f     5R      $
 Name��6J    . 
 61   �� *name+6X     5S      $
 Year��6J    . 
 61   �� *year+6X     5T      $ Rating��6L    . 
 61   �� *rating+6\   5V      5W      5X      5Y      5Z      5[      