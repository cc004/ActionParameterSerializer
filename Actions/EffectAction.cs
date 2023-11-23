namespace ActionParameterSerializer.Actions;





public class EffectAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Implement_some_visual_effects_to_s1"), targetParameter.BuildTargetClause());
    }
}
