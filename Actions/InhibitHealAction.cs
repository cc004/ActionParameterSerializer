namespace ActionParameterSerializer.Actions;









public class InhibitHealAction : ActionParameter
{
    public enum InhibitType
    {
        inhibit = 0,
        decrease = 1
    }

    public InhibitType inhibitType;
    public List<ActionValue> durationValues = new();

    public
    override void ChildInit()
    {
        base.ChildInit();
        actionValues.Add(new ActionValue(actionValue1, actionValue4, null));
        durationValues.Add(new ActionValue(actionValue2, actionValue3, null));
        inhibitType = (InhibitType)(actionDetail1);
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return inhibitType switch
        {
            InhibitType.inhibit => Utils.JavaFormat(Utils.GetString("When_s1_receive_healing_deal_s2_healing_amount_damage_instead_last_for_s3_sec_or_unlimited_time_if_triggered_by_field"),
                                    targetParameter.BuildTargetClause(),
                                    actionValue1.ValueString(),
                                    BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)),
            InhibitType.decrease => Utils.JavaFormat(Utils.GetString("Decreases_s1_healing_received_by_s2_last_for_s3_sec_or_unlimited_time_if_triggered_by_field"),
                                    Utils.RoundIfNeed(actionValue1.value * 100),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
