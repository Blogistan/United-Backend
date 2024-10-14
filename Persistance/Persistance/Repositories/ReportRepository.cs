using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class ReportRepository : EfRepositoryBase<Report, int, EFDbContext>,IReportRepository
    {
        public ReportRepository(EFDbContext context) : base(context)
        {
        }
    }
}
