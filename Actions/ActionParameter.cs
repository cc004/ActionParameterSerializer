using System;
using System.Text;

namespace ActionParameterSerializer.Actions;

















public class ActionParameter
{
    public static ActionParameter Type(int rawType)
    {
        return rawType switch
        {
            1 => new DamageAction(),
            2 => new MoveAction(),
            3 => new KnockAction(),
            4 => new HealAction(),
            5 => new CureAction(),
            6 => new BarrierAction(),
            7 => new ReflexiveAction(),
            8 or 9 or 12 or 13 => new AilmentAction(),
            10 => new AuraAction(),
            11 => new CharmAction(),
            14 => new ModeChangeAction(),
            15 => new SummonAction(),
            16 => new ChangeEnergyAction(),
            17 => new TriggerAction(),
            18 => new DamageChargeAction(),
            19 => new ChargeAction(),
            20 => new DecoyAction(),
            21 => new NoDamageAction(),
            22 => new ChangePatternAction(),
            23 => new IfForChildrenAction(),
            24 => new RevivalAction(),
            25 => new ContinuousAttackAction(),
            26 => new AdditiveAction(),
            27 => new MultipleAction(),
            28 => new IfForAllAction(),
            29 => new SearchAreaChangeAction(),
            30 => new DestroyAction(),
            31 => new ContinuousAttackNearbyAction(),
            32 => new EnchantLifeStealAction(),
            33 => new EnchantStrikeBackAction(),
            34 => new AccumulativeDamageAction(),
            35 => new SealAction(),
            36 => new AttackFieldAction(),
            37 => new HealFieldAction(),
            38 => new ChangeParameterFieldAction(),
            39 => new AbnormalStateFieldAction(),
            40 => new ChangeSpeedFieldAction(),
            41 => new UBChangeTimeAction(),
            42 => new LoopTriggerAction(),
            43 => new IfHasTargetAction(),
            44 => new WaveStartIdleAction(),
            45 => new SkillExecCountAction(),
            46 => new RatioDamageAction(),
            47 => new UpperLimitAttackAction(),
            48 => new RegenerationAction(),
            49 => new DispelAction(),
            50 => new ChannelAction(),
            52 => new ChangeBodyWidthAction(),
            53 => new IFExistsFieldForAllAction(),
            54 => new StealthAction(),
            55 => new MovePartsAction(),
            56 => new CountBlindAction(),
            57 => new CountDownAction(),
            58 => new StopFieldAction(),
            59 => new InhibitHealAction(),
            60 => new AttackSealAction(),
            61 => new FearAction(),
            62 => new AweAction(),
            63 => new LoopMotionRepeatAction(),
            69 => new ToadAction(),
            71 => new KnightGuardAction(),
            72 => new DamageCutAction(),
            73 => new LogBarrierAction(),
            74 => new DivideAction(),
            75 => new ActionByHitCountAction(),
            76 => new HealDownAction(),
            77 => new PassiveSealAction(),
            78 => new PassiveDamageUpAction(),
            79 => new DamageByBehaviourAction(),
            83 => new ChangeSpeedOverlapAction(),
            90 => new PassiveAction(),
            91 => new PassiveInermittentAction(),
            92 => new ChangeEnergyRecoveryRatioByDamageAction(),
            93 => new IgnoreDecoyAction(),
            94 => new EffectAction(),
            95 => new SpyAction(),
            96 => new ChangeEnergyFieldAction(),
            97 => new ChargeEnergyByDamageAction(),
            98 => new EnergyDamageReduceAction(),
            99 => new ChangeSpeedOverwriteFieldAction(),
            100 => new UnableStateGuardAction(),
            101 => new AttackSealActionForAllEnemy(),
            102 => new AccumulativeDamageActionForAllEnemy(),
            103 => new CopyAtkParamAction(),
            104 => new EveryAttackCriticalAction(),
            105 => new EnvironmentAction(),
            106 => new ProtectAction(),
            107 => new ChangeCriticalReferenceAction(),
            108 => new IfContainsUnitGroupAction(),
            /*
case 901:
return new ExStartPassiveAction();
case 902:
return new ExConditionPassiveAction();*/
            _ => new ActionParameter(),
        };
    }

    public bool isEnemySkill;
    public int dependActionId;
    public List<SkillAction> childrenAction;

