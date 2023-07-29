using Infrastructure.IpStack.Models;

namespace Infrastructure.IpStack.Abstract
{
    public interface IIpStackService
    {
        Task<IPInfo> GetClientIpInfo(string IpAddress);
    }
}
