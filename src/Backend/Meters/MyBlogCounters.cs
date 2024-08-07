using System.Diagnostics.Metrics;

namespace MyBlog.Backend.Counters;

public class MyBlogCounters
{
    private readonly Counter<int> _counterCache;
    private readonly Counter<int> _counterFilter;
    public MyBlogCounters(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("myblog.counter");
        _counterCache = meter.CreateCounter<int>("myblog.counter.cache");
        _counterFilter = meter.CreateCounter<int>("myblog.counter.filter");
    }
    
    public void IncrementCache(string source, string methodName)
    {
        _counterCache.Add(1, 
            new KeyValuePair<string, object?>("myblog.counter.cache.source", source),
            new KeyValuePair<string, object?>("myblog.counter.cache.method_name", methodName)
            );
    }
    
    public void IncrementFilter(string filterName, int id)
    {
        _counterFilter.Add(1, 
            new KeyValuePair<string, object?>("myblog.counter.filter.name", filterName),
            new KeyValuePair<string, object?>("myblog.counter.filter.id", id)
            );
    }
}