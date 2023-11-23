namespace ActionParameterSerializer.Actions;





public class StopFieldAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Remove_field_of_skill_d1_1_represents_the_first_skill_in_this_list_effect_d2"),
                actionDetail1 / 100 % 10,
                actionDetail1 % 10);
    }
}
