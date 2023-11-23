namespace ActionParameterSerializer.Actions;










public class DamageCutAction : ActionParameter
{

    public enum DamageType
    {
        Physical = 1,
        Magical = 2,
        All = 3
    }

    public DamageType damageType;
    public List<ActionValue> durationValues = new();

    public
    override void ChildInit()
    {
        damageType = (DamageType)actionDetail1;
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return damageType switch
        {
            DamageType.Physical => Utils.JavaFormat(Utils.GetString("Reduce_s1_physical_damage_taken_by_s2_for_s3_sec"),
                                    BuildExpression(level, actionValues, null, property),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, durationValues, null, property)),
            DamageType.Magical => Utils.JavaFormat(Utils.GetString("Reduce_s1_magical_damage_taken_by_s2_for_s3_sec"),
                                    BuildExpression(level, actionValues, null, property),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, durationValues, null, property)),
            DamageType.All => Utils.JavaFormat(Utils.GetString("Reduce_s1_all_damage_taken_by_s2_for_s3_sec"),
                                    BuildExpression(level, actionValues, null, property),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, durationValues, null, property)),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
