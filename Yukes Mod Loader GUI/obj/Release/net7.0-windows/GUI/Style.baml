   M S B A M L   `   `   `  ���� O  KYukes Mod Loader GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Yukes_Mod_Loader_GUI.GUIYukes_Mod_Loader_GUI.GUI  C&clr-namespace:Yukes_Mod_Loader_GUI.GUIYukes_Mod_Loader_GUI.GUI  M+clr-namespace:Yukes_Mod_Loader_GUI.ControlsYukes_Mod_Loader_GUI.Controls  W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089b ^SharpVectors.Converters.Wpf, Version=1.8.2.0, Culture=neutral, PublicKeyToken=b532964b8548be77  �� MergedDictionaries%    Yukes_Mod_Loader_GUI.GUI.Colors8   2Yukes_Mod_Loader_GUI.GUI.BoolToVisibilityConverter   BoolToVisibilityConverter  	TextStyle �� 
TargetType �� Property  	LightText �� Value  WindowBackGradient �� 
StartPoint �� EndPoint  DarkerBackColor � Color � Offset  DarkBackColor  DarkFore �� 
TargetType	 �� 
Background
 �� Bullet �� RelativeSource �� 	Converter �� Triggers P� Property P� Value  	LightBack  MidText J� SnapsToDevicePixels �� Triggers �� 
TargetName 	 AccentHover&   System.Windows.Controls.DataGrid)  #System.Windows.Controls.DataGridRow=  7System.Windows.Controls.Primitives.DataGridColumnHeader 

 Accent �� Padding*  $System.Windows.Controls.DataGridCell ��Name  DarkText �� CornerRadius(   "Yukes_Mod_Loader_GUI.Controls.Icon   Code  FontAwesome   Size   
Foreground  
DarkButton  DarkBack%  SharpVectors.Converters.SvgIcon�� H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml     0local&clr-namespace:Yukes_Mod_Loader_GUI.GUI  1svgc&http://sharpvectors.codeplex.com/svgc/  .gui&clr-namespace:Yukes_Mod_Loader_GUI.GUI  8controls+clr-namespace:Yukes_Mod_Loader_GUI.Controls       %K  &	        &	      &	 t     '�� �     7   '��     '  n    '  	    '  m    '  n    '�� �    '�� �    '  �    &	 
    '�� A    '  �      �� " ��.���  � ��# �  ��  � ��$ 14(���  � ��$ 
Arial Bold)���  � ��$ Normal$��� $ 0.5,0&�$ 0.5,1&�.��� # �  $ 0Q�� # �  $ 1Q��� " ��.���  � ��# �  ��  � �� �� " ��.R��� 
	 ���� 
 �� $��18��$��18��
������ #��z��#��z��._�� .+��� $	��✔��#��z��$��Center=�$��Center=�y��� *	IsChecked+ �� *TemplatedParent+8   ._��� $
��Left=�$��Center=�$��5,0,0,0q� P�  � {�$ False��. ���  � �� �#808080�� " ��.���  � ��# �  ��  � ��# �  ��  � ��$ 1q���  � �� �� " ��.���  � �� �� " ��.R��� $��Border��$��0q�$
 true��._���  P�  �@��
IsSelected$
 true��. ��� $ Border��# � 	  � ���� # �   � ���� # �   � ����  � �� �� " ��.R��� .���� �� "  .���  �@ HeadersVisibility$ Column=���  � ��
 ���� ��  � ��$ 0q���  � ��
 ���� ��  � ��# �  ��   �@ AlternatingRowBackground
 ���� ��  �@ CanUserResizeColumns$ False����  �@ CanUserResizeRows$ False����  �@ CanUserReorderColumns$ False����  �@ AutoGenerateColumns$ False����  �@ 
IsReadOnly$
 True���� "  .���  � ��$ 40�� P�  �@��AlternationIndex$ 0��. ���  � ��# �  �� "  .���  � ��$ 50����  � ��$ 24(���  � ��
 ���� ��  � ��# � 
 ��  � �� �� .R��� $��5,0,0,0q����� ��  � �� �� "  .R��� #��z��#��z��#��z��# z��$
 True��._��� #��z��# z #��z���� "  .���  � ��
 ���� ��  � ��$ 0q��� " ��.���  � ��$ 5q���  �@�FocusVisualStyle Z� ��  � ��# �  ��  � ��$ 2q���  � ��$ 30����  � ��$ Center=���  � ��# �  ��  � �� �� " ��.R��� #��z��#��z��#��z��$
 True��._��� $ PART_ContentHost���� " ��.���  � ��# � 
 ��  � ��# �  ��  � ��$ 0q���  � ��$
 10,5q���  �@�Cursor$
 Hand����  �@��EdgeMode$ Aliased=���  � �� �� " ��.R��� $��Border��$ 0��$��2q�#��� 
 #��� 
 ._��� $��2q�$��Center=�$��Center=�$
��True�� P�  �@J�IsKeyboardFocused$
 true��. ��� $ Border�� � �� �#202020P�  �@��IsDefaulted$
 true��. ��� $ Border�� � �� �#202020P�  �@J�IsMouseOver$
 true��. ��� $ Border��# � 	  � ���� $ Border��# � 	  � ��P�  � ��$
 true��. ��� $ Border�� � �� �#E0E0E0�� $ Border�� � �� �#606060P�  � {�$ false��. ��� $ Border�� � �� �#EEEEEE�� $ Border�� � �� �#AAAAAA��  � �� �#888888 P�  � {�$ False��. ���  � �� �#808080��  � �� �#3C3C3CP�  �@J�IsMouseOver$
 True��. ���  � ��# � 	 ��  � ��# � 	 �� "  .���  � �� �� "  .R��� #��z #���  $
��0, 5q�$��Center=�$��Center=�#��z #��z �� " ��.���  � ��
 ���� ��  � ��# � 
 ��  � ��$ 0q���  � ��$	 5,5q���  �@�Cursor$
 Hand����  � ��$ 72����  � ��$ 64����  � ��$ 10, 0q���  �@��EdgeMode$ Aliased=���  � �� �� " ��.R��� $��Border��$ 0��$��0q�
������ ._��� $��2q�$��Center=�$��Center=�$
��True�� P�  �@J�IsKeyboardFocused$
 true��P�  �@��IsDefaulted$
 true��P�  �@J�IsMouseOver$
 true��. ��� $ Border��# � 	  � ��P�  � ��$
 true��. ��� $ Border�� � �� �#E0E0E0P�  � {�$ false��. ��� $ Border�� � �� �#EEEEEE P�  � {�$ False��. ���  � �� �#808080��  � �� �#3C3C3CP�  �@J�IsMouseOver$
 True��. ���  � ��# � 	 ��  � ��# � 	 �� " ��.���  � ��# �  ��  � ��# �  ��  � ��$ 2q��� "  .���  �@ Fill# �  