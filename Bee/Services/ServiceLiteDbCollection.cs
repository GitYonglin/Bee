using LiteDB;

namespace Bee.Services;

public class ServiceLiteDbCollection<T>(ServiceLiteDb db) where T : class
{
  private readonly ILiteCollection<T> _collection = db.GetCollection<T>();
  /// <summary>
  /// 检查集合是否存在
  /// </summary>
  /// <returns></returns>
  public bool CollectionExists() => db.CollectionExists<T>();
  // // 确保索引存在
  // public void EnsureIndex<K>(Expression<Func<T, K>> property)
  // {
  //   _collection.EnsureIndex(property);
  // }

  public T GetOneId(int id) => _collection.FindById(id);
  
  public T? Insert(T item) => _collection.Insert(item) as T;

  public bool Update(T item) => _collection.Update(item);

  public bool Update(int id, string fieldName, object value)
  {
    var bs = BsonExpression.Create($"$.{fieldName} = @0", new BsonValue(value)) as T;
    return bs != null && _collection.Update(id, bs);
  }
}