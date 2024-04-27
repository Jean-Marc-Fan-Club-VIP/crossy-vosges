using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataService : IDataService
{
    public void SaveEntity<T>(string path, T entity)
    {
        var entityPath = Path.Join(Application.persistentDataPath, path);
        File.WriteAllText(entityPath, JsonConvert.SerializeObject(entity));
    }

    public T LoadEntity<T>(string path)
    {
        var entityPath = Path.Join(Application.persistentDataPath, path);
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(entityPath));
    }
}