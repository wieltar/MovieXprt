namespace MovieXprt.Common.Mappers
{
    public interface IMapper<TSource, TDestination> 
    {
        TDestination Map(TSource source);
    }
}
