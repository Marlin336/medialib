   M S B A M L   `   `   `  ���� H  DMedialib_proj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Medialib_projMedialib_proj      Medialib_proj.Add_music   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f43ext)http://schemas.xceed.com/wpf/xaml/toolkit  'localclr-namespace:Medialib_proj    � Title$  Add music record��$	��532��$	��382�� � 
ResizeMode$ NoResize=�.��� .+�]� $	��5,0q�.+��� $	��5,5q� �� Orientation$ 
Horizontal=�$	��Top=�.+��� .��Name:�� -    ��Name$ tb_name�� �� TextWrapping$ NoWrap=�$	��294���� $	��5,5q�$ 
Horizontal=�$	��Top=�.+��� .��Year:%  Xceed.Wpf.Toolkit.IntegerUpDown  -     Name$ num_year��$	��100���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   DefaultValue$
 2000X��� $	��5,5q�$ 
Horizontal=�$	��Top=�.+��� .��Album:�� ���	#FFABADB3$��1q�._��� -    ��Name$ tb_album��$	��5,0q�$	��200��$��17���� -    ��Name$ b_pickalbum��$	��5,0q�$��Albums��$��77���� $ 
Horizontal=�$	��5,5q�$	��Top=�.+��� .��	Genres:�� -   	 ��Name$	 	cb_genres��$	��208��{�.  �� -   $ b_pickgenres��$	��5,0q�$��Genres��$��79���� $ 
Horizontal=�$	��5,5q�$	��Top=�.+��� .��
Composers:�� -   $	 cb_composers��$	��185��{�.  �� -   $ b_pickcomposers��$	��5,0q�$��	Composers��$��79���� $ 
Horizontal=�$	��5,5q�$	��Top=�.+��� .��
Singers:�� -	   $	 
cb_singers��$	��206��{�.  �� -
   $ b_picksingers��$	��5,0q�$��Singers��$��79���� $	��5,5q�$ 
Horizontal=�$	��Top=�.+��� .��	Number:  -   $ 
num_number��$	��100���� $	��5,5q�$ 
Horizontal=�$	��Top=�.+��� .��	Duration:"  Xceed.Wpf.Toolkit.TimePicker  -   
  Name$
 tp_duration��&   Xceed.Wpf.Toolkit.DateTimeUpDown   Format$ LongTime=�5  /Xceed.Wpf.Toolkit.Primitives.DateTimePickerBase   ShowDropDownButton .  �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   DefaultValue$ 0:00:00X�$��24��$	��134��$	��Top=��� $	��5,5q�$ 
Horizontal=�$	��Top=�.+��� .��Description:�� -   $ tb_descript��$
 Wrap=�$	��270��$	��150���� -   $ b_add��$	��Add��$��5q�$	��332��