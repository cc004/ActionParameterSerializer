namespace ActionParameterSerializer.Actions;





public class WaveStartIdleAction : ActionParameter
{
    public 
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Appear_after_s_sec_since_wave_start"),
                actionValue1.ValueString());
    }
}
