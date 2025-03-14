﻿using I_am_Hero_API.Models;

namespace I_am_Hero_API.Services.Interfaces
{
    public interface ICommonService
    {
        IEnumerable<cLevelCalculationType> GetAllcLevelCalculationTypes();
        IEnumerable<cAttributeType> GetAllcAttributeTypes();
        IEnumerable<Application> GetAllApplications();
        IEnumerable<cRarity> GetAllcRarities();
        IEnumerable<cDifficulty> GetAllcDifficulties();
        IEnumerable<cQuestStatus> GetAllcQuestStatuses();
        IEnumerable<cCalendarBehaviour> GetAllcCalendarBehaviours();
        IEnumerable<cCalendarStatus> GetAllcCalendarStatuses();
        IEnumerable<cPopupInterval> GetAllcPopupIntervals();
    }
}
