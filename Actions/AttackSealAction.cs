namespace ActionParameterSerializer.Actions;










public class AttackSealAction : ActionParameter
{

    public enum Condition
    {
        unknown = -1,
        damage = 1,
        target = 2,
        hit = 3,
        criticalHit = 4
    }

    public enum Target
    {
        unknown = -1,
        target = 0,
        owner = 1
    }

    public Condition condition;
    public Target target;
    public List<ActionValue> durationValues = new();

    public
    override void childInit()
    {
        base.childInit();
        condition = (Condition)(actionDetail1);
        target = (Target)(actionDetail3);
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public
    override string localizedDetail(int level, Property property)
    {
        if (condition == Condition.hit)
        {
            return Utils.JavaFormat(Utils.GetString("Make_s1_when_get_one_hit_by_the_caster_gain_one_mark_stack_max_s2_ID_s3_for_s4_sec"),
                    targetParameter.buildTargetClause(),
                    Utils.roundDownDouble(actionValue1.value),
                    Utils.roundDownDouble(actionValue2.value),
                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
        else if (condition == Condition.damage && target == Target.owner)
        {
            return Utils.JavaFormat(Utils.GetString("Make_s1_when_deal_damage_gain_one_mark_stack_max_s2_ID_s3_for_s4_sec"),
                    targetParameter.buildTargetClause(),
                    Utils.roundDownDouble(actionValue1.value),
                    Utils.roundDownDouble(actionValue2.value),
                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
        else if (condition == Condition.criticalHit && target == Target.owner)
        {
            return Utils.JavaFormat(Utils.GetString("Make_s1_when_deal_critical_damage_gain_one_mark_stack_max_s2_ID_s3_for_s4_sec"),
                    targetParameter.buildTargetClause(),
                    Utils.roundDownDouble(actionValue1.value),
                    Utils.roundDownDouble(actionValue2.value),
                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
        else
        {
            return base.localizedDetail(level, property);
        }
    }
}
