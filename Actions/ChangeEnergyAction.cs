namespace ActionParameterSerializer.Actions;







public class ChangeEnergyAction : ActionParameter
{

    public
    override void ChildInit()
    {
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        switch (actionDetail1)
        {
            case 1:
                if (targetParameter.targetType == TargetType.self)
                {
                    return Utils.JavaFormat(Utils.GetString("Restore_s1_s2_TP"), targetParameter.BuildTargetClause(), BuildExpression(level, null, RoundingMode.CEILING, property, false, true, false));
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("Restore_s1_s2_TP"), targetParameter.BuildTargetClause(), BuildExpression(level, RoundingMode.CEILING, property));
                }
            default:
                return Utils.JavaFormat(Utils.GetString("Make_s1_lose_s2_TP"), targetParameter.BuildTargetClause(), BuildExpression(level, RoundingMode.CEILING, property));
        }
    }
}
