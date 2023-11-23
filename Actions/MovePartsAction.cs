namespace ActionParameterSerializer.Actions;





public class MovePartsAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Move_Part_d1_d2_forward_then_return"),
                (int)actionValue4.value, (int)-actionValue1.value);
    }
}
