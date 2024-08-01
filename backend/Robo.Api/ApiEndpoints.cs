namespace Robo.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Robo
    {
        private const string Base = $"{ApiBase}/robo";
        
        public const string Get = $"{Base}";

        public const string InclicarCabeca = $"{Base}/cabeca/inclinar";
        public const string RotacionarCabeca = $"{Base}/cabeca/rotacionar";


        public const string ContrairBracoEsquerdo = $"{Base}/braco/esquerdo/contrair";
        public const string RatacionarBracoEsquerdo = $"{Base}/braco/esquerdo/rotacionar";

        public const string ContrairBracoDireito = $"{Base}/braco/direito/contrair";
        public const string RatacionarBracoDireito = $"{Base}/braco/direito/rotacionar";
    }
}
