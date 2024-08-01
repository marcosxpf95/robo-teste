namespace Robo.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Robo
    {
        private const string Base = $"{ApiBase}/robo";

        public const string Get = $"{Base}";

        public const string Put = $"{Base}";
    }
}
