using System.Text;

namespace ActionParameterSerializer.Actions;







public class DamageAction : ActionParameter
{

    public ClassModifier damageClass;
    public CriticalModifier criticalModifier;

    public
    override void childInit()
    {
        damageClass = (ClassModifier)(actionDetail1);
        criticalModifier = (int)actionValue5.value == 1 ? CriticalModifier.critical : CriticalModifier.normal;

        switch (damageClass)
        {
            case ClassModifier.magical:
                actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
                actionValues.Add(new ActionValue(actionValue3, actionValue4, PropertyKey.magicStr));
                break;
            case ClassModifier.physical:
            case ClassModifier.inevitablePhysical:
                actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
                actionValues.Add(new ActionValue(actionValue3, actionValue4, PropertyKey.atk));
                break;
            default:
                break;
        }
    }

    public
    override string localizedDetail(int level, Property property)
    {
        StringBuilder str = new StringBuilder();
        switch (criticalModifier)
        {
            case CriticalModifier.normal:
                str.Append(Utils.JavaFormat(Utils.GetString("Deal_s1_s2_damage_to_s3"), buildExpression(level, property), damageClass.description(), targetParameter.buildTargetClause()));
                break;
            case CriticalModifier.critical:
                str.Append(Utils.JavaFormat(Utils.GetString("Deal_s1_s2_damage_to_s3_and_this_attack_is_ensured_critical"), buildExpression(level, property), damageClass.description(), targetParameter.buildTargetClause(), Utils.roundIfNeed(actionValue5.value)));
                break;
        }
        if (actionValue6.value != 0)
        {
            str.Append(Utils.JavaFormat(Utils.GetString("Critical_damage_is_s_times_as_normal_damage"), 2 * actionValue6.value));
        }
        return str.ToString();
    }
}
