   M S B A M L   `   `   `  ���� d  `DotNetProjects.Input.Toolkit, Version=6.1.94.0, Culture=neutral, PublicKeyToken=79778c5f2eed289bgKclr-namespace:System.Windows.Controls;assembly=DotNetProjects.Input.ToolkitSystem.Windows.Controls   ElectricPro   ElectricPro.View.MainP   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _MaterialDesignThemes.MahApps, Version=0.2.8.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   <d2http://schemas.microsoft.com/expression/blend/2008   [ControlKclr-namespace:System.Windows.Controls;assembly=DotNetProjects.Input.Toolkit     N� Title$  MainP�� � 	Resources    ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius �� Triggers	 P� Property
 P� Value �� 
TargetName  ButtonStyle2.  (MaterialDesignThemes.Wpf.SnackbarMessage �� BasedOn��%�  &	        &	 �    '  �    7 �� " ��.���  � �� �� " ��.R�� .+��� $ border�����#373E46$��45��$ 5��$	��Top=�._��� �� $ ��/Assets/img/search_img.png��$
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
 . . ��� $ border�� � �� �#292F34�� 8   "  .���  � ��$ Center=�.��� ���#282D33 � RowDefinitions �� $��60 ��� $��* �.+�� $��0��.+��� $%�� حالة مخارج اليوم��$��Center=�$��Right=�$��	0,0,175,0q�$
��Lato)�$��DemiBold"�$��30(����	#FFFFDD00��  �� Orientation$ 
Horizontal=�$��Center=�$��	0,0,160,0q�.+�#  MaterialDesignThemes.Wpf.Card     UniformCornerRadius$ 5Q�$��45��$	��186��$��25,0,0,0q�.��-   'System.Windows.Controls.AutoCompleteBox  -     Name$ FilterByText��$��22(�$
��Bold"�$	��180��$��40�����	#FFFFDD00��Z� �� $��: بحث��$��Center=�$��Right=�$��0,0,0,0q�$
��Lato)�$��DemiBold"�$��28(����	#FFFFDD00�� -    ��Name$ FilterByCombo��$	��220��$��40��$	��Top=� �� SelectedIndex$ 0��$��Center=�$��
40,10,10,0q����	#FFFFDD00$
��Lato)�$��DemiBold"�$��24(��� $��: حسب��$��Center=�$��Right=�$��0,0,0,0q�$
��Lato)�$��DemiBold"�$��28(����	#FFFFDD00� $��1��.+��� -    ��Name$ listView��$��Disabled=�$��0,10,0,0q����� .R��  � Orientation$ 
Horizontal=�$��Center=����� .R�    Name$ SelectُExit��$��0,0,0,0q�$	��330��$	��350��$ 5Q����� *	ColorCard+.���� $��0,5,5,0q�.+��� $ 
Horizontal=�$��Right=�$��0,0,5,0q�.+��� $��Center=�$��0,0,4,0q�$��Right=�$
��Wrap=�$
��Lato)�$��DemiBold"�$��28(����#FFDD00 �� TextAlignment$ Right=����� *	Station+�� $��: اسم المحطة��$��Center=�$��Right=�$��0,0,0,0q�$
��Lato)�$��DemiBold"�$��28(�
��������� $ 
Horizontal=�$��Right=�$��0,10,5,0q�.+��� $��Center=�$��0,0,4,0q�$��Right=�$
��Wrap=�$
��Lato)�$��DemiBold"�$��28(����#FFDD00$ Right=����� *Exit+�� $��: اسم المخرج��$��Center=�$��Right=�$
��Lato)�$��DemiBold"�$��28(�
��������� $ 
Horizontal=�$��Right=�$��0,10,5,0q�.+��� $��Center=�$��0,0,4,0q�$��Right=�$
��Wrap=�$
��Lato)�$��DemiBold"�$��28(����#FFDD00$ Right=����� *Amper+�� $��: الأمبير��$��Center=�$��0,0,4,0q�$��Right=�$
��Lato)�$��DemiBold"�$��28(�
��������� $ 
Horizontal=�$��Right=�$��0,10,5,0q�.+��� $��Center=�$��0,0,2,0q�$��WrapWithOverflow=�$
��Lato)�$��DemiBold"�$��20(����#FFDD00$ Right=�$	��154������ *	StartTime+�� $��: وقت التشغيل��$��Center=�$��0,0,4,0q�$��Right=�$
��Lato)�$��DemiBold"�$��28(�
��������� $ 
Horizontal=�$��Right=�$��0,10,5,0q�.+��� $��Center=�$��0,0,2,0q�$��WrapWithOverflow=�$
��Lato)�$��DemiBold"�$��20(����#FFDD00$ Right=����� *OnTime+�� $��: مدة التشغيل��$��Center=�$��0,0,4,0q�$��Right=�$
��Lato)�$��DemiBold"�$��28(�
��������� $ 
Horizontal=�$��Right=�$��0,10,5,0q�.+��� $��Center=�$��0,0,2,0q�$��WrapWithOverflow=�$
��Lato)�$��DemiBold"�$��20(����#FFDD00$ Right=�$	��154������ *
LastUpdate+�� $��: أخر تحديث��$��Center=�$��0,0,4,0q�$��Right=�$
��Lato)�$��DemiBold"�$��28(�
�������)  #MahApps.Metro.Controls.ProgressRing  -     Name$ loading��   IsActive . $	��150��$	��150��
�������'  !MaterialDesignThemes.Wpf.Snackbar  -     Name$ SnackbarFive��   MessageF  @MaterialDesignThemes.Wpf.Converters.SnackbarMessageTypeConverter$* $..........خطأ في الإتصال $	��600��$��Bottom=�$��Stretch=����	#FFED7147
�������$��Segoe UI Black)�$��32(�$
��Bold"