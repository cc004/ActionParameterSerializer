namespace ActionParameterSerializer.Actions;






public class ModeChangeAction : ActionParameter
{

    public enum ModeChangeType
    {
        unknown = 0,
        time = 1,
        energy = 2,
        release = 3
    }

    public enum EAdditionalAbnormalType
    {
        NONE,
        FLIGHT
    }

    public enum ECancelTriggerType
    {
        NONE,
        UNABLE_ABNORMAL_OR_CHARM
    }

    private ModeChangeType modeChangeType;
    private EAdditionalAbnormalType additionalAbnormalType;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "<Pending>")]
    private ECancelTriggerType cancelTriggerType;

    public
    override void ChildInit()
    {
        modeChangeType = (ModeChangeType)(actionDetail1);
        additionalAbnormalType = (EAdditionalAbnormalType)(actionValue5.value);
        cancelTriggerType = (ECancelTriggerType)(actionValue6.value);
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return modeChangeType switch
        {
            ModeChangeType.time => Utils.JavaFormat(Utils.GetString("Change_attack_pattern_to_d1_for_s2_sec_set_abnormal_state_s3_to_self"),
                                    actionDetail2 % 10, actionValue1.ValueString(), additionalAbnormalType.GetPascalDescription()),
            ModeChangeType.energy => Utils.JavaFormat(Utils.GetString("Cost_s1_TP_sec_change_attack_pattern_to_d2_until_TP_is_zero"),
                                    Utils.RoundDownDouble(actionValue1.value), actionDetail2 % 10),
            ModeChangeType.release => Utils.JavaFormat(Utils.GetString("Change_attack_pattern_back_to_d_after_effect_over"),
                                    actionDetail2 % 10),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
