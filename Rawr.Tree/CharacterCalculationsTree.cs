﻿using System;
using System.Collections.Generic;

namespace Rawr.Tree
{

    public class CharacterCalculationsTree : CharacterCalculationsBase
    {
        private Stats basicStats;
        public Stats BasicStats
        {
            get { return basicStats; }
            set { basicStats = value; }
        }

        private float overallPoints;
        public override float OverallPoints
        {
            get { return overallPoints; }
            set { overallPoints = value; }
        }

        private float[] subPoints = new float[] { 0f, 0f, 0f, 0f };
        public override float[] SubPoints
        {
            get { return subPoints; }
            set { subPoints = value; }
        }

        public float HpSPoints
        {
            get { return subPoints[0]; }
            set { subPoints[0] = value; }
        }

        public float Mp5Points
        {
            get { return subPoints[1]; }
        }

        public void AddMp5Points(float value, String source)
        {
            if (value == 0)
                return;
            _Mp5PointsBreakdown += String.Format("\n{0:0.0} mp5 ({1})", value, source);
            subPoints[1] += value;
        }
        
        private String _Mp5PointsBreakdown = "Breakdown:";

        public String Mp5PointsBreakdown
        {
            get { return _Mp5PointsBreakdown; }
        }

        public float SurvivalPoints
        {
            get { return subPoints[2]; }
            set { subPoints[2] = value; }
        }

        public float ToLPoints
        {
            get { return subPoints[3]; }
            set { subPoints[3] = value; }
        }

        public float OS5SRRegen
        {
            get;
            set;
        }

        public float IS5SRRegen
        {
            get;
            set;
        }

        public float FightFraction
        {
            get;
            set;
        }

        public float FightLength
        {
            get;
            set;
        }

        public int NumCycles
        {
            get;
            set;
        }

        public int NumCyclesAfterFilter
        {
            get;
            set;
        }

        public int NumCyclesPerRotation
        {
            get;
            set;
        }

        public long NumRotations
        {
            get;
            set;
        }

        public String DebugText
        {
            get;
            set;
        }

        public List<Spell> Spells
        {
            get;
            set;
        }

        public SpellRotation BestSpellRotation
        {
            get;
            set;
        }

        public override Dictionary<string, string> GetCharacterDisplayCalculationValues()
        {
            Dictionary<string, string> dictValues = new Dictionary<string, string>();

            dictValues.Add("Health", BasicStats.Health.ToString());
            dictValues.Add("Stamina", BasicStats.Stamina.ToString());
            dictValues.Add("Mana", BasicStats.Mana.ToString());
            dictValues.Add("Intellect", BasicStats.Intellect.ToString());
            dictValues.Add("Spirit", BasicStats.Spirit.ToString());
            dictValues.Add("Healing", BasicStats.Healing.ToString());
            dictValues.Add("Mp5", string.Format("{0}*{1} mp5 outside the 5-second rule",
                (int) (5*IS5SRRegen),
                (int) (5*OS5SRRegen)));

            dictValues.Add("Spell Crit", string.Format("{0}%*{1} Spell Crit rating",
                BasicStats.SpellCrit, BasicStats.SpellCritRating.ToString()));
            
            dictValues.Add("Spell Haste", string.Format("{0}%*{1} Spell Haste rating\nGlobal cooldown is {2} seconds", 
                Math.Round(BasicStats.SpellHasteRating / 15.7, 2),
                BasicStats.SpellHasteRating.ToString(),
                Math.Round((1.5f * 1570f) / (1570f + BasicStats.SpellHasteRating), 2)));

            dictValues.Add("Mana per Cast (5%)", BasicStats.ManaRestorePerCast_5_15.ToString());

            dictValues.Add("HPS Points", String.Format("{0:0}", HpSPoints));
            dictValues.Add("Mp5 Points", String.Format("{0:0}*{1}", Mp5Points, Mp5PointsBreakdown));
            dictValues.Add("Survival Points", String.Format("{0:0}*Survival points are based on separate scaling above/below a target health", SurvivalPoints));
            dictValues.Add("ToL Points", String.Format("{0:0}*Tree of Life points is the strength of your aura", ToLPoints));
            dictValues.Add("Overall Points", String.Format("{0:0}", OverallPoints));

            if (BestSpellRotation == null)
            {
                dictValues.Add("Rotation duration", "--");
                dictValues.Add("Rotation heal", "--");
                dictValues.Add("Rotation cost", "--");
                dictValues.Add("Rotation HPS", "--");
                dictValues.Add("Rotation HPM", "--");
                dictValues.Add("Max fight duration", "--");
            }
            else
            {
                dictValues.Add("Rotation duration", String.Format("{0:0.0}*{1}{2} different cycles filtered down to {3}\n{4} cycles per rotation means {5} rotations had to be enumerated\n{6}",
                    BestSpellRotation.bestCycleDuration, BestSpellRotation.cycleSpells,
                    NumCycles, NumCyclesAfterFilter,
                    NumCyclesPerRotation, NumRotations,
                    DebugText));
                dictValues.Add("Rotation heal", BestSpellRotation.healPerCycle.ToString());

                float netCost = BestSpellRotation.manaPerCycle - Mp5Points * BestSpellRotation.bestCycleDuration / 5;
                if (netCost < 0)
                    netCost = 0;
                dictValues.Add("Rotation cost", String.Format("{0:0}*{1:0}-{2:0}",
                    netCost, BestSpellRotation.manaPerCycle, Mp5Points * BestSpellRotation.bestCycleDuration / 5));
                dictValues.Add("Rotation HPS", String.Format("{0:0}", BestSpellRotation.healPerCycle / BestSpellRotation.bestCycleDuration));
                dictValues.Add("Rotation HPM", String.Format("{0:0}", BestSpellRotation.HPM));

                if (netCost > 0)
                {
                    float maxLength = (BasicStats.Mana / netCost * BestSpellRotation.bestCycleDuration);

                    dictValues.Add("Max fight duration", String.Format("{0}m{1}s*{2:0}% of the target length",
                        (int)maxLength / 60, (int)maxLength % 60, FightFraction * 100)); //maxLength / (FightLength*60) * 100));
                }
                else
                {
                    dictValues.Add("Max fight duration", "Infinity");
                }
            }

            addSpellValues(dictValues);

            return dictValues;
        }

