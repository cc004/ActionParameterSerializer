namespace ActionParameterSerializer.Actions;





public class SkillExecCountAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Add_d1_to_the_counter_d2"), actionDetail1, (int)actionValue1.value);
    }
}
