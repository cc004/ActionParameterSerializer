namespace ActionParameterSerializer.Actions;

internal class DamageByBehaviourAction : ActionParameter
{

    public Ailment ailment;
    public List<ActionValue> durationValues = new();

    public enum EDamageType
    {
        normal = 0,
        current_max_hp = 1,
        current_hp = 2,
        start_max_hp = 3
    }
    
    public
    override void ChildInit()
    {
        base.ChildInit();
        ailment = new Ailment(rawActionType, actionDetail1);
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        var dmg = BuildExpression(level, property);
        var dmgType = (EDamageType) actionDetail2;

        if (dmgType != EDamageType.normal)
        {
            dmg = Utils.JavaFormat(Utils.GetString("s1_percent_of_s2_with_max_d3"), 
                dmg, dmgType.description(), actionValue5.value);
        }
        else
        {
            dmg = Utils.JavaFormat(" [%1$s] ", dmg);
        }

        return Utils.JavaFormat(Utils.GetString("s1_will_be_applied_the_s2_once_they_take_any_actions_will_take_s3_damage_every_second_lasted_4s_seconds"),
                targetParameter.BuildTargetClause(),
                ailment.description(),
                dmg,
                BuildExpression(level, durationValues, RoundingMode.HALF_UP, property));
    }
}
