namespace ClientMonitoring.Web.Domain.SharedKernel
{
    public class CacheOptions
    {
        public TimeSpan AbsoluteExpiration { get; set; } = TimeSpan.FromMinutes(30);
        public TimeSpan LocalCacheExpiration { get; set; } = TimeSpan.FromMinutes(5);
        public int MaxCacheSize { get; set; } = 1000;
    }
}
