namespace ActionParameterSerializer.Actions;

internal class PassiveSealAction : ActionParameter
{
    public int sealNumLimit;
    public List<ActionValue> sealDuration = new();
    public List<ActionValue> lifeTime = new();
    public EPassiveTiming passiveTiming;
    public ESealTarget sealTarget;

    public
    override void ChildInit()
    {
        sealNumLimit = (int)actionValue1.value;
        sealDuration.Add(new ActionValue(actionValue3, actionValue4, null));
        lifeTime.Add(new ActionValue(actionValue5, actionValue6, null));
        passiveTiming = (EPassiveTiming)(actionDetail1);
        sealTarget = (ESealTarget)(actionDetail3);
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Passive_Whenever_s1_get_s2_seals_s3_with_d4_marks_id_d5_for_s6_sec_caps_at_d7_This_passive_skill_will_listen_for_s8_sec"),
                targetParameter.BuildTargetClause(),
                passiveTiming.Description(),
                sealTarget.Description(),
                actionDetail2,
                (int)actionValue2.value,
                BuildExpression(level, sealDuration, RoundingMode.UNNECESSARY, property),
                (int)actionValue1.value,
                BuildExpression(level, lifeTime, RoundingMode.UNNECESSARY, property)
        );
    }

    public enum EPassiveTiming
    {
        Unknown = -1,
        Buff = 1,
        Damage = 2
    }

    public enum ESealTarget
    {
        Unknown = -1,
        Self = 0
    }
}
