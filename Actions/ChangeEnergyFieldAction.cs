namespace ActionParameterSerializer.Actions;





public class ChangeEnergyFieldAction : ActionParameter
{
    public List<ActionValue> durationValues = new();

    public
    override void ChildInit()
    {
        base.ChildInit();
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return actionDetail1 switch
        {
            1 => Utils.JavaFormat(Utils.GetString("Summon_a_field_with_radius_d1_at_position_s2_which_continuous_restore_tp_s3_of_units_within_the_field_for_s4_sec"),
                                    (int)actionValue5.value,
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, actionValues, RoundingMode.CEILING, property, false, targetParameter.targetType == TargetType.self, false),
                                    BuildExpression(level, durationValues, null, property)),
            2 => Utils.JavaFormat(Utils.GetString("Summon_a_field_with_radius_d1_at_position_s2_which_continuous_lose_tp_s3_of_units_within_the_field_for_s4_sec"),
                                    (int)actionValue5.value,
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, actionValues, RoundingMode.CEILING, property, false, targetParameter.targetType == TargetType.self, false),
                                    BuildExpression(level, durationValues, null, property)),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
