using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Bee.Models;
using Bee.ViewModels;
using Bee.Views;

namespace Bee;

public partial class App : Application
{
  public override void Initialize()
  {
    AvaloniaXamlLoader.Load(this);
  }

  public string CurrentTheme { get; set; } = "Light";

  /// <summary>
  /// 框架初始化
  /// </summary>
  public override void OnFrameworkInitializationCompleted()
  {
    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
      // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
      // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
      DisableAvaloniaDataAnnotationValidation();
      desktop.MainWindow = new MainWindow // 设置主窗口 （这里其实就是 Views/MainWindow.axaml.cs 中的 MainWindow 类）
      {
        DataContext =
          new MainWindowViewModel(), // 主窗口关联的 Model （这里其实就是 ViewModels/MainWindowViewModel.cs 中的 MainWindowViewModel 类）
      };
    }

    base.OnFrameworkInitializationCompleted();
    //加载后设置主题，比如用户切换主题后保存到数据库，再次启动时切换
    // ThemeManager.SetTheme();
    //this.SetTheme();
  }

  private void DisableAvaloniaDataAnnotationValidation()
  {
    // Get an array of plugins to remove
    var dataValidationPluginsToRemove =
      BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

    // remove each entry found
    foreach (var plugin in dataValidationPluginsToRemove)
    {
      BindingPlugins.DataValidators.Remove(plugin);
    }
  }
}