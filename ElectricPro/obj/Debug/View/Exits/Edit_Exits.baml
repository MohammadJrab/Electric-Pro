   M S B A M L   `   `   `  ����F  BElectricPro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:ElectricPro.View.ExitsElectricPro.View.Exits  5      '    !ElectricPro.View.Exits.Edit_Exits   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:ElectricPro.View.Exits     5      c _MaterialDesignThemes.MahApps, Version=0.2.8.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5      X TXceed.Wpf.Toolkit, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45wpsfx)http://schemas.xceed.com/wpf/xaml/toolkit  5	        N� Title$  MainP��5       � 	Resources 5         ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius �� Triggers	 P� Property
 P� Value �� 
TargetName��%b  &	        �� 5   
   " ��5      .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $ border��6%   ���#373E46$��60��5   !   $ 5��6-   $	��Top=�5   !   ._�5   "   �� 5      �� 5      $��/Assets/img/edit_icon.png��6$   $
��Left=�5       $��Center=�5        $��18,0,0,0q�5!       $
��None=�5"       �� 5$      ���#E9EBED$��Center=�5%   $   $
��Left=�5&   $   $
��Bold"�5'   $   $��28(�5(   $   $��تعديل المخرج��5)   $   $
��Lato)�5*   $   $��	70,3,10,0q�5+   $   $��45��67   5,       5.      P� 50      	 �@J�IsMouseOver
 . . �52   "   �� $ border��53   %    � �� �#292F3454   %   55      P� 56      	 � ��
 . . �58   "   �� $ border��59   %    � �� �#292F345:   %   5<      5?      5A      5C      5F      .��5G      � ���#282D33 � RowDefinitions 5H   
   �� 5J      $��* �6   .+�5L   
   � $��0��6   .+�5M      �� $��: تعديل المخرج��6   $��	0,15,20,0q�6.   $
��Lato)�6B   $��45(�6T   $	��Top=�6b   $��Center=�6z   ���#FFDD00$
��Bold"�6�   �� 5O       �� Orientation$ Vertical=�6   $	��Top=�60   $��	0,120,0,0q�6H   .+�5Q      �� $ 
Horizontal=�6   $��Center=�66   $��Center=�6S   .+�5S      �� -   5         ��Name$ FilterByCombo��5S      $	��220��66   $��40��6B   $��Center=�6N    �� SelectedIndex$ 0��6i   $��Right=�6|   $��0,0,15,0q�6�   
�������$
��Lato)�6�   $��DemiBold"�6�   $��24(�6�   6'  �� 5T      $��: اسم المحطة��6    $��0,0,0,0q�64   $
��Lato)�6F   $��40(�6X   $��Center=�6f   ���#FFDD00$
��Bold"�6�   5V      �� 5W      $ 
Horizontal=�6   $��Center=�66   $��Center=�6S   $��0,25,0,0q�6n   .+�5Y      �� -   5        $ ExitByCombo��5Y      $	��220��64   $��40��6@   $��Center=�6L   $ 0��6g   $��Right=�6z   $��0,0,15,0q�6�   
�������$
��Lato)�6�   $��DemiBold"�6�   $��24(�6�   6�   �� 5Z      $��: اسم المخرج��6    $��0,0,0,0q�64   $
��Lato)�6F   $��40(�6X   $��Center=�6f   ���#FFDD00$
��Bold"�6�   5\      �� 5]      $ 
Horizontal=�6   $��Center=�67   .+�5^      �� $ 
Horizontal=�6!   $��
121,25,0,0q�6:   .+�5_      #  MaterialDesignThemes.Wpf.Card     UniformCornerRadius$ 5Q�6.   $��45��6F   $	��225��6R   $��5,0,10,0q�6^   ���	#FF373737.��5`      �� -   5         ��Name$ 
NewExitTxt��5`   &   $��22(�6:   $
��Bold"�6H   $	��220��6Z   $��40��6f   ���	#FFFDFDFD �� TextAlignment$ Right=�6�   ��6s   Z� 6�   5a      �� 5b      $,��&: الاسم الجديد للمخرج��6$   $��0,0,0,0q�6A   $
��Lato)�6S   $��39(�6e   $��Center=�6s   ���#FFDD00$
��Bold"�6�   5c      5d      �� 5e      $ 
Horizontal=�6   $��Center=�67   $��
0,40,212,0q�6T   .+�5f      �� -   5         ��Name$ EditBtn��5f      $��Center=�6/   #��[  6J   $��60��6{   6�   5g      5h      5j      '  !MaterialDesignThemes.Wpf.Snackbar  5k   
   -   5          Name$ SnackbarFive��5k   #      MessageF  @MaterialDesignThemes.Wpf.Converters.SnackbarMessageTypeConverter$* $..........خطأ في الإتصال 6;   $	��600��6_   $��Bottom=�6k   $��Stretch=�6�   ���	#FFED7147
�������$��Segoe UI Black)�6�   $��32(�6�   $
��Bold"�6�     5l   
   -   5        $ SnackbarState��5m      $��34(�5l   #   $
��Bold"�61   $��0,0,0,20q�6C   ���	#FF3E79E6$��Center=�5n      $��Bottom=�6*   . 5o      .  (MaterialDesignThemes.Wpf.SnackbarMessage  -   5          Name$ MessageTextState��5o   -   ���	#FF3E79E6$��28(�6�   $
��Bold"�6�   $��Arial Rounded MT Bold)�6�      ActionContent$ OK��5p      $��
رسالة��5q      5r      5s      5t      