namespace ActionParameterSerializer.Actions;







public class SummonAction : ActionParameter
{

    public enum Side
    {
        unknown = -1,
        ours = 1,
        other = 2
    }

    public enum UnitType
    {
        unknown = -1,
        normal = 1,
        phantom = 2
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "<Pending>")]
    private Side side;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "<Pending>")]
    private UnitType unitType;


    public
    override void ChildInit()
    {
        side = (Side)(actionDetail3);
        unitType = (UnitType)((int)actionValue5.value);
    }

    public
    override string LocalizedDetail(int level, Property property)
    {
        if (actionValue7.value > 0)
        {
            return Utils.JavaFormat(Utils.GetString("At_d1_in_front_of_s2_summon_a_minion_id_d3"),
                    (int)actionValue7.value, targetParameter.BuildTargetClause(), actionDetail2);
        }
        else if (actionValue7.value < 0)
        {
            return Utils.JavaFormat(Utils.GetString("At_d1_behind_of_s2_summon_a_minion_id_d3"),
                    (int)-actionValue7.value, targetParameter.BuildTargetClause(), actionDetail2);
        }
        else
        {
            return Utils.JavaFormat(Utils.GetString("At_the_position_of_s1_summon_a_minion_id_d2"),
                    targetParameter.BuildTargetClause(), actionDetail2);
        }
    }
}
