using System.Data;

namespace SoccerBet.Data.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection Connection();
    }
}
