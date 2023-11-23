namespace ActionParameterSerializer.Actions;





public class LoopTriggerAction : ActionParameter
{

    public enum TriggerType
    {
        unknown = 0,
        dodge = 1,
        damaged = 2,
        hp = 3,
        dead = 4,
        criticalDamaged = 5,
        getCriticalDamagedWithSummon = 6
    }

    public TriggerType triggerType;

    public
    override void ChildInit()
    {
        base.ChildInit();
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        triggerType = (TriggerType)(actionDetail1);
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return triggerType switch
        {
            TriggerType.damaged => Utils.JavaFormat(Utils.GetString("Condition_s1_chance_use_d2_when_takes_damage_within_s3_sec"),
                                    BuildExpression(level, property), actionDetail2 % 10, actionValue4.ValueString()),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
