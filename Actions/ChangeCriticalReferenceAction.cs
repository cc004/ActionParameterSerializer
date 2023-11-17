using Newtonsoft.Json.Linq;

namespace ActionParameterSerializer.Actions;

public class ChangeCriticalReferenceAction : ActionParameter
{
    public enum eCriticalReference
    {
        normal,
        Physical_Critical,
        Magic_Critical,
        sum_critical
    }

    private eCriticalReference refType;
    private int actionId;

    public override void childInit()
    {
        base.childInit();
        refType = (eCriticalReference) actionDetail2;
        actionId = actionDetail1 % 10;
    }

    public override string localizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Use_critical_reference_s1_for_skill_d2"),
            refType.rawDescription(),
            actionId);
    }
}