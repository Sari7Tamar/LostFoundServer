using AutoMapper;
using DTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DL
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<LostFound, LostFound>().ReverseMap();


            CreateMap<LostFound, NewLF>()

                .ForMember(dest => dest.LF,
                              opts => opts.MapFrom(src => src))
                .ForMember(dest => dest.address,
                              opts => opts.MapFrom(src => src.Addresses.FirstOrDefault()))
                .ForMember(dest => dest.publicTransport,
                            opts => opts.MapFrom(src => src.PublicTransports.FirstOrDefault()));

            CreateMap<NewLF, LostFound>()
                 .ForMember(dest => dest.Id,
                              opts => opts.MapFrom(src => src.LF.Id))
                 .ForMember(dest => dest.Date,
                              opts => opts.MapFrom(src => src.LF.Date))
                 .ForMember(dest => dest.Description,
                              opts => opts.MapFrom(src => src.LF.Description))
                 .ForMember(dest => dest.Image,
                              opts => opts.MapFrom(src => src.LF.Image))
                 .ForMember(dest => dest.UserId,
                              opts => opts.MapFrom(src => src.LF.UserId))
                 .ForMember(dest => dest.LocationType,
                              opts => opts.MapFrom(src => src.LF.LocationType))
                 .ForMember(dest => dest.Type,
                              opts => opts.MapFrom(src => src.LF.Type))
                 .ForMember(dest => dest.AddedDate,
                              opts => opts.MapFrom(src => src.LF.AddedDate))
                 .AfterMap((nlf, lf) =>
                 {
                     Address a = nlf.address;
                     if (a!=null)
                        lf.Addresses.Add(a);
                     PublicTransport p = nlf.publicTransport;
                     if (p != null)
                         lf.PublicTransports.Add(p);
                     
                 });





                /*.ForMember(dest => dest.Addresses,
                              opts => opts.MapFrom(src => src.address))
               .ForMember(dest => dest.PublicTransports,
                            opts => opts.MapFrom(src => src.publicTransport));*/
        }
    }
}



