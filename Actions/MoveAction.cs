namespace ActionParameterSerializer.Actions;






public class MoveAction : ActionParameter
{

    public enum MoveType
    {
        unknown = 0,
        targetReturn = 1,
        absoluteReturn = 2,
        target = 3,
        absolute = 4,
        targetByVelocity = 5,
        absoluteByVelocity = 6,
        absoluteWithoutDirection = 7
    }

    private MoveType moveType;

    public
    override void ChildInit()
    {
        moveType = (MoveType)actionDetail1;
    }
    public
    override string LocalizedDetail(int level, Property property)
    {
        switch (moveType)
        {
            case MoveType.targetReturn:
                return Utils.JavaFormat(Utils.GetString("Change_self_position_to_s_then_return"), targetParameter.BuildTargetClause());
            case MoveType.absoluteReturn:
                if (actionValue1.value > 0)
                {
                    return Utils.JavaFormat(Utils.GetString("Change_self_position_s_forward_then_return"), Utils.roundDownDouble(actionValue1.value));
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("Change_self_position_s_backward_then_return"), Utils.roundDownDouble(-actionValue1.value));
                }

            case MoveType.target:
                return Utils.JavaFormat(Utils.GetString("Change_self_position_to_s"), targetParameter.BuildTargetClause());
            case MoveType.absolute:
            case MoveType.absoluteWithoutDirection:
                if (actionValue1.value > 0)
                {
                    return Utils.JavaFormat(Utils.GetString("Change_self_position_s_forward"), Utils.roundDownDouble(actionValue1.value));
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("Change_self_position_s_backward"), Utils.roundDownDouble(-actionValue1.value));
                }

            case MoveType.targetByVelocity:
                if (actionValue1.value > 0)
                {
                    return Utils.JavaFormat(Utils.GetString("Move_to_s1_in_front_of_s2_with_velocity_s3_sec"), Utils.roundDownDouble(actionValue1.value), targetParameter.BuildTargetClause(), actionValue2.ValueString());
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("Move_to_s1_behind_of_s2_with_velocity_s3_sec"), Utils.roundDownDouble(-actionValue1.value), targetParameter.BuildTargetClause(), actionValue2.ValueString());
                }

            case MoveType.absoluteByVelocity:
                if (actionValue1.value > 0)
                {
                    return Utils.JavaFormat(Utils.GetString("Move_forward_s1_with_velocity_s2_sec"), Utils.roundDownDouble(actionValue1.value), actionValue2.ValueString());
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("Move_backward_s1_with_velocity_s2_sec"), Utils.roundDownDouble(-actionValue1.value), actionValue2.ValueString());
                }

            default:
                return base.LocalizedDetail(level, property);
        }
    }

}
