   M S B A M L   `   `   `  ����  CafeOrderManager"    CafeOrderManager.OrderDialog   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Детали заказа��5   	   $	��600��5	   	   $	��800��5
   	    � WindowStartupLocation$ CenterOwner=�5   	      MaterialDesignPaper#���   5   	     MaterialDesignBody#���  5   	   #���  5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	   .��5      � $��16q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5#   
   �� $��0��6   $��0��6    $��2��60   $��Меню��5$        MaterialDesignGroupBox#��[ 5%      .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ PrimaryLight=�5&      $��0,0,8,0q�5'      .��5(      ]� .+�5)      �� -   5         ��Name$ MenuItemsListView��5)      $	��Top=�5*       �� View 5+      �� 5,      .P�5-      �� $��Название��6-   	 �� Width$		 150��6e   
 �� DisplayMemberBinding
 6?   �� *Name+6e   �� 5.      $��Цена��6-   $		 100��6;    �� CellTemplate 5/   "   �� 50   &   .R�51   *   �� ��64   �� *Price+ �� StringFormat$ 
{0:N0} ₽��52   '   54      55      57      58      59      �� 5<   
   $��1��6   $��1��6"   $��Center=�5=      $��8q�5>      .+�5?      �� -   5        $��Добавить →��5?        MaterialDesignRaisedButton#��[ 5A      $��0,0,0,8q�5B      �� 5C      -   5        $��← Удалить��5C      #��[ 5E      5F      �� 5I   
   $��0��6   $��2��6    $��2��60   $��Текущий заказ��5J      #��[ 5K      $ SecondaryLight=�5L      $��8,0,0,0q�5M      .��5N      ]� .+�5O      �� -   5        $ OrderItemsListView��5O      $	��Top=�5P       5Q      �� 5R      .P�5S      �� $��Название��6-   $		 150��6n   
 6?   �� *MenuDish.Name+6n   �� 5T      $��Количество��6-   $	 80��6k   
 6A   �� *
Quantity+6k   �� 5U      $��Цена��6-   $		 100��6;    5V   "   �� 5W   &   .R�5X   *   �� ��64   �� *Price+$ 
{0:N0} ₽��5Y   '   5[      5\      5^      5_      5`      �� 5c   
   $��2��6   $��2��6"    �� Orientation$ 
Horizontal=�5d      $��Right=�5e      $��	0,16,0,16q�5f      .+�5g      �� $��Итого: ��6    $  MaterialDesignHeadline6TextBlock#��[ 5h      $��Center=�5i      �� 5j      -   5         ��Name$ TotalAmountText��5j      #��[ 5l      $��Center=�5m      ��5k      �� *TotalAmount+$ 
{0:N0} ₽��5m      5n      �� 5q   
   $��3��6   $��0��6"   $��3��62   $ 
Horizontal=�5r      $��Right=�5s      .+�5t      �� -   5        $��Отмена��5t        MaterialDesignFlatButton#��[ 5v      $��0,0,8,0q�5w      �� 5x      -   5        $��Создать заказ��5x      #��[ 5z      5{      5|      5}      