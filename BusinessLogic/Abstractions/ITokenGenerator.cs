using Envisio.Models;
using System.Threading.Tasks;

namespace Envisio.BusinessLogic
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AppUser user);
    }
}