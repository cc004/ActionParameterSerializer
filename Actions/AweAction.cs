namespace ActionParameterSerializer.Actions;









public class AweAction : ActionParameter
{

    public enum AweType
    {
        unknown = -1,
        ubOnly = 0,
        ubAndSkill = 1
    }

    public AweType aweType;
    protected List<ActionValue> durationValues = new();
    protected List<ActionValue> percentValues = new();

    public override void ChildInit()
    {
        base.ChildInit();
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        percentValues.Add(new ActionValue(actionValue1, actionValue2, null));
        aweType = (AweType)(actionDetail1);
    }

    public
        override string LocalizedDetail(int level, Property property)
    {
        return aweType switch
        {
            AweType.ubAndSkill => Utils.JavaFormat(Utils.GetString("Reduce_s1_damage_or_instant_healing_effect_of_union_burst_and_main_skills_cast_by_s2_for_s3_sec"),
                                    BuildExpression(level, percentValues, RoundingMode.UNNECESSARY, property),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)),
            AweType.ubOnly => Utils.JavaFormat(Utils.GetString("Reduce_s1_damage_or_instant_healing_effect_of_union_burst_cast_by_s2_for_s3_sec"),
                                    BuildExpression(level, percentValues, RoundingMode.UNNECESSARY, property),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
