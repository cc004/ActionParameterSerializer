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

    public enum eAdditionalAbnormalType
    {
        NONE,
        FLIGHT
    }

    public enum eCancelTriggerType
    {
        NONE,
        UNABLE_ABNORMAL_OR_CHARM
    }

    private ModeChangeType modeChangeType;
    private eAdditionalAbnormalType additionalAbnormalType;
    private eCancelTriggerType cancelTriggerType;

    public
    override void childInit()
    {
        modeChangeType = (ModeChangeType)(actionDetail1);
        additionalAbnormalType = (eAdditionalAbnormalType)(actionValue5.value);
        cancelTriggerType = (eCancelTriggerType)(actionValue6.value);
    }

    public
    override string localizedDetail(int level, Property property)
    {
        switch (modeChangeType)
        {
            case ModeChangeType.time:
                return Utils.JavaFormat(Utils.GetString("Change_attack_pattern_to_d1_for_s2_sec_set_abnormal_state_s3_to_self"),
                        actionDetail2 % 10, actionValue1.valueString(), additionalAbnormalType.GetDescription());
            case ModeChangeType.energy:
                return Utils.JavaFormat(Utils.GetString("Cost_s1_TP_sec_change_attack_pattern_to_d2_until_TP_is_zero"),
                        Utils.roundDownDouble(actionValue1.value), actionDetail2 % 10);
            case ModeChangeType.release:
                return Utils.JavaFormat(Utils.GetString("Change_attack_pattern_back_to_d_after_effect_over"),
                        actionDetail2 % 10);
            default:
                return base.localizedDetail(level, property);
        }
    }
}
