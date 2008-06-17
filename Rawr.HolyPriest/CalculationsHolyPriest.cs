﻿using System;
using System.Collections.Generic;

using Rawr;

namespace Rawr.HolyPriest
{
    [System.ComponentModel.DisplayName("HolyPriest|Spell_Holy_Renew")]
    public class CalculationsHolyPriest : CalculationsBase 
    {
        public override Character.CharacterClass TargetClass { get { return Character.CharacterClass.Priest; } }

        private string _currentChartName = null;
        
        private Dictionary<string, System.Drawing.Color> _subPointNameColors = null;
        public override Dictionary<string, System.Drawing.Color> SubPointNameColors
        {
            get
            {
                _subPointNameColors = new Dictionary<string, System.Drawing.Color>();
                switch (_currentChartName)
                {
                    case "Spell HpS":
                        _subPointNameColors.Add("HpS", System.Drawing.Color.Red);
                        break;
                    case "Spell HpM":
                        _subPointNameColors.Add("HpM", System.Drawing.Color.Red);
                        break;
                    default:
                        _subPointNameColors.Add("Healing", System.Drawing.Color.Red);
                        _subPointNameColors.Add("Regen", System.Drawing.Color.Blue);
                        break;
                }
               
                return _subPointNameColors;
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
					"Basic Stats:Regen InFSR",
					"Basic Stats:Regen OutFSR",
					"Basic Stats:Spell Crit",
					"Basic Stats:Spell Haste",
                    "Spells:Renew",
                    "Spells:Greater Heal",
                    "Spells:Flash Heal",
				    "Spells:Binding Heal",
                    "Spells:Prayer of Mending",
                    "Spells:PoH",
				    "Spells:CoH",
                    "Spells:Power Word Shield",
                    "Spells:Heal",
				    "Spells:Holy Nova",
                    "Spells:Lightwell"
				};
                return _characterDisplayCalculationLabels;
            }
        }

        private CalculationOptionsPanelBase _calculationOptionsPanel = null;
        public override CalculationOptionsPanelBase CalculationOptionsPanel
        {
            get {
                if (_calculationOptionsPanel == null)
                {
                    _calculationOptionsPanel = new CalculationOptionsPanelHolyPriest();
                }
                return _calculationOptionsPanel;
            }
        }

        private string[] _customChartNames = null;
        public override string[] CustomChartNames
        {
            get
            {
                if (_customChartNames == null)
                    _customChartNames = new string[] { "Spell HpS", "Spell HpM" };
                return _customChartNames;
            }
        }

        public override ComparisonCalculationBase CreateNewComparisonCalculation() { return new ComparisonCalculationHolyPriest(); }
        public override CharacterCalculationsBase CreateNewCharacterCalculations() { return new CharacterCalculationsHolyPriest(); }

        private List<Item.ItemType> _relevantItemTypes = null;
        public override List<Item.ItemType> RelevantItemTypes
        {
            get {
                if (_relevantItemTypes == null) {
                    _relevantItemTypes = new List<Item.ItemType>(new Item.ItemType[]{
                        Item.ItemType.None,
                        Item.ItemType.Cloth,
                        Item.ItemType.Dagger,
                        Item.ItemType.Wand,
                        Item.ItemType.OneHandMace,
                        Item.ItemType.Staff
                    });
                }
                return _relevantItemTypes;
            }
        }

