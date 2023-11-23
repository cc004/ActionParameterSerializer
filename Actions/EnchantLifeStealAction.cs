namespace ActionParameterSerializer.Actions;










public class EnchantLifeStealAction : ActionParameter
{

    private readonly List<ActionValue> stackValues = new();

    public
    override void ChildInit()
    {
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        stackValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Add_additional_s1_s2_to_s3_for_next_s4_attacks"),
                BuildExpression(level, property), PropertyKey.lifeSteal.description(), targetParameter.BuildTargetClause(), BuildExpression(level, stackValues, RoundingMode.FLOOR, property));
    }
}
