using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;


namespace Biz.Common
{
    public static class MapperHelper
    {
        public static int? ConvertToNullableInt(string value)
            => int.TryParse(value, out int v) ? v : (int?)null;

        public static IMapperConfigurationExpression Configure(IMapperConfigurationExpression cfg)
        {
            _ = cfg.CreateMap<string[], RowModel>()
                            .ForMember(dest => dest.Column, opt => opt.MapFrom(v => v.Length > 0 ? v[0] : string.Empty))
                            .ForMember(dest => dest.ColumnA, opt => opt.MapFrom(v => v.Length > 1 ? v[1] : string.Empty))
                            .ForMember(dest => dest.ColumnB, opt => opt.MapFrom(v => v.Length > 2 ? v[2] : string.Empty))
                            .ForMember(dest => dest.ColumnC, opt => opt.MapFrom(v => v.Length > 3 ? ConvertToNullableInt(v[3]) : null))
                            .ForMember(dest => dest.ColumnD, opt => opt.MapFrom(v => v.Length > 4 ? ConvertToNullableInt(v[4]) : null))
                            .ForMember(dest => dest.OtherColumn, opt => opt.MapFrom(v => v.Length > 5 ? v[5] : string.Empty))
                            ;
            return cfg;
        }
    }
}
