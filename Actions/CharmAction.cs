namespace ActionParameterSerializer.Actions;









public class CharmAction : ActionParameter
{

    public enum CharmType
    {
        unknown = -1,
        charm = 0,
        confusion = 1
    }

    private readonly List<ActionValue> chanceValues = new();
    private readonly List<ActionValue> durationValues = new();
    private CharmType charmType;

    public
    override void ChildInit()
    {
        charmType = (CharmType)(actionDetail1);
        durationValues.Add(new ActionValue(actionValue1, actionValue2, null));
        switch (charmType)
        {
            case CharmType.charm:
                chanceValues.Add(new ActionValue(actionValue3.value, actionValue4.value * 100, EActionValue.VALUE3, EActionValue.VALUE4, null));
                break;
            default:
                chanceValues.Add(new ActionValue(actionValue3.value * 100, actionValue4.value * 100, EActionValue.VALUE3, EActionValue.VALUE4, null));
                break;
        }
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return charmType switch
        {
            CharmType.charm => Utils.JavaFormat(
                                    Utils.GetString("Charm_s1_with_s2_chance_for_s3_sec"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property),
                                    BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
                            ),
            CharmType.confusion => Utils.JavaFormat(Utils.GetString("Confuse_s1_with_s2_chance_for_s3_sec"), targetParameter.BuildTargetClause(), BuildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property), BuildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
