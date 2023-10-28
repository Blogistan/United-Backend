using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class ReportTypeRepository : EfRepositoryBase<ReportType, int, EFDbContext>,IReportTypeRepository
    {
        public ReportTypeRepository(EFDbContext context) : base(context)
        {
        }
    }
}
