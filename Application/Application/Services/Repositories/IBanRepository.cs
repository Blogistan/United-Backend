using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
	public interface IBanRepository:IRepository<Ban, int>,IAsyncRepository<Ban, int>
	{

	}
}
