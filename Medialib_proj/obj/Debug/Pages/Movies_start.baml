   M S B A M L   `   `   `  ����H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Medialib_proj.PagesMedialib_proj.Pages  5      &     Medialib_proj.Pages.Movies_start   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:Medialib_proj.Pages     5        N� Title$  List of movies��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��47* �6   �� 5      $
��153* �6    � RowDefinitions 5   
   �� 5      $��33 �6   �� 5      .+�5   
   �� -   5         ��Name$ 	tb_search��5      $��5,5,50,5q�6%    �� TextWrapping$
 Wrap=�67   $��	Search...��6K   
�������$��14(�6n   $��2��6�   �� 5   
   -   5         ��Name$ b_search��5      $��0,5,5,5q�6#   $��Right=�64   $��40��6P   $��1��6r   .��5      �� $!��../Resourses/img_search.png��6   $��1q�69   5      �� 5   
   $��10q�6   $��1��6   ��5      �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� .��6   Genres5      �� 5      -   5        $ b_all��5      $��30��6,   .��5      �� {�.   d� 	IsChecked$
 TrueY�65   5       �� 5!      -   5        $ b_none��5!      $��30��6-   .��5"      �� {�.  $ FalseY�65   5#      5$      .��5&      �� -   5         ��Name$ _Genres��5&      ��5'      �� 5(      .R�5)      �� -   5         5)   #   �� *	IsChecked+��6C   �� *
item.value+6   5*      5,      5-      ]� 5.   
   $��1��6   $��1��6$   ��. .+�5/      � -   5        	 �Name$	 manager_panel��5/      $��Bottom=�6*   $��40��6B   $	��0,5q�6N    50      �� 51      �� 52      $��3* �6'   �� 53      �� 54      $��3* �6'   �� 55      �� 56      $��3* �6'   �� 57      �� 58      $��3* �6'   �� 59      .+�5;      �� -   5        $ b_add��5;      $��1��6(   {�. .��5<      �� $��../Resourses/img_plus.png��6   5=      �� 5>      -	   5        $ b_del��5>      $��3��6(   {�.  .��5?      �� $ ��../Resourses/img_minus.png��6   5@      �� 5A      -
   5        $ b_edit��5A      $��5��6)   {�.  .��5B      �� $��../Resourses/img_edit.png��6   5C      �� 5D      -   5        $ b_lang��5D      $��7��6)   {�.  .��5E      �� $#��../Resourses/img_language.png��6   5F      5G      &   System.Windows.Controls.DataGrid  5H      -   5        
  Name$
 grid_movies��5H         SelectionMode$ Single=�6,      SelectionUnit$ FullRow=�6C   
�������   AlternatingRowBackground
 �����$	��Top=�6�   ��5I      �� 5J       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5K      -   5        5L         Columns 5N      0  *System.Windows.Controls.DataGridTextColumn  5O      ,  &System.Windows.Controls.DataGridColumn   Header$ id��6@      
IsReadOnly .    
Visibility$ Hidden=�6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *id+6^     5P      $ Original name��6G    .  6)   �� *	orig_name+6^     5Q      $ Localized name��6H    .  6)   �� *
local_name+6`     5R      $
 Year��6B    .  6)   �� *year+6P     5S      $ Duration��6F    .  6)   �� *
duration+6X     5T      $ Rating��6D    .  6)   �� *rating+6T   5V      5W      5X      5Y      