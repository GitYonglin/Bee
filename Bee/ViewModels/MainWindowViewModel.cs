using System;
using System.Diagnostics;
using Bee.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bee.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
  [ObservableProperty]
  public partial string Greeting { get; set; } = "Welcome to Avalonia!";

  [RelayCommand]
  private void ThemeSwitch()
  {
    Debug.WriteLine("ThemeSwitch");
    ThemeManager.Light7Dark();
    
  }

}