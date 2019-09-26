namespace SampleProject.Infrastructure
{
    public interface ICacheKey<TItem>
    {
        string CacheKey { get; }
    }
}