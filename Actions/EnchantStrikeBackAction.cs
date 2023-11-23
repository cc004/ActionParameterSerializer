namespace ActionParameterSerializer.Actions;








public class EnchantStrikeBackAction : BarrierAction
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return barrierType switch
        {
            BarrierType.physicalGuard => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_when_taking_physical_damage"),
                                    targetParameter.BuildTargetClause(), BuildExpression(level, RoundingMode.CEILING, property), actionValue3.ValueString()),
            BarrierType.magicalGuard => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_when_taking_magical_damage"),
                                    targetParameter.BuildTargetClause(), BuildExpression(level, RoundingMode.CEILING, property), actionValue3.ValueString()),
            BarrierType.physicalDrain => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_and_recover_the_same_HP_when_taking_physical_damage"),
                                    targetParameter.BuildTargetClause(), BuildExpression(level, RoundingMode.CEILING, property), actionValue3.ValueString()),
            BarrierType.magicalDrain => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_and_recover_the_same_HP_when_taking_magical_damage"),
                                    targetParameter.BuildTargetClause(), BuildExpression(level, RoundingMode.CEILING, property), actionValue3.ValueString()),
            BarrierType.bothGuard => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_when_taking_damage"),
                                    targetParameter.BuildTargetClause(), BuildExpression(level, RoundingMode.CEILING, property), actionValue3.ValueString()),
            BarrierType.bothDrain => Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_and_recover_the_same_HP_when_taking_damage"),
                                    targetParameter.BuildTargetClause(), BuildExpression(level, RoundingMode.CEILING, property), actionValue3.ValueString()),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
