namespace Robo.Application.Repositories;
using Robo.Application.Models;
public class RoboRepository : IRoboRepository
{
    private Robo Robo { get; set; }
    public RoboRepository()
    {
        Robo = new Robo();
    }

    public Robo? Get()
    {
        return Robo;
    }
}
