using Yooresh.Domain.Enums;
using Yooresh.Domain.Events;
using Yooresh.Domain.Exceptions;
using Yooresh.Domain.Interfaces;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Domain.Entities.Villages;

public class ResourceBuilding : BaseEntity, IUpgradable<ResourceBuilding>, IGatherable
{
    public string Name { get; set; }

    #region Upgrade

    public string UpgradeName { get; set; }
    public Resource UpgradeCost { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public Guid? TargetId { get; set; }
    public ResourceBuilding? Target { get; set; }
    public bool NeedBuilderForUpgrade { get; set; }
    public int Level { get; set; }

    public void StartUpgrade(Village village)
    {
        if (TargetId == null)
            return;
        CheckAvailableResources(village);
        CheckAvailableBuilders(village);
        SendAWorkerToDoTheJob(village);
        AddDomainEvent(new UpgradeResourceBuildingRequestedEvent(village.Id, Id));
    }

    private void CheckAvailableResources(Village village)
    {
        if (Target!.UpgradeCost > village.Resource)
        {
            throw new NotEnoughResourcesException();
        }
    }

    private void CheckAvailableBuilders(Village village)
    {
        if (NeedBuilderForUpgrade && village.AvailableBuilders == 0)
        {
            throw new NotAvailableBuildersException();
        }
    }

    private void SendAWorkerToDoTheJob(Village village)
    {
        if (NeedBuilderForUpgrade)
        {
            village.AvailableBuilders -= 1;
        }
    }

    #endregion

    #region Resource Production

    public ResourceType ProductionType { get; set; }
    public Resource HourlyProduction { get; set; }
    public DateTimeOffset LastResourceGatherDate { get; set; }

    public void GatherProducedResources(Village village)
    {
        var elapsedTimeSinceLastResourceChangeTime = (DateTimeOffset.Now - LastResourceGatherDate).TotalHours;

        village.Resource += (HourlyProduction * elapsedTimeSinceLastResourceChangeTime);

        LastResourceGatherDate = DateTimeOffset.Now;
    }

    #endregion
}