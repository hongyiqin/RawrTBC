﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rawr.Healadin
{
	[System.ComponentModel.DisplayName("Healadin|Spell_Holy_HolyBolt")]
    public class CalculationsHealadin : CalculationsBase
    {
        
        private CalculationOptionsPanelBase _calculationOptionsPanel = null;
        public override CalculationOptionsPanelBase CalculationOptionsPanel
        {
            get
            {
                if (_calculationOptionsPanel == null)
                {
                    _calculationOptionsPanel = new CalculationOptionsPanelHealadin();
                }
                return _calculationOptionsPanel;
            }
        }

        private string[] _characterDisplayCalculationLabels = null;
        public override string[] CharacterDisplayCalculationLabels
        {
            get
            {
                if (_characterDisplayCalculationLabels == null)
                    _characterDisplayCalculationLabels = new string[] {
					"Basic Stats:Health",
					"Basic Stats:Mana",
					"Basic Stats:Stamina",
					"Basic Stats:Intellect",
					"Basic Stats:Spirit",
					"Basic Stats:Healing",
					"Basic Stats:Mp5",
					"Basic Stats:Spell Crit",
					"Basic Stats:Spell Haste",
					"Cycle Stats:Cycle Uptime",
					"Cycle Stats:Cycle Hps",
					"Cycle Stats:Total Healed",
					"Cycle Stats:Average Hps",
					"Cycle Stats:Average Hpm",
                    "Spell Stats:Flash of Light",
                    "Spell Stats:Holy Light 11",
                    "Spell Stats:Holy Light 10",
                    "Spell Stats:Holy Light 9",
                    "Spell Stats:Holy Light 8",
                    "Spell Stats:Holy Light 7",
                    "Spell Stats:Holy Light 6",
                    "Spell Stats:Holy Light 5",
                    "Spell Stats:Holy Light 4"
				};
                return _characterDisplayCalculationLabels;
            }
        }

        private string[] _customChartNames = null;
        public override string[] CustomChartNames
        {
            get
            {
                if (_customChartNames == null)
                    _customChartNames = new string[] {
					"Healing per second",
					"Healing per mana",
					"Mana per second",
                    "Average heal"
					};
                return _customChartNames;
            }
        }

        private Dictionary<string, System.Drawing.Color> _subPointNameColors = null;
        public override Dictionary<string, System.Drawing.Color> SubPointNameColors
        {
            get
            {
                if (_subPointNameColors == null)
                {
                    _subPointNameColors = new Dictionary<string, System.Drawing.Color>();
                    _subPointNameColors.Add("Throughput", System.Drawing.Color.Red);
                    _subPointNameColors.Add("Longevity", System.Drawing.Color.Blue);
                }
                return _subPointNameColors;
            }
        }

        private List<Item.ItemType> _relevantItemTypes = null;
        public override List<Item.ItemType> RelevantItemTypes
        {
            get
            {
                if (_relevantItemTypes == null)
                {
                    _relevantItemTypes = new List<Item.ItemType>(new Item.ItemType[]
					{
                        Item.ItemType.Plate,
                        Item.ItemType.None,
						Item.ItemType.Shield,
						Item.ItemType.Libram,
						Item.ItemType.OneHandAxe,
						Item.ItemType.OneHandMace,
						Item.ItemType.OneHandSword,
						Item.ItemType.TwoHandAxe,
						Item.ItemType.TwoHandMace,
						Item.ItemType.TwoHandSword
					});
                }
                return _relevantItemTypes;
            }
        }

		public override Character.CharacterClass TargetClass { get { return Character.CharacterClass.Paladin; } }
		public override ComparisonCalculationBase CreateNewComparisonCalculation() { return new ComparisonCalculationHealadin(); }
        public override CharacterCalculationsBase CreateNewCharacterCalculations() { return new CharacterCalculationsHealadin(); }

        public override CharacterCalculationsBase GetCharacterCalculations(Character character, Item additionalItem)
        {
            _cachedCharacter = character;
            Stats stats = GetCharacterStats(character, additionalItem);
            CharacterCalculationsHealadin calculatedStats = new CharacterCalculationsHealadin();
            calculatedStats.BasicStats = stats;

            float activity = int.Parse(character.CalculationOptions["Activity"]) / 100f;
			float length = float.Parse(character.CalculationOptions["Length"], System.Globalization.CultureInfo.InvariantCulture) * 60;
            float totalMana = stats.Mana + (length * stats.Mp5 / 5) + (float.Parse(character.CalculationOptions["Spriest"]) * length / 5) +
                ((1 + stats.BonusManaPotion) * float.Parse(character.CalculationOptions["ManaAmt"]) * (float)Math.Ceiling((length / 60 - 1) / float.Parse(character.CalculationOptions["ManaTime"])));
            length *= activity; 

            calculatedStats[0] = new Spell("Flash of Light", 7);
            calculatedStats[1] = new Spell("Holy Light", 11);
            calculatedStats[2] = new Spell("Holy Light", 10);
            calculatedStats[3] = new Spell("Holy Light", 9);
            calculatedStats[4] = new Spell("Holy Light", 8);
            calculatedStats[5] = new Spell("Holy Light", 7);
            calculatedStats[6] = new Spell("Holy Light", 6);
            calculatedStats[7] = new Spell("Holy Light", 5);
            calculatedStats[8] = new Spell("Holy Light", 4);

            int[] r = {0, 12 - int.Parse(character.CalculationOptions["Rank1"]), 12 - int.Parse(character.CalculationOptions["Rank2"]),
                              12 - int.Parse(character.CalculationOptions["Rank3"]), 12 - int.Parse(character.CalculationOptions["Rank4"]),
                              12 - int.Parse(character.CalculationOptions["Rank5"])};

            #region Spell Cycle Modelling
            float[] cycle = {int.Parse(character.CalculationOptions["CycleFoL"]) * calculatedStats[r[0]].CastTime,
                int.Parse(character.CalculationOptions["CycleHL1"]) * calculatedStats[r[1]].CastTime,
                int.Parse(character.CalculationOptions["CycleHL2"]) * calculatedStats[r[2]].CastTime,
                int.Parse(character.CalculationOptions["CycleHL3"]) * calculatedStats[r[3]].CastTime,
                int.Parse(character.CalculationOptions["CycleHL4"]) * calculatedStats[r[4]].CastTime,
                int.Parse(character.CalculationOptions["CycleHL5"]) * calculatedStats[r[5]].CastTime};
            float mpc = 0, ctime = 0, hpc = 0;
            for (int i = 0; i < 6; i++)
            {
                ctime += cycle[i];
                mpc += calculatedStats[r[i]].Mps * cycle[i];
                hpc += calculatedStats[r[i]].Hps * cycle[i];
            }
            #endregion

            #region Divine Illumination Modelling
            calculatedStats.Calculate(true);
            float dicycles = (float)Math.Ceiling(length / 180);
            length -= dicycles * 15;
            float[] dicycle = {int.Parse(character.CalculationOptions["DIFoL"]) * calculatedStats[r[0]].CastTime,
                int.Parse(character.CalculationOptions["DIHL1"]) * calculatedStats[r[1]].CastTime,
                int.Parse(character.CalculationOptions["DIHL2"]) * calculatedStats[r[2]].CastTime,
                int.Parse(character.CalculationOptions["DIHL3"]) * calculatedStats[r[3]].CastTime,
                int.Parse(character.CalculationOptions["DIHL4"]) * calculatedStats[r[4]].CastTime,
                int.Parse(character.CalculationOptions["DIHL5"]) * calculatedStats[r[5]].CastTime};
            float dimana = 0, dihpc = 0, dictime = 0;
            for (int i = 0; i < 6; i++)
            {
                dictime += dicycle[i];
                dimana += calculatedStats[r[i]].Mps * dicycle[i];
                dihpc += calculatedStats[r[i]].Hps * dicycle[i];
            }
            dimana *= 15 / dictime;
            dihpc *= 15 / dictime;
            totalMana -= dimana * dicycles;
            calculatedStats.Calculate(false);
            #endregion

            float maxcycles =  length / ctime;
            float cycles = Math.Min(totalMana / mpc, maxcycles);

            calculatedStats.LongevityPoints = calculatedStats.Healed = (hpc * cycles) + (dihpc * dicycles);
            calculatedStats.ThroughputPoints = (hpc * maxcycles) + (dihpc * dicycles);
            calculatedStats.Hps = ((hpc / ctime * 2.75f) + (dihpc / 15 * .25f)) / 3;
            calculatedStats.Uptime = ((cycles*ctime) + (dicycles*15)) / ((maxcycles*ctime) + (dicycles*15));
            calculatedStats.AvgHpm = calculatedStats.Healed / (totalMana + (dimana * dicycles));
            calculatedStats.AvgHps = calculatedStats.Healed / (length + (dicycles * 15));

            calculatedStats.OverallPoints = calculatedStats.ThroughputPoints + calculatedStats.LongevityPoints;

            return calculatedStats;
        }

        public override Stats GetCharacterStats(Character character, Item additionalItem)
        {
            Stats statsRace = new Stats() { Health = 3197, Mana = 2673, Stamina = 118, Intellect = 86, Spirit = 88 };
            Stats statsBaseGear = GetItemStats(character, additionalItem);
            Stats statsEnchants = GetEnchantsStats(character);
            Stats statsBuffs = GetBuffsStats(character.ActiveBuffs);

            Stats statsTotal = statsBaseGear + statsEnchants + statsBuffs + statsRace;

            statsTotal.Stamina = (float)Math.Round((statsTotal.Stamina) * (1 + statsTotal.BonusStaminaMultiplier));
            statsTotal.Intellect = (float)Math.Round((1.1f * (statsTotal.Intellect)) * (1 + statsTotal.BonusIntellectMultiplier));
            statsTotal.Spirit = (float)Math.Round((statsTotal.Spirit) * (1 + statsTotal.BonusSpiritMultiplier));
            statsTotal.Healing = (float)Math.Round(statsTotal.Healing + (0.35f * statsTotal.Intellect) + (statsTotal.SpellDamageFromSpiritPercentage * statsTotal.Spirit));
            statsTotal.Mana = statsTotal.Mana + (statsTotal.Intellect * 15);
            statsTotal.Health = statsTotal.Health + (statsTotal.Stamina * 10f);
            return statsTotal;
        }

        public override ComparisonCalculationBase[] GetCustomChartData(Character character, string chartName)
        {
            CharacterCalculationsHealadin calc = GetCharacterCalculations(character) as CharacterCalculationsHealadin;
            ComparisonCalculationHealadin FoL = new ComparisonCalculationHealadin("Flash of Light");
            ComparisonCalculationHealadin HL11 = new ComparisonCalculationHealadin("Holy Light 11");
            ComparisonCalculationHealadin HL10 = new ComparisonCalculationHealadin("Holy Light 10");
            ComparisonCalculationHealadin HL9 = new ComparisonCalculationHealadin("Holy Light 9");
            ComparisonCalculationHealadin HL8 = new ComparisonCalculationHealadin("Holy Light 8");
            ComparisonCalculationHealadin HL7 = new ComparisonCalculationHealadin("Holy Light 7");
            ComparisonCalculationHealadin HL6 = new ComparisonCalculationHealadin("Holy Light 6");
            ComparisonCalculationHealadin HL5 = new ComparisonCalculationHealadin("Holy Light 5");
            ComparisonCalculationHealadin HL4 = new ComparisonCalculationHealadin("Holy Light 4");

            calc[0] = new Spell("Flash of Light", 7);
            calc[1] = new Spell("Holy Light", 11);
            calc[2] = new Spell("Holy Light", 10);
            calc[3] = new Spell("Holy Light", 9);
            calc[4] = new Spell("Holy Light", 8);
            calc[5] = new Spell("Holy Light", 7);
            calc[6] = new Spell("Holy Light", 6);
            calc[7] = new Spell("Holy Light", 5);
            calc[8] = new Spell("Holy Light", 4);

            switch (chartName)
            {
                case "Healing per second":
                    FoL.OverallPoints = FoL.ThroughputPoints = calc[0].Hps;
                    HL11.OverallPoints = HL11.ThroughputPoints = calc[1].Hps;
                    HL10.OverallPoints = HL10.ThroughputPoints = calc[2].Hps;
                    HL9.OverallPoints = HL9.ThroughputPoints = calc[3].Hps;
                    HL8.OverallPoints = HL8.ThroughputPoints = calc[4].Hps;
                    HL7.OverallPoints = HL7.ThroughputPoints = calc[5].Hps;
                    HL6.OverallPoints = HL6.ThroughputPoints = calc[6].Hps;
                    HL5.OverallPoints = HL5.ThroughputPoints = calc[7].Hps;
                    HL4.OverallPoints = HL4.ThroughputPoints = calc[8].Hps;
                    break;
                case "Average heal":
                    FoL.OverallPoints = FoL.ThroughputPoints = calc[0].AverageHeal;
                    HL11.OverallPoints = HL11.ThroughputPoints = calc[1].AverageHeal;
                    HL10.OverallPoints = HL10.ThroughputPoints = calc[2].AverageHeal;
                    HL9.OverallPoints = HL9.ThroughputPoints = calc[3].AverageHeal;
                    HL8.OverallPoints = HL8.ThroughputPoints = calc[4].AverageHeal;
                    HL7.OverallPoints = HL7.ThroughputPoints = calc[5].AverageHeal;
                    HL6.OverallPoints = HL6.ThroughputPoints = calc[6].AverageHeal;
                    HL5.OverallPoints = HL5.ThroughputPoints = calc[7].AverageHeal;
                    HL4.OverallPoints = HL4.ThroughputPoints = calc[8].AverageHeal;
                    break;
                case "Healing per mana":
                    FoL.OverallPoints = FoL.LongevityPoints = calc[0].Hpm;
                    HL11.OverallPoints = HL11.LongevityPoints = calc[1].Hpm;
                    HL10.OverallPoints = HL10.LongevityPoints = calc[2].Hpm;
                    HL9.OverallPoints = HL9.LongevityPoints = calc[3].Hpm;
                    HL8.OverallPoints = HL8.LongevityPoints = calc[4].Hpm;
                    HL7.OverallPoints = HL7.LongevityPoints = calc[5].Hpm;
                    HL6.OverallPoints = HL6.LongevityPoints = calc[6].Hpm;
                    HL5.OverallPoints = HL5.LongevityPoints = calc[7].Hpm;
                    HL4.OverallPoints = HL4.LongevityPoints = calc[8].Hpm;
                    break;
                case "Mana per second":
                    FoL.OverallPoints = FoL.LongevityPoints = calc[0].Mps;
                    HL11.OverallPoints = HL11.LongevityPoints = calc[1].Mps;
                    HL10.OverallPoints = HL10.LongevityPoints = calc[2].Mps;
                    HL9.OverallPoints = HL9.LongevityPoints = calc[3].Mps;
                    HL8.OverallPoints = HL8.LongevityPoints = calc[4].Mps;
                    HL7.OverallPoints = HL7.LongevityPoints = calc[5].Mps;
                    HL6.OverallPoints = HL6.LongevityPoints = calc[6].Mps;
                    HL5.OverallPoints = HL5.LongevityPoints = calc[7].Mps;
                    HL4.OverallPoints = HL4.LongevityPoints = calc[8].Mps;
                    break;
            }

            return new ComparisonCalculationBase[] { FoL, HL11, HL10, HL9, HL8, HL7, HL6, HL5, HL4 };
        }

        public override Stats GetRelevantStats(Stats stats)
        {
            return new Stats()
            {
                Stamina = stats.Stamina,
                Intellect = stats.Intellect,
                Mp5 = stats.Mp5,
                Healing = stats.Healing,
                SpellCritRating = stats.SpellCritRating,
                SpellHasteRating = stats.SpellHasteRating,
                Health = stats.Health,
                Mana = stats.Mana,
                Spirit = stats.Spirit,
            };
        }

        public override bool HasRelevantStats(Stats stats)
        {
            return (stats.Intellect + stats.Spirit + stats.Stamina + stats.Mp5 + stats.Healing + stats.SpellCritRating
                + stats.SpellHasteRating + stats.BonusSpiritMultiplier + stats.SpellDamageFromSpiritPercentage + stats.BonusIntellectMultiplier
                + stats.FoLBoL + stats.FoLHeal + stats.FoLMultiple + stats.HLBoL + stats.HLCost + stats.HLCrit + stats.HLHeal) > 0;
        }
    }
}
