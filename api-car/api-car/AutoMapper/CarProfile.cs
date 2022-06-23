using api_car.Domains.Model;
using api_car.Web.DTO;
using AutoMapper;

namespace api_car.AutoMapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarInputModelDTO, Car>();
            CreateMap<CarOutputModelDTO, Car>();
        }
    }
}
