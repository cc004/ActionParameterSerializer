using ActionParameterSerializer.Actions;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static ActionParameterSerializer.Actions.ModeChangeAction;

namespace ActionParameterSerializer
{
    public class Property
    {
        public double GetItem(PropertyKey? key)
        {
            return key switch
            {
                PropertyKey.atk => this.atk,
                PropertyKey.def => this.def,
                PropertyKey.dodge => this.dodge,
                PropertyKey.energyRecoveryRate => this.energyRecoveryRate,
                PropertyKey.energyReduceRate => this.energyReduceRate,
                PropertyKey.hp => this.hp,
                PropertyKey.hpRecoveryRate => this.hpRecoveryRate,
                PropertyKey.lifeSteal => this.lifeSteal,
                PropertyKey.magicCritical => this.magicCritical,
                PropertyKey.magicDef => this.magicDef,
                PropertyKey.magicPenetrate => this.magicPenetrate,
                PropertyKey.magicStr => this.magicStr,
                PropertyKey.physicalCritical => this.physicalCritical,
                PropertyKey.physicalPenetrate => this.physicalPenetrate,
                PropertyKey.waveEnergyRecovery => this.waveEnergyRecovery,
                PropertyKey.waveHpRecovery => this.waveHpRecovery,
                PropertyKey.accuracy => this.accuracy,
                _ => 0,
            };
        }

        public static Property GetPropertyWithKeyAndValue(Property property, PropertyKey key, double value)
        {
            property ??= new Property();

            switch (key)
            {
                case PropertyKey.atk:
                    property.atk += value;
                    return property;
                case PropertyKey.def:
                    property.def += value;
                    return property;
                case PropertyKey.dodge:
                    property.dodge += value;
                    return property;
                case PropertyKey.energyRecoveryRate:
                    property.energyRecoveryRate += value;
                    return property;
                case PropertyKey.energyReduceRate:
                    property.energyReduceRate += value;
                    return property;
                case PropertyKey.hp:
                    property.hp += value;
                    return property;
                case PropertyKey.hpRecoveryRate:
                    property.hpRecoveryRate += value;
                    return property;
                case PropertyKey.lifeSteal:
                    property.lifeSteal += value;
                    return property;
                case PropertyKey.magicCritical:
                    property.magicCritical += value;
                    return property;
                case PropertyKey.magicDef:
                    property.magicDef += value;
                    return property;
                case PropertyKey.magicPenetrate:
                    property.magicPenetrate += value;
                    return property;
                case PropertyKey.magicStr:
                    property.magicStr += value;
                    return property;
                case PropertyKey.physicalCritical:
                    property.physicalCritical += value;
                    return property;
                case PropertyKey.physicalPenetrate:
                    property.physicalPenetrate += value;
                    return property;
                case PropertyKey.waveEnergyRecovery:
                    property.waveEnergyRecovery += value;
                    return property;
                case PropertyKey.waveHpRecovery:
                    property.waveHpRecovery += value;
                    return property;
                case PropertyKey.accuracy:
                    property.accuracy += value;
                    return property;
                default:
                    return property;
            }
        }

        public double hp;
        public double atk;
        public double magicStr;
        public double def;
        public double magicDef;
        public double physicalCritical;
        public double magicCritical;
        public double waveHpRecovery;
        public double waveEnergyRecovery;
        public double dodge;
        public double physicalPenetrate;
        public double magicPenetrate;
        public double lifeSteal;
        public double hpRecoveryRate;
        public double energyRecoveryRate;
        public double energyReduceRate;
        public double accuracy;

    }

    public class SkillAction
    {
        public ActionParameter parameter;
        public static int GetActionId()
        {
            return 0;
        }
    }

    public class UserSettings
    {
        public const int EXPRESSION_EXPRESSION = 0;
        public const int EXPRESSION_ORIGINAL = 1;
        public const int EXPRESSION_VALUE = 2;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        public int GetExpression()
        {
            return expression;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
        public static int expression = EXPRESSION_ORIGINAL;

        public static UserSettings Get()
        {
            return new UserSettings();
        }
    }
    public static class EnumEx
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<long, string>> descDictionary = new();

        public static string GetDescription(this Enum input)
        {
            return input.GetDescription<Attribute>();
        }

