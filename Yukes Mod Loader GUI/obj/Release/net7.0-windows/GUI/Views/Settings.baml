   M S B A M L   `   `   `  ���� O  KYukes Mod Loader GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Yukes_Mod_Loader_GUI.GUI.ViewModels#Yukes_Mod_Loader_GUI.GUI.ViewModels  -    'Yukes_Mod_Loader_GUI.GUI.Views.Settings   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     H
viewModels1clr-namespace:Yukes_Mod_Loader_GUI.GUI.ViewModels       � Title$  Settings��$	��480��$	��400��   	TextStyle#��[    DarkBack#���   � WindowStyle$
 None=� � AllowsTransparency$
 True�� � DataContext ;   5Yukes_Mod_Loader_GUI.GUI.ViewModels.SettingsViewModel  .���� .+���  �� Orientation$ 
Horizontal=�$��10q�.+��� $��32��  SettingsDrawingImageAccent#���  �� $��10,0,0,0q�$��30(�
Settings�� -   $��SettingsBorder��$��10q� �� CornerRadius$ 8��._��� $	��7.5q�.+��� $��
Games List��$
��Bold"�$��14(�$��0,0,0,2q��� $ 
0, 0, 0, 8��._��� -   $��GameListBox��$
��75px������ *	TempGames+ �� SelectedItem �� *SelectedGame+ �� Mode$ TwoWay=����� .R��� -    � Cursor$
 Hand��
������ 
������ $��Stretch=�	 �� 
IsReadOnly$
	 True������ *EditableName+
 �� UpdateSourceTrigger$
 PropertyChanged=��� $ 
Horizontal=�$��
0, 2.5,0,0q�$��Right=�.+��� -   $��+��$��10(�$��25��$��0, 0, 2.5, 0q��� -   $��-��$��10(�$��25��$��0, 0, 2.5, 0q��� -   $��GameSettingsPanel�� �� *SelectedGame+.+��� $��Iso files directory:��$��0,0,0,5q�� $��0,0,0,15q� � ColumnDefinitions �� $��* ��� $
��Auto �.+��� -   $��IsoDirectoryTextBox��$��0��$��Center=�$��	0,0,-15,0q����� *IsoDir+$
 PropertyChanged=��� -   $��1��$��30��$��Center=�$��30��$	��...���� $ ��Extracted files directory:��$��0,0,0,5q�� $��0,0,0,15q� �� $��* ��� $
��Auto �.+��� -	   $��FilesDirectoryTextBox��$��0��$��Center=�$��	0,0,-15,0q����� *ExtDir+$
 PropertyChanged=��� -
   $��1��$��30��$��Center=�$��30��$	��...���� $3��-Mods directory (leave blank for auto-create):��$��0,0,0,5q�� $��0,0,0,15q� �� $��* ��� $
��Auto �.+��� -   $��ModsDirectoryTextBox��$��0��$��Center=�$��	0,0,-10,0q����� *ModDir+$
 PropertyChanged=��� -   $��1��$��30��$��Center=�$��30��$	��...���� -   $��KeepDipCheckBox��$��Keep DIP on repack��$��10,0,0,0q� d� 	IsChecked �� *	KeepDip+$
 PropertyChanged=��� -   $��Center=�$��75��$��0,15,0,5q�.���� $
��24px��  SaveDrawingImageDark#��[ 