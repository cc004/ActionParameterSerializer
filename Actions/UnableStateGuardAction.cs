namespace ActionParameterSerializer.Actions;



public class UnableStateGuardAction : ActionParameter
{

    protected List<ActionValue> durationValues = new();

    public
    override void ChildInit()
    {
        base.ChildInit();
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        if (actionValue1.value == -1 && actionValue2.value == 0)
        {
            return Utils.JavaFormat(Utils.GetString("Enable_s1_to_resist_all_sorts_of_incapacity_efficacies_in_a_period_of_s2_sec"),
                targetParameter.BuildTargetClause(),
                BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
            );
        }
        
        return Utils.JavaFormat(Utils.GetString("Enable_s1_to_resist_all_sorts_of_incapacity_efficacies_up_to_s2_times_in_a_period_of_s3_sec"),
                targetParameter.BuildTargetClause(),
                BuildExpression(level, property),
                BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
        );
    }
}
