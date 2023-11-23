namespace ActionParameterSerializer.Actions;







public class ChannelAction : AuraAction
{

    public enum ReleaseType
    {
        damage = 1,
        unknown = 2
    }

    protected ReleaseType releaseType;

    public override void ChildInit()
    {
        base.ChildInit();
        releaseType = (ReleaseType)actionDetail2;
    }

    public
        override string LocalizedDetail(int level, Property property)
    {
        return releaseType switch
        {
            ReleaseType.damage => Utils.JavaFormat(Utils.GetString("Channeling_for_s1_sec_disrupted_by_taking_damage_d2_times_s3_s4_s5_s6_s7"),
                                    BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property),
                                    actionDetail3,
                                    auraActionType.Description(),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, RoundingMode.UP, property),
                                    percentModifier.Description(),
                                    auraType.Description()),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
