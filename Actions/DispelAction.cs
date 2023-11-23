namespace ActionParameterSerializer.Actions;









public class DispelAction : ActionParameter
{

    public enum DispelType
    {
        unknown = 0,
        buff = 1,
        debuff = 2,
        statusUpBuff = 3,
        barriers = 10
    }

    public DispelType dispelType;
    public List<ActionValue> chanceValues = new();

    public
    override void ChildInit()
    {
        base.ChildInit();
        dispelType = (DispelType)(actionDetail1);
        chanceValues.Add(new ActionValue(actionValue1, actionValue2, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Clear_all_s1_on_s2_with_chance_s3"),
                dispelType.Description(),
                targetParameter.BuildTargetClause(),
                BuildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property));
    }
}
