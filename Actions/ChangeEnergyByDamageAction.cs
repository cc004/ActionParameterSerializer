namespace ActionParameterSerializer.Actions;





public class ChargeEnergyByDamageAction : ActionParameter
{
    public
    override void ChildInit()
    {
        base.ChildInit();
        actionValues.Add(new ActionValue(actionValue1.value, actionValue2.value, EActionValue.VALUE1, EActionValue.VALUE2, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return actionDetail1 switch
        {
            1 => Utils.JavaFormat(Utils.GetString("Adds_s1_marks_to_s5_max_s2_id_d3_lasts_for_s4_sec_removes_1_mark_while_taking_dmg_and_restores_s6_tp"),
                                    actionValue3.ValueString(),
                                    actionValue4.ValueString(),
                                    actionDetail2,
                                    actionValue5.ValueString(),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, actionValues, null, property)
                                    ),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
