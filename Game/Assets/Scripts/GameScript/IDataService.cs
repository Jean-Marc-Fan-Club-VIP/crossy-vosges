public interface IDataService
{
    void SaveEntity<T>(string path, T entity);
    T LoadEntity<T>(string path);
}