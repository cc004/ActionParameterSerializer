using static ActionParameterSerializer.Actions.EveryAttackCriticalAction;

namespace ActionParameterSerializer.Actions;

public class EnvironmentAction : ActionParameter
{
    enum EnvironmentType
    {
        thundering = 137
    }

    private EnvironmentType environmentType;
    public override void childInit()
    {
        base.childInit();
        environmentType = (EnvironmentType) actionDetail2;
    }

    public override string localizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Summon_field_of_s1_environment_for_s2_for_s3_sec"),
            environmentType.description(),
            targetParameter.buildTargetClause(),
            actionValue1.value);
    }
}