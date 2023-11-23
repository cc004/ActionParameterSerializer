namespace ActionParameterSerializer.Actions;





public class DestroyAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Kill_s_instantly"), targetParameter.BuildTargetClause());
    }
}
