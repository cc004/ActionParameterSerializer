namespace ActionParameterSerializer.Actions;









public class AbnormalStateFieldAction : ActionParameter
{

    public List<ActionValue> durationValues = new();

    public
    override void ChildInit()
    {
        base.ChildInit();
        durationValues.Add(new ActionValue(actionValue1, actionValue2, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Summon_a_field_of_radius_d1_on_s2_to_cast_effect_d3_for_s4_sec"),
                (int)actionValue3.value,
                targetParameter.BuildTargetClause(),
                actionDetail1 % 10,
                BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
    }
}
