namespace ActionParameterSerializer.Actions;






public class SealAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        if (actionValue4.value >= 0)
        {
            return Utils.JavaFormat(Utils.GetString("Add_s1_mark_stacks_max_s2_ID_s3_on_s4_for_s5_sec"),
                    Utils.RoundDownDouble(actionValue4.value),
                    Utils.RoundDownDouble(actionValue1.value),
                    Utils.RoundDownDouble(actionValue2.value),
                    targetParameter.BuildTargetClause(),
                    Utils.RoundDouble(actionValue3.value));
        }
        else
        {
            return Utils.JavaFormat(Utils.GetString("Remove_s1_mark_stacks_ID_s2_on_s3"),
                    Utils.RoundDownDouble(-actionValue4.value),
                    Utils.RoundDownDouble(actionValue2.value),
                    targetParameter.BuildTargetClause());
        }
    }
}
