using api_car.Domains.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_car.Domains.Interface
{
    public interface ICarRepository
    {
        Task<long> CountAsync();

        Task<IEnumerable<Car>> GetAllAsync();

        Task<Car> GetByIdAsync(string carId);

        Task CreateAsync(Car car);

        Task UpdateAsync(Car car);

        Task DeleteAsync(string carId);
    }
}