        public override CharacterCalculationsBase GetCharacterCalculations(Character character, Item additionalItem)
        {
            Stats stats = GetCharacterStats(character, additionalItem);
            Stats statsRace = GetRaceStats(character);
            CharacterCalculationsHolyPriest calculatedStats = new CharacterCalculationsHolyPriest();
            CalculationOptionsPriest calculationOptions = character.CalculationOptions as CalculationOptionsPriest;
            
            calculatedStats.BasicStats = stats;
            calculatedStats.Talents = character.Talents;

            calculatedStats.BasicStats.Spirit = statsRace.Spirit + (calculatedStats.BasicStats.Spirit - statsRace.Spirit) * (1 + character.Talents.GetTalent("Spirit of Redemption").PointsInvested * 0.05f);

            calculatedStats.SpiritRegen = (float)Math.Floor(5 * 0.0093271 * calculatedStats.BasicStats.Spirit * Math.Sqrt(calculatedStats.BasicStats.Intellect));
            calculatedStats.RegenInFSR = (float)Math.Floor((calculatedStats.BasicStats.Mp5 + character.Talents.GetTalent("Meditation").PointsInvested * 0.1f * calculatedStats.SpiritRegen * (1 + calculatedStats.BasicStats.BonusManaregenWhileCastingMultiplier)));
            calculatedStats.RegenOutFSR = calculatedStats.BasicStats.Mp5 + calculatedStats.SpiritRegen;
            
            calculatedStats.BasicStats.SpellCrit = (float)Math.Round((calculatedStats.BasicStats.Intellect / 80) +
                (calculatedStats.BasicStats.SpellCritRating / 22.08) + 1.85 + character.Talents.GetTalent("Holy Specialization").PointsInvested, 2);

            calculatedStats.BasicStats.Healing += calculatedStats.BasicStats.Spirit * character.Talents.GetTalent("Spiritual Guidance").PointsInvested * 0.05f;
            
            calculatedStats.HealPoints = calculatedStats.BasicStats.Healing;
            calculatedStats.RegenPoints = (calculatedStats.RegenInFSR * calculationOptions.TimeInFSR*0.01f +
                                           calculatedStats.RegenOutFSR * (100 - calculationOptions.TimeInFSR) * 0.01f);
            calculatedStats.OverallPoints = calculatedStats.HealPoints + calculatedStats.RegenPoints;

            return calculatedStats;
        }

        public Stats GetRaceStats(Character character)
        {
            switch (character.Race)
            {
                case Character.CharacterRace.NightElf:
                    return new Stats()
                    {
                        Health = 3434f,
                        Mana = 2470f,
                        Stamina = 57f,
                        Agility = 50f,
                        Intellect = 147f,
                        Spirit = 151f
                    };
                case Character.CharacterRace.Dwarf:
                    return new Stats()
                    {
                        Health = 3434f,
                        Mana = 2470f,
                        Stamina = 61f,
                        Agility = 41f,
                        Intellect = 144f,
                        Spirit = 150f
                    };
                case Character.CharacterRace.Draenei:
                    return new Stats()
                    {
                        Health = 3434f,
                        Mana = 2470f,
                        Stamina = 57f,
                        Agility = 42f,
                        Intellect = 146f,
                        Spirit = 160f
                    };
                case Character.CharacterRace.Human:
                    return new Stats()
                    {
                        Health = 3434f,
                        Mana = 2470f,
                        Stamina = 58f,
                        Agility = 45f,
                        Intellect = 145f,
                        Spirit = 174f
                    };
                case Character.CharacterRace.BloodElf:
                    return new Stats()
                    {
                        Health = 3211f,
                        Mana = 2620f,
                        Stamina = 56f,
                        Agility = 47f,
                        Intellect = 149f,
                        Spirit = 157f
                    };
                case Character.CharacterRace.Troll:
                    return new Stats()
                    {
                        Health = 3211f,
                        Mana = 2620f,
                        Stamina = 59f,
                        Agility = 59f,
                        Intellect = 141f,
                        Spirit = 159f
                    };
                case Character.CharacterRace.Undead:
                    return new Stats()
                    {
                        Health = 3181,
                        Mana = 2530f,
                        Stamina = 59f,
                        Agility = 43f,
                        Intellect = 145f,
                        Spirit = 156f,
                    };
            }
            return null;
        }

        public override Stats GetCharacterStats(Character character, Item additionalItem)
        {
            Stats statsRace = GetRaceStats(character);
            Stats statsBaseGear = GetItemStats(character, additionalItem);
            Stats statsEnchants = GetEnchantsStats(character);
            Stats statsBuffs = GetBuffsStats(character.ActiveBuffs);

            Stats statsTotal = statsBaseGear + statsEnchants + statsBuffs + statsRace;

            statsTotal.Stamina = (float)Math.Round((statsTotal.Stamina) * (1 + statsTotal.BonusStaminaMultiplier));
            statsTotal.Intellect = (float)Math.Round(statsTotal.Intellect * (1 + statsTotal.BonusIntellectMultiplier));
            statsTotal.Spirit = (float)Math.Round((statsTotal.Spirit) * (1 + statsTotal.BonusSpiritMultiplier));
            statsTotal.Healing = (float)Math.Round(statsTotal.Healing + (statsTotal.SpellDamageFromSpiritPercentage * statsTotal.Spirit));
            statsTotal.Mana = statsTotal.Mana + ((statsTotal.Intellect - 20f) * 15f + 20f);
            statsTotal.Health = statsTotal.Health + (statsTotal.Stamina * 10f);

            return statsTotal;
        }

