using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionParameterSerializer.Actions
{
    class CopyAtkParamAction : ActionParameter
    {
        private enum EAtkType
        {
            ATK = 1, Magic_STR = 2
        }

        private int targetAction;
        private EAtkType propType;

        public override void ChildInit()
        {
            propType = (EAtkType) actionDetail1;
            targetAction = actionDetail2 % 10;
            base.ChildInit();
        }

        public override string LocalizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Use_param_s1_of_s2_for_action_d3"),
                propType.RawDescription(),
                targetParameter.BuildTargetClause(),
                targetAction
            );
        }
    }
}
