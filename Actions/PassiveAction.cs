namespace ActionParameterSerializer.Actions;






public class PassiveAction : ActionParameter
{

    public PropertyKey propertyKey;

    public
    override void ChildInit()
    {
        propertyKey = actionDetail1 switch
        {
            1 => PropertyKey.hp,
            2 => PropertyKey.atk,
            3 => PropertyKey.def,
            4 => PropertyKey.magicStr,
            5 => PropertyKey.magicDef,
            _ => PropertyKey.unknown,
        };
        actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Raise_s1_s2"), BuildExpression(level, property), propertyKey.Description());
    }

    public Property PropertyItem(int level)
    {
        return Property.GetPropertyWithKeyAndValue(null, propertyKey, actionValue2.value + actionValue3.value * level);
    }
}