        public override ComparisonCalculationBase[] GetCustomChartData(Character character, string chartName)
        {
            List<ComparisonCalculationBase> comparisonList = new List<ComparisonCalculationBase>();
            ComparisonCalculationBase comparison;
            CharacterCalculationsHolyPriest p;
            Spell[] spellList;

            switch (chartName)
            {
                case "Spell HpS":
                    _currentChartName = "Spell HpS";
                    p = GetCharacterCalculations(character) as CharacterCalculationsHolyPriest;
                    spellList = new Spell[] {
                                                new Renew(p.BasicStats, character.Talents), 
                                                new FlashHeal(p.BasicStats, character.Talents), 
                                                new GreaterHeal(p.BasicStats, character.Talents),
                                                new Heal(p.BasicStats, character.Talents),
                                                new PrayerOfHealing(p.BasicStats, character.Talents),
                                                new BindingHeal(p.BasicStats, character.Talents),
                                                new PrayerOfMending(p.BasicStats, character.Talents),
                                                new CircleOfHealing(p.BasicStats, character.Talents),
                                                new HolyNova(p.BasicStats, character.Talents),
                                                new Lightwell(p.BasicStats, character.Talents)
                                            };
                    foreach (Spell spell in spellList)
                    {
                        if(spell.AvgHeal == 0)
                            continue;

                        comparison = CreateNewComparisonCalculation();
                        comparison.Name = spell.Name;
                        comparison.Equipped = false;
                        comparison.SubPoints[0] = spell.HpS;
                        comparison.OverallPoints = comparison.SubPoints[0];
                        comparisonList.Add(comparison);
                    }

                    return comparisonList.ToArray();
                case "Spell HpM":
                    _currentChartName = "Spell HpM";
                    p = GetCharacterCalculations(character) as CharacterCalculationsHolyPriest;
                    spellList = new Spell[] {
                                                new Renew(p.BasicStats, character.Talents), 
                                                new FlashHeal(p.BasicStats, character.Talents), 
                                                new GreaterHeal(p.BasicStats, character.Talents),
                                                new Heal(p.BasicStats, character.Talents),
                                                new PrayerOfHealing(p.BasicStats, character.Talents),
                                                new BindingHeal(p.BasicStats, character.Talents),
                                                new PrayerOfMending(p.BasicStats, character.Talents),
                                                new CircleOfHealing(p.BasicStats, character.Talents),
                                                new PowerWordShield(p.BasicStats, character.Talents),
                                                new HolyNova(p.BasicStats, character.Talents),
                                                new Lightwell(p.BasicStats, character.Talents)
                                            };
                    foreach (Spell spell in spellList)
                    {
                        if (spell.AvgHeal == 0)
                            continue;

                        comparison = CreateNewComparisonCalculation();
                        comparison.Name = spell.Name;
                        comparison.Equipped = false;
                        comparison.OverallPoints = spell.HpM;
                        comparison.SubPoints[0] = comparison.OverallPoints;
                        comparisonList.Add(comparison);
                    }
                    return comparisonList.ToArray();
                default:
                    _currentChartName = null;
                    return new ComparisonCalculationBase[0];
            }
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
                BonusManaPotion = stats.BonusManaPotion,
                MementoProc = stats.MementoProc,
                BonusManaregenWhileCastingMultiplier = stats.BonusManaregenWhileCastingMultiplier,
                BonusPoHManaCostReductionMultiplier = stats.BonusPoHManaCostReductionMultiplier,
                BonusGHHealingMultiplier = stats.BonusGHHealingMultiplier
            };
        }

        public override bool HasRelevantStats(Stats stats)
        {
            return (stats.Stamina + stats.Intellect + stats.Spirit + stats.Mp5 + stats.Healing + stats.SpellCritRating
                + stats.SpellHasteRating + stats.BonusSpiritMultiplier + stats.SpellDamageFromSpiritPercentage + stats.BonusIntellectMultiplier
                + stats.BonusManaPotion + stats.MementoProc + stats.BonusManaregenWhileCastingMultiplier
                + stats.BonusPoHManaCostReductionMultiplier + stats.BonusManaregenWhileCastingMultiplier) > 0;
        }

        public override ICalculationOptionBase DeserializeDataObject(string xml)
        {
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(CalculationOptionsPriest));
            System.IO.StringReader reader = new System.IO.StringReader(xml);
            CalculationOptionsPriest calcOpts = serializer.Deserialize(reader) as CalculationOptionsPriest;
            return calcOpts;
        }
    }
}
