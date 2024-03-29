using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace ActionParameterSerializer.Actions;





public class TargetParameter
{

    public TargetAssignment targetAssignment;
    public TargetNumber targetNumber;
    public int rawTargetType;
    public TargetType targetType;
    public TargetRange targetRange;
    public DirectionType direction;
    public TargetCount targetCount;

    private readonly SkillAction dependAction;

    public TargetParameter(int targetAssignment, int targetNumber, int targetType, int targetRange, int targetArea, int targetCount, SkillAction dependAction)
    {
        this.targetAssignment = (TargetAssignment)(targetAssignment);
        this.targetNumber = (TargetNumber)(targetNumber);
        rawTargetType = targetType;
        this.targetType = (TargetType)(targetType);
        this.targetRange = new TargetRange(targetRange);
        direction = (DirectionType)(targetArea);
        this.targetCount = (TargetCount)(targetCount);
        if (!Enum.IsDefined(this.targetCount)) this.targetCount = TargetCount.all;
        this.dependAction = dependAction;
        SetBooleans();
    }

    private bool hasRelationPhrase;
    private bool hasCountPhrase;
    private bool hasRangePhrase;
    private bool hasNthModifier;
    private bool hasDirectionPhrase;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "<Pending>")]
    private bool hasTargetType;
    private bool HasDependAction()
    {
        return dependAction != null && (
            SkillAction.GetActionId() != 0
            && targetType != TargetType.absolute
            && dependAction.parameter.actionType != ActionType.chooseArea
        );
    }

    private void SetBooleans()
    {
        hasRelationPhrase = targetType != TargetType.self
                && targetType != TargetType.absolute;
        hasCountPhrase = targetType != TargetType.self
                && !(targetType == TargetType.none && targetCount == TargetCount.zero);

        hasRangePhrase = targetRange.rangeType == TargetRange.FINITE;

        hasNthModifier = targetNumber == TargetNumber.second
                || targetNumber == TargetNumber.third
                || targetNumber == TargetNumber.fourth
                || targetNumber == TargetNumber.fifth;
        hasDirectionPhrase = direction == DirectionType.front
                && (hasRangePhrase || targetCount == TargetCount.all);
        hasTargetType = !(targetType.ExclusiveWithAll() == ExclusiveAllType.exclusive && targetCount == TargetCount.all);
    }


    public string BuildTargetClause(bool anyOfModifier)
    {
        if (targetCount.PluralModifier() == PluralModifier.many && anyOfModifier)
        {
            return Utils.JavaFormat(Utils.GetString("any_of_s"), BuildTargetClause());
        }
        else
        {
            return BuildTargetClause();
        }
    }

    public string BuildTargetClause()
    {
        if (HasDependAction())
        {
            if (dependAction.parameter.actionType == ActionType.damage)
            {
                return Utils.JavaFormat(Utils.GetString("targets_those_damaged_by_effect_d"), SkillAction.GetActionId() % 100);
            }
            else
            {
                return Utils.JavaFormat(Utils.GetString("targets_of_effect_d"), SkillAction.GetActionId() % 100);
            }
        }
        else if (!hasRelationPhrase)
        {
            return targetType.Description();
        }
        else if (!hasCountPhrase && !hasNthModifier && !hasRangePhrase && hasRelationPhrase)
        {
            return Utils.JavaFormat(Utils.GetString("targets_of_last_effect"));
        }
        else if (hasCountPhrase && !hasNthModifier && !hasRangePhrase && hasRelationPhrase && !hasDirectionPhrase)
        {
            if (targetCount == TargetCount.all)
            {
                if (targetType.ExclusiveWithAll() == ExclusiveAllType.exclusive)
                {
                    return Utils.JavaFormat(Utils.GetString("all_s_targets"), targetAssignment.Description());
                }
                else if (targetType.ExclusiveWithAll() == ExclusiveAllType.not)
                {
                    return Utils.JavaFormat(Utils.GetString("all_s_s_targets"), targetAssignment.Description(), targetType.Description());
                }
                else if (targetType.ExclusiveWithAll() == ExclusiveAllType.halfExclusive)
                {
                    return Utils.JavaFormat(Utils.GetString("all_s_targets"), targetAssignment.Description()) + Utils.JavaFormat(Utils.GetString("except_self"));
                }
            }
            else if (targetCount == TargetCount.one && targetType.IgnoresOne())
            {
                return Utils.JavaFormat(Utils.GetString("s_s_target"), targetType.Description(), targetAssignment.Description());
            }
            else
            {
                return Utils.JavaFormat(Utils.GetString("s_s_s"), targetType.Description(), targetAssignment.Description(), targetCount.Description());
            }
        }
        else if (hasCountPhrase && !hasNthModifier && !hasRangePhrase && hasRelationPhrase && hasDirectionPhrase && targetType.ExclusiveWithAll() == ExclusiveAllType.exclusive)
        {
            return targetAssignment switch
            {
                TargetAssignment.enemy => Utils.JavaFormat(Utils.GetString("all_front_enemy_targets")),
                TargetAssignment.friendly => Utils.JavaFormat(Utils.GetString("all_front_including_self_friendly_targets")),
                _ => Utils.JavaFormat(Utils.GetString("all_front_targets")),
            };
        }
        else if (hasCountPhrase && !hasNthModifier && !hasRangePhrase && hasRelationPhrase && hasDirectionPhrase && targetType.ExclusiveWithAll() == ExclusiveAllType.not)
        {
            return targetAssignment switch
            {
                TargetAssignment.enemy => Utils.JavaFormat(Utils.GetString("all_front_s_enemy_targets"), targetType.Description()),
                TargetAssignment.friendly => Utils.JavaFormat(Utils.GetString("all_front_including_self_s_friendly_targets"), targetType.Description()),
                _ => Utils.JavaFormat(Utils.GetString("all_front_s_targets"), targetType.Description()),
            };
        }
        else if (hasCountPhrase && !hasNthModifier && !hasRangePhrase && hasRelationPhrase && hasDirectionPhrase && targetType.ExclusiveWithAll() == ExclusiveAllType.halfExclusive)
        {
            return targetAssignment switch
            {
                TargetAssignment.enemy => Utils.JavaFormat(Utils.GetString("all_front_enemy_targets")) + Utils.JavaFormat(Utils.GetString("except_self")),
                TargetAssignment.friendly => Utils.JavaFormat(Utils.GetString("all_front_including_self_friendly_targets")),
                _ => Utils.JavaFormat(Utils.GetString("all_front_targets")) + Utils.JavaFormat(Utils.GetString("except_self")),
            };
        }
        else if (!hasCountPhrase && !hasNthModifier && hasRangePhrase && hasRelationPhrase && !hasDirectionPhrase)
        {
            return Utils.JavaFormat(Utils.GetString("s1_targets_in_range_d2"), targetAssignment.Description(), targetRange.rawRange);
        }
        else if (!hasCountPhrase && !hasNthModifier && hasRangePhrase && hasRelationPhrase && hasDirectionPhrase)
        {
            return Utils.JavaFormat(Utils.GetString("front_s1_targets_in_range_d2"), targetAssignment.Description(), targetRange.rawRange);
        }
        else if (!hasCountPhrase && hasNthModifier && hasRangePhrase && hasRelationPhrase)
        {
            return Utils.JavaFormat(Utils.GetString("targets_of_last_effect"));
        }
        else if (hasCountPhrase && !hasNthModifier && hasRangePhrase && hasRelationPhrase && !hasDirectionPhrase)
        {
            if (targetCount == TargetCount.all)
            {
                if (targetType.ExclusiveWithAll() == ExclusiveAllType.exclusive)
                {
                    return Utils.JavaFormat(Utils.GetString("s1_targets_in_range_d2"), targetAssignment.Description(), targetRange.rawRange);
                }
                else if (targetType.ExclusiveWithAll() == ExclusiveAllType.not)
                {
                    return Utils.JavaFormat(Utils.GetString("s1_s2_target_in_range_d3"), targetAssignment.Description(), targetType.Description(), targetRange.rawRange);
                }
                else if (targetType.ExclusiveWithAll() == ExclusiveAllType.halfExclusive)
                {
                    return Utils.JavaFormat(Utils.GetString("s1_targets_in_range_d2"), targetAssignment.Description() + Utils.JavaFormat(Utils.GetString("except_self")), targetRange.rawRange);
                }
            }
            else if (targetCount == TargetCount.one && targetType.IgnoresOne())
            {
                return Utils.JavaFormat(Utils.GetString("s1_s2_target_in_range_d3"), targetType.Description(), targetAssignment.Description(), targetRange.rawRange);
            }
            else
            {
                return Utils.JavaFormat(Utils.GetString("s1_s2_s3_in_range_d4"), targetType.Description(), targetAssignment.Description(), targetCount.Description(), targetRange.rawRange);
            }
        }
        else if (hasCountPhrase && !hasNthModifier && hasRangePhrase && hasRelationPhrase && hasDirectionPhrase)
        {
            if (targetCount == TargetCount.all)
            {
                if (targetType.ExclusiveWithAll() == ExclusiveAllType.exclusive)
                {
                    return Utils.JavaFormat(Utils.GetString("front_s1_targets_in_range_d2"), targetAssignment.Description(), targetRange.rawRange);
                }
                else if (targetType.ExclusiveWithAll() == ExclusiveAllType.not)
                {
                    return Utils.JavaFormat(Utils.GetString("front_s1_s2_targets_in_range_d3"), targetAssignment.Description(), targetType.Description(), targetRange.rawRange);
                }
                else if (targetType.ExclusiveWithAll() == ExclusiveAllType.halfExclusive)
                {
                    return Utils.JavaFormat(Utils.GetString("front_s1_targets_in_range_d2"), targetAssignment.Description() + Utils.JavaFormat(Utils.GetString("except_self")), targetRange.rawRange);
                }
            }
            else if (targetCount == TargetCount.one && targetType.IgnoresOne())
            {
                return Utils.JavaFormat(Utils.GetString("s1_front_s2_target_in_range_d3"), targetType.Description(), targetAssignment.Description(), targetRange.rawRange);
            }
            else
            {
                return Utils.JavaFormat(Utils.GetString("s1_front_s2_s3_in_range_d4"), targetType.Description(), targetAssignment.Description(), targetCount.Description(), targetRange.rawRange);
            }
        }
        else if (hasCountPhrase && hasNthModifier && !hasRangePhrase && hasRelationPhrase && !hasDirectionPhrase)
        {
            if (targetCount == TargetCount.one && targetType.IgnoresOne())
            {
                return Utils.JavaFormat(Utils.GetString("s_s_target"), targetType.Description(targetNumber, null), targetAssignment.Description());
            }
            else
            {
                string modifier = Utils.JavaFormat(Utils.GetString("s1_to_s2"), targetNumber.Description(), GetUntilNumber().Description());
                return Utils.JavaFormat(Utils.GetString("s_s_s"), targetType.Description(targetNumber, modifier), targetAssignment.Description(), targetCount.PluralModifier().Description());
            }
        }
        else if (hasCountPhrase && hasNthModifier && !hasRangePhrase && hasRelationPhrase && hasDirectionPhrase)
        {
            string modifier = Utils.JavaFormat(Utils.GetString("s1_to_s2"), targetNumber.Description(), GetUntilNumber().Description());
            return Utils.JavaFormat(Utils.GetString("s1_front_s2_s3"), targetType.Description(targetNumber, modifier), targetAssignment.Description(), targetCount.PluralModifier().Description());
        }
        else if (hasCountPhrase && hasNthModifier && hasRangePhrase && hasRelationPhrase && !hasDirectionPhrase)
        {
            if (targetCount == TargetCount.one && targetType.IgnoresOne())
            {
                return Utils.JavaFormat(Utils.GetString("s1_s2_target_in_range_d3"), targetType.Description(targetNumber, null), targetAssignment.Description(), targetRange.rawRange);
            }
            else
            {
                string modifier = Utils.JavaFormat(Utils.GetString("s1_to_s2"), targetNumber.Description(), GetUntilNumber().Description());
                return Utils.JavaFormat(Utils.GetString("s1_s2_s3_in_range_d4"), targetType.Description(targetNumber, modifier), targetAssignment.Description(), targetCount.PluralModifier().Description(), targetRange.rawRange);
            }
        }
        else if (hasCountPhrase && hasNthModifier && hasRangePhrase && hasRelationPhrase && hasDirectionPhrase)
        {
            if (targetCount == TargetCount.one && targetType.IgnoresOne())
            {
                return Utils.JavaFormat(Utils.GetString("s1_front_s2_target_in_range_d3"), targetType.Description(targetNumber, null), targetAssignment.Description(), targetRange.rawRange);
            }
            else
            {
                string modifier = Utils.JavaFormat(Utils.GetString("s1_to_s2"), targetNumber.Description(), GetUntilNumber().Description());
                return Utils.JavaFormat(Utils.GetString("s1_front_s2_s3_in_range_d4"), targetType.Description(targetNumber, modifier), targetAssignment.Description(), targetCount.PluralModifier().Description(), targetRange.rawRange);
            }
        }
        return "";
    }

    private TargetNumber GetUntilNumber()
    {
        TargetNumber tUntil = targetNumber + (int)targetCount;
        if (tUntil == TargetNumber.other || !Enum.IsDefined(tUntil))
        {
            return TargetNumber.fifth;
        }
        else
        {
            return tUntil;
        }
    }

}

public enum PluralModifier
{
    [Description("target")]
    one,
    [Description("targets")]
    many
}

public enum TargetCount
{
    zero, one, two, three, four, all = 99
}

public enum TargetNumber
{
    first,
    second,
    third,
    fourth,
    fifth,
    other
}
public enum ExclusiveAllType
{
    not, exclusive, halfExclusive
}

public enum TargetAssignment
{
    none = 0,
    enemy = 1,
    friendly = 2,
    all = 3
}

public class TargetRange
{
    public const int ZERO = 0;
    public const int ALL = 1;
    public const int FINITE = 2;
    public const int INFINITE = 3;
    public const int UNKNOWN = 4;

    public int rawRange;
    public int rangeType;

    public TargetRange(int range)
    {
        if (range == -1)
        {
            rangeType = INFINITE;
        }
        else if (range == 0)
        {
            rangeType = ZERO;
        }
        else if (range > 0 && range < 2160)
        {
            rangeType = FINITE;
        }
        else if (range >= 2160)
        {
            rangeType = ALL;
            rawRange = 2160;
            return;
        }
        else
        {
            rangeType = UNKNOWN;
        }
        rawRange = range;
    }
}


public enum TargetType
{
    unknown = -1,
    zero = 0,
    none = 1,
    random = 2,
    near = 3,
    far = 4,
    hpAscending = 5,
    hpDescending = 6,
    self = 7,
    randomOnce = 8,
    forward = 9,
    backward = 10,
    absolute = 11,
    tpDescending = 12,
    tpAscending = 13,
    atkDescending = 14,
    atkAscending = 15,
    magicSTRDescending = 16,
    magicSTRAscending = 17,
    summon = 18,
    tpReducing = 19,
    physics = 20,
    magic = 21,
    allSummonRandom = 22,
    selfSummonRandom = 23,
    boss = 24,
    hpAscendingOrNear = 25,
    hpDescendingOrNear = 26,
    tpDescendingOrNear = 27,
    tpAscendingOrNear = 28,
    atkDescendingOrNear = 29,
    atkAscendingOrNear = 30,
    magicSTRDescendingOrNear = 31,
    magicSTRAscendingOrNear = 32,
    shadow = 33,
    nearWithoutSelf = 34,
    hpDescendingOrNearForward = 35,
    hpAscendingOrNearForward = 36,
    tpDescendingOrMaxForward = 37,
    bothAtkDescending = 38,
    bothAtkAscending = 39,
    energyAscBackWithoutOwner = 41,
    parentTargetParts = 42,
    atkDecForwardWithoutOwner = 43,
    hpAscWithoutOwner,
    atkDefAscForward,
    magicDefAscForward,
    flightOnly
}

public enum DirectionType
{
    front = 1,
    frontAndBack = 2,
    all = 3
}