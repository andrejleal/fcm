namespace Framework.Infrastruture
{
    public interface IMapperProvider
    {
        TOut Map<TIn, TOut>(TIn inputObj);
    }
}