        public static string GetDescription<TArrtibute>(this Enum input, string attrPropName = "Description") where TArrtibute : Attribute
        {
            RegisterDescription(input.GetType(), typeof(TArrtibute), out var dictionary, attrPropName);
            long key = Convert.ToInt64(input);
            if (dictionary != null && dictionary.Count > 0 && dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            return input?.ToString();
        }

        public static IDictionary<TEnum, string> GetDescriptions<TEnum>() where TEnum : struct
        {
            Type typeFromHandle = typeof(TEnum);
            if (typeFromHandle.IsEnum && descDictionary.TryGetValue(typeFromHandle, out var value) && value != null)
            {
                return value.ToDictionary((KeyValuePair<long, string> k) => Enum.TryParse<TEnum>(k.Key.ToString(), ignoreCase: true, out TEnum result) ? result : result, (KeyValuePair<long, string> v) => v.Value);
            }
            return null;
        }
        public static string GetPascalDescription(this Enum input)
        {
            string[] words = input.GetDescription<Attribute>().Split('_');
            return string.Join("", words.Select(w => char.ToUpper(w[0]) + w[1..].ToLower()));
        }

        public static void RegisterDescription<TEnum, TArrtibute>(string attrPropName = "Description") where TEnum : struct where TArrtibute : Attribute
        {
            RegisterDescription(typeof(TEnum), typeof(TArrtibute), out Dictionary<long, string> _, attrPropName);
        }

        public static void RegisterDescription<TEnum>() where TEnum : struct
        {
            RegisterDescription<TEnum, DescriptionAttribute>();
        }

        public static void RegisterDescription<TEnum>(IDictionary<TEnum, string> dictionary) where TEnum : struct
        {
            Type typeFromHandle = typeof(TEnum);
            if (typeFromHandle.IsEnum)
            {
                Dictionary<long, string> dic = dictionary.ToDictionary((KeyValuePair<TEnum, string> k) => Convert.ToInt64(k.Key), (KeyValuePair<TEnum, string> v) => v.Value);
                descDictionary.AddOrUpdate(typeFromHandle, dic, (Type k, Dictionary<long, string> v) => dic);
            }
        }

        private static void RegisterDescription(Type enumType, Type attrType, out Dictionary<long, string> dictionary, string attrPropName)
        {
            dictionary = null;
            if (!enumType.IsEnum || descDictionary.TryGetValue(enumType, out dictionary))
            {
                return;
            }
            Array values = Enum.GetValues(enumType);
            dictionary = new Dictionary<long, string>();
            foreach (object item in values)
            {
                string text = item.ToString();
                object[] customAttributes = enumType.GetField(text).GetCustomAttributes(attrType, inherit: false);
                if (customAttributes.Length != 0)
                {
                    PropertyInfo property = customAttributes[0].GetType().GetProperty(attrPropName);
                    if (property != null)
                    {
                        text = property.GetValue(customAttributes[0]).ToString();
                    }
                }
                if (!dictionary.ContainsKey(Convert.ToInt64(item)))
                {
                    dictionary.Add(Convert.ToInt64(item), text);
                }
            }
            descDictionary.TryAdd(enumType, dictionary);
        }
    }
    public static class Utils
    {
        public static ExclusiveAllType ExclusiveWithAll(this TargetType type)
        {
            return type switch
            {
                TargetType.unknown or TargetType.magic or TargetType.physics or TargetType.summon or TargetType.boss => ExclusiveAllType.not,
                TargetType.nearWithoutSelf => ExclusiveAllType.halfExclusive,
                _ => ExclusiveAllType.exclusive,
            };
        }
        public static AuraAction.AuraActionType Toggle(this AuraAction.AuraActionType type)
        {
            return type switch
            {
                AuraAction.AuraActionType.raise => AuraAction.AuraActionType.reduce,
                AuraAction.AuraActionType.reduce => AuraAction.AuraActionType.raise,
                _ => AuraAction.AuraActionType.raise,
            };
        }

        private static readonly Regex re = new(@"%((\d)\$)?[ds]", RegexOptions.Compiled);

        public static string JavaFormat(string format, params object[] args)
        {
            var i = 0;
            return re.Replace(format, match =>
            {
                if (match.Groups[1].Success) return args[int.Parse(match.Groups[2].Value) - 1].ToString();
                return args[i++].ToString();
            }).Replace("%%", "%");
            //$"{format}({string.Join(",", args)})");
        }

