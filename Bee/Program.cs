using Avalonia;
using System;

namespace Bee;

sealed class Program
{
  // Initialization code. Don't use any Avalonia, third-party APIs or any
  // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
  // yet and stuff might break.
  [STAThread]
  public static void Main(string[] args) => BuildAvaloniaApp()
    .StartWithClassicDesktopLifetime(args);

  // Avalonia configuration, don't remove; also used by visual designer.
  public static AppBuilder BuildAvaloniaApp()
    => AppBuilder.Configure<App>() // 添加程序入口（这里及时就是 App.axaml.cs 中的 App 类）
      .UsePlatformDetect() // 配置运行平台依赖
      .WithInterFont() //注册字体资源
      .LogToTrace();// 日志记录
}