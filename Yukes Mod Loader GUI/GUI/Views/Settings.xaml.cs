using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Yukes_Mod_Loader_GUI.GUI.Models;
using Yukes_Mod_Loader_GUI.GUI.ViewModels;
using Cursors = System.Windows.Input.Cursors;
using TextBox = System.Windows.Controls.TextBox;

namespace Yukes_Mod_Loader_GUI.GUI.Views;

public partial class Settings : Window
{
    public Settings()
    {
        InitializeComponent();
        MouseDown += delegate{DragMove();};
        if (DataContext is SettingsViewModel viewModel)
        {
            viewModel.Initialize();
            if (viewModel.Games.Count != 0) viewModel.SelectedGame = viewModel.Games[0];
        }
    }

    private void AddGame_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SettingsViewModel viewModel)
        {
            viewModel.AddGame();
        }
    }

    private void RemoveGame_Click(object sender, RoutedEventArgs e)
    {
        // Logic to remove the selected game from the list
        if (GameListBox.SelectedIndex != -1 && DataContext is SettingsViewModel viewModel)
        {
            viewModel.RemoveGame((GameListBox.SelectedItem as GameViewModel).EditableName);
        }
    }

    private void GameListBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
        if (GameListBox.SelectedIndex != -1)
        {
            GameSettingsPanel.Visibility = Visibility.Visible;
        }
        else
        {
            GameSettingsPanel.Visibility = Visibility.Collapsed;
        }
    }

    private void SaveSettings_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void SelectIsoDirectory_Click(object sender, RoutedEventArgs e)
    {
        using var folderDialog = new FolderBrowserDialog();
        var result = folderDialog.ShowDialog();
        if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
        {
            (DataContext as SettingsViewModel).SelectedGame.IsoDir = folderDialog.SelectedPath;
        }
    }

    private void SelectFilesDirectory_Click(object sender, RoutedEventArgs e)
    {
        using var folderDialog = new FolderBrowserDialog();
        var result = folderDialog.ShowDialog();
        if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
        {
            (DataContext as SettingsViewModel).SelectedGame.ExtDir = folderDialog.SelectedPath;
        }
    }

    private void SelectModsDirectory_Click(object sender, RoutedEventArgs e)
    {
        using var folderDialog = new FolderBrowserDialog();
        var result = folderDialog.ShowDialog();
        if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
        {
            (DataContext as SettingsViewModel).SelectedGame.ModDir = folderDialog.SelectedPath;
        }
    }

    private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        // Set focus on the ListBox to ensure the ListBox item gets selected
        GameListBox.Focus();

        // Manually set the ListBox selection based on the TextBox's DataContext
        if (sender is TextBox textBox && textBox.DataContext is GameViewModel selectedItem)
        {
            GameListBox.SelectedItem = selectedItem;
        }
    }

    private void TextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            textBox.IsReadOnly = false;
            textBox.Cursor = Cursors.IBeam;
            textBox.Foreground = new SolidColorBrush(Colors.AccentColor);
            textBox.Background = new SolidColorBrush(Colors.DarkBackColor);
            textBox.SelectionStart = textBox.Text.Length;
            textBox.SelectionLength = 0;
            textBox.CaretBrush = new SolidColorBrush(Colors.MidTextColor);
            e.Handled = true;
        }
        
    }

    private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            textBox.IsReadOnly = true;
            textBox.Cursor = Cursors.Hand;
            textBox.Background = Brushes.Transparent;
            textBox.Foreground = new SolidColorBrush(Colors.LightTextColor);
            textBox.SelectionLength = 0;
            textBox.CaretBrush = Brushes.Transparent;
            e.Handled = true;
        }
    }
}