        public static PluralModifier PluralModifier(this TargetCount count)
        {
            if (count == TargetCount.one)
            {
                return Actions.PluralModifier.one;
            }
            else
            {
                return Actions.PluralModifier.many;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
        public static string path = "string.json";

        private static Dictionary<string, string> cache;

        public static string GetString(string name)
        {
            cache ??= JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path));

            if (cache.TryGetValue(name, out var val)) return val;
            if (cache.TryGetValue(name.ToUpper(), out val)) return val;
            if (cache.TryGetValue(name[..1].ToUpper() + name[1..], out val)) return val;

            return name;
#if DEBUG
            throw new NotImplementedException();
#else
#pragma warning disable CS0162 // Unreachable code detected
            return "不明效果";
#pragma warning restore CS0162 // Unreachable code detected
#endif
        }

        private static readonly Regex re2 = new($"[A-Z]", RegexOptions.Compiled);

        public static string Description(this Enum t)
        {
            return GetString(re2.Replace(t.GetDescription(), match => "_" + match.Groups[0].Value.ToLower()));
        }
        public static string RawDescription(this Enum t)
        {
            return GetString(t.GetDescription());
        }

        public static string Description(this AuraAction.AuraType val)
        {
            switch (val)
            {
                case AuraAction.AuraType.moveSpeed: return Utils.GetString("Move_Speed");
                case AuraAction.AuraType.physicalCriticalDamage: return Utils.GetString("Physical_Critical_Damage");
                case AuraAction.AuraType.magicalCriticalDamage: return Utils.GetString("Magical_Critical_Damage");
                case AuraAction.AuraType.accuracy: return PropertyKey.accuracy.Description();
                case AuraAction.AuraType.receivedCriticalDamage: return Utils.GetString("Received_Critical_Damage");
                case AuraAction.AuraType.receivedDamage: return Utils.GetString("received_damage");
                case AuraAction.AuraType.receivedPhysicalDamage: return Utils.GetString("received_physical_damage");
                case AuraAction.AuraType.receivedMagicalDamage: return Utils.GetString("received_magical_damage");
                case AuraAction.AuraType.maxHP: return Utils.GetString("max_HP");
                default:
                    if (Enum.TryParse<PropertyKey>(val.ToString(), out var key))
                        return key.Description();
                    return ((Enum) val).Description();
            }
        }

        public static string Description(this PercentModifier val)
        {
            return val switch
            {
                PercentModifier.percent => "%%%",
                _ => "",
            };
        }

        public static string Description(this AuraAction.AuraActionType val)
        {
            return val switch
            {
                AuraAction.AuraActionType.raise => GetString("Raise"),
                AuraAction.AuraActionType.reduce => GetString("Reduce"),
                _ => GetString("Raise"),
            };
        }

        public static string Description(this ActionParameter.EActionValue val)
        {
            return GetString(val.ToString().ToLower());
        }

        public static string Description(this PropertyKey? key)
        {
            return key.Value.Description();
        }

        public static string Description(this PropertyKey key)
        {
            return key switch
            {
                PropertyKey.atk => Utils.GetString("ATK"),
                PropertyKey.def => Utils.GetString("DEF"),
                PropertyKey.dodge => Utils.GetString("Dodge"),
                PropertyKey.energyRecoveryRate => Utils.GetString("Energy_Recovery_Rate"),
                PropertyKey.energyReduceRate => Utils.GetString("Energy_Reduce_Rate"),
                PropertyKey.hp => Utils.GetString("HP"),
                PropertyKey.hpRecoveryRate => Utils.GetString("HP_Recovery_Rate"),
                PropertyKey.lifeSteal => Utils.GetString("Life_Steal"),
                PropertyKey.magicCritical => Utils.GetString("Magic_Critical"),
                PropertyKey.magicDef => Utils.GetString("Magic_DEF"),
                PropertyKey.magicPenetrate => Utils.GetString("Magic_Penetrate"),
                PropertyKey.magicStr => Utils.GetString("Magic_STR"),
                PropertyKey.physicalCritical => Utils.GetString("Physical_Critical"),
                PropertyKey.physicalPenetrate => Utils.GetString("Physical_Penetrate"),
                PropertyKey.waveEnergyRecovery => Utils.GetString("Wave_Energy_Recovery"),
                PropertyKey.waveHpRecovery => Utils.GetString("Wave_HP_Recovery"),
                PropertyKey.accuracy => Utils.GetString("Accuracy"),
                _ => Utils.GetString("Unknown"),
            };
        }

