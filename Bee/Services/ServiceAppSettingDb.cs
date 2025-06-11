using Bee.Models;

namespace Bee.Services;

public class AppSetting
{
  public int Id { get; set; }
  public string? ThemeName { get; set; }
}

public class ServiceAppSettingDb
{
  private readonly ServiceLiteDbCollection<AppSetting> _db;

  public ServiceAppSettingDb(ServiceLiteDbCollection<AppSetting> db)
  {
    _db = db;
    if (!_db.CollectionExists())
    {
      _db.Insert(new AppSetting()
      {
        Id = 1,
        ThemeName = ThemeNames.Default
      });
    } 
  }
  
  public string GetThemeName()
  {
    return _db.GetOneId(1)?.ThemeName ?? ThemeNames.Default;
  }

  public void Update(string themeName)
  {
    var update = _db.GetOneId(1);
    update.ThemeName = themeName;
    _db.Update(update);
  }
}