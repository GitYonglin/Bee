using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Styling;

namespace Bee.Models;

public static class ThemeManager
{
    public const string Default = "Default";
    public const string Light = "Light";
    public const string Dark = "Dark";

    private static readonly Dictionary<string, ThemeVariant> ThemeCache = new()
    {
        ["Default"] = ThemeVariant.Default
    };

    static ThemeManager()
    {
        // 预加载 ThemeVariant 的所有静态属性
        foreach (var prop in typeof(ThemeVariant).GetProperties(BindingFlags.Public | BindingFlags.Static))
        {
            if (prop.PropertyType == typeof(ThemeVariant))
            {
                ThemeCache[prop.Name] = (ThemeVariant)prop.GetValue(null)!;
            }
        }
    }

    /// <summary>
    /// 切换主题
    /// </summary>
    /// <param name="themeName"></param>
    public static void SetTheme(string? themeName = Default)
    {
        if (Application.Current == null)
            return;

        Application.Current.RequestedThemeVariant =
          ThemeCache!.GetValueOrDefault(themeName, ThemeVariant.Default);
    }
    public static void Light7Dark() => SetTheme(Application.Current?.RequestedThemeVariant != ThemeVariant.Dark ? Dark : Light);
}