        public static bool IgnoresOne(this TargetType type)
        {
            return type switch
            {
                TargetType.unknown or TargetType.random or TargetType.randomOnce or TargetType.absolute or TargetType.summon or TargetType.selfSummonRandom or TargetType.allSummonRandom or TargetType.magic or TargetType.physics => false,
                _ => true,
            };
        }
        public static string RoundDownDouble(double value)
        {
            return (Math.Floor(value)).ToString();
        }
        public static string RoundUpDouble(double value)
        {
            return (Math.Ceiling(value)).ToString();
        }
        public static string RoundDouble(double value)
        {
            return (Math.Round(value)).ToString();
        }
        public static string RoundIfNeed(double value)
        {
            if (value % 1 == 0)
            {
                return RoundDouble(value);
            }
            else
            {
                return (value).ToString();
            }
        }

        public static StringBuilder RemoveRange(this StringBuilder sb, int start, int end) =>
            sb.Remove(start, end - start);

        public static string Description(this TargetType type)
        {
            return type switch
            {
                TargetType.unknown => Utils.JavaFormat(Utils.GetString("unknown")),
                TargetType.random or TargetType.randomOnce => Utils.JavaFormat(Utils.GetString("random")),
                TargetType.zero or TargetType.near or TargetType.none => Utils.JavaFormat(Utils.GetString("the_nearest")),
                TargetType.far => Utils.JavaFormat(Utils.GetString("the_farthest")),
                TargetType.hpAscending or TargetType.hpAscendingOrNear => Utils.JavaFormat(Utils.GetString("the_lowest_HP_ratio")),
                TargetType.hpAscendingOrNearForward => Utils.JavaFormat(Utils.GetString("the_lowest_HP")),
                TargetType.hpDescendingOrNearForward => Utils.JavaFormat(Utils.GetString("the_highest_HP")),
                TargetType.hpDescending or TargetType.hpDescendingOrNear => Utils.JavaFormat(Utils.GetString("the_highest_HP_ratio")),
                TargetType.self => Utils.JavaFormat(Utils.GetString("self")),
                TargetType.forward => Utils.JavaFormat(Utils.GetString("the_most_backward")),
                TargetType.backward => Utils.JavaFormat(Utils.GetString("the_most_forward")),
                TargetType.absolute => Utils.JavaFormat(Utils.GetString("targets_within_the_scope")),
                TargetType.tpDescending or TargetType.tpDescendingOrNear or TargetType.tpDescendingOrMaxForward => Utils.JavaFormat(Utils.GetString("the_highest_TP")),
                TargetType.tpAscending or TargetType.tpReducing or TargetType.tpAscendingOrNear => Utils.JavaFormat(Utils.GetString("the_lowest_TP")),
                TargetType.atkDescending or TargetType.atkDescendingOrNear or TargetType.atkDecForwardWithoutOwner => Utils.JavaFormat(Utils.GetString("the_highest_ATK")),
                TargetType.atkAscending or TargetType.atkAscendingOrNear => Utils.JavaFormat(Utils.GetString("the_lowest_ATK")),
                TargetType.magicSTRDescending or TargetType.magicSTRDescendingOrNear => Utils.JavaFormat(Utils.GetString("the_highest_Magic_STR")),
                TargetType.magicSTRAscending or TargetType.magicSTRAscendingOrNear => Utils.JavaFormat(Utils.GetString("the_lowest_Magic_STR")),
                TargetType.summon => Utils.JavaFormat(Utils.GetString("minion")),
                TargetType.physics => Utils.JavaFormat(Utils.GetString("physics")),
                TargetType.magic => Utils.JavaFormat(Utils.GetString("magic")),
                TargetType.allSummonRandom => Utils.JavaFormat(Utils.GetString("random_minion")),
                TargetType.selfSummonRandom => Utils.JavaFormat(Utils.GetString("random_self_minion")),
                TargetType.boss => Utils.JavaFormat(Utils.GetString("boss")),
                TargetType.shadow => Utils.JavaFormat(Utils.GetString("shadow")),
                TargetType.nearWithoutSelf => Utils.JavaFormat(Utils.GetString("nearest_without_self")),
                TargetType.bothAtkDescending => Utils.JavaFormat(Utils.GetString("the_highest_ATK_or_Magic_STR")),
                TargetType.bothAtkAscending => Utils.JavaFormat(Utils.GetString("the_lowest_ATK_or_Magic_STR")),
                TargetType.energyAscBackWithoutOwner => Utils.JavaFormat(Utils.GetString("the_lowest_TP_except_self")),
                TargetType.atkDefAscForward => Utils.JavaFormat(Utils.GetString("the_lowest_DEF")),
                TargetType.magicDefAscForward => Utils.JavaFormat(Utils.GetString("the_lowest_Magic_DEF")),
                _ => Utils.JavaFormat(((Enum)type).Description()),
            };
        }
        
