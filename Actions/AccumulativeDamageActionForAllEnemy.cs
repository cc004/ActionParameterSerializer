namespace ActionParameterSerializer.Actions;

public class AccumulativeDamageActionForAllEnemy : AccumulativeDamageAction
{
    public
        override string LocalizedDetail(int level, Property property)
    {
        return Utils.JavaFormat(Utils.GetString("Add_additional_s1_damage_per_attack_with_max_s2_stacks_to_current_target_for_all_enemy"),
            BuildExpression(level, property), BuildExpression(level, stackValues, RoundingMode.FLOOR, property));
    }
}