    public int actionId;
    public int classId;
    public int rawActionType;

    public int actionDetail1;
    public int actionDetail2;
    public int actionDetail3;
    public List<int> actionDetails = new();

    public class DoubleValue
    {
        public double value;
        public string description;
        public EActionValue index;

        public DoubleValue(double value, EActionValue index)
        {
            this.value = value;
            this.index = index;
            description = index.Description();
        }

        public string ValueString()
        {
            return Utils.RoundIfNeed(value);
        }
    }

    public enum EActionValue
    {
        VALUE1,
        VALUE2,
        VALUE3,
        VALUE4,
        VALUE5,
        VALUE6,
        VALUE7
    }

    public DoubleValue actionValue1;
    public DoubleValue actionValue2;
    public DoubleValue actionValue3;
    public DoubleValue actionValue4;
    public DoubleValue actionValue5;
    public DoubleValue actionValue6;
    public DoubleValue actionValue7;
    public List<double> rawActionValues = new();

    public ActionType actionType;

    public TargetParameter targetParameter;

    public ActionParameter Init(bool isEnemySkill, int actionId, int dependActionId, int classId, int actionType, int actionDetail1, int actionDetail2, int actionDetail3, double actionValue1, double actionValue2, double actionValue3, double actionValue4, double actionValue5, double actionValue6, double actionValue7, int targetAssignment, int targetArea, int targetRange, int targetType, int targetNumber, int targetCount, SkillAction dependAction, List<SkillAction> childrenAction)
    {
        this.isEnemySkill = isEnemySkill;
        this.actionId = actionId;
        this.dependActionId = dependActionId;
        this.classId = classId;
        rawActionType = actionType;
        this.actionType = (ActionType)(actionType);
        this.actionDetail1 = actionDetail1;
        this.actionDetail2 = actionDetail2;
        this.actionDetail3 = actionDetail3;
        if (actionDetail1 != 0)
        {
            actionDetails.Add(actionDetail1);
        }

        if (actionDetail2 != 0)
        {
            actionDetails.Add(actionDetail2);
        }

        if (actionDetail3 != 0)
        {
            actionDetails.Add(actionDetail3);
        }

        this.actionValue1 = new DoubleValue(actionValue1, EActionValue.VALUE1);
        this.actionValue2 = new DoubleValue(actionValue2, EActionValue.VALUE2);
        this.actionValue3 = new DoubleValue(actionValue3, EActionValue.VALUE3);
        this.actionValue4 = new DoubleValue(actionValue4, EActionValue.VALUE4);
        this.actionValue5 = new DoubleValue(actionValue5, EActionValue.VALUE5);
        this.actionValue6 = new DoubleValue(actionValue6, EActionValue.VALUE6);
        this.actionValue7 = new DoubleValue(actionValue7, EActionValue.VALUE7);
        if (actionValue1 != 0)
        {
            rawActionValues.Add(actionValue1);
        }

        if (actionValue2 != 0)
        {
            rawActionValues.Add(actionValue2);
        }

        if (actionValue3 != 0)
        {
            rawActionValues.Add(actionValue3);
        }

        if (actionValue4 != 0)
        {
            rawActionValues.Add(actionValue4);
        }

        if (actionValue5 != 0)
        {
            rawActionValues.Add(actionValue5);
        }

        if (actionValue6 != 0)
        {
            rawActionValues.Add(actionValue6);
        }

        if (actionValue7 != 0)
        {
            rawActionValues.Add(actionValue7);
        }

        if (childrenAction != null)
        {
            this.childrenAction = childrenAction;
        }
        targetParameter = new TargetParameter(targetAssignment, targetNumber, targetType, targetRange, targetArea, targetCount, dependAction);
        ChildInit();
        return this;
    }

    public virtual void ChildInit()
    {
    }

    private static string BracesIfNeeded(string content)
    {
        if (content.Contains('+'))
        {
            return Utils.JavaFormat("(%s)", content);
        }
        else
        {
            return content;
        }
    }

    public virtual string LocalizedDetail(int level, Property property)
    {
        if (rawActionType == 0)
        {
            return Utils.JavaFormat(Utils.GetString("no_effect"));
        }
        return Utils.JavaFormat(Utils.GetString("Unknown_effect_d1_to_s2_with_details_s3_values_s4"),
                rawActionType,
                targetParameter.BuildTargetClause(),
                string.Join(",", actionDetails),
                string.Join(",", rawActionValues));
    }