        public static string Description(this TargetType type, TargetNumber targetNumber, string localizedNumber)
        {

            if (targetNumber == TargetNumber.second
                || targetNumber == TargetNumber.third
                || targetNumber == TargetNumber.fourth
                || targetNumber == TargetNumber.fifth)
            {

                string localizedModifier = localizedNumber ?? targetNumber.Description();
                return type switch
                {
                    TargetType.unknown => Utils.JavaFormat(Utils.GetString("the_s_unknown_type"), localizedModifier),
                    TargetType.zero or TargetType.near or TargetType.none => Utils.JavaFormat(Utils.GetString("the_s_nearest"), localizedModifier),
                    TargetType.far => Utils.JavaFormat(Utils.GetString("the_s_farthest"), localizedModifier),
                    TargetType.hpAscending or TargetType.hpAscendingOrNear => Utils.JavaFormat(Utils.GetString("the_s_lowest_HP_ratio"), localizedModifier),
                    TargetType.hpDescending or TargetType.hpDescendingOrNear => Utils.JavaFormat(Utils.GetString("the_s_highest_HP_ratio"), localizedModifier),
                    TargetType.hpAscendingOrNearForward => Utils.JavaFormat(Utils.GetString("the_s_lowest_HP"), localizedModifier),
                    TargetType.hpDescendingOrNearForward => Utils.JavaFormat(Utils.GetString("the_s_highest_HP"), localizedModifier),
                    TargetType.forward => Utils.JavaFormat(Utils.GetString("the_s_most_backward"), localizedModifier),
                    TargetType.backward => Utils.JavaFormat(Utils.GetString("the_s_most_forward"), localizedModifier),
                    TargetType.tpDescending or TargetType.tpDescendingOrNear => Utils.JavaFormat(Utils.GetString("the_s_highest_TP"), localizedModifier),
                    TargetType.tpAscending or TargetType.tpReducing or TargetType.tpAscendingOrNear => Utils.JavaFormat(Utils.GetString("the_s_lowest_TP"), localizedModifier),
                    TargetType.atkDescending or TargetType.atkDescendingOrNear => Utils.JavaFormat(Utils.GetString("the_s_highest_ATK"), localizedModifier),
                    TargetType.atkAscending or TargetType.atkAscendingOrNear => Utils.JavaFormat(Utils.GetString("the_s_lowest_ATK"), localizedModifier),
                    TargetType.magicSTRDescending or TargetType.magicSTRDescendingOrNear => Utils.JavaFormat(Utils.GetString("the_s_highest_Magic_STR"), localizedModifier),
                    TargetType.magicSTRAscending or TargetType.magicSTRAscendingOrNear => Utils.JavaFormat(Utils.GetString("the_s_lowest_Magic_STR"), localizedModifier),
                    TargetType.nearWithoutSelf => Utils.JavaFormat(Utils.GetString("the_s_nearest_without_self")),
                    TargetType.bothAtkDescending => Utils.JavaFormat(Utils.GetString("the_s_highest_ATK_or_Magic_STR"), localizedModifier),
                    TargetType.bothAtkAscending => Utils.JavaFormat(Utils.GetString("the_s_lowest_ATK_or_Magic_STR"), localizedModifier),
                    TargetType.energyAscBackWithoutOwner => Utils.JavaFormat(Utils.GetString("the_s_th_lowest_TP_except_self"), localizedModifier),
                    TargetType.atkDefAscForward => Utils.JavaFormat(Utils.GetString("the_s_lowest_DEF"), localizedModifier),
                    TargetType.magicDefAscForward => Utils.JavaFormat(Utils.GetString("the_s_lowest_Magic_DEF"), localizedModifier),
                    _ => Utils.JavaFormat("s_" + ((Enum)type).Description(), localizedModifier),
                };
            }
            else
            {
                return type.Description();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static string Round(double bigDecimal, RoundingMode? roundingMode)
        {
            return bigDecimal.ToString();
        }
    }
}
