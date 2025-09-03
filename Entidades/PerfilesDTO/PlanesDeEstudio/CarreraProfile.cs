using AutoMapper;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Entidades.DTO.PlanesDeEstudio.Carreras;

namespace Entidades.PerfilesDTO.PlanesDeEstudio
{
    public class CarreraProfile : Profile
    {
        public CarreraProfile()
        {
            CreateMap<E_Carrera, CarreraDTO>().ReverseMap();
        }
    }
}
