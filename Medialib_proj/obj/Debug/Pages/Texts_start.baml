   M S B A M L   `   `   `  ����H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Medialib_proj.PagesMedialib_proj.Pages  5      %    Medialib_proj.Pages.Texts_start   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:Medialib_proj.Pages     5        N� Title$  Texts_start��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��47* �6   �� 5      $
��153* �6    � RowDefinitions 5   
   �� 5      $��33 �6   �� 5      .+�5   
   �� -   5         ��Name$ 	tb_search��5      $��5,5,50,5q�6%    �� TextWrapping$
 Wrap=�67   $��	Search...��6K   
�������$��14(�6n   $��2��6�   �� 5   
   -   5         ��Name$ b_search��5      $��0,5,5,5q�6#   $��Right=�64   $��40��6P   $��1��6r   .��5      �� $!��../Resourses/img_search.png��6   $��1q�69   5      ]� 5   
   $��1��6   $��1��6$   ��. .+�5      � -   5         �Name$ manager_panel��5      $��Bottom=�6*   $��40��6B   $	��0,5q�6N    5      �� 5      �� 5      $��3* �6'   �� 5      �� 5      $��3* �6'   �� 5      �� 5       $��3* �6'   �� 5!      .+�5#      �� -   5        $ b_add��5#      $��1��6(   {�. .��5$      �� $��../Resourses/img_plus.png��6   5%      �� 5&      -   5        $ b_del��5&      $��3��6(   {�.  .��5'      �� $ ��../Resourses/img_minus.png��6   5(      �� 5)      -   5        $ b_edit��5)      $��5��6)   {�.  .��5*      �� $��../Resourses/img_edit.png��6   5+      5,      &   System.Windows.Controls.DataGrid  5-      -   5          Name$ 
grid_texts��5-         SelectionMode$ Single=�6+   	   SelectionUnit$	 FullRow=�6B   
�������
   AlternatingRowBackground

 �����$	��Top=�6�   ��5.      �� 5/       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�50      -   5        51         Columns 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Header$ id��6@      
IsReadOnly .    
Visibility$ Hidden=�6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *id+6^     55      $
 Name��6B    .  6)   �� *name+6P     56      $
 Year��6B    .  6)   �� *year+6P     57      $ Rating��6D    .  6)   �� *rating+6T   59      5:      �� 5;   
   $��10q�6   $��1��6   ��5<      �� 5=       �� Orientation$ 
Horizontal=�6   .+�5>      �� .��6   Genres5@      �� 5A      -	   5        $ b_all��5A      $��30��6,   .��5B      �� {�.   d� 	IsChecked$
 TrueY�65   5C      �� 5D      -
   5        $ b_none��5D      $��30��6-   .��5E      �� {�.  $ FalseY�65   5F      5G      .��5I      �� -   5         ��Name$ _Genres��5I      ��5J      �� 5K      .R�5L      �� -   5         5L   #   �� *	IsChecked+��6C   �� *
item.value+6}   5M      5O      5P      5Q      5R      