        private void addSpellValues(Dictionary<string, string> dictValues) {
            Spell spell;
            if ((spell = Spells.Find(delegate(Spell s) { return s.Name.Equals("Lifebloom"); })) != null)
			{
				dictValues.Add("LB Tick", string.Format("{0}*{1} ticks",Math.Round(spell.PeriodicTick, 2),spell.PeriodicTicks));
                dictValues.Add("LB Heal", string.Format("{0}*{1}", spell.AverageTotalHeal.ToString(), spell.HealInterval));
                dictValues.Add("LB HPS", string.Format("{0}*HPS is the average amount healed divided by the time to cast the spell", Math.Round(spell.HPS)));
                dictValues.Add("LB HPM", string.Format("{0}*{1} mana", spell.HPM, spell.Cost));
			}
			else
			{
				dictValues.Add("LB Tick", "--");
				dictValues.Add("LB Heal", "--");
				dictValues.Add("LB HPS", "--");
				dictValues.Add("LB HPM", "--");
			}

            if ((spell = Spells.Find(delegate(Spell s) { return s.Name.Equals("Lifebloom Stack"); })) != null)
            {
                dictValues.Add("LBS Tick", string.Format("{0}*{1} ticks",Math.Round(spell.PeriodicTick, 2),spell.PeriodicTicks));
                dictValues.Add("LBS HPS", string.Format("{0}*HPS is the average amount healed divided by the time to cast the spell", Math.Round(spell.HPS)));
                dictValues.Add("LBS HPM", string.Format("{0}*{1} mana", spell.HPM, spell.Cost));
			}
			else
			{
				dictValues.Add("LBS Tick", "--");
				dictValues.Add("LBS HPS", "--");
				dictValues.Add("LBS HPM", "--");
			}

            if ((spell = Spells.Find(delegate(Spell s) { return s.Name.Equals("Rejuvenation"); })) != null)
			{
                dictValues.Add("RJ Tick", string.Format("{0}*{1} ticks",Math.Round(spell.PeriodicTick, 2),spell.PeriodicTicks));
                dictValues.Add("RJ HPS", string.Format("{0}*HPS is the average amount healed divided by the time to cast the spell", Math.Round(spell.HPS)));
                dictValues.Add("RJ HPM", string.Format("{0}*{1} mana", spell.HPM, spell.Cost));
			}
			else
			{
				dictValues.Add("RJ Tick", "--");
				dictValues.Add("RJ HPS", "--");
				dictValues.Add("RJ HPM", "--");
			}

            if ((spell = Spells.Find(delegate(Spell s) { return s.Name.Equals("Regrowth"); })) != null)
            {
                dictValues.Add("RG Tick", string.Format("{0}*{1} ticks",Math.Round(spell.PeriodicTick, 2),spell.PeriodicTicks));
                dictValues.Add("RG Heal", String.Format("{0}*{1}", spell.AverageTotalHeal, spell.HealInterval));
                dictValues.Add("RG HPS", string.Format("{0}*HPS is the average amount healed divided by the time to cast the spell\nCasttime: {1}", Math.Round(spell.HPS), Math.Round(spell.CastTime, 2)));
                dictValues.Add("RG HPM", string.Format("{0}*{1} mana", spell.HPM, spell.Cost));
			}
			else
			{
				dictValues.Add("Regrowth Tick", "--");
				dictValues.Add("Regrowth Heal", "--");
				dictValues.Add("Regrowth HPS", "--");
				dictValues.Add("Regrowth HPM", "--");
			}

            if ((spell = Spells.Find(delegate(Spell s) { return s.Name.Equals("Healing Touch"); })) != null)
            {
                dictValues.Add("HT Heal", String.Format("{0}*{1}", spell.AverageTotalHeal, spell.HealInterval));
                dictValues.Add("HT HPS", string.Format("{0}*HPS is the average amount healed divided by the time to cast the spell\nCasttime: {1}", Math.Round(spell.HPS), Math.Round(spell.CastTime, 2)));
                dictValues.Add("HT HPM", string.Format("{0}*{1} mana", spell.HPM, spell.Cost));
			}
			else
			{
				dictValues.Add("HT Heal", "--");
				dictValues.Add("HT HPS", "--");
				dictValues.Add("HT HPM", "--");
			}
        }
    }
}
