   M S B A M L   `   `   `  ���� H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Medialib_proj.PagesMedialib_proj.Pages  %    Medialib_proj.Pages.Texts_start   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:Medialib_proj.Pages       N� Title$  Texts_start��.���  � ColumnDefinitions �� $	��47* ��� $
��153* � � RowDefinitions �� $��33 ��� .+��� -    ��Name$ 	tb_search��$��5,5,50,5q� �� TextWrapping$
 Wrap=�$��	Search...��
�������$��14(�$��2���� -    ��Name$ b_search��$��0,5,5,5q�$��Right=�$��40��$��1��.���� $!��../Resourses/img_search.png��$��1q�]� $��1��$��1����. .+�� -    �Name$ manager_panel��$��Bottom=�$��40��$	��0,5q� �� �� $��3* ��� �� $��3* ��� �� $��3* ��� .+��� -   $ b_add��$��1��{�. .���� $��../Resourses/img_plus.png���� -   $ b_del��$��3��{�.  .���� $ ��../Resourses/img_minus.png���� -   $ b_edit��$��5��{�.  .���� $��../Resourses/img_edit.png��&   System.Windows.Controls.DataGrid  -     Name$ 
grid_texts��   SelectionMode$ Single=�	   SelectionUnit$	 FullRow=�
�������
   AlternatingRowBackground

 �����$	��Top=�����  �� 
TargetType)  #System.Windows.Controls.DataGridRow"  .�-      Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ id��   
IsReadOnly .    
Visibility$ Hidden=�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *id+  $
 Name�� .  �� *name+  $
 Year�� .  �� *year+  $ Rating�� .  �� *rating+�� $��10q�$��1������  �� Orientation$ 
Horizontal=�.+��� .��Genres�� -	   $ b_all��$��30��.���� {�.   d� 	IsChecked$
 TrueY��� -
   $ b_none��$��30��.���� {�.  $ FalseY�.���� -    ��Name$ _Genres������ .R��� -    �� *	IsChecked+���� *
item.value+