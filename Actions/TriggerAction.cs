namespace ActionParameterSerializer.Actions;






public class TriggerAction : ActionParameter
{

    public enum TriggerType
    {
        unknown = 0,
        dodge = 1,
        damage = 2,
        hp = 3,
        dead = 4,
        critical = 5,
        criticalWithSummon = 6,
        limitTime = 7,
        stealthFree = 8,
        Break = 9,
        dot = 10,
        allBreak = 11
    }

    private TriggerType triggerType;

    public
    override void ChildInit()
    {
        triggerType = (TriggerType)(actionDetail1);
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return triggerType switch
        {
            TriggerType.hp => Utils.JavaFormat(Utils.GetString("Trigger_HP_is_below_d"), Math.Round(actionValue3.value)),
            TriggerType.limitTime => Utils.JavaFormat(Utils.GetString("Trigger_Left_time_is_below_s_sec"), Math.Round(actionValue3.value)),
            TriggerType.damage => Utils.JavaFormat(Utils.GetString("Trigger_d_on_damaged"), Math.Round(actionValue1.value)),
            TriggerType.dead => Utils.JavaFormat(Utils.GetString("Trigger_d_on_dead"), Math.Round(actionValue1.value)),
            TriggerType.critical => Utils.JavaFormat(Utils.GetString("Trigger_d_on_critical_damaged"), Math.Round(actionValue1.value)),
            TriggerType.stealthFree => Utils.JavaFormat(Utils.GetString("Trigger_d_on_stealth"), Math.Round(actionValue1.value)),
            TriggerType.Break => Utils.JavaFormat(Utils.GetString("Trigger_d1_on_break_and_last_for_s2_sec"), Math.Round(actionValue1.value), actionValue3.value),
            TriggerType.dot => Utils.JavaFormat(Utils.GetString("Trigger_d_on_dot_damaged"), Math.Round(actionValue1.value)),
            TriggerType.allBreak => Utils.JavaFormat(Utils.GetString("Trigger_d_on_all_targets_break"),
                                    Math.Round(actionValue1.value)),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
