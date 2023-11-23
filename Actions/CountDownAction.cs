namespace ActionParameterSerializer.Actions;





public class CountDownAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Set_a_countdown_timer_on_s1_trigger_effect_d2_after_s3_sec"),
                targetParameter.BuildTargetClause(), actionDetail1 % 10, actionValue1.ValueString());
    }
}