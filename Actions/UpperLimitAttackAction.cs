namespace ActionParameterSerializer.Actions;





public class UpperLimitAttackAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("s_Damage_is_reduced_on_low_level_players"),
                base.LocalizedDetail(level, property));
    }
}
