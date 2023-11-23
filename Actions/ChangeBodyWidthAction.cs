namespace ActionParameterSerializer.Actions;





public class ChangeBodyWidthAction : ActionParameter
{
    public override void ChildInit()
    {
        base.ChildInit();
    }
    
    public override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Change_body_width_to_s"), actionValue1.ValueString());
    }
}
