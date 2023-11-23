namespace ActionParameterSerializer
{
    public class Ailment
    {

        public class AilmentDetail
        {
            public object detail;
            public void SetDetail(object obj)
            {
                detail = obj;
            }

            public string Description()
            {
                if (detail is DotDetail dotDetail)
                {
                    return dotDetail.Description();
                }
                else if (detail is ActionDetail actionDetail)
                {
                    return actionDetail.Description();
                }
                else if (detail is CharmDetail charmDetail)
                {
                    return charmDetail.Description();
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("unknown"));
                }
            }
        }

        public enum DotDetail
        {
            detain = 0,
            poison = 1,
            burn = 2,
            curse = 3,
            violentPoison = 4,
            hex = 5,
            compensation = 6,
            world_lightning = 10,
            unknown = -1
        }

        public enum CharmDetail
        {
            charm = 0,
            confuse = 1
        }

        public enum ActionDetail
        {
            slow = 1,
            haste = 2,
            paralyse = 3,
            freeze = 4,
            bind = 5,
            sleep = 6,
            stun = 7,
            petrify = 8,
            detain = 9,
            faint = 10,
            timeStop = 11,
            unknown = 12
        }

        public enum AilmentType
        {
            knockBack = 3,
            action = 8,
            dot = 9,
            charm = 11,
            darken = 12,
            silence = 13,
            confuse = 19,
            instantDeath = 30,
            countBlind = 56,
            inhibitHeal = 59,
            attackSeal = 60,
            fear = 61,
            awe = 62,
            toad = 69,
            maxHP = 70,
            hPRegenerationDown = 76,
            damageTakenIncreased = 78,
            damageByBehaviour = 79,
            unknown = 80
        }
        public AilmentType ailmentType;
        public AilmentDetail ailmentDetail;

        public Ailment(int type, int detail)
        {

            ailmentType = (AilmentType)(type);
            ailmentDetail = new AilmentDetail();
            switch (ailmentType)
            {
                case AilmentType.action:
                    ailmentDetail.SetDetail((ActionDetail)(detail));
                    break;
                case AilmentType.dot:
                case AilmentType.damageByBehaviour:
                    ailmentDetail.SetDetail((DotDetail)(detail));
                    break;
                case AilmentType.charm:
                    ailmentDetail.SetDetail((CharmDetail)(detail));
                    break;
                default:
                    ailmentDetail = null;
                    break;
            }
        }

        public string Description()
        {
            if (ailmentDetail != null)
            {
                return ailmentDetail.Description();
            }
            else
            {
                return ailmentType.Description();
            }
        }
    }



}
