namespace ActionParameterSerializer.Actions;

internal class DamageByBehaviourAction : ActionParameter
{

    public Ailment ailment;
    public List<ActionValue> durationValues = new();

    public
    override void childInit()
    {
        base.childInit();
        ailment = new Ailment(rawActionType, actionDetail1);
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public
    override string localizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("s1_will_be_applied_the_s2_once_they_take_any_actions_will_take_s3_damage_every_second_lasted_4s_seconds"),
                targetParameter.buildTargetClause(),
                ailment.description(),
                buildExpression(level, property),
                buildExpression(level, durationValues, RoundingMode.HALF_UP, property));
    }
}
