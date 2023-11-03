namespace ActionParameterSerializer.Actions;



public class UnableStateGuardAction : ActionParameter
{

    protected List<ActionValue> durationValues = new();

    public
    override void childInit()
    {
        base.childInit();
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public
    override string localizedDetail(int level, Property property)
    {
        string amount = buildExpression(level, property);
        try
        {
            int intAmount = int.Parse(amount);
            if (intAmount < 0)
            {
                amount = ((long)int.MaxValue - int.MinValue + intAmount).ToString();
            }
        }
        catch (Exception ignored) { }

        return Utils.JavaFormat(Utils.GetString("Enable_s1_to_resist_all_sorts_of_incapacity_efficacies_up_to_s2_times_in_a_period_of_s3_sec"),
                targetParameter.buildTargetClause(),
                amount,
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
        );
    }
}
