﻿using OmniVoice.Presentation.ViewModelContracts;

using System.Windows;

namespace OmniVoice.Presentation.Views;

public partial class MainWindow : Window
{
    public MainWindow(IMainWindowModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}