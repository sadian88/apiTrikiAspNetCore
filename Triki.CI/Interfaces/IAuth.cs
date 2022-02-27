using System.Threading.Tasks;
using Triki.CI.Dto;

namespace Triki.CI.Interfaces
{
    public interface IAuth
    {
        Task<ResponseBaseDto> Authenticate(AuthDto data);

        Task<LoginDto> RefreshToken();
    }
}
