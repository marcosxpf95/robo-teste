namespace Robo.Application.Repositories;

using Robo.Application.Data;
using Robo.Application.Models;
public class RoboRepository : IRoboRepository
{
    private Robo Robo { get; set; }
    public RoboRepository()
    {
        Robo = RoboInitializer.CriarInitializedRobo();
    }

    public Robo? Get()
    {
        return Robo;
    }
}
