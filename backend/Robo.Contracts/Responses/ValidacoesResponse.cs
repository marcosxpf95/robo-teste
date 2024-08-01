namespace Robo.Contracts.Requests;
public class ValicaoResponse
{
    public required bool Success { get; init; }
    public string? Message { get; init; }
}