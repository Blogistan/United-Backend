using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IReportTypeRepository:IRepository<ReportType,int>,IAsyncRepository<ReportType,int>
    {
    }
}
