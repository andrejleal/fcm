namespace Framework.Infrastruture
{
    public interface IConfigurationProvider
    {
        string GetConnectionString(string name);
    }
}
