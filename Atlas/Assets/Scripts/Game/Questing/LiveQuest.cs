using System.Collections.Generic;
using System.Linq;
using Game.SavingSystem.Datas;
using UnityEngine;

namespace Game.Questing
{
    public struct LiveQuest
        {
            public readonly Quest Quest;
            public readonly GameObject toSpawnReward;
            public readonly LiveRequirement[] Requirements;
            public bool IsFinished => Requirements.Length > 0 && Requirements.All(it => it.CurrentlyAccomplished >= it.Requirement.Count);

            public LiveQuest(Quest quest)
            {
                Quest = quest;
                toSpawnReward = quest.toSpawn;
                Requirements = quest.Requirements.Select(questRequirement => new LiveRequirement(questRequirement, 0)).ToArray();
            }

            public LiveQuest(Quest quest, IEnumerable<MapData.RequirementData> requirementDatas)
            {
                Quest = quest;
                toSpawnReward = quest.toSpawn;
                var requirements = new List<LiveRequirement>();
                foreach (var requirementData in requirementDatas)
                {
                    var requirement = quest.Requirements.Single(it =>
                        it.Argument.Id == requirementData.ItemAbstractId &&
                        it.Condition.Id == requirementData.ConditionId);

                    if (!requirement.Equals(null))
                    {
                        requirements.Add(new LiveRequirement(requirement, requirementData.CurrentlyAccomplished));
                    }
                    else
                    {
                        Debug.LogWarning($"Requirement with Condition ID: {requirementData.ConditionId} and Item Id : {requirementData.ItemAbstractId} does not exist in Quest : {quest.Name}");
                    }
                }

                Requirements = requirements.ToArray();
            }
        }
    
}