namespace ActionParameterSerializer.Actions;






public class ChangePatternAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        switch (actionDetail1)
        {
            case 1:
                if (actionValue1.value > 0)
                {
                    return Utils.JavaFormat(Utils.GetString("Change_attack_pattern_to_d1_for_s2_sec"),
                            actionDetail2 % 10, Utils.RoundDouble(actionValue1.value));
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("Change_attack_pattern_to_d"),
                            actionDetail2 % 10);
                }
            case 2:
                return Utils.JavaFormat(Utils.GetString("Change_skill_visual_effect_for_s_sec"),
                        Utils.RoundDouble(actionValue1.value));
            default:
                return base.LocalizedDetail(level, property);
        }
    }
}
