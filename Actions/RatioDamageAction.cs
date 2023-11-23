namespace ActionParameterSerializer.Actions;








public class RatioDamageAction : ActionParameter
{

    public enum HPtype
    {
        unknown = 0,
        max = 1,
        current = 2,
        originalMax = 3
    }

    public HPtype hptype;

    public
    override void ChildInit()
    {
        base.ChildInit();
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        hptype = (HPtype)(actionDetail1);
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        string r = BuildExpression(level, RoundingMode.UNNECESSARY, property);
        if (UserSettings.Get().GetExpression() != UserSettings.EXPRESSION_VALUE)
        {
            r = Utils.JavaFormat("(%s)", r);
        }
        return hptype switch
        {
            HPtype.max => Utils.JavaFormat(Utils.GetString("Deal_damage_equal_to_s1_of_target_max_HP_to_s2"),
                                    r, targetParameter.BuildTargetClause()),
            HPtype.current => Utils.JavaFormat(Utils.GetString("Deal_damage_equal_to_s1_of_target_current_HP_to_s2"),
                                    r, targetParameter.BuildTargetClause()),
            HPtype.originalMax => Utils.JavaFormat(Utils.GetString("Deal_damage_equal_to_s1_of_targets_original_max_HP_to_s2"),
                                    r, targetParameter.BuildTargetClause()),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
