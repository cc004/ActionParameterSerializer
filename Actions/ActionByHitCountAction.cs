namespace ActionParameterSerializer.Actions;










public class ActionByHitCountAction : ActionParameter
{

    public enum ConditionType
    {
        unknown = 0,
        damage = 1,
        target = 2,
        hit = 3,
        critical = 4
    }

    public ConditionType conditionType;
    public List<ActionValue> durationValues = new();

    public
    override void ChildInit()
    {
        conditionType = (ConditionType)(actionDetail1);
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        string limitation;
        if (actionValue5.value > 0)
        {
            limitation = Utils.JavaFormat(Utils.GetString("max_s_times"), Utils.RoundIfNeed(actionValue5.value));
        }
        else
        {
            limitation = "";
        }
        return conditionType switch
        {
            ConditionType.hit => Utils.JavaFormat(Utils.GetString("Use_d1_s2_every_s3_hits_in_next_s4_sec"),
                                actionDetail2 % 10,
                                limitation,
                                Utils.RoundIfNeed(actionValue1.value),
                                BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
                            ),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
