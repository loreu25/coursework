   M S B A M L   `   `   `  ����  CafeOrderManager%    CafeOrderManager.MenuItemDialog   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          �Name$  
ThisWindow��5   	    � Title$ 
Блюдо��5	   	   $	��500��6   $	��400��6$    � WindowStartupLocation$ CenterOwner=�5
   	      MaterialDesignPaper#���   5   	     MaterialDesignBody#���  5   	   #���  5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	   .��5      ]� $��16q�6   .+�5   
   �� $��Bottom=�6    �� Orientation$ 
Horizontal=�5      $��Right=�5      $��0,16,0,0q�5      .+�5      �� -   5        $��Отмена��5      $��0,0,8,0q�5      �� 5      -   5        $��Сохранить��5      5      5      �� 5   
   .+�5       �� -   5         ��Name$ NameTextBox��5       )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Название��5!       ! MaterialDesignOutlinedTextBox#��[ 5"      $	��0,8q�5$      ��5#      ��  �� ElementName$ 
ThisWindow��	 �� Path$	 DishName�
 �� UpdateSourceTrigger$
 PropertyChanged=�5$      �� 5&      -   5        $ DescriptionTextBox��5&      $ Описание��5'      #��[ 5(       �� TextWrapping$
 Wrap=�5*       �� AcceptsReturn$
 True��5+      $	��100��5,      $	��0,8q�5-      ��5)      �� $ 
ThisWindow��$	 DishDescription�$
 PropertyChanged=�5-      �� 5/      -   5        $ PriceTextBox��5/      $ Цена��50      #��[ 51      $	��0,8q�54      ��52      �� $ 
ThisWindow��$	 	DishPrice�$
 PropertyChanged=� �� StringFormat$ N2��54      �� 56      -   5         ��Name$ CategoryComboBox��56      $ Категория��57       " MaterialDesignOutlinedComboBox#��[ 58      $	��0,8q�5;      ��59      �� $ 
ThisWindow��$	 
Categories� �� SelectedItem 5:      �� $ 
ThisWindow��$	 DishCategory�$
 PropertyChanged=�5;      �� 5=      -   5         ��Name$ IsAvailableCheckBox��5=      $��Доступно��5>        MaterialDesignCheckBox#��[ 5@      $
��0,16q�5A       d� 	IsChecked 5?      �� $ 
ThisWindow��$	 DishIsAvailable�$
 PropertyChanged=�5A      5B      5C      5D      