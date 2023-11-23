using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace ActionParameterSerializer.Actions;

public class ChangeCriticalReferenceAction : ActionParameter
{
    public enum ECriticalReference
    {
        normal,
        Physical_Critical,
        Magic_Critical,
        sum_critical
    }

    private ECriticalReference refType;
    private new int actionId;

    public override void ChildInit()
    {
        base.ChildInit();
        refType = (ECriticalReference) actionDetail2;
        actionId = actionDetail1 % 10;
    }

    public override string LocalizedDetail(int level, Property property)
    {
        if (refType == ECriticalReference.normal)
            return Utils.GetString("no_effect");
        return Utils.JavaFormat(Utils.GetString("Use_critical_reference_s1_for_skill_d2"),
            refType.RawDescription(),
            actionId);
    }
}