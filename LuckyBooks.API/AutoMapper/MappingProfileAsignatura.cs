using AutoMapper;
using Bussines;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuckyBooks.API.AutoMapper
{
    public class MappingProfileAsignatura : Profile
    {
        public MappingProfileAsignatura()
        {
            //GET: Asignatura y AsignaturaDto 
            // A     =====>     //B  
            CreateMap<Asignatura, AsignaturaDto>()
                .ForMember(b => b.codigoAsignatura, opt => opt.MapFrom(a => a.id_asig))
                .ForMember(b => b.descripcion, opt => opt.MapFrom(a => a.descripcion))
                .ForMember(b => b.condicion, opt => opt.MapFrom(a => a.estado));


            //POST: Asignatura y AsignaturaForCreationDto 
            // A     =====>     //B  
            CreateMap<AsignaturaForCreationDto, Asignatura>()
                  .ForMember(b => b.id_asig, opt => opt.MapFrom(a => a.codigoAsignatura))
                .ForMember(b => b.descripcion, opt => opt.MapFrom(a => a.descripcion))
                .ForMember(b => b.estado, opt => opt.MapFrom(a => a.condicion));


            //PUT: Asignatura y AsignaturaForUpdateDto 
            // A     =====>     //B  
            CreateMap<AsignaturaForUpdateDto, Asignatura>()
                  .ForMember(b => b.id_asig, opt => opt.MapFrom(a => a.codigoAsignatura))
                .ForMember(b => b.descripcion, opt => opt.MapFrom(a => a.descripcion))
                .ForMember(b => b.estado, opt => opt.MapFrom(a => a.condicion));                
        }
    }
}
