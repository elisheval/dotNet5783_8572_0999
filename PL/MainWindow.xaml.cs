﻿using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PL.Manager;
using PL.Order;
using PL.Product;
namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region variable
    BlApi.IBl? bl = BlApi.Factory.Get();
    #endregion

    #region constructor
    public MainWindow()
    {
        InitializeComponent();
    }
    #endregion

    #region navigate to a choose window 
    private void NavigateToManagerWindow(object sender, RoutedEventArgs e)
    {
        new ManagerWindow().Show();
        this.Close();
    }
    private void NavigateToCatalogWindow(object sender, RoutedEventArgs e)
    {
        new ProductCatalog().Show();
        this.Close();

    }
    private void NavigateToOrderTrackingWindow(object sender, RoutedEventArgs e)
    {
        new OrderTracking().Show();
        this.Close();
    }
    #endregion

    private void StartSimulatorBtnClick(object sender, RoutedEventArgs e)
    {
        new StartSimulatorWindow().Show();
    }
}
