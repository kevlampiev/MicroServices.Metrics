using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.DTO;
using MetricsAgent.Models.Requests;

namespace MetricsAgent.Mappings
{
    public class MapperProfile:Profile
    {
        public MapperProfile() 
        {
            CreateMap<CPUMetricCreateRequest, CPUMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));
            CreateMap<CPUMetric, CPUMetricDTO>();
            
            CreateMap<DotNetMetricCreateRequest, DotNetMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));
            CreateMap<DotNetMetric, DotNetMetricDTO>();
            
            CreateMap<HDDMetricCreateRequest, HDDMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));
            CreateMap<HDDMetric, HDDMetricDTO>();
            
            CreateMap<NetworkMetricCreateRequest, NetworkMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));
            CreateMap<NetworkMetric, NetworkMetricDTO>();
            
            CreateMap<RAMMetricCreateRequest, RAMMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));
            CreateMap<RAMMetric, RAMMetricDTO>();
        }
    }
}
