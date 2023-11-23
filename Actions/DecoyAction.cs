namespace ActionParameterSerializer.Actions;





public class DecoyAction : ActionParameter
{
    public
    override void ChildInit()
    {
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Make_s1_attract_enemy_attacks_last_for_s2_sec"),
                targetParameter.BuildTargetClause(), BuildExpression(level, property));
    }
}
