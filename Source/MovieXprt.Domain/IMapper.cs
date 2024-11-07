namespace MovieXprt.Domain
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }
}
