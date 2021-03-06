namespace ActionParameterSerializer.Actions;





public class IfForChildrenAction : ActionParameter
{

    private string trueClause;
    private string falseClause;
    private IfType ifType;

    public
    override void childInit()
    {

        if (actionDetail2 != 0)
        {
            ifType = (IfType)(actionDetail1);
            if (Enum.IsDefined(ifType))
            {
                trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_s3"),
                        actionDetail2 % 100, targetParameter.buildTargetClause(true), ifType.description());
            }
            else
            {
                if ((actionDetail1 >= 600 && actionDetail1 < 700) || actionDetail1 == 710)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_in_state_of_ID_d3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 600);
                }
                else if (actionDetail1 == 700)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_it_is_alone"),
                            actionDetail2 % 10, targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 >= 901 && actionDetail1 < 1000)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_HP_is_below_d3"),
                            actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 900);
                }
                else if (actionDetail1 == 1300)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_target_is_magical_type"),
                            actionDetail2 % 10, targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 >= 6000 && actionDetail1 < 7000)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_in_state_of_ID_d3"),
                            actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 6000);
                }
            }
        }

        if (actionDetail3 != 0)
        {
            ifType = (IfType)(actionDetail1);
            if (Enum.IsDefined(ifType))
            {
                falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_s3"),
                        actionDetail3 % 100, targetParameter.buildTargetClause(true), ifType.description());
            }
            else
            {
                if ((actionDetail1 >= 600 && actionDetail1 < 700) || actionDetail1 == 710)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_in_state_of_ID_d3"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 600);
                }
                else if (actionDetail1 == 700)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_it_is_not_alone"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 >= 901 && actionDetail1 < 1000)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_HP_is_not_below_d3"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 900);
                }
                else if (actionDetail1 == 1300)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_target_is_not_magical_type"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 >= 6000 && actionDetail1 < 7000)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_in_state_of_ID_d3"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 6000);
                }
            }
        }

    }

    public
    override string localizedDetail(int level, Property property)
    {
        if (actionDetail1 == 100 || actionDetail1 == 101 || actionDetail1 == 200 || actionDetail1 == 300 || actionDetail1 == 500 || actionDetail1 == 501
                || actionDetail1 == 502 || actionDetail1 == 503 || actionDetail1 == 512
                || (actionDetail1 >= 600 && actionDetail1 < 900) || (actionDetail1 >= 901 && actionDetail1 < 1000)
                || actionDetail1 == 1300 || actionDetail1 == 1400 || (actionDetail1 >= 6000 && actionDetail1 < 7000))
        {
            if (trueClause != null && falseClause != null)
            {
                return Utils.JavaFormat(Utils.GetString("Condition_s"), trueClause + falseClause);
            }
            else if (trueClause != null)
            {
                return Utils.JavaFormat(Utils.GetString("Condition_s"), trueClause);
            }
            else if (falseClause != null)
            {
                return Utils.JavaFormat(Utils.GetString("Condition_s"), falseClause);
            }
        }
        else if (actionDetail1 >= 0 && actionDetail1 < 100)
        {
            if (actionDetail2 != 0 && actionDetail3 != 0)
            {
                return Utils.JavaFormat(Utils.GetString("Random_event_d1_chance_use_d2_otherwise_d3"), actionDetail1, actionDetail2 % 10, actionDetail3 % 10);
            }
            else if (actionDetail2 != 0)
            {
                return Utils.JavaFormat(Utils.GetString("Random_event_d1_chance_use_d2"), actionDetail1, actionDetail2 % 10);
            }
            else if (actionDetail3 != 0)
            {
                return Utils.JavaFormat(Utils.GetString("Random_event_d1_chance_use_d2"), 100 - actionDetail1, actionDetail3 % 10);
            }
        }
        return base.localizedDetail(level, property);
    }
}

public enum IfType
{
    controlled = 100,
    hastened = 101,
    blinded = 200,
    charmed_or_confused = 300,
    decoying = 400,
    burned = 500,
    cursed = 501,
    poisoned = 502,
    venomed = 503,
    poisoned_or_venomed = 512,
    breaking = 710,
    polymorphed = 1400
}