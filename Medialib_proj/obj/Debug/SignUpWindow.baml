   M S B A M L   `   `   `  ����H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Medialib_projMedialib_proj  5   	        Medialib_proj.SignUpWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Medialib_proj     5   	     � Title$  Sign Up��5   	   $	��239��6   $	��390��6&    � 
ResizeMode$ NoResize=�62   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��145* �6   �� 5      $	��53* �6    � ColumnDefinitions 5   
   �� 5      .+�5   
   �� $��0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��10, 5q�62   .+�5      �� $��Login: ��6   �� 5      -   5         ��Name$ tb_login��5      $	��270��6,   5      �� 5      $ 
Horizontal=�6   $��10, 5q�62   .+�5      �� $��
Password: ��6   G� 5      -   5         G�Name$ tb_pass��5      $	��250��6/   5      �� 5      $ 
Horizontal=�6   $��10, 5q�62   .+�5      �� $��	Confirm: ��6   G� 5      -   5        $ tb_confpass��5      $	��257��63   5      �� 5      $ 
Horizontal=�6   $��10, 5q�62   .+�5      �� $��
Language: ��6   �� 5       -   5         ��Name$ cb_lang��5       $	��188��6,   ��5!      �� 5"      .R�5#      �� ��6$   �� *lang+5$      5&      5'      5(      �� 5)   
   -   5         ��Name$
 b_ok��5)      $��Ok��6   $
��Left=�6,   $��	31,0,0,10q�6G   $��1��6Z   $��Bottom=�6g   $��33��6�   $	��130��6�   $
��Bold"�6�   $��14(�6�   6�   �� 5*   
   -   5        $ b_cancel��5*      $��Cancel��6#   $��	0,0,31,10q�64   $��1��6G   $	��130��6T   $��Right=�6`   $��33��6|   $��Bottom=�6�   $��14(�6�   6�   5,      5-      