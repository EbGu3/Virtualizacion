namespace OrleansURLShortener.Interfaz
{
    public interface IUrlShortenerGrain: IGrainWithStringKey
    {
        Task SetUrl(string fullUrl);

        Task<string> GetUrl();
    }
}
