   M S B A M L   `   `   `  ����H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Medialib_proj.PagesMedialib_proj.Pages  5      '    !Medialib_proj.Pages.Userlist_view   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:Medialib_proj.Pages     5        N� Title$  Userlist_view��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��47* �6   �� 5      $
��153* �6    � RowDefinitions 5   
   �� 5      $��33 �6   �� 5      .+�5   
   �� -   5         ��Name$ 	tb_search��5      $��5,5,50,5q�6%    �� TextWrapping$
 Wrap=�67   $��	Search...��6K   
�������$��14(�6n   $��2��6�   �� 5   
   -   5         ��Name$ b_search��5      $��0,5,5,5q�6#   $��Right=�64   $��40��6P   $��1��6r   .��5      �� $!��../Resourses/img_search.png��6   $��1q�69   5      �� 5   
   $��10q�6   $��1��6   ��5      �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� .��6   
Privileges5      �� 5      -   5        $ b_all��5      $��30��6,   .��5      �� {�.   d� 	IsChecked$
 TrueY�65   5       �� 5!      -   5        $ b_none��5!      $��30��6-   .��5"      �� {�.  $ FalseY�65   5#      5$      .��5&      �� -   5         ��Name$ _Privileges��5&      ��5'      �� 5(      .R�5)      �� -   5         5)   #   �� *	IsChecked+��6C   �� *	Privilege+6|   5*      5,      5-      &   System.Windows.Controls.DataGrid  5.   
   -   5        	  Name$	 
grid_users��5.      
�������
   AlternatingRowBackground

 �����$��1��6_   $��1��6o   ��5/      �� 50       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�51      -   5        52         Columns 54      0  *System.Windows.Controls.DataGridTextColumn  55      ,  &System.Windows.Controls.DataGridColumn   Header$ id��6<      
IsReadOnly .    
Visibility$ Hidden=�6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *id+6Z     56      $ Login��6?    .  6%   �� *login+6N   4  .System.Windows.Controls.DataGridCheckBoxColumn  57      $ Admin��6C    .  6)   �� *admin+6R     58      $ Moderor��6E    .  6)   �� *	moderor+6V     59      $ Film manager��6J    .  6)   �� *film_manager+6`     5:      $ Music manager��6K    .  6)   �� *music_manager+6b     5;      $ Picture manager��6M    .  6)   �� *picture_manager+6f     5<      $ Text manager��6J    .  6)   �� *text_manager+6`   5>      5?      5@      