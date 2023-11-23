namespace ActionParameterSerializer.Actions;





public class SpyAction : ActionParameter
{
    public enum CancelType
    {
        None = 0,
        Damaged = 1
    }

    public CancelType cancelType;
    public List<ActionValue> durationValues = new();

    public
    override void ChildInit()
    {
        base.ChildInit();
        cancelType = (CancelType)actionDetail2;
        durationValues.Add(new ActionValue(actionValue1, actionValue2, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return cancelType switch
        {
            CancelType.Damaged => Utils.JavaFormat(Utils.GetString("Make_s1_invisible_for_s2_cancels_on_taking_damage"),
                                    targetParameter.BuildTargetClause(),
                                    BuildExpression(level, actionValues, null, property)),
            _ => base.LocalizedDetail(level, property),
        };
    }
}
