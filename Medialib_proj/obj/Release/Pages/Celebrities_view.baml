   M S B A M L   `   `   `  ���� H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Medialib_proj.PagesMedialib_proj.Pages  *    $Medialib_proj.Pages.Celebrities_view   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:Medialib_proj.Pages       N� Title$  Celebrities_view��.��� .+��� $
��Auto=�.���� .+���  �� Orientation$ 
Horizontal=�.+��� $��Name:��$
��Bold"�$��13(��� -    ��Name$ lab_name��$
��name��$��13(��� $ 
Horizontal=�.+��� $��Birtday:��$
��Bold"�$��13(��� -   $ lab_birthday��$��birthday��$��13(��� $ 
Horizontal=�.+��� $
��Age:��$
��Bold"�$��13(��� -   $ lab_age��$	��age��$��13(��� $ Vertical=�.+��� $��Description:��$
��Bold"�$��13(��� 
���   �$��2q�._��� -    ��Name$ tb_descript��$	��5,0q�$
��Wrap=�$��13(�(Description text)�� -    ��Name$ movie_produced_part��.+��� $
��Bold"�$��Center=�.��Produced movies&   System.Windows.Controls.DataGrid  -     Name$ grid_movie_produced��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ id��   
IsReadOnly . 	   
Visibility$	 Hidden=�1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *id+  $ Original name�� . 
 �� *	orig_name+  $ Localized name�� . 
 �� *
local_name+  $
 Year�� . 
 �� *year+  $ Duration�� . 
 �� *
duration+  $ Rating�� . 
 �� *rating+�� -   $ movie_actor_part��.+��� $
��Bold"�$��Center=�.��Played in movies  -   $ grid_movie_actor��   $ id�� . $	 Hidden=�
 �� *id+  $ Original name�� . 
 �� *	orig_name+  $ Localized name�� . 
 �� *
local_name+  $
 Year�� . 
 �� *year+  $ Duration�� . 
 �� *
duration+  $ Rating�� . 
 �� *rating+�� -	   $ music_composed_part��.+��� $
��Bold"�$��Center=�.��Composed music  -
   $ grid_music_composed��   $ id�� . $	 Hidden=�
 �� *id+  $ Musician/Band�� . 
 �� *
musician+  $ Album�� . 
 �� *album+  $
 Year�� . 
 �� *year+  $ Number�� . 
 �� *number+  $
 Name�� . 
 �� *name+  $ Duration�� . 
 �� *
duration+  $ Rating�� . 
 �� *rating+�� -   $ music_sung_part��.+��� $
��Bold"�$��Center=�.��
Sung songs  -   $ grid_music_sung��   $ id�� . $	 Hidden=�
 �� *id+  $ Musician/Band�� . 
 �� *
musician+  $ Album�� . 
 �� *album+  $
 Year�� . 
 �� *year+  $ Number�� . 
 �� *number+  $
 Name�� . 
 �� *name+  $ Duration�� . 
 �� *
duration+  $ Rating�� . 
 �� *rating+�� -   $ picture_drawn_part��.+��� $
��Bold"�$��Center=�.��Drawn pictures  -   $ grid_picture_drawn��   $ id�� . $	 Hidden=�
 �� *id+  $
 Name�� . 
 �� *name+  $
 Year�� . 
 �� *year+  $ Rating�� . 
 �� *rating+�� -   $ text_written_part��.+��� $
��Bold"�$��Center=�.��Written texts  -   $ grid_text_written��   $ id�� . $	 Hidden=�
 �� *id+  $
 Name�� . 
 �� *name+  $
 Year�� . 
 �� *year+  $ Rating�� . 
 �� *rating+