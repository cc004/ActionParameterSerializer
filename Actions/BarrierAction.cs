namespace ActionParameterSerializer.Actions;






public class BarrierAction : ActionParameter
{

    public enum BarrierType
    {
        unknown = 0,
        physicalGuard = 1,
        magicalGuard = 2,
        physicalDrain = 3,
        magicalDrain = 4,
        bothGuard = 5,
        bothDrain = 6
    }

    public BarrierType barrierType;

    public
    override void ChildInit()
    {
        barrierType = (BarrierType)(actionDetail1);
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return barrierType switch
        {
            BarrierType.physicalGuard => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_nullify_s2_physical_damage_for_s3_sec"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, property),
                                    Utils.RoundDouble(actionValue3.value)),
            BarrierType.magicalGuard => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_nullify_s2_magical_damage_for_s3_sec"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, property),
                                    Utils.RoundDouble(actionValue3.value)),
            BarrierType.physicalDrain => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_absorb_s2_physical_damage_for_s3_sec"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, property),
                                    Utils.RoundDouble(actionValue3.value)),
            BarrierType.magicalDrain => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_absorb_s2_magical_damage_for_s3_sec"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, property),
                                    Utils.RoundDouble(actionValue3.value)),
            BarrierType.bothDrain => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_absorb_s2_physical_and_magical_damage_for_s3_sec"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, property),
                                    Utils.RoundDouble(actionValue3.value)),
            BarrierType.bothGuard => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_nullify_s2_physical_and_magical_damage_for_s3_sec"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, property),
                                    Utils.RoundDouble(actionValue3.value)),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
