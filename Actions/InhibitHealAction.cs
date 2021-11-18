namespace ActionParameterSerializer.Actions;









public class InhibitHealAction : ActionParameter
{

    public List<ActionValue> durationValues = new();

    public
    override void childInit()
    {
        base.childInit();
        durationValues.Add(new ActionValue(actionValue2, actionValue3, null));
    }

    public
    override string localizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("When_s1_receive_healing_deal_s2_healing_amount_damage_instead_last_for_s3_sec_or_unlimited_time_if_triggered_by_field"),
                targetParameter.buildTargetClause(),
                actionValue1.valueString(),
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
    }
}
