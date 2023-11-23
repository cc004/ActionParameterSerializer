namespace ActionParameterSerializer.Actions;









public class FearAction : ActionParameter
{

    public List<ActionValue> durationValues = new();
    public List<ActionValue> chanceValues = new();

    public
    override void ChildInit()
    {
        base.ChildInit();
        durationValues.Add(new ActionValue(actionValue1, actionValue2, null));
        chanceValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Fear_s1_with_s2_chance_for_s3_sec"),
                targetParameter.BuildTargetClause(),
                BuildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property),
                BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
    }
}
