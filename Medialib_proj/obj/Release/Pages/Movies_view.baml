   M S B A M L   `   `   `  ���� H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Medialib_proj.PagesMedialib_proj.Pages  %    Medialib_proj.Pages.Movies_view   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:Medialib_proj.Pages       N� Title$  Movies_item��.���  � RowDefinitions �� .+���  �� TabStripPlacement$	 Top=�.@��� $��Description��.���  �� $
��113* ��� $	��98* �.+�]� $��5,0,0,0q�.+���  �� Orientation$ 
Horizontal=�$	��Top=�.+��� $��Name:��$��1��$��13(�$
��Bold"��� -    ��Name$ lab_name��$��nametext��$��13(��� -   $ lab_year��$
��Year��$��13(�$	��Top=��� $ 
Horizontal=�$	��Top=�.+��� $��Genre:��$��13(�$
��Bold"��� -   $ 	lab_genre��$��	genrelist��$��13(��� $ 
Horizontal=�$	��Top=�.+��� $��Directed by:��$��13(�$
��Bold"��� -   $ lab_dirlist��$��directorlist��$��13(��� $ 
Horizontal=�$	��Top=�.+��� $��Actors:��$��13(�$
��Bold"��� -   $ lab_actlist��$��	actorlist��$��13(��� $ 
Horizontal=�$	��Top=�.+��� $��	Location:��$��13(�$
��Bold"��� -   $ 	lab_locat��$��location��$��13(��� $ 
Horizontal=�$	��Top=�.+��� $��	Duration:��$��13(�$
��Bold"��� -   $ lab_duration��$��duration��$��13(��� $ 
Horizontal=�$	��Top=�.+��� $��Rating:��$��13(�$
��Bold"��� -   $ 
lab_rating��$��rating��$��13(��� $��1��$��5q�$
��Auto=�.���� -	    ��Name$ tb_descript��$
��Wrap=�$��	TextBlock��$��13(��� -
   $��Comments��.��� .+�]� ��. .+��� $ 
Horizontal=�$��5,5,5,0q�$	��Top=�.+��� $��Your grade: ��$��18(�$��Center=��� .���� -    ��Name$ star_1��$!��../Resourses/img_unstar.png��$��60��$��60���� .���� -   $ star_2��$!��../Resourses/img_unstar.png��$��60��$��60���� .���� -   $ star_3��$!��../Resourses/img_unstar.png��$��60��$��60���� .���� -   $ star_4��$!��../Resourses/img_unstar.png��$��60��$��60���� .���� -   $ star_5��$!��../Resourses/img_unstar.png��$��60��$��60���� $��Bottom=�$
��Auto=�.���� -    ��Name$ comment_field��$��5q� �� TextWrapping$
 Wrap=�$	��100���� -   	 ��Name$	 
lb_comment��|�. $��5,5,5,0q�$	��Top=����� .R��� -   
���   �$��1,1,0,1q�

 � Tag
 �� *id+._��� .+��� 
�������.���� ���� *data+�� $��7q����� *	comment+