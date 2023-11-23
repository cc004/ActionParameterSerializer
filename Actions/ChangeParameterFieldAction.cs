namespace ActionParameterSerializer.Actions;







public class ChangeParameterFieldAction : AuraAction
{

    public
    override void ChildInit()
    {
        base.ChildInit();
        actionValues.Clear();
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        durationValues.Clear();
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        base.percentModifier = actionDetail2 == 2 ? PercentModifier.percent : PercentModifier.number;
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        if (targetParameter.targetType == TargetType.absolute)
        {
            return Utils.JavaFormat(Utils.GetString("Summon_a_field_of_radius_d1_to_s2_s3_s4_s5_for_s6_sec"),
                    (int)actionValue5.value,
                    auraActionType.Description(),
                    targetParameter.BuildTargetClause(),
                    BuildExpression(level, RoundingMode.UP, property),
                    auraType.Description(),
                    BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property),
                    percentModifier.Description());
        }
        else
        {
            return Utils.JavaFormat(Utils.GetString("Summon_a_field_of_radius_d1_at_position_of_s2_to_s3_s4_s5_for_s6_sec"),
                    (int)actionValue5.value,
                    targetParameter.BuildTargetClause(),
                    auraActionType.Description(),
                    BuildExpression(level, RoundingMode.UP, property),
                    auraType.Description(),
                    BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property),
                    percentModifier.Description());
        }
    }
}
