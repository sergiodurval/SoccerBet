using SoccerBet.Business.Models;
using System.Threading.Tasks;

namespace SoccerBet.Business.Interfaces
{
    public interface IBetRepository
    {
        Task<Bet> Add(Bet bet);
    }
}
