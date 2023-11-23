namespace ActionParameterSerializer.Actions;







public class CountBlindAction : ActionParameter
{

    public enum CountType
    {
        unknown = -1,
        time = 1,
        count = 2
    }

    public CountType countType;

    public
    override void ChildInit()
    {
        countType = (CountType)((int)actionValue1.value);
        actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return countType switch
        {
            CountType.time => Utils.JavaFormat(Utils.GetString("In_nex_s1_sec_s2_physical_attacks_will_miss"),
                                    BuildExpression(level, RoundingMode.UNNECESSARY, property), targetParameter.BuildTargetClause()),
            CountType.count => Utils.JavaFormat(Utils.GetString("In_next_s1_attacks_s2_physical_attacks_will_miss"),
                                    BuildExpression(level, property), targetParameter.BuildTargetClause()),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
