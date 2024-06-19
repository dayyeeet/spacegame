public static class NoiseFilterFactory
{
    public static INoiseFilter CreateNoiseFilter(NoiseSettings settings)
    {
        return settings.filterType switch
        {
            NoiseSettings.FilterType.Rigid => new RigidNoiseFilter(settings),
            NoiseSettings.FilterType.Simple => new SimpleNoiseFilter(settings),
            _ => new SimpleNoiseFilter(settings)
        };
    }
}