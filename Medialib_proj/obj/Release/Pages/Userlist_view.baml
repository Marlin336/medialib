   M S B A M L   `   `   `  ���� H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Medialib_proj.PagesMedialib_proj.Pages  '    !Medialib_proj.Pages.Userlist_view   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:Medialib_proj.Pages       N� Title$  Userlist_view��.���  � ColumnDefinitions �� $	��47* ��� $
��153* � � RowDefinitions �� $��33 ��� .+��� -    ��Name$ 	tb_search��$��5,5,50,5q� �� TextWrapping$
 Wrap=�$��	Search...��
�������$��14(�$��2���� -    ��Name$ b_search��$��0,5,5,5q�$��Right=�$��40��$��1��.���� $!��../Resourses/img_search.png��$��1q��� $��10q�$��1������  �� Orientation$ 
Horizontal=�.+��� .��
Privileges�� -   $ b_all��$��30��.���� {�.   d� 	IsChecked$
 TrueY��� -   $ b_none��$��30��.���� {�.  $ FalseY�.���� -    ��Name$ _Privileges������ .R��� -    �� *	IsChecked+���� *	Privilege+&   System.Windows.Controls.DataGrid  -   	  Name$	 
grid_users��
�������
   AlternatingRowBackground

 �����$��1��$��1������  �� 
TargetType)  #System.Windows.Controls.DataGridRow"  .�-      Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ id��   
IsReadOnly .    
Visibility$ Hidden=�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *id+  $ Login�� .  �� *login+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ Admin�� .  �� *admin+  $ Moderor�� .  �� *	moderor+  $ Movie manager�� .  �� *film_manager+  $ Music manager�� .  �� *music_manager+  $ Picture manager�� .  �� *picture_manager+  $ Text manager�� .  �� *text_manager+