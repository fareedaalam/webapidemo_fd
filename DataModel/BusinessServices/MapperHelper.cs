using AutoMapper;
using BusinessEntities;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
   public static class MapperHelper    {

        public static void MapEntityInit()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<tbl_Product, ProductEntity>().ReverseMap();
            });
        }
    }
}
