using Orleans.Runtime;
using OrleansURLShortener.Interfaz;

namespace OrleansURLShortener.Data
{
    public sealed class UrlShortenerGrain
    (
        [PersistentState
        (
            stateName: "url",
            storageName: "urls"
        )]
        IPersistentState<UrlDetails> state
    ) : Grain, IUrlShortenerGrain
    
    {
        public Task<string> GetUrl()
            => Task.FromResult(state.State.FullUrl);


        public async Task SetUrl(string fullUrl)
        {
            state.State = new UrlDetails()
            {
                ShortenedRouteSegment = this.GetPrimaryKeyString(),
                FullUrl = fullUrl
            };

            await state.WriteStateAsync();
        }
    }

}
