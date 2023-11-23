namespace ActionParameterSerializer.Actions;





public class IgnoreDecoyAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Ignore_the_other_units_taunt_when_attacking_s"), targetParameter.BuildTargetClause());
    }
}
