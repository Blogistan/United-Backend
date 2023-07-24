using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories
{
    public interface IForgotPasswordRepository:IRepository<ForgotPassword,int>,IAsyncRepository<ForgotPassword,int>
    {
    }
}
