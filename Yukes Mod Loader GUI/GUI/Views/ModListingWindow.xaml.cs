using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yukes_Mod_Loader_GUI.GUI;
using Yukes_Mod_Loader_GUI.GUI.ViewModels;

namespace Yukes_Mod_Loader_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ModListingWindow : Window
    {
        public ModListingWindow()
        {
            InitializeComponent();
            if (DataContext is ModListingViewModel viewModel)
            {
                viewModel.Initialize();
            }
            MouseDown += delegate{DragMove();};
        }

        private void ModsDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModsDataGrid.SelectedItem is ModViewModel selectedMod)
            {
                ((ModListingViewModel)DataContext).SelectedMod = selectedMod;
            }
        }
    }
}