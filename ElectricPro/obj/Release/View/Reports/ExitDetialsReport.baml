   M S B A M L   `   `   `  ����   ElectricPro0    *ElectricPro.View.Reports.ExitDetialsReport   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _MaterialDesignThemes.MahApps, Version=0.2.8.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   <d2http://schemas.microsoft.com/expression/blend/2008   X TXceed.Wpf.Toolkit, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45wpsfx)http://schemas.xceed.com/wpf/xaml/toolkit    N� Title$  MainP�� � 	Resources    ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius �� Triggers	 P� Property
 P� Value �� 
TargetName  ButtonStyle2��%�  &	        &	 �    �� " ��.���  � �� �� " ��.R�� .+��� $ border�����#373E46$��45��$ 5��$	��Top=�._��� �� $ ��/Assets/img/search_img.png��$
��Left=�$	��Top=�$��	10,-2,0,0q�$
��None=��� ���#E9EBED$��Center=�$
��Left=�$
��Bold"�$��28(�$��بحث��$
��Lato)�$��	70,3,10,0q�$��45�� P� 	 �@J�IsMouseOver
 . . ��� $ border�� � �� �#292F34P� 	 � ��
 . . ��� $ border�� � �� �#292F34�� " ��.���  � �� �� " ��.R�� .+��� $ border�����#373E46$��45��$ 5��$	��Top=�._��� �� $��/Assets/img/print_img.png��$
��Left=�$	��Top=�$��	10,-2,0,0q�$
��None=��� ���#E9EBED$��Center=�$
��Left=�$
��Bold"�$��25(�$��
طباعة��$
��Lato)�$��	70,6,10,0q�$��45�� P� 	 �@J�IsMouseOver
 . . ��� $ border�� � �� �#292F34P� 	 � ��
 . . ��� $ border�� � �� �#292F34.��� ���#282D33 � RowDefinitions �� $��55 ��� $��60 ��� $��* �.+�� $��0��.+���  �� Orientation$ 
Horizontal=�$��0,0,20,0q�$��Right=�.+��� -    ��Name$ FilterByCombo��$	��220��$��40��$��Center=� �� SelectedIndex$ 0��$��Right=�$��0,0,10,0q�
�������$
��Lato)�$��DemiBold"�$��24(��� $��: اسم المحطة��$��0,0,0,0q�$
��Lato)�$��30(�$��Center=����#FFDD00$
��Bold"��� $ 
Horizontal=�$��0,0,10,0q�$��Center=�.+��� -   $ FilterByComboExit��$	��220��$��40��$��Center=�$ 0��$��Right=�$��0,0,10,0q�
�������$
��Lato)�$��DemiBold"�$��24(��� $��: اسم المخرج��$��0,0,0,0q�$
��Lato)�$��30(�$��Center=����#FFDD00$
��Bold"��� $&�� مجموع ساعات العمل��$��	28,10,0,0q�$��Center=�$
��Left=�$
��Lato)�$��24(�$
��Bold"�$��ExtraExpanded&����	#FFFFDD00� $��1���� $ 
Horizontal=�$��1��$��Right=�$��0,0,20,0q�.+�&   Xceed.Wpf.Toolkit.DateTimePicker  -   $��	startDate��$��0,0,12,0q�$��42�����#373E46
�������$��Center=�$	��250��$
��Lato)�&   Xceed.Wpf.Toolkit.DateTimeUpDown   FormatString$ MM/dd/yyyy HH:mm:ss�� J� IsManipulationEnabled . $
��Bold"�$��21(�   Format$ Custom=�,  &Xceed.Wpf.Toolkit.Primitives.InputBase   TextAlignment$ Center=��� $��: من��$��0,0,0,0q�$
��Lato)�$��30(�$��Center=����#FFDD00$
��Bold"��� $ 
Horizontal=�$��1��$��Center=�$��	250,0,0,0q�.+�  -   $��endDate��$��0,0,12,0q�$��42��$��Right=����#373E46
�������$��Center=�$	��250��$
��Lato)�$ MM/dd/yyyy HH:mm:ss��$ Custom=�$
��Bold"�$��21(�$ Center=��� $��: إلى��$��0,0,0,0q�$
��Lato)�$��30(�$��Center=����#FFDD00$
��Bold"��� $ 
Horizontal=�$��1��$
��Left=�$��	465,0,0,0q�.+��� -    ��Name$ enterBtn��$��Center=�#��[  �� $ 
Horizontal=�$��1��$
��Left=�$��	270,3,0,0q�.+��� -   $ PrintBtn��$��Center=�#��[ � $��1��$	��230��$
��Left=�$	��Top=�$��15,6,0,0q�.+��� -   $ border�����#373E46$��45��$ 5��$	��Top=��� -    ��Name$ 	totalHtxt��$�� �� �� Padding$
 55,5q�$��25(�
�������$��DemiBold"�$
��Lato)�&   System.Windows.Controls.DataGrid  -	     Name$ dataGrid��$��	10,0,10,8q����#373E46   AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $��18(����	#FFFFDD00$   HorizontalScrollBarVisibility$ Visible=�   
IsReadOnly . $��2��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
المدة��   MinWidth$	 100Q�    Width5  /System.Windows.Controls.DataGridLengthConverter$  * !   
Foreground! �	#FFFFDD00"   FontSize$" 20Q�#   
FontWeight$# DemiBold"�1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ �� *dir+  $ وقت الإيقاف��$	 100Q�$  * ! �	#FFFFDD00$" 20Q�$# DemiBold"�$ �� *	endTime+  $ وقت التشغيل��$ 80Q�$  * ! �	#FFFFDD00$" 20Q�$# DemiBold"�$ �� *	startTime+'	  !MaterialDesignThemes.Wpf.Snackbar	  -
   % 	 Name$% SnackbarFive��$��2��& 	  MessageF
  @MaterialDesignThemes.Wpf.Converters.SnackbarMessageTypeConverter$*& $..........خطأ في الإتصال
 $	��600��$��Bottom=�$��Stretch=����	#FFED7147
�������$��Segoe UI Black)�$��32(�$
��Bold"