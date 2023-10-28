using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IReportRepository:IRepository<Report,Guid>, IAsyncRepository<Report, Guid>
    {
    }
}
