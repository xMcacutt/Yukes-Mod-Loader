   M S B A M L   `   `   `  ���� O  KYukes Mod Loader GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Yukes_Mod_Loader_GUI.ControlsYukes_Mod_Loader_GUI.Controls  Y1clr-namespace:Yukes_Mod_Loader_GUI.GUI.ViewModels#Yukes_Mod_Loader_GUI.GUI.ViewModels  U QSystem.Runtime, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7,clr-namespace:System;assembly=System.RuntimeSystem +    %Yukes_Mod_Loader_GUI.ModListingWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @controls+clr-namespace:Yukes_Mod_Loader_GUI.Controls     H
viewModels1clr-namespace:Yukes_Mod_Loader_GUI.GUI.ViewModels     <sys,clr-namespace:System;assembly=System.Runtime       � Title$  Yuke's PS2 Mod Loader��$	��600��$	��900��   	TextStyle#��[    WindowBackGradient#���   � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � AllowsTransparency$
 True�� � DataContext =   7Yukes_Mod_Loader_GUI.GUI.ViewModels.ModListingViewModel  .���� .+�� $��15, 5, 0, 5q� � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�$��10, 0q�.+�(   "Yukes_Mod_Loader_GUI.Controls.Icon    IcoCode   Code# �   
 Accent   
Foreground# �  	   Size$	 40Q��� $��YPS2ML��$��Bottom=�$��10,0,0,0q�$��50(��� $��1��$��Right=�$ 
Horizontal=�.+���   
DarkButton#��[ .���� .+�    
IcoRefresh# �  # �  $	 30Q��� $��Center=�$��Refresh��  MidText#���  �� #��[ .���� .+�    IcoSettings# �  # �  $	 30Q��� $��Center=�$��Settings��#���  �� #��[ .���� .+�    IcoTools# �  # �  $	 30Q��� $��Center=�$��Tools��#���  �� #��[ .���� .+�   	 IcoHelp# � 	 # �  $	 30Q��� $��Center=�$
��Help��#���  �� #��[ .���� .+�   

 IcoMin# � 
 # �  $	 30Q��� $��Center=�$	��Min��#���  �� #��[ .���� .+�    IcoExit# �  # �  $	 30Q��� $��Center=�$
��Exit��#���  �� &   System.Windows.Controls.DataGrid  -   $��ModsDataGrid��
   SelectionMode$
 Single=�$��10, 0, 10, 10q�$	��500������ *Mods+   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Mod Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    CellTemplate �� .R��� $��Center=����� *EditableName+z�T�  T� X$ 5Q�  $ 	Game Name��$	 250  �� .R��� $��Center=����� *Game+z�T� $ 5Q�  $
 Date��$	 150  �� .R��� $��Center=����� *Date+z�T� $ 5Q�  $  ��$	 124  �� .R���  ��Name$ ButtonStackPanel��$y�	Collapsed=�$ 
Horizontal=�$��Center=�$��Center=�.+��� $��
0, 0, 2, 0q�$��38��$��38��$��Center=�.��    IcoEdit# �  �� $��0q�$��38��$��38��$��Center=�.��    	IcoDelete# �  �� $��
2, 0, 0, 0q�$��38��$��38��$��Center=�.��    IcoLoad# �  ����  �� 
TargetType" �� �� Triggers ��  �� Binding ��  �� Path$ 
IsSelected� �� RelativeSource ��  �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� Value	 False.a���  �� Property � y� �� Value$ 	Collapsed=���  �� $ 
IsSelected� �� "   True.a���  � y�$ Visible=