    public string BuildExpression(int level, Property property)
    {
        return BuildExpression(level, actionValues, null, property, false, false, false);
    }

    public string BuildExpression(int level, RoundingMode? roundingMode, Property property)
    {
        return BuildExpression(level, actionValues, roundingMode, property, false, false, false);
    }

    public string BuildExpression(int level, List<ActionValue> actionValues, RoundingMode? roundingMode, Property property)
    {
        return BuildExpression(level, actionValues, roundingMode, property, false, false, false);
    }

    public String BuildExpression(int level, RoundingMode? roundingMode, Property property, bool isConstant)
    {
        return BuildExpression(level, actionValues, roundingMode, property, false, false, false, isConstant);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public string BuildExpression(int level,
                                  List<ActionValue> actionValues,
                                  RoundingMode? roundingMode,
                                  Property property,
                                  bool isHealing,
                                  bool isSelfTPRestoring,
                                  bool hasBracesIfNeeded,
                                  params bool[] redundancy)
    {
        bool isConstant = redundancy.Length > 0 && redundancy[0];
        actionValues ??= this.actionValues;

        roundingMode ??= RoundingMode.DOWN;

        property ??= new Property();

        if (UserSettings.Get().GetExpression() == UserSettings.EXPRESSION_EXPRESSION)
        {
            StringBuilder expression = new();
            foreach (ActionValue value in actionValues)
            {
                StringBuilder part = new();
                if (value.initial != null && value.perLevel != null)
                {
                    double initialValue = double.Parse(value.initial);
                    double perLevelValue = double.Parse(value.perLevel);
                    if (initialValue == 0 && perLevelValue == 0)
                    {
                        continue;
                    }
                    else if (initialValue == 0)
                    {
                        part.Append(Utils.JavaFormat("%s * %s", perLevelValue, Utils.JavaFormat(Utils.GetString("SLv"))));
                    }
                    else if (perLevelValue == 0)
                    {
                        if (value.key == null && roundingMode != RoundingMode.UNNECESSARY)
                        {
                            part.Append((int)Math.Round(initialValue));
                        }
                        else
                        {
                            part.Append(initialValue);
                        }
                    }
                    else
                    {
                        part.Append(Utils.JavaFormat("%s + %s * %s", initialValue, perLevelValue, Utils.JavaFormat(Utils.GetString("SLv"))));
                    }
                    if (value.key != null)
                    {
                        if (initialValue == 0 && perLevelValue == 0)
                        {
                            continue;
                        }
                        else if (initialValue == 0 || perLevelValue == 0)
                        {
                            part.Append(Utils.JavaFormat(" * %s", value.key.Description()));
                        }
                        else
                        {
                            string c = part.ToString();
                            part.Clear();
                            part.Append(Utils.JavaFormat("(%s) * %s", c, value.key.Description()));
                        }
                    }
                }
                if (part.Length != 0)
                {
                    expression.Append(part).Append(" + ");
                }
            }
            if (expression.Length == 0)
            {
                return "0";
            }
            else
            {
                expression.RemoveRange(expression.ToString().LastIndexOf(" +"), expression.Length);
                return hasBracesIfNeeded ? BracesIfNeeded(expression.ToString()) : expression.ToString();
            }
        }
        else if (UserSettings.Get().GetExpression() == UserSettings.EXPRESSION_ORIGINAL && !isConstant)
        {
            StringBuilder expression = new();
            foreach (ActionValue value in actionValues)
            {
                StringBuilder part = new();
                if (value.initial != null && value.perLevel != null)
                {
                    double initialValue = double.Parse(value.initial);
                    double perLevelValue = double.Parse(value.perLevel);
                    if (initialValue == 0 && perLevelValue == 0)
                    {
                        continue;
                    }
                    else if (initialValue == 0)
                    {
                        part.Append(Utils.JavaFormat("##%s##%s * %s", value.perLevelValue.description, perLevelValue, Utils.JavaFormat(Utils.GetString("SLv"))));
                    }
                    else if (perLevelValue == 0)
                    {
                        if (value.key == null && roundingMode != RoundingMode.UNNECESSARY)
                        {
                            double bigDecimal = initialValue;
                            part.Append(Utils.JavaFormat("##%s##%s", value.initialValue.description, Utils.Round(bigDecimal, roundingMode)));
                        }
                        else
                        {
                            part.Append(Utils.JavaFormat("##%s##%s", value.initialValue.description, initialValue));
                        }
                    }
                    else
                    {
                        part.Append(Utils.JavaFormat("##%s##%s + ##%s##%s * %s", value.initialValue.description, initialValue, value.perLevelValue.description, perLevelValue, Utils.JavaFormat(Utils.GetString("SLv"))));
                    }
                    if (value.key != null)
                    {
                        if (initialValue == 0 && perLevelValue == 0)
                        {
                            continue;
                        }
                        else if (initialValue == 0 || perLevelValue == 0)
                        {
                            part.Append(Utils.JavaFormat(" * %s", value.key.Description()));
                        }
                        else
                        {
                            string c = part.ToString();
                            part.Clear();
                            part.Append(Utils.JavaFormat("(%s) * %s", c, value.key.Description()));
                        }
                    }
                }
                if (part.Length != 0)
                {
                    expression.Append(part).Append(" + ");
                }
            }
            if (expression.Length == 0)
            {
                return "0";
            }
            else
            {
                expression.RemoveRange(expression.ToString().LastIndexOf(" +"), expression.Length);
                return hasBracesIfNeeded ? BracesIfNeeded(expression.ToString()) : expression.ToString();
            }
        }
        else
        {
            double fixedValue = 0.0;
            foreach (ActionValue value in actionValues)
            {
                double part = .0;
                if (value.initial != null && value.perLevel != null)
                {
                    double initialValue = double.Parse(value.initial);
                    double perLevelValue = double.Parse(value.perLevel);
                    part = initialValue + (perLevelValue * (level));
                }
                if (value.key != null)
                {
                    part *= ((property.GetItem(value.key)));
                }
                //                int num = (int)part;
                //                if (UnitUtils.Companion.approximately(part, (double)num)) {
                //                    part = num;
                //                }
                fixedValue += (part);
            }
            if (roundingMode == RoundingMode.UNNECESSARY)
            {
                return fixedValue.ToString();
            }

            return Utils.Round(fixedValue, roundingMode);
        }
    }

    public List<ActionValue> actionValues = new();

    public void SetActionValues(List<ActionValue> actionValues)
    {
        this.actionValues = actionValues;
    }
    public List<ActionValue> GetActionValues()
    {
        return actionValues;
    }

    public class ActionValue
    {
        public string initial;
        public string perLevel;
        public PropertyKey? key;
        public DoubleValue initialValue;
        public DoubleValue perLevelValue;

        public ActionValue(DoubleValue initial, DoubleValue perLevel, PropertyKey? key)
        {
            initialValue = initial;
            perLevelValue = perLevel;
            this.initial = initial.ValueString();
            this.perLevel = perLevel.ValueString();
            this.key = key;
        }

        public ActionValue(double initial, double perLevel, EActionValue vInitial, EActionValue vPerLevel, PropertyKey? key)
        {
            initialValue = new DoubleValue(initial, vInitial);
            perLevelValue = new DoubleValue(perLevel, vPerLevel);
            this.initial = (initial).ToString();
            this.perLevel = (perLevel).ToString();
            this.key = key;
        }
    }
}

public enum PercentModifier
{
    percent,
    number
}

public enum ClassModifier
{
    unknown = 0,
    physical = 1,
    magical = 2,
    inevitablePhysical = 3
}

public enum CriticalModifier
{
    normal = 0,
    critical = 1
}

public enum PropertyKey
{
    atk,
    def,
    dodge,
    energyRecoveryRate,
    energyReduceRate,
    hp,
    hpRecoveryRate,
    lifeSteal,
    magicCritical,
    magicDef,
    magicPenetrate,
    magicStr,
    physicalCritical,
    physicalPenetrate,
    waveEnergyRecovery,
    waveHpRecovery,
    accuracy,
    unknown
}

public enum RoundingMode
{
    UNNECESSARY,
    FLOOR,
    CEILING,
    DOWN,
    UP,
    HALF_UP
}