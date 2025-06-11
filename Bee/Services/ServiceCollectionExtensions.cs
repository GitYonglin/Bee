using System;
using System.Text.Json;
using Avalonia.Controls;
using Bee.Models;
using Bee.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Bee.Services;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    // 注入 ViewModel
    services.AddSingleton<ServiceLiteDb>();
    services.AddSingleton<ServiceLiteDbCollection<AppSetting>>();
    services.AddSingleton<ServiceAppSettingDb>();
    services.AddSingleton<ThemeManager>();
    services.AddTransient<MainWindowViewModel>();

    // 获取 菜单配置文件 Configs/menus.js
    // var menuItems = JsonSerializer.Deserialize<MenuItem[]>(
    //   File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Configs", "menus.json"))
    // );
    // // 单例注入
    // services.AddSingleton(Options.Create(menuItems!));
    return services;
  }
}