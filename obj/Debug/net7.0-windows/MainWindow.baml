   M S B A M L   `   `   `  ����K  GCafeOrderManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:CafeOrderManagerCafeOrderManager  5   	   !    CafeOrderManager.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   *localclr-namespace:CafeOrderManager  5   	     � Title$  Cafe Order Manager��5	   	   $	��720��6$   $
��1280��61      MaterialDesignBody#���   5
   	     MaterialDesignPaper#���  5   	   $��Medium"�5   	   $��14(�5   	    � WindowStartupLocation$ CenterScreen=�5   	   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension   � 	Resources 5       �� 
TargetType  MaterialDesignRaisedButton �� BasedOn �� Property �� Value��%�   '��       7  �� 5   
   " ��6   8   .�5      ��  � ��$ 8q�6'   �� 5       � ��$
 16,8q�6(   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   #���  5       $��16q�5!      .+�5"      �� $��Cafe Order Manager��6    $  MaterialDesignHeadline6TextBlock#��[ 62   $��Center=�5#      �� 5$      $ 
Horizontal=�6   $��Right=�62   .+�5%      �� -   5        $��Меню��5%      6(   �� 5&      -   5        $��Заказы��5&      6*   5'      d� 5)      -   5        	 d�Name$	 ThemeToggle��5)       $  MaterialDesignSwitchToggleButton#��[ 5*      
 � ToolTip$%
 Переключить тему��5+       d� 	IsChecked$
 TrueY�5,      $��16,0,0,0q�5.      �� 5/      $��Тёмная тема��6   $��Center=�50      $��8,0,0,0q�51      52       � 55   
   -   5          �Name$ 	MainFrame��55      $��1��6#     � NavigationUIVisibility$ Hidden=�60   56      57      