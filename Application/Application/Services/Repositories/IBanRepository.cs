using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
	public interface IBanRepository:IRepository<Ban,Guid>,IAsyncRepository<Ban,Guid>
	{

	}
}
