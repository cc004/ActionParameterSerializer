namespace ActionParameterSerializer.Actions;





public class StealthAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Stealth_for_s_sec"), actionValue1.ValueString());
    }
}
