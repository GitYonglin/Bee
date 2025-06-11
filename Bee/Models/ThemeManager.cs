using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Styling;
using Avalonia;
using Bee.Services;

namespace Bee.Models;

public static class ThemeNames
{
    public const string Default = "Default";
    public const string Light = "Light";
    public const string Dark = "Dark";
}
public class ThemeManager
{
    private ServiceAppSettingDb _serviceAppSettingDb;
    private readonly Dictionary<string, ThemeVariant> _themeCache = new()
    {
        ["Default"] = ThemeVariant.Default
    };

    public ThemeManager(ServiceAppSettingDb db)
    {
        _serviceAppSettingDb = db;
        // 预加载 ThemeVariant 的所有静态属性
        foreach (var prop in typeof(ThemeVariant).GetProperties(BindingFlags.Public | BindingFlags.Static))
        {
            if (prop.PropertyType == typeof(ThemeVariant))
            {
                _themeCache[prop.Name] = (ThemeVariant)prop.GetValue(null)!;
            }
        }

        SetTheme(_serviceAppSettingDb.GetThemeName());
    }

    /// <summary>
    /// 切换主题
    /// </summary>
    /// <param name="themeName"></param>
    public void SetTheme(string themeName = ThemeNames.Default)
    {
        if (Application.Current == null)
            return;

        Application.Current.RequestedThemeVariant =
          _themeCache!.GetValueOrDefault(themeName, ThemeVariant.Default);
        _serviceAppSettingDb.Update(themeName);
    }
    public void Light7Dark() => SetTheme(Application.Current?.RequestedThemeVariant != ThemeVariant.Dark ? ThemeNames.Dark : ThemeNames.Light);
}
