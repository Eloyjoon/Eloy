using AutoMapper;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Application.Villages.Dto;

public class ResourceBuildingDto : IMapFrom<ResourceBuilding>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public Resource UpgradeCost { get; set; }
    public int Level { get; set; }
    public Guid? TargetId { get; set; }
    public ResourceBuildingDto? Target { get; set; }
    public Resource HourlyProduction { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ResourceBuilding, ResourceBuildingDto>()
            .ReverseMap();
    }
}