namespace ActionParameterSerializer.Actions;









public class ToadAction : ActionParameter
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
        return Utils.JavaFormat(Utils.GetString("Polymorph_s1_for_s2_sec"),
                targetParameter.BuildTargetClause(),
                BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
    }
}
