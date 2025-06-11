using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using LiteDB;

namespace Bee.Services;

public class ServiceLiteDb
{
  /// <summary>
  /// 处理数据库保持位置
  /// </summary>
  /// <returns></returns>
  private static string GetDatabasePath()
  {
    // 对于桌面应用
    var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    var appName = Assembly.GetEntryAssembly()?.GetName().Name ?? "AvaloniaApp";
    var dir = Path.Combine(appData, appName);

    if (!Directory.Exists(dir))
    {
      Directory.CreateDirectory(dir);
    }

    return Path.Combine(dir, $"{appName}.db");
  }

  private readonly LiteDatabase _db;

  public ServiceLiteDb()
  {
    var dbPath = GetDatabasePath();
    _db = new LiteDatabase($"Filename={dbPath};Connection=shared");
  }
  /// <summary>
  /// 检查集合是否存在
  /// </summary>
  /// <returns></returns>
  public bool CollectionExists<T>() => _db.CollectionExists(typeof(T).Name);
  // 获取集合
  public ILiteCollection<T> GetCollection<T>()=> _db.GetCollection<T>(typeof(T).Name);
 
  public void Dispose()
  {
    _db.Dispose();
  }
}


