namespace ActionParameterSerializer.Actions;








public class NoDamageAction : ActionParameter
{

    public enum NoDamageType
    {
        unknown = 0,
        noDamage = 1,
        dodgePhysics = 2,
        dodgeAll = 3,
        abnormal = 4,
        debuff = 5,
        Break = 6
    }

    private NoDamageType noDamageType;

    public
    override void ChildInit()
    {
        noDamageType = (NoDamageType)(actionDetail1);
        actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return noDamageType switch
        {
            NoDamageType.noDamage => Utils.JavaFormat(Utils.GetString("Make_s1_to_be_invulnerable_for_s2_sec"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, RoundingMode.UNNECESSARY, property)),
            NoDamageType.dodgePhysics => Utils.JavaFormat(Utils.GetString("Make_s1_to_be_invulnerable_to_physical_damage_for_s2_sec"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, RoundingMode.UNNECESSARY, property)),
            NoDamageType.Break => Utils.JavaFormat(Utils.GetString("Make_s1_to_be_invulnerable_to_break_for_s2_sec"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, RoundingMode.UNNECESSARY, property)),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
