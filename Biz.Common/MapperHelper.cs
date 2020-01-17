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

            _ = cfg.CreateMap<(int i, string[] v), RowModel>()
                .ForMember(dest => dest.Column, opt => opt.MapFrom(v => v.v.Length > 0 ? v.v[0] : string.Empty))
                .ForMember(dest => dest.ColumnA, opt => opt.MapFrom(v => v.v.Length > 1 ? v.v[1] : string.Empty))
                .ForMember(dest => dest.ColumnB, opt => opt.MapFrom(v => v.v.Length > 2 ? v.v[2] : string.Empty))
                .ForMember(dest => dest.ColumnC, opt => opt.MapFrom(v => v.v.Length > 3 ? ConvertToNullableInt(v.v[3]) : null))
                .ForMember(dest => dest.ColumnD, opt => opt.MapFrom(v => v.v.Length > 4 ? ConvertToNullableInt(v.v[4]) : null))
                .ForMember(dest => dest.OtherColumn, opt => opt.MapFrom(v => v.v.Length > 5 ? v.v[5] : string.Empty))
                .ForMember(dest => dest.LineNumber, opt => opt.MapFrom(v => v.i))
                ;

            _ = cfg.CreateMap<RowModel, JsonRowModel>()
                            .ForMember(dest => dest.Type, opt => opt.MapFrom(v => v.IsOk ? KindOfType.ok : KindOfType.error))
                            .ForMember(dest => dest.ConcatAB, opt => opt.MapFrom(v => v.ConcatAB))
                            .ForMember(dest => dest.LineNumber, opt => opt.MapFrom(v => v.LineNumber))
                            .ForMember(dest => dest.SumCD, opt => opt.MapFrom(v => v.Message == null ? v.SumCD : null))
                            .ForMember(dest => dest.ErrorMessage, opt => opt.MapFrom(v => v.Message));
            return cfg;
        }
    }
}
