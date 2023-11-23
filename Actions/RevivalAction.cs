namespace ActionParameterSerializer.Actions;





public class RevivalAction : ActionParameter
{

    public enum RevivalType
    {
        unknown = 0,
        normal = 1,
        phoenix = 2
    }

    private RevivalType revivalType;

    public
    override void ChildInit()
    {
        revivalType = (RevivalType)(actionDetail1);
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return revivalType switch
        {
            RevivalType.normal => Utils.JavaFormat(Utils.GetString("Revive_s1_with_d2_HP"),
                                    targetParameter.BuildTargetClause(), Math.Round(actionValue2.value * 100)),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
