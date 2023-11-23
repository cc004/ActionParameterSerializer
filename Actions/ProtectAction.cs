namespace ActionParameterSerializer.Actions;

public class ProtectAction : ActionParameter
{
    private readonly List<ActionValue> counterValues = new(), durationValues = new();

    public override void ChildInit()
    {
        base.ChildInit();
        counterValues.Add(new ActionValue(actionValue1, actionValue2, null));
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public override string LocalizedDetail(int level, Property property)
    {
        if (actionValue1.value == 0 && actionDetail2 == 0)
            return Utils.JavaFormat(Utils.GetString("Protect_s1_from_certain_skill_for_s2_sec"),
                targetParameter.BuildTargetClause(),
                BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));

        return Utils.JavaFormat(Utils.GetString("Protect_s1_from_certain_skill_max_s2_for_s3_sec"),
            targetParameter.BuildTargetClause(),
            BuildExpression(level, counterValues, RoundingMode.FLOOR, property),
            BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
    }
}