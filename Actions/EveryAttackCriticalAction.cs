using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace ActionParameterSerializer.Actions;

public class EveryAttackCriticalAction : ActionParameter
{

    private enum EEveryAtkCriticalType
    {
        physical = 1,
        magical,
        both
    }

    private readonly List<ActionValue> durationValues = new();
    private EEveryAtkCriticalType atkType;

    public override void ChildInit()
    {
        base.ChildInit();
        atkType = (EEveryAtkCriticalType) actionDetail1;
        durationValues.Add(new ActionValue(actionValue1, actionValue2, null));
    }

    public override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Enchant_self_with_s1_attack_critical_for_s2_sec"),
             atkType.Description(),
            BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
    }
}