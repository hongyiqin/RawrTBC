﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Xml.Serialization;
using System.Reflection;
using System.Xml;

namespace Rawr.Mage
{
    [Serializable]
    public class CalculationOptionsMage : ICalculationOptionBase
    {
        public int TargetLevel { get; set; }
        public int AoeTargetLevel { get; set; }
        public float Latency { get; set; }
        public int AoeTargets { get; set; }
        public float ArcaneResist { get; set; }
        public float FireResist { get; set; }
        public float FrostResist { get; set; }
        public float NatureResist { get; set; }
        public float ShadowResist { get; set; }
        public float FightDuration { get; set; }
        public float TpsLimit { get; set; }
        public float ShadowPriest { get; set; }
        public bool HeroismAvailable { get; set; }
        public bool DestructionPotion { get; set; }
        public bool FlameCap { get; set; }
        public bool ABCycles { get; set; }
        public float MoltenFuryPercentage { get; set; }
        public bool MaintainScorch { get; set; }
        public float InterruptFrequency { get; set; }
        public bool JudgementOfWisdom { get; set; }
        public float EvocationWeapon { get; set; }
        public float EvocationSpirit { get; set; }
        public float AoeDuration { get; set; }
        public bool SmartOptimization { get; set; }
        public float DpsTime { get; set; }
        public bool DrumsOfBattle { get; set; }
        public bool AutomaticArmor { get; set; }
        public bool IncrementalOptimizations { get; set; }

        [XmlIgnore]
        public int[] IncrementalSetCooldowns;
        [XmlIgnore]
        public int[] IncrementalSetSortedCooldowns;
        [XmlIgnore]
        public int[] IncrementalSetSegments;
        [XmlIgnore]
        public SpellId[] IncrementalSetSpells;
        [XmlIgnore]
        public string IncrementalSetArmor;

        public bool ReconstructSequence { get; set; }
        public float Innervate { get; set; }
        public float ManaTide { get; set; }
        public float Fragmentation { get; set; }
        public bool SMP { get; set; }
        public bool SMPDisplay { get; set; }
        public float SurvivabilityRating { get; set; }

        public CalculationOptionsMage Clone()
        {
            CalculationOptionsMage clone = (CalculationOptionsMage)MemberwiseClone();
            clone.IncrementalSetArmor = null;
            clone.IncrementalSetCooldowns = null;
            clone.IncrementalSetSegments = null;
            clone.IncrementalSetSpells = null;
            return clone;
        }

        public int GetTalentByName(string name)
        {
            Type t = typeof(CalculationOptionsMage);
            return (int)t.GetProperty(name).GetValue(this, null);
        }

        public void SetTalentByName(string name, int value)
        {
            Type t = typeof(CalculationOptionsMage);
            t.GetProperty(name).SetValue(this, value, null);
        }

        public int Pyromaniac { get; set; }
        public int ElementalPrecision { get; set; }
        public int FrostChanneling { get; set; }
        public int MasterOfElements { get; set; }
        public int ArcaneConcentration { get; set; }
        public int MindMastery { get; set; }
        public int ArcaneInstability { get; set; }
        public int ArcanePotency { get; set; }
        public int ArcaneFocus { get; set; }
        public int PlayingWithFire { get; set; }
        public int MoltenFury { get; set; }
        public int FirePower { get; set; }
        public int PiercingIce { get; set; }
        public int SpellPower { get; set; }
        public int Ignite { get; set; }
        public int IceShards { get; set; }
        public int CriticalMass { get; set; }
        public int Combustion { get; set; }
        public int ImprovedFrostbolt { get; set; }
        public int EmpoweredFrostbolt { get; set; }
        public int ImprovedFireball { get; set; }
        public int EmpoweredFireball { get; set; }
        public int ArcaneImpact { get; set; }
        public int EmpoweredArcaneMissiles { get; set; }
        public int Incinerate { get; set; }
        public int ImprovedScorch { get; set; }
        public int WintersChill { get; set; }
        public int BurningSoul { get; set; }
        public int ImprovedArcaneMissiles { get; set; }
        public int WandSpecialization { get; set; }
        public int BlastWave { get; set; }
        public int DragonsBreath { get; set; }
        public int ArcanePower { get; set; }
        public int IcyVeins { get; set; }
        public int ColdSnap { get; set; }
        public int IceFloes { get; set; }
        public int SummonWaterElemental { get; set; }
        public int ArcaneMind { get; set; }
        public int ArcaneFortitude { get; set; }
        public int MagicAbsorption { get; set; }
        public int FrostWarding { get; set; }
        public int ArcaneMeditation { get; set; }
        public int ArcaneSubtlety { get; set; }
        public int ImprovedFireBlast { get; set; }
        public int ImprovedFlamestrike { get; set; }
        public int ImprovedFrostNova { get; set; }
        public int ImprovedConeOfCold { get; set; }
        public int ArcticWinds { get; set; }

        // not implemented
        public int IceBarrier { get; set; }
        public int FrozenCore { get; set; }
        public int Shatter { get; set; }
        public int ArcticReach { get; set; }
        public int ImprovedBlizzard { get; set; }
        public int Permafrost { get; set; }
        public int Frostbite { get; set; }
        public int BlazingSpeed { get; set; }
        public int ImprovedFireWard { get; set; }
        public int Pyroblast { get; set; }
        public int FlameThrowing { get; set; }
        public int Impact { get; set; }
        public int Slow { get; set; }
        public int PrismaticCloak { get; set; }
        public int PresenceOfMind { get; set; }
        public int ImprovedBlink { get; set; }
        public int ImprovedCounterspell { get; set; }
        public int ImprovedManaShield { get; set; }
        public int MagicAttunement { get; set; }

        private CalculationOptionsMage()
        {
        }

        public CalculationOptionsMage(Character character)
        {
            TargetLevel = 73;
            AoeTargetLevel = 70;
            Latency = 0.05f;
            AoeTargets = 9;
            ArcaneResist = 0;
            FireResist = 0;
            FrostResist = 0;
            NatureResist = 0;
            ShadowResist = 0;
            FightDuration = 300;
            ShadowPriest = 175;
            HeroismAvailable = true;
            MoltenFuryPercentage = 0.15f;
            DestructionPotion = true;
            FlameCap = true;
            ABCycles = true;
            DpsTime = 1;
            MaintainScorch = true;
            InterruptFrequency = 0;
            EvocationWeapon = 0;
            AoeDuration = 0;
            SmartOptimization = false;
            DrumsOfBattle = false;
            AutomaticArmor = true;
            TpsLimit = 5000;
            IncrementalOptimizations = true;
            ReconstructSequence = false;
            Innervate = 0;
            ManaTide = 0;
            Fragmentation = 0;
            SMP = false;
            SMPDisplay = false;
            EvocationSpirit = 0;
            SurvivabilityRating = 0.0001f;

            // pull talents
            #region Mage Talents Import
            try
            {
                WebRequestWrapper wrw = new WebRequestWrapper();

                if (character.Class == Character.CharacterClass.Mage && character.Name != null && character.Realm != null)
                {
                    XmlDocument docTalents = wrw.DownloadCharacterTalentTree(character.Name, character.Region, character.Realm);

                    //<talentTab>
                    //  <talentTree value="2550050300230151333125100000000000000000000002030302010000000000000"/>
                    //</talentTab>
                    if (docTalents != null)
                    {
                        string talentCode = docTalents.SelectSingleNode("page/characterInfo/talentTab/talentTree").Attributes["value"].Value;
                        ArcaneSubtlety = int.Parse(talentCode.Substring(0, 1));
                        ArcaneFocus = int.Parse(talentCode.Substring(1, 1));
                        ImprovedArcaneMissiles = int.Parse(talentCode.Substring(2, 1));
                        WandSpecialization = int.Parse(talentCode.Substring(3, 1));
                        MagicAbsorption = int.Parse(talentCode.Substring(4, 1));
                        ArcaneConcentration = int.Parse(talentCode.Substring(5, 1));
                        MagicAttunement = int.Parse(talentCode.Substring(6, 1));
                        ArcaneImpact = int.Parse(talentCode.Substring(7, 1));
                        ArcaneFortitude = int.Parse(talentCode.Substring(8, 1));
                        ImprovedManaShield = int.Parse(talentCode.Substring(9, 1));
                        ImprovedCounterspell = int.Parse(talentCode.Substring(10, 1));
                        ArcaneMeditation = int.Parse(talentCode.Substring(11, 1));
                        ImprovedBlink = int.Parse(talentCode.Substring(12, 1));
                        PresenceOfMind = int.Parse(talentCode.Substring(13, 1));
                        ArcaneMind = int.Parse(talentCode.Substring(14, 1));
                        PrismaticCloak = int.Parse(talentCode.Substring(15, 1));
                        ArcaneInstability = int.Parse(talentCode.Substring(16, 1));
                        ArcanePotency = int.Parse(talentCode.Substring(17, 1));
                        EmpoweredArcaneMissiles = int.Parse(talentCode.Substring(18, 1));
                        ArcanePower = int.Parse(talentCode.Substring(19, 1));
                        SpellPower = int.Parse(talentCode.Substring(20, 1));
                        MindMastery = int.Parse(talentCode.Substring(21, 1));
                        Slow = int.Parse(talentCode.Substring(22, 1));
                        ImprovedFireball = int.Parse(talentCode.Substring(23, 1));
                        Impact = int.Parse(talentCode.Substring(24, 1));
                        Ignite = int.Parse(talentCode.Substring(25, 1));
                        FlameThrowing = int.Parse(talentCode.Substring(26, 1));
                        ImprovedFireBlast = int.Parse(talentCode.Substring(27, 1));
                        Incinerate = int.Parse(talentCode.Substring(28, 1));
                        ImprovedFlamestrike = int.Parse(talentCode.Substring(29, 1));
                        Pyroblast = int.Parse(talentCode.Substring(30, 1));
                        BurningSoul = int.Parse(talentCode.Substring(31, 1));
                        ImprovedScorch = int.Parse(talentCode.Substring(32, 1));
                        ImprovedFireWard = int.Parse(talentCode.Substring(33, 1));
                        MasterOfElements = int.Parse(talentCode.Substring(34, 1));
                        PlayingWithFire = int.Parse(talentCode.Substring(35, 1));
                        CriticalMass = int.Parse(talentCode.Substring(36, 1));
                        BlastWave = int.Parse(talentCode.Substring(37, 1));
                        BlazingSpeed = int.Parse(talentCode.Substring(38, 1));
                        FirePower = int.Parse(talentCode.Substring(39, 1));
                        Pyromaniac = int.Parse(talentCode.Substring(40, 1));
                        Combustion = int.Parse(talentCode.Substring(41, 1));
                        MoltenFury = int.Parse(talentCode.Substring(42, 1));
                        EmpoweredFireball = int.Parse(talentCode.Substring(43, 1));
                        DragonsBreath = int.Parse(talentCode.Substring(44, 1));
                        FrostWarding = int.Parse(talentCode.Substring(45, 1));
                        ImprovedFrostbolt = int.Parse(talentCode.Substring(46, 1));
                        ElementalPrecision = int.Parse(talentCode.Substring(47, 1));
                        IceShards = int.Parse(talentCode.Substring(48, 1));
                        Frostbite = int.Parse(talentCode.Substring(49, 1));
                        ImprovedFrostNova = int.Parse(talentCode.Substring(50, 1));
                        Permafrost = int.Parse(talentCode.Substring(51, 1));
                        PiercingIce = int.Parse(talentCode.Substring(52, 1));
                        IcyVeins = int.Parse(talentCode.Substring(53, 1));
                        ImprovedBlizzard = int.Parse(talentCode.Substring(54, 1));
                        ArcticReach = int.Parse(talentCode.Substring(55, 1));
                        FrostChanneling = int.Parse(talentCode.Substring(56, 1));
                        Shatter = int.Parse(talentCode.Substring(57, 1));
                        FrozenCore = int.Parse(talentCode.Substring(58, 1));
                        ColdSnap = int.Parse(talentCode.Substring(59, 1));
                        ImprovedConeOfCold = int.Parse(talentCode.Substring(60, 1));
                        IceFloes = int.Parse(talentCode.Substring(61, 1));
                        WintersChill = int.Parse(talentCode.Substring(62, 1));
                        IceBarrier = int.Parse(talentCode.Substring(63, 1));
                        ArcticWinds = int.Parse(talentCode.Substring(64, 1));
                        EmpoweredFrostbolt = int.Parse(talentCode.Substring(65, 1));
                        SummonWaterElemental = int.Parse(talentCode.Substring(66, 1));
                    }
                }
            }
            catch (Exception)
            {
            }
            #endregion
        }

        string ICalculationOptionBase.GetXml()
        {
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(CalculationOptionsMage));
            StringBuilder xml = new StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(xml);
            serializer.Serialize(writer, this);
            return xml.ToString();
        }
    }

    public enum Cooldown
    {
        ArcanePower,
        IcyVeins,
        MoltenFury,
        Heroism,
        DestructionPotion,
        FlameCap,
        Trinket1,
        Trinket2,
        DrumsOfBattle,
        Combustion
    }

    public class CharacterCalculationsMage : CharacterCalculationsBase
    {
        private float _overallPoints = 0f;
        public override float OverallPoints
        {
            get { return _overallPoints; }
            set { _overallPoints = value; }
        }

        private float[] _subPoints = new float[] { 0f, 0f };
        public override float[] SubPoints
        {
            get { return _subPoints; }
            set { _subPoints = value; }
        }

        public float DpsRating
        {
            get
            {
                return _subPoints[0];
            }
        }

        public float SurvivabilityRating
        {
            get
            {
                return _subPoints[1];
            }
        }

        private Stats _basicStats;
        public Stats BasicStats
        {
            get { return _basicStats; }
            set { _basicStats = value; }
        }

        public CalculationOptionsMage CalculationOptions { get; set; }

        public Character Character { get; set; }

        public int IncrementalSetIndex { get; set; }
        public int[] IncrementalSetCooldown { get; set; }
        public int[] IncrementalSetSegment { get; set; }
        public SpellId[] IncrementalSetSpell { get; set; }

        public float SpellCrit { get; set; }
        public float SpellHit { get; set; }
        public float CastingSpeed { get; set; }
        public float GlobalCooldown { get; set; }
        public float GlobalCooldownLimit { get; set; }

        public float ArcaneDamage { get; set; }
        public float FireDamage { get; set; }
        public float FrostDamage { get; set; }
        public float NatureDamage { get; set; }
        public float ShadowDamage { get; set; }

        public float SpiritRegen { get; set; }
        public float ManaRegen { get; set; }
        public float ManaRegen5SR { get; set; }
        public float ManaRegenDrinking { get; set; }
        public float HealthRegen { get; set; }
        public float HealthRegenCombat { get; set; }
        public float HealthRegenEating { get; set; }
        public float MeleeMitigation { get; set; }
        public float Defense { get; set; }
        public float PhysicalCritReduction { get; set; }
        public float SpellCritReduction { get; set; }
        public float CritDamageReduction { get; set; }
        public float Dodge { get; set; }

        public float ArcaneSpellModifier { get; set; }
        public float FireSpellModifier { get; set; }
        public float FrostSpellModifier { get; set; }
        public float NatureSpellModifier { get; set; }
        public float ShadowSpellModifier { get; set; }

        public float ArcaneCritBonus { get; set; }
        public float FireCritBonus { get; set; }
        public float FrostCritBonus { get; set; }
        public float NatureCritBonus { get; set; }
        public float ShadowCritBonus { get; set; }

        public float ArcaneCritRate { get; set; }
        public float FireCritRate { get; set; }
        public float FrostCritRate { get; set; }
        public float NatureCritRate { get; set; }
        public float ShadowCritRate { get; set; }

        public float ArcaneHitRate { get; set; }
        public float FireHitRate { get; set; }
        public float FrostHitRate { get; set; }
        public float NatureHitRate { get; set; }
        public float ShadowHitRate { get; set; }

        public float ArcaneThreatMultiplier { get; set; }
        public float FireThreatMultiplier { get; set; }
        public float FrostThreatMultiplier { get; set; }
        public float NatureThreatMultiplier { get; set; }
        public float ShadowThreatMultiplier { get; set; }

        public float ResilienceCritDamageReduction { get; set; }
        public float ResilienceCritRateReduction { get; set; }
        public float Latency { get; set; }
        public float FightDuration { get; set; }
        public float ClearcastingChance { get; set; }

        public int GetHex()
        {
            int hex = 0;
            hex = (hex << 1) + (ArcanePower ? 1 : 0);
            hex = (hex << 1) + (Combustion ? 1 : 0);
            hex = (hex << 1) + (DestructionPotion ? 1 : 0);
            hex = (hex << 1) + (DrumsOfBattle ? 1 : 0);
            hex = (hex << 1) + (FlameCap ? 1 : 0);
            hex = (hex << 1) + (Heroism ? 1 : 0);
            hex = (hex << 1) + (IcyVeins ? 1 : 0);
            hex = (hex << 1) + (MoltenFury ? 1 : 0);
            hex = (hex << 1) + (Trinket1 ? 1 : 0);
            hex = (hex << 1) + (Trinket2 ? 1 : 0);
            return hex;
        }

        public bool GetCooldown(Cooldown cooldown)
        {
            switch (cooldown)
            {
                case Cooldown.ArcanePower:
                    return ArcanePower;
                case Cooldown.Combustion:
                    return Combustion;
                case Cooldown.DestructionPotion:
                    return DestructionPotion;
                case Cooldown.DrumsOfBattle:
                    return DrumsOfBattle;
                case Cooldown.FlameCap:
                    return FlameCap;
                case Cooldown.Heroism:
                    return Heroism;
                case Cooldown.IcyVeins:
                    return IcyVeins;
                case Cooldown.MoltenFury:
                    return MoltenFury;
                case Cooldown.Trinket1:
                    return Trinket1;
                case Cooldown.Trinket2:
                    return Trinket2;
                default:
                    return false;
            }
        }

        public bool ArcanePower { get; set; }
        public bool IcyVeins { get; set; }
        public bool MoltenFury { get; set; }
        public bool Heroism { get; set; }
        public bool DestructionPotion { get; set; }
        public bool FlameCap { get; set; }
        public bool Trinket1 { get; set; }
        public bool Trinket2 { get; set; }
        public bool DrumsOfBattle { get; set; }
        public bool WaterElemental { get; set; }
        public bool Combustion { get; set; }
        public float CombustionDuration { get; set; }
        public float Mp5OnCastFor20Sec { get; set; }

        public float WaterElementalDps { get; set; }
        public float WaterElementalDuration { get; set; }
        public float WaterElementalDamage { get; set; }

        public string BuffLabel { get; set; }
        public string MageArmor { get; set; }

        public double EvocationDuration;
        public double EvocationRegen;
        public double ManaPotionTime = 0.1f;
        public double Trinket1Duration;
        public double Trinket1Cooldown;
        public double Trinket2Duration;
        public double Trinket2Cooldown;
        public string Trinket1Name;
        public string Trinket2Name;
        public int MaxManaPotion;
        public int MaxManaGem;
        public string[] SolutionLabel;
        public double[] Solution;
        public CharacterCalculationsMage[] SolutionStats;
        public Spell[] SolutionSpells;
        public int[] SolutionSegments;
        public float Tps;

        public double RealFightDuration
        {
            get
            {
                return FightDuration - Solution[3] - Solution[4];
            }
        }

        /*private static int SpellIdCount;

        static CharacterCalculationsMage()
        {
            SpellIdCount = Enum.GetValues(typeof(SpellId)).Length;
        }

        private Spell[] Spells = new Spell[SpellIdCount];*/

        private Dictionary<int, Spell> Spells = new Dictionary<int, Spell>(); // profiling doesn't show any noticeable benefit using plain array, so use dictionary to save on size

        public void SetSpell(SpellId spellId, Spell spell)
        {
            Spells[(int)spellId] = spell;
        }

        public Spell GetSpell(SpellId spellId)
        {
            //Spell s = Spells[(int)spellId];
            //if (s != null) return s;
            Spell s = null;
            if (Spells.TryGetValue((int)spellId, out s)) return s;

            switch (spellId)
            {
                case SpellId.LightningBolt:
                    s = new LightningBolt(Character, this);
                    break;
                case SpellId.ArcaneMissiles:
                    s = new ArcaneMissiles(Character, this);
                    break;
                case SpellId.ArcaneMissilesCC:
                    s = new ArcaneMissilesCC(Character, this);
                    break;
                case SpellId.ArcaneMissilesNoProc:
                    s = new ArcaneMissiles(Character, this, true, false, false);
                    break;
                /*case SpellId.ArcaneMissilesFTF:
                    s = new ArcaneMissiles(Character, this);
                    break;
                case SpellId.ArcaneMissilesFTT:
                    s = new ArcaneMissiles(Character, this);
                    break;*/
                case SpellId.Frostbolt:
                    s = new Frostbolt(Character, this);
                    break;
                case SpellId.FrostboltNoCC:
                    s = new Frostbolt(Character, this, false);
                    break;
                case SpellId.Fireball:
                    s = new Fireball(Character, this);
                    break;
                case SpellId.Pyroblast:
                    s = new Pyroblast(Character, this);
                    break;
                case SpellId.FireBlast:
                    s = new FireBlast(Character, this);
                    break;
                case SpellId.Scorch:
                    s = new Scorch(Character, this);
                    break;
                case SpellId.ScorchNoCC:
                    s = new Scorch(Character, this, false);
                    break;
                case SpellId.ArcaneBlast33:
                    s = new ArcaneBlast(Character, this, 3, 3);
                    break;
                case SpellId.ArcaneBlast33NoCC:
                    s = new ArcaneBlast(Character, this, 3, 3, false);
                    break;
                case SpellId.ArcaneBlast00:
                    s = new ArcaneBlast(Character, this, 0, 0);
                    break;
                case SpellId.ArcaneBlast00NoCC:
                    s = new ArcaneBlast(Character, this, 0, 0, false);
                    break;
                case SpellId.ArcaneBlast10:
                    s = new ArcaneBlast(Character, this, 1, 0);
                    break;
                case SpellId.ArcaneBlast01:
                    s = new ArcaneBlast(Character, this, 0, 1);
                    break;
                case SpellId.ArcaneBlast11:
                    s = new ArcaneBlast(Character, this, 1, 1);
                    break;
                case SpellId.ArcaneBlast11NoCC:
                    s = new ArcaneBlast(Character, this, 1, 1, false);
                    break;
                case SpellId.ArcaneBlast22:
                    s = new ArcaneBlast(Character, this, 2, 2);
                    break;
                case SpellId.ArcaneBlast22NoCC:
                    s = new ArcaneBlast(Character, this, 2, 2, false);
                    break;
                case SpellId.ArcaneBlast12:
                    s = new ArcaneBlast(Character, this, 1, 2);
                    break;
                case SpellId.ArcaneBlast23:
                    s = new ArcaneBlast(Character, this, 2, 3);
                    break;
                case SpellId.ArcaneBlast30:
                    s = new ArcaneBlast(Character, this, 3, 0);
                    break;
                case SpellId.ABAM:
                    s = new ABAM(Character, this);
                    break;
                case SpellId.ABAMP:
                    s = new ABAMP(Character, this);
                    break;
                case SpellId.AB3AMSc:
                    s = new AB3AMSc(Character, this);
                    break;
                case SpellId.ABAM3Sc:
                    s = new ABAM3Sc(Character, this);
                    break;
                case SpellId.ABAM3Sc2:
                    s = new ABAM3Sc2(Character, this);
                    break;
                case SpellId.ABAM3FrB:
                    s = new ABAM3FrB(Character, this);
                    break;
                case SpellId.ABAM3FrB2:
                    s = new ABAM3FrB2(Character, this);
                    break;
                case SpellId.AB3FrB:
                    s = new AB3FrB(Character, this);
                    break;
                case SpellId.ABFrB3FrB:
                    s = new ABFrB3FrB(Character, this);
                    break;
                case SpellId.ABFrB3FrB2:
                    s = new ABFrB3FrB2(Character, this);
                    break;
                case SpellId.ABFrB3FrBSc:
                    s = new ABFrB3FrBSc(Character, this);
                    break;
                case SpellId.ABFB3FBSc:
                    s = new ABFB3FBSc(Character, this);
                    break;
                case SpellId.AB3Sc:
                    s = new AB3Sc(Character, this);
                    break;
                case SpellId.FireballScorch:
                    s = new FireballScorch(Character, this);
                    break;
                case SpellId.FireballFireBlast:
                    s = new FireballFireBlast(Character, this);
                    break;
                case SpellId.ABAM3ScCCAM:
                    s = new ABAM3ScCCAM(Character, this);
                    break;
                case SpellId.ABAM3Sc2CCAM:
                    s = new ABAM3Sc2CCAM(Character, this);
                    break;
                case SpellId.ABAM3FrBCCAM:
                    s = new ABAM3FrBCCAM(Character, this);
                    break;
                case SpellId.ABAM3FrBCCAMFail:
                    s = new ABAM3FrBCCAMFail(Character, this);
                    break;
                case SpellId.ABAM3FrBScCCAM:
                    s = new ABAM3FrBScCCAM(Character, this);
                    break;
                case SpellId.ABAMCCAM:
                    s = new ABAMCCAM(Character, this);
                    break;
                case SpellId.ABAM3CCAM:
                    s = new ABAM3CCAM(Character, this);
                    break;
                case SpellId.ArcaneExplosion:
                    s = new ArcaneExplosion(Character, this);
                    break;
                case SpellId.FlamestrikeSpammed:
                    s = new Flamestrike(Character, this, true);
                    break;
                case SpellId.FlamestrikeSingle:
                    s = new Flamestrike(Character, this, false);
                    break;
                case SpellId.Blizzard:
                    s = new Blizzard(Character, this);
                    break;
                case SpellId.BlastWave:
                    s = new BlastWave(Character, this);
                    break;
                case SpellId.DragonsBreath:
                    s = new DragonsBreath(Character, this);
                    break;
                case SpellId.ConeOfCold:
                    s = new ConeOfCold(Character, this);
                    break;
            }
            if (s != null) Spells[(int)spellId] = s;

            return s;
        }

        private static string TimeFormat(double time)
        {
            TimeSpan span = new TimeSpan((long)(Math.Round(time, 2) / 0.0000001));
            return string.Format("{0:00}:{1:00}.{2:000}", span.Minutes, span.Seconds, span.Milliseconds);
        }

        private class SequenceItem : ICloneable
        {
            public static CharacterCalculationsMage Calculations;

            private SequenceItem() { }
            public SequenceItem(int index, double duration) : this(index, duration, null) { }

            public SequenceItem(int index, double duration, List<SequenceGroup> group)
            {
                if (group == null) group = new List<SequenceGroup>();
                this.Group = group;
                this.index = index;
                this.Duration = duration;
                this.spell = Calculations.SolutionSpells[index];
                this.stats = Calculations.SolutionStats[index];
                if (Calculations.SolutionSegments != null) this.segment = Calculations.SolutionSegments[index];
                if (stats == null) stats = Calculations;

                switch (index)
                {
                    case 0:
                        mps = -Calculations.ManaRegen;
                        break;
                    case 1:
                        spell = Calculations.GetSpell(SpellId.Wand);
                        mps = spell.CostPerSecond - spell.ManaRegenPerSecond;
                        break;
                    case 2:
                    case 3:
                    case 4:
                        mps = 0;
                        break;
                    case 5:
                        mps = -Calculations.ManaRegen5SR;
                        break;
                    default:
                        mps = spell.CostPerSecond - spell.ManaRegenPerSecond;
                        break;
                }
                minTime = 0;
                maxTime = Calculations.RealFightDuration;
            }

            private int index;
            public int Index
            {
                get
                {
                    return index;
                }
            }

            private int segment;
            public int Segment
            {
                get
                {
                    return segment;
                }
            }
            
            public double Duration;
            public double Timestamp;

            private double minTime;
            public double MinTime
            {
                get
                {
                    return minTime;
                }
                set
                {
                    minTime = Math.Max(minTime, value);
                }
            }

            private double maxTime;
            public double MaxTime
            {
                get
                {
                    return maxTime;
                }
                set
                {
                    maxTime = Math.Min(maxTime, value);
                }
            }

            public List<SequenceGroup> Group;
            public SequenceGroup SuperGroup;

            // helper variables
            public int SuperIndex;
            public List<SequenceGroup> Tail;
            public int CooldownHex;
            public int OrderIndex;

            private Spell spell;
            public Spell Spell
            {
                get
                {
                    return spell;
                }
            }

            private CharacterCalculationsMage stats;
            public CharacterCalculationsMage Stats
            {
                get
                {
                    return stats;
                }
            }

            private double mps;
            public double Mps
            {
                get
                {
                    return mps;
                }
            }

            #region ICloneable Members

            object ICloneable.Clone()
            {
                return Clone();
            }

            public SequenceItem Clone()
            {
                SequenceItem clone = (SequenceItem)MemberwiseClone();
                clone.Group = new List<SequenceGroup>(Group);
                return clone;
            }

            #endregion

            public override string ToString()
            {
                if (spell == null) return index.ToString();
                return stats.BuffLabel + "+" + spell.Name;
            }
        }

        private class CooldownConstraint
        {
            public SequenceGroup Group;
            public double Cooldown;
            public double Duration;
            public bool ColdSnap;
        }

        private class SequenceGroup
        {
            public double Mana;
            public double Duration;
            public double Mps
            {
                get
                {
                    if (Duration > 0)
                    {
                        return Mana / Duration;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            public List<SequenceItem> Item = new List<SequenceItem>();
            public List<CooldownConstraint> Constraint = new List<CooldownConstraint>();

            public double MinTime
            {
                get
                {
                    double t = 0;
                    double min = 0;
                    foreach (SequenceItem item in Item)
                    {
                        min = Math.Max(min, item.MinTime - t);
                        t += item.Duration;
                    }
                    return min;
                }
                set
                {
                    foreach (SequenceItem item in Item)
                    {
                        item.MinTime = value;
                    }
                }
            }

            public double MaxTime
            {
                get
                {
                    double t = 0;
                    double max = SequenceItem.Calculations.RealFightDuration;
                    foreach (SequenceItem item in Item)
                    {
                        max = Math.Min(max, item.MaxTime - t);
                        t += item.Duration;
                    }
                    return max;
                }
                set
                {
                    foreach (SequenceItem item in Item)
                    {
                        item.MaxTime = value;
                    }
                }
            }

            public int Segment
            {
                get
                {
                    if (Item.Count == 0) return -1;
                    int seg = int.MaxValue;
                    foreach (SequenceItem item in Item)
                    {
                        seg = Math.Min(seg, item.Segment);
                    }
                    return seg;
                }
            }

            public void Add(SequenceItem item)
            {
                Mana += item.Mps * item.Duration;
                Duration += item.Duration;
                Item.Add(item);
            }

            public void AddRange(IEnumerable<SequenceItem> collection)
            {
                foreach (SequenceItem item in collection)
                {
                    Add(item);
                }
            }

            public void SortByMps(double minMps, double maxMps)
            {
                List<SequenceItem> Item2 = new List<SequenceItem>(Item);
                List<SequenceItem> sorted = new List<SequenceItem>();
                while (Item2.Count > 0)
                {
                    foreach (SequenceItem item in Item2)
                    {
                        item.Tail = new List<SequenceGroup>(item.Group);
                    }

                    foreach (SequenceItem item in Item2)
                    {
                        foreach (SequenceItem tailitem in Item2)
                        {
                            List<SequenceGroup> intersect = Rawr.Mage.ListUtils.Intersect<SequenceGroup>(item.Group, tailitem.Group);
                            if (intersect.Count > 0)
                            {
								item.Tail = Rawr.Mage.ListUtils.Intersect<SequenceGroup>(intersect, item.Tail);
                            }
                        }
                    }

                    SequenceItem best = null;
                    foreach (SequenceItem item in Item2)
                    {
                        if (best == null || Compare(item, best, (sorted.Count > 0) ? sorted[sorted.Count - 1].Group : null, minMps, maxMps) < 0)
                        {
                            best = item;
                        }
                    }
                    Item2.Remove(best);
                    sorted.Add(best);
                }
                //Item = sorted;
                for (int i = 0; i < sorted.Count; i++)
                {
                    sorted[i].SuperIndex = i;
                }
            }

            public void SetSuperIndex()
            {
                for (int i = 0; i < Item.Count; i++)
                {
                    Item[i].SuperIndex = i;
                }
            }

            private int Compare(SequenceItem x, SequenceItem y, List<SequenceGroup> tail, double minMps, double maxMps)
            {
                bool xsingletail = x.Tail.Count > 0;
                bool ysingletail = y.Tail.Count > 0;
                int compare = ysingletail.CompareTo(xsingletail);
                if (compare != 0) return compare;
                int xintersect = (tail == null) ? 0 : Rawr.Mage.ListUtils.Intersect<SequenceGroup>(x.Group, tail).Count;
				int yintersect = (tail == null) ? 0 : Rawr.Mage.ListUtils.Intersect<SequenceGroup>(y.Group, tail).Count;
                compare = yintersect.CompareTo(xintersect);
                if (compare != 0) return compare;
                return Sequence.CompareMps(x.Mps, y.Mps, minMps, maxMps);
            }
        }

        private class Sequence : IComparer<SequenceItem>
        {
            private List<SequenceItem> sequence = new List<SequenceItem>();

            public void Add(SequenceItem item)
            {
                sequence.Add(item);
            }

            public bool IsCooldownBreakpoint(int index)
            {
                if (index == 0) return true;
                if (sequence[index].Index == 3 || sequence[index].Index == 4 || sequence[index].Index == 5) return false;
                CharacterCalculationsMage stats = sequence[index].Stats;
                CharacterCalculationsMage lastStats = null;
                int lastindex = index - 1;
                while (lastindex >= 0 && (sequence[lastindex].Index == 3 || sequence[lastindex].Index == 4 || sequence[lastindex].Index == 5))
                {
                    lastindex--;
                }
                if (lastindex >= 0) lastStats = sequence[lastindex].Stats;
                if (lastStats == null || !((lastStats.ArcanePower && stats.ArcanePower) || (lastStats.IcyVeins && stats.IcyVeins) || (lastStats.Heroism && stats.Heroism) || (lastStats.MoltenFury && stats.MoltenFury) || (lastStats.DestructionPotion && stats.DestructionPotion) || (lastStats.FlameCap && stats.FlameCap) || (lastStats.DrumsOfBattle && stats.DrumsOfBattle)))
                {
                    return true;
                }
                return false;
            }

            List<SequenceGroup> superGroup = new List<SequenceGroup>();

            private void CalculateSuperGroups()
            {
                superGroup.Clear();
                SequenceGroup group = null;
                List<SequenceGroup> lastGroup = null;
                for (int i = 0; i < sequence.Count; i++)
                {
                    if (lastGroup == null || (sequence[i].Index != 3 && sequence[i].Index != 4 && Rawr.Mage.ListUtils.Intersect<SequenceGroup>(lastGroup, sequence[i].Group).Count == 0))
                    {
                        group = new SequenceGroup();
                        superGroup.Add(group);
                    }
                    if (sequence[i].Index != 3 && sequence[i].Index != 4) lastGroup = sequence[i].Group;
                    group.Add(sequence[i]);
                    sequence[i].SuperGroup = group;
                }
            }

            private bool sortByMps = true;
            private bool preserveCooldowns = true;
            private double sortMaxMps = double.PositiveInfinity;
            private double sortMinMps = double.NegativeInfinity;
            private double sortStartTime;
            private double sortTargetTime;

            public void ComputeTimestamps()
            {
                double t = 0;
                for (int i = 0; i < sequence.Count; i++)
                {
                    double d = sequence[i].Duration;
                    if (sequence[i].Index == 3 || sequence[i].Index == 4) d = 0;
                    sequence[i].Timestamp = t;
                    t += d;
                }
            }

            private double MinTime(SequenceGroup super, int placedUpTo)
            {
                double minTime = super.MinTime;
                foreach (SequenceGroup group in GetAllGroups(super.Item))
                {
                    double diff = group.MinTime - super.MinTime;
                    foreach (CooldownConstraint constraint in group.Constraint)
                    {
                        for (int j = 0; j <= placedUpTo; j++)
                        {
                            if (sequence[j].Group.Contains(constraint.Group))
                            {
                                if (!constraint.ColdSnap || (group.MinTime - sequence[j].MinTime >= 180 - 0.000001)) // make sure to ignore coldsnapped constraints
                                {
                                    minTime = Math.Max(minTime, sequence[j].Timestamp + constraint.Cooldown - diff);
                                }
                                break;
                            }
                        }
                    }
                }
                return minTime;
            }

            private double MinTime(int i, int placedUpTo)
            {
                if (placedUpTo >= i) placedUpTo = i - 1;
                return MinTime(sequence[i].SuperGroup, placedUpTo) + sequence[i].MinTime - sequence[i].SuperGroup.MinTime;
            }

            public void SortByMps(bool preserveCooldowns, double minMps, double maxMps, double startTime, double targetTime, double extraMana, double startMana)
            {
                if (minMps > maxMps) maxMps = minMps;

                this.sortByMps = true;
                this.preserveCooldowns = preserveCooldowns;
                this.sortMaxMps = maxMps;
                this.sortMinMps = minMps;
                this.sortStartTime = startTime;
                this.sortTargetTime = targetTime;

                CalculateSuperGroups();

                double maxMana = maxMps * (targetTime - startTime);
                double minMana = minMps * (targetTime - startTime);
                double mana = 0;

                double t = 0;
                int i;
                SequenceGroup lastGroup = null;
                for (i = 0; i < sequence.Count; i++)
                {
                    double d = sequence[i].Duration;
                    if (sequence[i].Index == 3 || sequence[i].Index == 4) d = 0;
                    if (d > 0 && t + 0.000001 >= startTime)
                    {
                        if (lastGroup != sequence[i].SuperGroup) break;
                        else mana += sequence[i].Mps * d;
                    }
                    else
                    {
                        if (t + d > startTime) mana += sequence[i].Mps * (t + d - startTime);
                        lastGroup = sequence[i].SuperGroup;
                    }
                    t += d;
                }
                if (extraMana == 0)
                {
                    foreach (SequenceGroup group in superGroup)
                    {
                        //group.SortByMps(minMps, maxMps);
                        group.SetSuperIndex();
                    }
                    sequence.Sort(i, sequence.Count - i, this);
                    ComputeTimestamps();
                }
                if (targetTime < t) return; // there is nothing we can do at this point
                double T = t;
                double Mana = mana;
            Retry:
                // first we have sections in the right mps range, then higher, then lower (at the end sections that are not ready yet)
                // so the constraint that will be broken first is maxmana (unless no high burn section is available at the moment)
                // forward to target time
                int j;
                int lastHigh = i;
                double tLastHigh = t;
                double overflowMana = startMana - Mana; // for overflow calculations assume there are no mana consumables placed after start time yet, if we skipped a super group since startTime then we have to adjust starting mana
                double overflowLimit = BasicStats.Mana; // was maxMana before, but I think when we have splittable group we can insert just enough to not go over (I did not think this through too much, so if something is fishy look into this)
                SequenceGroup lastSuper = null;
                for (j = i; j < sequence.Count; j++)
                {
                    double d = sequence[j].Duration;
                    if (sequence[j].Index == 3 || sequence[j].Index == 4) d = 0;
                    if (lastSuper != sequence[j].SuperGroup)
                    {
                        if (sequence[j].Group.Count > 0)
                        {
                            overflowLimit = BasicStats.Mana - overflowMana;
                        }
                        else
                        {
                            overflowLimit = BasicStats.Mana; // this too
                        }
                        lastSuper = sequence[j].SuperGroup;
                    }
                    if (t < MinTime(j, j - 1) - 0.000001)
                    {
                        // sequence positioned too early, we have to buffer up with something that can
                        // be positioned at t and is either small enough not to disrupt max time
                        // or is splittable
                        bool updated = false;
                        double minbuffer = MinTime(j, j - 1) - t;
                        double buffer = sequence[j].MaxTime - t;
                        int k;
                        for (k = j + 1; k < sequence.Count && minbuffer > 0 && buffer > 0; k++)
                        {
                            if (sequence[k].SuperGroup != lastSuper) // intra super ordering not allowed
                            {
                                if (MinTime(k, j - 1) <= t)
                                {
                                    if (sequence[k].Group.Count == 0)
                                    {
                                        if (sequence[k].Duration > minbuffer + 0.000001)
                                        {
                                            SplitAt(k, minbuffer);
                                        }
                                        SequenceItem copy = sequence[k];
                                        sequence.RemoveAt(k);
                                        sequence.Insert(j, copy);
                                        ComputeTimestamps();
                                        minbuffer -= copy.Duration;
                                        buffer -= copy.Duration;
                                        t += copy.Duration;
                                        updated = true;
                                        j++;
                                        k = j;
                                    }
                                    else if (sequence[k].SuperGroup.Duration <= buffer)
                                    {
                                        int l;
                                        for (l = k + 1; l < sequence.Count; l++)
                                        {
                                            if (sequence[l].SuperGroup != sequence[k].SuperGroup) break;
                                        }
                                        List<SequenceItem> copy = sequence.GetRange(k, l - k);
                                        sequence.RemoveRange(k, l - k);
                                        sequence.InsertRange(j, copy);
                                        ComputeTimestamps();
                                        minbuffer -= copy[0].SuperGroup.Duration;
                                        buffer -= copy[0].SuperGroup.Duration;
                                        t += copy[0].SuperGroup.Duration;
                                        updated = true;
                                        j += copy.Count;
                                        k = j;
                                    }
                                }
                            }
                        }
                        if (updated)
                        {
                            t = T;
                            mana = Mana;
                            goto Retry;
                        }
                    }
                    if (d > 0 && sequence[j].SuperGroup.Mps > maxMps)
                    {
                        lastHigh = j;
                        tLastHigh = t;
                    }
                    if (t + d > targetTime)
                    {
                        mana += sequence[j].Mps * (targetTime - t);
                        overflowMana -= sequence[j].Mps * (targetTime - t);
                        break;
                    }
                    else
                    {
                        mana += sequence[j].Mps * d;
                        overflowMana -= sequence[j].Mps * d;
                        t += d;
                    }
                }
                // verify max time constraints
                double tt = T;
                int a;
                lastSuper = null;
                for (a = i; a < sequence.Count; a++)
                {
                    double d = sequence[a].Duration;
                    if (sequence[a].Index == 3 || sequence[a].Index == 4) d = 0;
                    if (lastSuper != sequence[a].SuperGroup)
                    {
                        lastSuper = sequence[a].SuperGroup;
                        if (tt > lastSuper.MaxTime + 0.000001 && tt > T && tt > MinTime(lastSuper, a - 1) + 0.000001) // there might be other cases where it is impossible to move back without breaking others, double check for infinite cycles
                        {
                            // compute buffer of items that can be moved way back
                            double buffer = 0;
                            int b;
                            for (b = i; b < a; b++)
                            {
                                if (sequence[b].MaxTime >= tt + lastSuper.Duration) buffer += sequence[b].Duration;
                            }
                            // place it at max time, but move back over non-splittable super groups
                            // if move breaks constraint on some other group cancel
                            double t3 = tt;
                            bool updated = false;
                            for (b = a - 1; b >= i; b--)
                            {
                                t3 -= sequence[b].Duration;
                                if (t3 <= lastSuper.MaxTime + 0.000001)
                                {
                                    // possible insert point
                                    if (sequence[b].Group.Count == 0)
                                    {
                                        if (sequence[b].MaxTime >= lastSuper.MaxTime + lastSuper.Duration - buffer)
                                        {
                                            // splittable, make a split at max time
                                            if (lastSuper.MaxTime > t3)
                                            {
                                                SplitAt(b, lastSuper.MaxTime - t3);
                                                a++;
                                                b++;
                                            }
                                            sequence.InsertRange(b, RemoveSuperGroup(a));
                                            ComputeTimestamps();
                                            updated = true;
                                        }
                                    }
                                    else
                                    {
                                        // we are in super group, move to start
                                        SequenceGroup super = sequence[b].SuperGroup;
                                        while (b >= i && sequence[b].SuperGroup == super)
                                        {
                                            b--;
                                            if (b >= 0) t3 -= sequence[b].Duration;
                                        }
                                        if (b >= 0) t3 += sequence[b].Duration;
                                        b++;
                                        if (super.MaxTime >= t3 + lastSuper.Duration - buffer)
                                        {
                                            sequence.InsertRange(b, RemoveSuperGroup(a));
                                            ComputeTimestamps();
                                            updated = true;
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    // make sure we wouldn't push it out of max
                                    if (sequence[b].MaxTime < t3 + lastSuper.Duration - buffer)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (updated)
                            {
                                t = T;
                                mana = Mana;
                                goto Retry;
                            }
                        }
                    }
                    tt += d;
                }
                bool extraMode = extraMana > 0;
                if (mana > maxMana + 0.000001 || extraMana > 0)
                {
                    // [i....j]XXX[k....]
                    int k;
                    // [i..|jj..j]XXX[k..|kk.]
                    int jj = j; double jT = targetTime - t;
                    double tjj = t;
                    double tkk = tLastHigh;
                    if (jT <= 0)
                    {
                        jj--;
                        if (jj >= 0 && jj < sequence.Count)
                        {
                            jT += sequence[jj].Duration;
                            tjj -= sequence[jj].Duration;
                        }
                    }
                    double maxPush = 0;
                    if (jj < sequence.Count) maxPush = sequence[jj].MaxTime - tjj; // you can assume jj won't be split
                    if (lastHigh <= j && j < sequence.Count - 1)
                    {
                        if (sequence[j].Group.Count == 0) lastHigh = j;
                        else lastHigh = j + 1;
                    }
                    for (k = lastHigh; k < sequence.Count; k++)
                    {
                        // make sure item is low mps and can be moved back
                        if (sequence[k].SuperGroup.Mps <= maxMps && MinTime(k, j) <= tjj + jT) break;
                        // everything we skip will have to be pushed so make sure there is space
                        maxPush = Math.Min(maxPush, sequence[k].MaxTime - tkk);
                        tkk += sequence[k].Duration;
                    }
                    if (k < sequence.Count && maxPush > 0)
                    {
                        int kk = k; double kT = 0;
                        double currentPush = 0;
                        do
                        {
                            double nextT = Math.Min(jT, sequence[kk].Duration - kT);
                            double mpsdiff = sequence[jj].Mps - sequence[kk].Mps;
                            if (mana - mpsdiff * nextT <= minMana)
                            {
                                nextT = (mana - minMana) / mpsdiff;
                            }
                            if (overflowLimit + sequence[kk].Mps * nextT < 0)
                            {
                                nextT = -overflowLimit / sequence[kk].Mps;
                            }
                            if (currentPush + nextT > maxPush)
                            {
                                nextT = maxPush - currentPush;
                            }
                            mana -= mpsdiff * nextT;
                            overflowLimit += sequence[kk].Mps * nextT;
                            currentPush += nextT;
                            if (extraMode) extraMana += sequence[kk].Mps * nextT;
                            jT -= nextT;
                            if (jT <= 0)
                            {
                                SequenceGroup currentSuper = sequence[jj].SuperGroup;
                                jj--;
                                if (jj >= 0)
                                {
                                    jT += sequence[jj].Duration;
                                    tjj -= sequence[jj].Duration;
                                    maxPush = Math.Min(maxPush, sequence[jj].MaxTime - tjj);
                                    // seems like this never really applies
                                    // if we don't have enough mana for long sequence then pushing it out
                                    // won't create new opportunities for mana consumables
                                    //if (extraMode && sequence[jj].SuperGroup != currentSuper) extraMana = double.NegativeInfinity;
                                }
                                else
                                {
                                    if (extraMode) extraMana = double.NegativeInfinity;
                                }
                            }
                            kT += nextT;
                            if (kT >= sequence[kk].Duration - 0.000001)
                            {
                                kT -= sequence[kk].Duration;
                                kk++;
                            }
                            if ((mana <= maxMana && (extraMana <= 0 || mana <= minMana)) || overflowLimit <= 0)
                            {
                                break;
                            }
                        } while (jj >= i && kk < sequence.Count && MinTime(k, jj) <= tjj + jT && MinTime(kk, jj) <= tjj + jT + currentPush - kT && currentPush < maxPush);
                        // [i..[k..||jj..j]XXXkk.]
                        if (kT > 0)
                        {
                            SplitAt(kk, kT);
                            kk++;
                        }
                        // if k has negative mps, then just placing it at the end won't work
                        // we're breaking max mana constraint, this means we're most likely oom
                        // placing -mps at the end won't help us get from negative
                        // we have to place it before it gets to that point
                        // when we're filling with extra mana this does not apply
                        List<SequenceItem> copy = sequence.GetRange(k, kk - k);
						double totalmana = 0;
						foreach (SequenceItem item in copy)
							totalmana += item.Mps * item.Duration;
                        while (jj >= i && currentPush <= maxPush && !extraMode && totalmana < 0)
                        {
                            if (sequence[jj].Mps * jT > -totalmana)
                            {
                                jT += totalmana / sequence[jj].Mps;
                                break;
                            }
                            totalmana += sequence[jj].Mps * jT;
                            jj--;
                            jT = sequence[jj].Duration;
                        }
                        if (jT >= sequence[jj].Duration)
                        {
                            jT -= sequence[jj].Duration;
                            jj++;
                        }
                        // don't split into supergroup, make a clean cut
                        if (jT > 0 && sequence[jj].Group.Count > 0 && currentPush <= maxPush) // if we're mid super group, and we can push the end we can push the whole super group
                        {
                            // move to start of super group
                            jT = 0;
                            SequenceGroup super = sequence[jj].SuperGroup;
                            while (jj >= 0 && sequence[jj].SuperGroup == super)
                            {
                                jj--;
                            }
                            jj++;
                        }
                        // final split and reinsert
                        if (jj >= i)
                        {
                            sequence.RemoveRange(k, kk - k);
                            if (jT > 0)
                            {
                                SplitAt(jj, jT);
                                jj++;
                                kk++;
                            }
                            sequence.InsertRange(jj, copy);
                        }
                        ComputeTimestamps();
                    }
                }
                else if (mana < minMana)
                {
                    // no high burn sequence is available yet
                    // take first super group with enough burn and place it as soon as possible
                    tt = T;
                    lastSuper = null;
                    for (a = i; a < sequence.Count; a++)
                    {
                        double d = sequence[a].Duration;
                        if (sequence[a].Index == 3 || sequence[a].Index == 4) d = 0;
                        if (lastSuper != sequence[a].SuperGroup)
                        {
                            lastSuper = sequence[a].SuperGroup;
                            double minLastSuper = MinTime(lastSuper, a - 1);
                            if (tt + lastSuper.Duration > targetTime && lastSuper.Mps > minMps && tt > T && tt > minLastSuper + 0.000001)
                            {
                                // compute buffer of items that can be moved way back
                                double buffer = 0;
                                int b;
                                for (b = i; b < a; b++)
                                {
                                    if (sequence[b].MaxTime >= tt + lastSuper.Duration) buffer += sequence[b].Duration;
                                }
                                // place it at min time, but move forward over non-splittable super groups
                                // if move breaks constraint on some other group cancel
                                int lastSafeInsert = a;
                                double t3 = tt;
                                bool updated = false;
                                for (b = a - 1; b >= i; b--)
                                {
                                    if (b == 0 || sequence[b].SuperGroup != sequence[b - 1].SuperGroup) lastSafeInsert = b;
                                    t3 -= sequence[b].Duration;
                                    if (t3 <= minLastSuper + 0.000001)
                                    {
                                        // possible insert point
                                        if (sequence[b].Group.Count == 0)
                                        {
                                            if (sequence[b].MaxTime >= minLastSuper + lastSuper.Duration - buffer)
                                            {
                                                // splittable, make a split at max time
                                                if (MinTime(lastSuper, a - 1) > t3)
                                                {
                                                    SplitAt(b, minLastSuper - t3);
                                                    a++;
                                                    b++;
                                                }
                                                sequence.InsertRange(b, RemoveSuperGroup(a));
                                                ComputeTimestamps();
                                                updated = true;
                                            }
                                        }
                                        else
                                        {
                                            // we are in super group, use last safe insert
                                            if (lastSafeInsert < a)
                                            {
                                                sequence.InsertRange(lastSafeInsert, RemoveSuperGroup(a));
                                                ComputeTimestamps();
                                                updated = true;
                                            }
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        // make sure we wouldn't push it out of max
                                        if (sequence[b].MaxTime < t3 + lastSuper.Duration - buffer)
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (updated)
                                {
                                    t = T;
                                    mana = Mana;
                                    goto Retry;
                                }
                            }
                        }
                        tt += d;
                    }
                }
            }

            int IComparer<SequenceItem>.Compare(SequenceItem x, SequenceItem y)
            {
                if (sortByMps)
                {
                    if (preserveCooldowns)
                    {
                        if (x.SuperGroup == y.SuperGroup)
                        {
                            return x.SuperIndex.CompareTo(y.SuperIndex);
                        }
                        else
                        {
                            bool xcritical = x.SuperGroup.MaxTime <= sortStartTime;
                            bool ycritical = y.SuperGroup.MaxTime <= sortStartTime;
                            int compare = ycritical.CompareTo(xcritical);
                            if (compare != 0) return compare;
                            if (x.Group.Count == 0 || y.Group.Count == 0)
                            {
                                // only sort by mps for normal casting without cooldowns
                                // if both are in super groups we have to sort in the way as prescribed by SortGroups solution
                                compare = CompareByMps(x.SuperGroup, y.SuperGroup);
                                if (compare != 0) return compare;
                            }
                            // if two super groups have same mps make sure to group by super group
                            return x.SuperGroup.MinTime.CompareTo(y.SuperGroup.MinTime); // is min time unique???
                        }
                    }
                    else
                    {
                        return CompareByMps(x, y);
                    }
                }
                return 0;
            }

            private int CompareByMps(SequenceItem x, SequenceItem y)
            {
                return CompareMps(x.Mps, y.Mps, sortMinMps, sortMaxMps);
            }

            private int CompareByMps(SequenceGroup x, SequenceGroup y)
            {
                return CompareMps(x.Mps, y.Mps, sortMinMps, sortMaxMps);
            }

            public static int CompareMps(double x, double y, double minMps, double maxMps)
            {
                int xrange, yrange;
                if (x < maxMps && x >= minMps) xrange = 0;
                else if (x >= maxMps) xrange = 1;
                else xrange = 2;
                if (y < maxMps && y >= minMps) yrange = 0;
                else if (y >= maxMps) yrange = 1;
                else yrange = 2;
                int compare = xrange.CompareTo(yrange);
                if (compare != 0) return compare;
                if (xrange == 0 || xrange == 2) return y.CompareTo(x);
                else return x.CompareTo(y);
            }

            private void SplitAt(double time)
            {
                double t = 0;
                for (int i = 0; i < sequence.Count; i++)
                {
                    double d = sequence[i].Duration;
                    if (sequence[i].Index == 3 || sequence[i].Index == 4) d = 0;
                    if (t + d > time)
                    {
                        if (time > t)
                        {
                            SplitAt(i, time - t);
                        }
                        return;
                    }
                    t += d;
                }
            }

            private List<SequenceItem> RemoveSuperGroup(int index)
            {
                int i;
                SequenceGroup super = sequence[index].SuperGroup;
                for (i = index; i < sequence.Count; i++)
                {
                    if (sequence[i].SuperGroup != super) break;
                }
                List<SequenceItem> copy = sequence.GetRange(index, i - index);
                sequence.RemoveRange(index, i - index);
                return copy;
            }

            private void SplitAt(int index, double time)
            {
                if (time > 0 && time < sequence[index].Duration)
                {
                    double d = sequence[index].Duration;
                    sequence.Insert(index, sequence[index].Clone());
                    sequence[index].Duration = time;
                    sequence[index + 1].Duration = d - time;
                }
            }

            private SequenceItem Split(SequenceItem item, double time)
            {
                int index = sequence.IndexOf(item);
                SplitAt(index, time);
                return sequence[index];
            }

            private double RemoveIndex(int index)
            {
                double ret = 0;
                for (int i = 0; i < sequence.Count; i++)
                {
                    if (sequence[i].Index == index)
                    {
                        ret += sequence[i].Duration;
                        sequence.RemoveAt(i);
                        i--;
                    }
                }
                return ret;
            }

            private SequenceItem InsertIndex(int index, double duration, double time)
            {
                SequenceItem item = new SequenceItem(index, duration);
                InsertIndex(item, time);
                return item;
            }

            private void InsertIndex(SequenceItem item, double time)
            {
                double t = 0;
                for (int i = 0; i < sequence.Count; i++)
                {
                    double d = sequence[i].Duration;
                    if (sequence[i].Index == 3 || sequence[i].Index == 4) d = 0;
                    if (t + d > time)
                    {
                        if (time <= t + 0.000001)
                        {
                            sequence.Insert(i, item);
                            return;
                        }
                        else
                        {
                            sequence.Insert(i, sequence[i].Clone());
                            sequence[i].Duration = time - t;
                            sequence[i + 1].Duration = d - (time - t);
                            sequence.Insert(i + 1, item);
                            return;
                        }
                    }
                    t += d;
                }
            }

            public enum EvaluationMode
            {
                Unexplained,
                ManaBelow,
                ManaAtTime,
                CooldownBreak,
            }

            private Stats BasicStats
            {
                get
                {
                    return SequenceItem.Calculations.BasicStats;
                }
            }

            private double EvocationRegen
            {
                get
                {
                    return SequenceItem.Calculations.EvocationRegen;
                }
            }

            private double EvocationDuration
            {
                get
                {
                    return SequenceItem.Calculations.EvocationDuration;
                }
            }

            private double FightDuration
            {
                get
                {
                    return SequenceItem.Calculations.FightDuration;
                }
            }

            private double RealFightDuration
            {
                get
                {
                    return SequenceItem.Calculations.RealFightDuration;
                }
            }

            private double ManaPotionTime
            {
                get
                {
                    return SequenceItem.Calculations.ManaPotionTime;
                }
            }

            private double Trinket1Duration
            {
                get
                {
                    return SequenceItem.Calculations.Trinket1Duration;
                }
            }

            private double Trinket2Duration
            {
                get
                {
                    return SequenceItem.Calculations.Trinket2Duration;
                }
            }

            public List<SequenceGroup> GroupTrinket1()
            {
                List<SequenceItem> list = new List<SequenceItem>();
                foreach (SequenceItem item in sequence)
                {
                    if (item.Stats.Trinket1) list.Add(item);
                }
                return GroupCooldown(list, Trinket1Duration, SequenceItem.Calculations.Trinket1Cooldown);
            }

            public List<SequenceGroup> GroupTrinket2()
            {
                List<SequenceItem> list = new List<SequenceItem>();
                foreach (SequenceItem item in sequence)
                {
                    if (item.Stats.Trinket2) list.Add(item);
                }
                return GroupCooldown(list, Trinket2Duration, SequenceItem.Calculations.Trinket2Cooldown);
            }

            public void ConstrainTrinkets(List<SequenceGroup> t1, List<SequenceGroup> t2)
            {
                if (t1.Count == 0 || t2.Count == 0) return;
                foreach (SequenceGroup g1 in t1)
                {
                    foreach (SequenceGroup g2 in t2)
                    {
                        g1.Constraint.Add(new CooldownConstraint() { Group = g2, Cooldown = g2.Duration });
                        g2.Constraint.Add(new CooldownConstraint() { Group = g1, Cooldown = g2.Duration });
                    }
                }
            }

            public void GroupCombustion()
            {
                List<SequenceItem> list = new List<SequenceItem>();
                foreach (SequenceItem item in sequence)
                {
                    if (item.Stats.Combustion) list.Add(item);
                }
                GroupCooldown(list, 0, 180, true, false);
            }

            public void GroupArcanePower()
            {
                List<SequenceItem> list = new List<SequenceItem>();
                foreach (SequenceItem item in sequence)
                {
                    if (item.Stats.ArcanePower) list.Add(item);
                }
                GroupCooldown(list, 15, 180);
            }

            public void GroupIcyVeins()
            {
                List<SequenceItem> list = new List<SequenceItem>();
                foreach (SequenceItem item in sequence)
                {
                    if (item.Stats.IcyVeins) list.Add(item);
                }
                GroupCooldown(list, 20, 180, false, SequenceItem.Calculations.CalculationOptions.ColdSnap == 1);
            }

            public void GroupFlameCap()
            {
                List<SequenceItem> list = new List<SequenceItem>();
                foreach (SequenceItem item in sequence)
                {
                    if (item.Stats.FlameCap) list.Add(item);
                }
                GroupCooldown(list, 60, 180);
            }

            public void GroupDestructionPotion()
            {
                List<SequenceItem> list = new List<SequenceItem>();
                foreach (SequenceItem item in sequence)
                {
                    if (item.Stats.DestructionPotion) list.Add(item);
                }
                GroupCooldown(list, 15, 120);
            }

            public void GroupDrumsOfBattle()
            {
                List<SequenceItem> list = new List<SequenceItem>();
                foreach (SequenceItem item in sequence)
                {
                    if (item.Stats.DrumsOfBattle) list.Add(item);
                }
                List<SequenceGroup> groups = GroupCooldown(list, 30 - SequenceItem.Calculations.GlobalCooldown, 120);
                double drums = RemoveIndex(5);
                for (int i = 0; i < groups.Count; i++)
                {
                    //double drum = Math.Min(drums, SequenceItem.Calculations.GlobalCooldown);
                    SequenceItem item = InsertIndex(5, drums / groups.Count, 0);
                    item.Group.Add(groups[i]);
                    groups[i].Add(item);
                    //drums -= drum;
                }
            }

            private List<SequenceGroup> GroupCooldown(List<SequenceItem> cooldownItems, double maxDuration, double cooldown)
            {
                return GroupCooldown(cooldownItems, maxDuration, cooldown, false, false);
            }

            private bool ItemsCompatible(List<SequenceItem> item1, List<SequenceItem> item2, double maxCooldown)
            {
                return ConstraintsCompatible(GetAllGroups(item1), GetAllGroups(item2), maxCooldown);
            }

            private bool ItemsCompatible(List<SequenceItem> item1, SequenceItem item2, double maxCooldown)
            {
                return ConstraintsCompatible(GetAllGroups(item1), item2.Group, maxCooldown);
            }

            private List<SequenceGroup> GetAllGroups(List<SequenceItem> items)
            {
                //return items.Aggregate<SequenceItem, IEnumerable<SequenceGroup>>(new List<SequenceGroup>(), (list, item) => list.Union(item.Group)).ToList();
                List<SequenceGroup> result = new List<SequenceGroup>();
                foreach (SequenceItem item in items)
                {
                    foreach (SequenceGroup group in item.Group)
                    {
                        if (!result.Contains(group)) result.Add(group);
                    }
                }
                return result;
            }

            private bool ConstraintsCompatible(List<SequenceGroup> group1, List<SequenceGroup> group2, double maxCooldown)
            {
                foreach (SequenceGroup group in group1)
                {
                    foreach (CooldownConstraint constraint in group.Constraint)
                    {
                        if (constraint.Cooldown > maxCooldown && group2.Contains(constraint.Group))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

            private List<SequenceGroup> GroupCooldown(List<SequenceItem> cooldownItems, double maxDuration, double cooldown, bool combustionMode, bool coldSnapMode)
            {
                List<SequenceGroup> existingGroup = new List<SequenceGroup>();
                List<SequenceGroup> unresolvedGroup = new List<SequenceGroup>();
                foreach (SequenceItem item in cooldownItems)
                {
                    foreach (SequenceGroup group in item.Group)
                    {
                        if (!existingGroup.Contains(group)) existingGroup.Add(group);
                    }
                }
                List<List<SequenceItem>> chains = new List<List<SequenceItem>>();
                foreach (SequenceGroup group in existingGroup)
                {
                    // if group duration is less than cooldown then all must be contiguous
                    if (group.Duration <= cooldown)
                    {
                        // check if any of the items is already an existing chain
                        List<SequenceItem> chain = null;
                        foreach (SequenceItem item in group.Item)
                        {
                            if (cooldownItems.Contains(item))
                            {
                                for (int i = 0; i < chains.Count; i++)
                                {
                                    List<SequenceItem> c = chains[i];
                                    if (c.Contains(item))
                                    {
                                        if (chain != null && chain != c)
                                        {
                                            // merge chains
                                            chain.AddRange(c);
                                            chains.RemoveAt(i);
                                            i--;
                                        }
                                        else
                                        {
                                            chain = c;
                                        }
                                    }
                                }
                            }
                        }
                        // add new items to chain
                        if (chain == null)
                        {
                            chain = new List<SequenceItem>();
                            chains.Add(chain);
                        }
                        foreach (SequenceItem item in group.Item)
                        {
                            if (cooldownItems.Contains(item))
                            {
                                if (!chain.Contains(item)) chain.Add(item);
                            }
                        }
                    }
                    else
                    {
                        unresolvedGroup.Add(group);
                    }
                }
                List<SequenceItem> unchained = new List<SequenceItem>();
                double totalDuration = 0;
                double combustionCount = 0;
                foreach (SequenceItem item in cooldownItems)
                {
                    totalDuration += item.Duration;
                    if (combustionMode) combustionCount += item.Duration / (item.Stats.CombustionDuration * item.Spell.CastTime / item.Spell.CastProcs);
                    bool chained = false;
                    foreach (List<SequenceItem> chain in chains)
                    {
                        if (chain.Contains(item))
                        {
                            chained = true;
                            break;
                        }
                    }
                    if (!chained) unchained.Add(item);
                }
                // at this point we have a number of chains and remaining unchained items
                // chains cannot be split, unchained items can
                int maxChains = 0;
                if (combustionMode)
                {
                    maxChains = (int)Math.Ceiling(combustionCount - 0.000001);
                }
                else
                {
                    maxChains = (int)Math.Ceiling(totalDuration / maxDuration);
                }
                List<SequenceGroup> partialGroups = new List<SequenceGroup>();
                foreach (List<SequenceItem> chain in chains)
                {
                    SequenceGroup group = new SequenceGroup();
                    group.AddRange(chain);
                    partialGroups.Add(group);
                }
                partialGroups.Sort((x, y) => y.Duration.CompareTo(x.Duration));
                if (partialGroups.Count < maxChains)
                {
                    int addCount = maxChains - partialGroups.Count;
                    for (int i = 0; i < addCount; i++)
                    {
                        partialGroups.Add(new SequenceGroup());
                    }
                }

                for (int i = 0; i < partialGroups.Count; i++)
                {
                    SequenceGroup group = partialGroups[i];
                    double gap = 0;
                    if (combustionMode)
                    {
						double tempSum = 0;
						foreach (SequenceItem item in group.Item)
							tempSum += item.Duration / (item.Stats.CombustionDuration * item.Spell.CastTime / item.Spell.CastProcs);
                        gap = 1 - tempSum;
                    }
                    else
                    {
                        gap = maxDuration - group.Duration;
                    }
                    if (gap > 0.000001)
                    {
                        int maxSegDistance = (int)Math.Ceiling(maxDuration / 30) + 1;
                        for (int j = i + 1; j < partialGroups.Count; j++)
                        {
                            SequenceGroup subgroup = partialGroups[j];
                            double gapReduction = 0;
                            if (combustionMode)
                            {
								double tempSum = 0;
								foreach (SequenceItem item in subgroup.Item)
									tempSum += item.Duration / (item.Stats.CombustionDuration * item.Spell.CastTime / item.Spell.CastProcs);
                                gapReduction = tempSum;
                            }
                            else
                            {
                                gapReduction = subgroup.Duration;
                            }
                            if (subgroup.Duration > 0 && gapReduction <= gap && Math.Abs(group.Segment - subgroup.Segment) < maxSegDistance && ItemsCompatible(group.Item, subgroup.Item, 0))
                            {
                                gap -= gapReduction;
                                group.AddRange(subgroup.Item);
                                partialGroups.RemoveAt(j);
                                j--;
                            }
                        }
                        for (int j = 0; j < unchained.Count; j++)
                        {
                            SequenceItem item = unchained[j];
                            if (group.Segment == -1 || Math.Abs(group.Segment - item.Segment) < maxSegDistance)
                            {
                                double gapReduction = 0;
                                if (combustionMode)
                                {
                                    gapReduction = item.Duration / (item.Stats.CombustionDuration * item.Spell.CastTime / item.Spell.CastProcs);
                                }
                                else
                                {
                                    gapReduction = item.Duration;
                                }
                                if (gapReduction <= gap + 0.000001)
                                {
                                    gap -= gapReduction;
                                    group.Add(item);
                                    unchained.RemoveAt(j);
                                    j--;
                                }
                                else
                                {
                                    double split = 0;
                                    if (combustionMode)
                                    {
                                        split = gap * (item.Stats.CombustionDuration * item.Spell.CastTime / item.Spell.CastProcs);
                                    }
                                    else
                                    {
                                        split = gap;
                                    }
                                    group.Add(Split(item, split));
                                    gap = 0;
                                    break;
                                }
                            }
                        }
                    }
                }

                // finalize groups
                for (int i = 0; i < partialGroups.Count; i++)
                {
                    SequenceGroup group = partialGroups[i];
                    if (group.Duration > 0)
                    {
                        foreach (SequenceItem item in group.Item)
                        {
                            item.Group.Add(group);
                        }
                    }
                    for (int j = 0; j < partialGroups.Count; j++)
                    {
                        if (i != j)
                        {
                            group.Constraint.Add(new CooldownConstraint() { Cooldown = cooldown, Duration = maxDuration, Group = partialGroups[j], ColdSnap = coldSnapMode });
                        }
                    }
                }
                partialGroups.RemoveAll(group => group.Duration == 0);
                return partialGroups;
            }

            public void GroupHeroism()
            {
                SequenceGroup group = new SequenceGroup();
                foreach (SequenceItem item in sequence)
                {
                    if (item.Stats.Heroism)
                    {
                        group.Add(item);
                        item.Group.Add(group);
                    }
                }
            }

            private double moltenFuryStart = 0;
            public void GroupMoltenFury()
            {
                SequenceGroup group = new SequenceGroup();
                foreach (SequenceItem item in sequence)
                {
                    if (item.Stats.MoltenFury)
                    {
                        group.Add(item);
                        item.Group.Add(group);
                    }
                }
                moltenFuryStart = SequenceItem.Calculations.RealFightDuration - group.Duration;
                group.MinTime = moltenFuryStart;
            }

            List<SequenceItem> compactItems;
            List<double> compactTime;
            double compactTotalTime;
            int compactGroupSplits;

            public void SortGroups()
            {
				List<SequenceItem> groupedItems = new List<SequenceItem>();
				foreach(SequenceItem item in sequence)
					if (item.Group.Count > 0)
						groupedItems.Add(item);

				compactTotalTime = double.PositiveInfinity;
                compactGroupSplits = int.MaxValue;
                //SortGroups_AddRemainingItems(new List<SequenceItem>(), new List<double>(), groupedItems);
                SortGroups_Compute(groupedItems);
                if (compactItems == null) return;

                for (int i = 0; i < compactItems.Count; i++)
                {
                    compactItems[i].MinTime = compactTime[i];
                }

                // compute max time
                double time = RealFightDuration;
                for (int i = compactItems.Count - 1; i >= 0; i--)
                {
                    SequenceItem item = compactItems[i];
                    time = Math.Min(time - item.Duration, item.MaxTime);
                    // check constraints
                    foreach (SequenceGroup group in item.Group)
                    {
                        // only compute max for first item in group
                        if (i == 0 || !compactItems[i - 1].Group.Contains(group))
                        {
                            foreach (CooldownConstraint constraint in group.Constraint)
                            {
                                for (int j = i + 1; j < compactItems.Count; j++)
                                {
                                    // skip cooldown constraints that are coldsnapped in the solution
                                    if (compactItems[j].Group.Contains(constraint.Group) && (!constraint.ColdSnap || (compactItems[j].MinTime - item.MinTime >= 180 - 0.000001)))
                                    {
                                        time = Math.Min(time, compactItems[j].MaxTime - constraint.Cooldown);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    item.MaxTime = time;
                    double t = time;
                    for (int j = i + 1; j < compactItems.Count && compactItems[j].SuperIndex == compactItems[j - 1].SuperIndex; j++)
                    {
                        t += compactItems[j - 1].Duration;
                        compactItems[j].MaxTime = t;
                    }
                }


                sequence.Sort((x, y) => {
                    bool xgrouped = x.Group.Count > 0;
                    bool ygrouped = y.Group.Count > 0;
                    int compare = xgrouped.CompareTo(ygrouped);
                    if (compare != 0) return compare;
                    return x.MinTime.CompareTo(y.MinTime);
                });
            }

            private int SortGroups_Compare(SequenceItem x, SequenceItem y, List<SequenceGroup> tail)
            {
                bool xsingletail = x.Tail.Count > 0;
                bool ysingletail = y.Tail.Count > 0;
                int compare = ysingletail.CompareTo(xsingletail);
                if (compare != 0) return compare;
                int xintersect = (tail == null) ? 0 : Rawr.Mage.ListUtils.Intersect<SequenceGroup>(x.Group, tail).Count;
                int yintersect = (tail == null) ? 0 : Rawr.Mage.ListUtils.Intersect<SequenceGroup>(y.Group, tail).Count;
                return yintersect.CompareTo(xintersect);
            }

            private int HexCount(int hex)
            {
                int count = 0;
                while (hex > 0)
                {
                    count += (hex & 1);
                    hex >>= 1;
                }
                return count;
            }

            private void SortGroups_Compute(List<SequenceItem> itemList)
            {
                int N = itemList.Count;
                if (N == 0) return;
                List<double> constructionTime = new List<double>();
                List<double>[] constructionTimeHistory = new List<double>[N];
                bool[] used = new bool[N];
                int[] index = new int[N];
                int[] coldsnap = new int[N];
                int[] maxIntersect = new int[N];
                for (int j = 0; j < N; j++) itemList[j].SuperIndex = -1;
                int super = -1;
                for (int j = 0; j < N; j++)
                {
                    if (itemList[j].SuperIndex == -1)
                    {
                        super++;
                        itemList[j].SuperIndex = super;
                        List<SequenceItem> superList = new List<SequenceItem>();
                        superList.Add(itemList[j]);
                        bool more = false;
                        do
                        {
                            more = false;
                            for (int k = j + 1; k < N; k++)
                            {
                                if (itemList[k].SuperIndex == -1)
                                {
                                    foreach (SequenceItem item in superList)
                                    {
                                        if (Rawr.Mage.ListUtils.Intersect<SequenceGroup>(item.Group, itemList[k].Group).Count > 0)
                                        {
                                            itemList[k].SuperIndex = super;
                                            superList.Add(itemList[k]);
                                            more = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        } while (more);
                        List<SequenceGroup> superGroups = new List<SequenceGroup>();
                        foreach (SequenceItem item in superList)
                        {
                            int hex = 0;
                            foreach (SequenceGroup group in item.Group)
                            {
                                if (!superGroups.Contains(group)) superGroups.Add(group);
                                hex |= (1 << superGroups.IndexOf(group));
                            }
                            item.CooldownHex = hex;
                        }
                    }
                }
                List<SequenceGroup> groupList = GetAllGroups(itemList);
                super++;
                int[] superLeft = new int[super];
                for (int j = 0; j < N; j++)
                {
                    superLeft[itemList[j].SuperIndex]++;
                }
                int i = 0;
                index[0] = 0;
                constructionTimeHistory[0] = constructionTime;
                coldsnap[0] = 2;
                do
                {
                    if (i == N)
                    {
                        double time = 0;
                        if (constructionTime.Count > 0) time = constructionTime[constructionTime.Count - 1] + itemList[index[N - 1]].Duration;
                        // compute group splits
                        int groupSplits = 0;
                        foreach (SequenceGroup group in groupList)
                        {
                            int minIndex = N - 1;
                            int maxIndex = 0;
                            foreach (SequenceItem item in group.Item)
                            {
                                if (item.OrderIndex < minIndex) minIndex = item.OrderIndex;
                                if (item.OrderIndex > maxIndex) maxIndex = item.OrderIndex;
                            }
                            groupSplits += (maxIndex - minIndex + 1) - group.Item.Count;
                        }
                        /*bool mf = false;
                        for (int j = 0; j < N; j++)
                        {
                            mf = mf || itemList[index[j]].Stats.MoltenFury;
                        }*/
                        if (groupSplits < compactGroupSplits || (groupSplits == compactGroupSplits && time < compactTotalTime)/* && (!mf || itemList[index[N - 1]].Stats.MoltenFury)*/)
                        {
                            compactGroupSplits = groupSplits;
                            compactTotalTime = time;
                            compactTime = new List<double>(constructionTime);
                            compactItems = new List<SequenceItem>();
                            for (int j = 0; j < N; j++)
                            {
                                compactItems.Add(itemList[index[j]]);
                            }
                        }
                        i--;
                        if (i >= 0)
                        {
                            constructionTime = constructionTimeHistory[i];
                            used[index[i]] = false;
                            superLeft[itemList[index[i]].SuperIndex]++;
                        }
                    }
                    else
                    {
                        coldsnap[i]--;
                        if (coldsnap[i] < 0 || used[index[i]])
                        {
                            coldsnap[i] = 1;
                            do
                            {
                                index[i]++;
                            } while (index[i] < N && used[index[i]]);
                        }
                        if (index[i] == N)
                        {
                            i--;
                            if (i >= 0)
                            {
                                constructionTime = constructionTimeHistory[i];
                                used[index[i]] = false;
                                superLeft[itemList[index[i]].SuperIndex]++;
                            }
                        }
                        else
                        {
                            // check if valid
                            SequenceItem item = itemList[index[i]];
                            if (i == 0 || superLeft[itemList[index[i - 1]].SuperIndex] == 0 || item.SuperIndex == itemList[index[i - 1]].SuperIndex)
                            {
                                int tail = item.CooldownHex;
                                int activeTail = 0;
                                int intersectHex = 0;
                                if (i > 0 && item.SuperIndex == itemList[index[i - 1]].SuperIndex)
                                {
                                    activeTail = itemList[index[i - 1]].CooldownHex;
                                    intersectHex = HexCount(tail & activeTail);
                                    if (intersectHex > maxIntersect[i]) maxIntersect[i] = intersectHex;
                                }
                                if (intersectHex >= maxIntersect[i])
                                {
                                    used[index[i]] = true;
                                    itemList[index[i]].OrderIndex = i;
                                    superLeft[itemList[index[i]].SuperIndex]--;
                                    if (coldsnap[i] == 0)
                                    {
                                        tail = 0;
                                        // skip tests, we already computed all this for coldsnap == 1
                                    }
                                    else
                                    {
                                        for (int j = 0; j < N; j++)
                                        {
                                            if (!used[j] && itemList[j].SuperIndex == item.SuperIndex)
                                            {
                                                if (i > 0 && item.SuperIndex == itemList[index[i - 1]].SuperIndex)
                                                {
                                                    int intersectHexJ = HexCount(itemList[j].CooldownHex & activeTail);
                                                    if (intersectHexJ > intersectHex)
                                                    {
                                                        // anything up to j is not valid, so skip ahead
                                                        used[index[i]] = false;
                                                        superLeft[itemList[index[i]].SuperIndex]++;
                                                        index[i] = j;
                                                        used[j] = true;
                                                        itemList[j].OrderIndex = i;
                                                        superLeft[itemList[index[i]].SuperIndex]--;
                                                        intersectHex = intersectHexJ;
                                                        maxIntersect[i] = intersectHex;
                                                        item = itemList[j];
                                                        tail = item.CooldownHex;
                                                        j = -1;
                                                        continue;
                                                    }
                                                }
                                                int intersect = item.CooldownHex & itemList[j].CooldownHex;
                                                if (intersect > 0)
                                                {
                                                    tail = intersect & tail;
                                                    if (tail == 0) break;
                                                }
                                            }
                                        }
                                    }
                                    if (tail > 0)
                                    {
                                        double time = 0;
                                        if (constructionTime.Count > 0) time = constructionTime[constructionTime.Count - 1] + itemList[index[i - 1]].Duration;
                                        time = Math.Max(time, item.MinTime);
                                        // check constraints
                                        List<int> coldsnapStarts = new List<int>();
                                        foreach (SequenceGroup group in item.Group)
                                        {
                                            foreach (CooldownConstraint constraint in group.Constraint)
                                            {
                                                if (!constraint.ColdSnap)
                                                {
                                                    for (int j = 0; j < i; j++)
                                                    {
                                                        if (itemList[index[j]].Group.Contains(constraint.Group))
                                                        {
                                                            time = Math.Max(time, constructionTime[j] + constraint.Cooldown);
                                                            break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // if we're in group with already placed item then no need to redo all this
                                                    if (i > 0 && itemList[index[i - 1]].Group.Contains(group)) continue;
                                                    int minIndex = i;
                                                    foreach (SequenceItem coldsnapItem in constraint.Group.Item)
                                                    {
                                                        if (coldsnapItem.OrderIndex >= 0 && coldsnapItem.OrderIndex < N && index[coldsnapItem.OrderIndex] < N && itemList[index[coldsnapItem.OrderIndex]] == coldsnapItem && used[index[coldsnapItem.OrderIndex]] && coldsnapItem.OrderIndex < minIndex)
                                                        {
                                                            minIndex = coldsnapItem.OrderIndex;
                                                        }
                                                    }
                                                    if (minIndex < i)
                                                    {
                                                        coldsnapStarts.Add(minIndex);
                                                    }
                                                }
                                            }
                                        }
                                        // we absolutely can't come faster than time
                                        // now check coldsnap constraints
                                        // the constraints should link to all the other icy veins groups
                                        // look at the ones that were placed already and sort them by order index
                                        // if the last one that needed coldsnap is farther than coldsnap cooldown then we can use it again
                                        // if we don't need to use coldsnap anyway then adjust coldsnap to 0
                                        if (coldsnapStarts.Count > 0)
                                        {
                                            // this is only called for first coldsnap item in group
                                            coldsnapStarts.Sort();
                                            int lastColdsnap = -1;
                                            for (int j = 0; j < coldsnapStarts.Count - 1; j++)
                                            {
                                                if (constructionTime[coldsnapStarts[j + 1]] - constructionTime[coldsnapStarts[j]] < 180 - 0.000001)
                                                {
                                                    lastColdsnap = j;
                                                }
                                            }
                                            if (time - constructionTime[coldsnapStarts[coldsnapStarts.Count - 1]] >= 180 - 0.000001)
                                            {
                                                // don't need coldsnap and can start right at time
                                                coldsnap[i] = 0;
                                            }
                                            else if (coldsnap[i] == 1)
                                            {
                                                // use coldsnap
                                                double normalTime = Math.Max(time, constructionTime[coldsnapStarts[coldsnapStarts.Count - 1]] + 180);
                                                double coldsnapTime = 0;
                                                if (lastColdsnap >= 0) coldsnapTime = constructionTime[coldsnapStarts[lastColdsnap]] + 8 * 60 * (1 - 0.1 * SequenceItem.Calculations.CalculationOptions.IceFloes);
                                                if (coldsnapTime >= normalTime)
                                                {
                                                    // coldsnap won't be ready until IV will be back anyway, so we don't actually need it
                                                    coldsnap[i] = 0;
                                                    time = normalTime;
                                                }
                                                else
                                                {
                                                    // go now or when coldsnap is ready
                                                    time = Math.Max(coldsnapTime, time);
                                                }
                                            }
                                            else
                                            {
                                                // we are not allowed to use coldsnap even if we could
                                                // make sure to adjust by coldsnap constraints
                                                time = Math.Max(time, constructionTime[coldsnapStarts[coldsnapStarts.Count - 1]] + 180); 
                                            }
                                        }
                                        else
                                        {
                                            // no coldsnap constraints active
                                            coldsnap[i] = 0;
                                        }
                                        List<double> adjustedConstructionTime = new List<double>(constructionTime);
                                        adjustedConstructionTime.Add(time);
                                        // adjust min time of items in same super group
                                        for (int j = adjustedConstructionTime.Count - 2; j >= 0 && itemList[index[j]].SuperIndex == item.SuperIndex; j--)
                                        {
                                            time -= itemList[index[j]].Duration;
                                            adjustedConstructionTime[j] = time;
                                        }
                                        constructionTimeHistory[i] = constructionTime;
                                        constructionTime = adjustedConstructionTime;
                                        i++;
                                        if (i < N)
                                        {
                                            index[i] = 0;
                                            maxIntersect[i] = 0;
                                            coldsnap[i] = 2;
                                        }
                                    }
                                    else
                                    {
                                        used[index[i]] = false;
                                        superLeft[itemList[index[i]].SuperIndex]++;
                                    }
                                }
                            }
                        }
                    }
                } while (i >= 0);
            }

            private void SortGroups_AddRemainingItems(List<SequenceItem> constructionList, List<double> constructionTime, List<SequenceItem> remainingList)
            {
                if (remainingList.Count == 0)
                {
                    double time = 0;
                    if (constructionTime.Count > 0) time = constructionTime[constructionTime.Count - 1] + constructionList[constructionTime.Count - 1].Duration;
                    if (time < compactTotalTime)
                    {
                        compactTotalTime = time;
                        compactTime = new List<double>(constructionTime);
                        compactItems = new List<SequenceItem>(constructionList);
                    }
                }
                else
                {
                    foreach (SequenceItem item in remainingList)
                    {
                        item.Tail = new List<SequenceGroup>(item.Group);
                    }

                    foreach (SequenceItem item in remainingList)
                    {
                        foreach (SequenceItem tailitem in remainingList)
                        {
                            List<SequenceGroup> intersect = Rawr.Mage.ListUtils.Intersect<SequenceGroup>(item.Group, tailitem.Group);
                            if (intersect.Count > 0)
                            {
                                item.Tail = Rawr.Mage.ListUtils.Intersect<SequenceGroup>(intersect, item.Tail);
                            }
                        }
                    }

                    List<SequenceGroup> tail = (constructionList.Count > 0) ? constructionList[constructionList.Count - 1].Group : null;
                    SequenceItem best = null;
                    foreach (SequenceItem item in remainingList)
                    {
                        if (best == null || SortGroups_Compare(item, best, tail) < 0)
                        {
                            best = item;
                        }
                    }

                    for (int i = 0; i < remainingList.Count; i++)
                    {
                        SequenceItem item = remainingList[i];
                        if (SortGroups_Compare(item, best, tail) == 0)
                        {
                            remainingList.RemoveAt(i);
                            constructionList.Add(item);
                            double time = 0;
                            if (constructionTime.Count > 0) time = constructionTime[constructionTime.Count - 1] + constructionList[constructionTime.Count - 1].Duration;
                            time = Math.Max(time, item.MinTime);
                            // check constraints
                            foreach (SequenceGroup group in item.Group)
                            {
                                foreach (CooldownConstraint constraint in group.Constraint)
                                {
                                    for (int j = 0; j < constructionList.Count; j++)
                                    {
                                        if (constructionList[j].Group.Contains(constraint.Group))
                                        {
                                            time = Math.Max(time, constructionTime[j] + constraint.Cooldown);
                                            break;
                                        }
                                    }
                                }
                            }
                            List<double> adjustedConstructionTime = new List<double>(constructionTime);
                            adjustedConstructionTime.Add(time);
                            // adjust min time of items in same super group
                            for (int j = adjustedConstructionTime.Count - 2; j >= 0 && Rawr.Mage.ListUtils.Intersect<SequenceGroup>(constructionList[j].Group, constructionList[j + 1].Group).Count > 0; j--)
                            {
                                time -= constructionList[j].Duration;
                                adjustedConstructionTime[j] = time;
                            }
                            SortGroups_AddRemainingItems(constructionList, adjustedConstructionTime, remainingList);
                            //constructionTime.RemoveAt(constructionTime.Count - 1);
                            constructionList.RemoveAt(constructionList.Count - 1);
                            remainingList.Insert(i, item);
                        }
                    }
                }
            }

            public void Compact()
            {
                for (int i = 0; i + 1 < sequence.Count; i++)
                {
                    if (sequence[i].Index == sequence[i + 1].Index && sequence[i].Group.Count == 0)
                    {
                        sequence[i].Duration += sequence[i + 1].Duration;
                        sequence.RemoveAt(i + 1);
                        i--;
                    }
                }
            }

            public void RepositionManaConsumption()
            {
                double ghostMana = Math.Max(0, -ManaCheck());
                double fight = RealFightDuration;
                double potTime = RemoveIndex(3);
                double gemTime = RemoveIndex(4);
                double evoTime = RemoveIndex(2);
                float[] gemValue = new float[] { 2400f, 2400f, 2400f, 1100f, 850f };
                float[] gemMaxValue = new float[] { 2460f, 2460f, 2460f, 1127f, 871f };
                int gemCount = 0;
                double time = 0;
                double nextGem = 0;
                double nextPot = 0;
                double nextEvo = 0;
                do
                {
                    double mana = Evaluate(null, EvaluationMode.ManaAtTime, time);
                    double maxMps = double.PositiveInfinity;
                    if (!((potTime > 0 && nextPot == 0) || (gemTime > 0 && nextGem == 0) || (evoTime > 0 && nextEvo == 0)))
                    {
                        double m = mana + ghostMana;
                        for (double _potTime = potTime; _potTime > 0; _potTime -= ManaPotionTime)
                        {
                            m += (1 + BasicStats.BonusManaPotion) * 2400;
                        }
                        int _gemCount = gemCount;
                        for (double _gemTime = gemTime; _gemTime > 0; _gemTime -= ManaPotionTime, _gemCount++)
                        {
                            m += (1 + BasicStats.BonusManaGem) * gemValue[_gemCount];
                        }
                        m += EvocationRegen * evoTime;
                        maxMps = m / (fight - time);
                    }
                    double minMps = double.NegativeInfinity;
                    double targetTime = fight;
                    if (nextGem > time && gemTime > 0)
                    {
                        double m = mana;
                        if (potTime > 0 && nextPot < nextGem) m += (1 + BasicStats.BonusManaPotion) * 2400;
                        if (evoTime > 0 && nextEvo < nextGem) m += EvocationRegen * Math.Min(evoTime, EvocationDuration);
                        double mps = m / (nextGem - time);
                        if (mps < maxMps)
                        {
                            maxMps = mps;
                            targetTime = nextGem;
                        }
                    }
                    if (nextPot > time && potTime > 0)
                    {
                        double m = mana;
                        if (gemTime > 0 && nextGem < nextPot) m += (1 + BasicStats.BonusManaGem) * gemValue[gemCount];
                        if (evoTime > 0 && nextEvo < nextPot) m += EvocationRegen * Math.Min(evoTime, EvocationDuration);
                        double mps = m / (nextPot - time);
                        if (mps < maxMps)
                        {
                            maxMps = mps;
                            targetTime = nextPot;
                        }
                    }
                    if (nextEvo > time && evoTime > 0)
                    {
                        double m = mana;
                        if (potTime > 0 && nextPot < nextEvo) m += (1 + BasicStats.BonusManaPotion) * 2400;
                        if (gemTime > 0 && nextGem < nextEvo) m += (1 + BasicStats.BonusManaGem) * gemValue[gemCount];
                        double mps = m / (nextEvo - time);
                        if (mps < maxMps)
                        {
                            maxMps = mps;
                            targetTime = nextEvo;
                        }
                    }
                    if (potTime > 0 && (nextPot <= nextGem || nextPot == 0) && (nextPot <= nextEvo || nextPot == 0 || evoTime <= 0))
                    {
                        if (nextPot <= time)
                        {
                            minMps = maxMps;
                        }
                        else
                        {
                            targetTime = nextPot;
                            minMps = ((1 + BasicStats.BonusManaPotion) * 3000 - (BasicStats.Mana - mana)) / (targetTime - time);
                        }
                    }
                    else if (gemTime > 0 && nextGem <= nextPot && (nextGem <= nextEvo || nextGem == 0 || evoTime <= 0))
                    {
                        if (nextGem <= time)
                        {
                            minMps = maxMps;
                        }
                        else
                        {
                            targetTime = nextGem;
                            minMps = ((1 + BasicStats.BonusManaGem) * gemMaxValue[gemCount] - (BasicStats.Mana - mana)) / (targetTime - time);
                        }
                    }
                    else if (evoTime > 0 && nextEvo <= nextPot && nextEvo <= nextGem)
                    {
                        if (nextEvo <= time)
                        {
                            minMps = maxMps;
                        }
                        else
                        {
                            targetTime = nextEvo;
                            minMps = (EvocationRegen * Math.Min(evoTime, EvocationDuration) - (BasicStats.Mana - mana)) / (targetTime - time);
                        }
                    }
                    if (potTime <= 0 && gemTime <= 0 && evoTime <= 0)
                    {
                        maxMps = mana / (targetTime - time);
                        minMps = -(BasicStats.Mana - mana) / (targetTime - time);
                    }
                    if (maxMps < minMps) maxMps = minMps; // if we have min mps constraint then at that point we'll be full on mana, whatever max mana has to be handled will have to deal with it later
                    double lastTargetMana = -1;
                    double extraMana = 0;
                Retry:
                    SortByMps(true, minMps, maxMps, time, targetTime, extraMana, mana);
                    // guard against trailing oom
                    double targetmana = Evaluate(null, EvaluationMode.ManaAtTime, targetTime);
                    if (targetmana != lastTargetMana)
                    {
                        double tmana = targetmana;
                        double t = 0;
                        int i;
                        for (i = 0; i < sequence.Count; i++)
                        {
                            double d = sequence[i].Duration;
                            if (sequence[i].Index == 3 || sequence[i].Index == 4) d = 0;
                            if (d > 0 && t + d > targetTime)
                            {
                                break;
                            }
                            t += d;
                        }
                        if (!(i >= sequence.Count || sequence[i].Group.Count == 0 || (targetTime <= t && (i == 0 || sequence[i - 1].SuperGroup != sequence[i].SuperGroup))))
                        {
                            // count mana till end of super group
                            SequenceGroup super = sequence[i].SuperGroup;
                            targetmana -= sequence[i].Mps * (sequence[i].Duration - (targetTime - t));
                            t += sequence[i].Duration;
                            i++;
                            while (i < sequence.Count && sequence[i].SuperGroup == super)
                            {
                                targetmana -= sequence[i].Mps * sequence[i].Duration;
                                t += sequence[i].Duration;
                                i++;
                            }
                            // account for to be used consumables (don't assume evo during super group)
                            double manaUsed = mana;
                            if (potTime > 0 && nextPot < t)
                            {
                                targetmana += (1 + BasicStats.BonusManaPotion) * 2400;
                            }
                            if (gemTime > 0 && nextGem < t)
                            {
                                targetmana += (1 + BasicStats.BonusManaGem) * gemValue[gemCount];
                            }
                            if (t >= fight) targetmana += ghostMana;
                            if (targetmana < 0)
                            {
                                //maxMps = (mana - (tmana - targetmana)) / (targetTime - time);
                                minMps = -(BasicStats.Mana - mana) / (targetTime - time);
                                extraMana = -targetmana;
                                lastTargetMana = tmana;
                                goto Retry;
                            }
                        }
                    }
                    Compact();
                    double gem = nextGem;
                    double pot = nextPot;
                    double evo = nextEvo;
                    if (gemTime > 0) gem = Evaluate(null, EvaluationMode.ManaBelow, BasicStats.Mana - (1 + BasicStats.BonusManaGem) * gemMaxValue[gemCount], Math.Max(time, nextGem), 4);
                    if (potTime > 0) pot = Evaluate(null, EvaluationMode.ManaBelow, BasicStats.Mana - (1 + BasicStats.BonusManaPotion) * 3000, Math.Max(time, nextPot), 3);
                    if (evoTime > 0)
                    {
                        evo = Evaluate(null, EvaluationMode.ManaBelow, BasicStats.Mana - EvocationRegen * Math.Min(evoTime, EvocationDuration), Math.Max(time, nextEvo), 2);
                        double breakpoint = Evaluate(null, EvaluationMode.CooldownBreak, evo);
                        if (breakpoint < fight) evo = breakpoint;
                    }
                    // always use pot & gem before evo, they need tighter packing
                    // always start with pot because pot needs more buffer than gem, unless
                    if (potTime > 0 && (pot <= gem || (nextPot == 0 && pot < gem + 30 && potTime >= gemTime)) && (pot <= evo || nextPot == 0 || evoTime <= 0))
                    {
                        InsertIndex(3, Math.Min(ManaPotionTime, potTime), pot);
                        time = pot;
                        nextPot = pot + 120;
                        potTime -= ManaPotionTime;
                        if (potTime <= 0.000001)
                        {
                            nextPot = fight;
                            potTime = 0.0;
                        }
                    }
                    else if (gemTime > 0 && gem <= pot && (gem <= evo || nextGem == 0 || evoTime <= 0))
                    {
                        InsertIndex(4, Math.Min(ManaPotionTime, gemTime), gem);
                        time = gem;
                        nextGem = gem + 120;
                        gemCount++;
                        gemTime -= ManaPotionTime;
                        if (gemTime <= 0.000001)
                        {
                            nextGem = fight;
                            gemTime = 0.0;
                        }
                    }
                    else if (evoTime > 0 && evo <= pot && evo <= gem)
                    {
                        InsertIndex(2, Math.Min(EvocationDuration, evoTime), evo);
                        time = evo + Math.Min(EvocationDuration, evoTime);
                        nextEvo = evo + 60 * 8;
                        evoTime -= EvocationDuration;
                        if (evoTime <= 0.000001)
                        {
                            evoTime = 0.0;
                            nextEvo = fight;
                        }
                    }
                    else
                    {
                        break;
                    }
                } while (true);
            }

            public double ManaCheck()
            {
                float[] gemValue = new float[] { 2400f, 2400f, 2400f, 1100f, 850f };
                double mana = SequenceItem.Calculations.BasicStats.Mana;
                for (int i = 0; i < sequence.Count; i++)
                {
                    int index = sequence[i].Index;
                    double duration = sequence[i].Duration;
                    double mps = sequence[i].Mps;
                    if (index == 3)
                    {
                        for (double _potTime = duration; _potTime > 0; _potTime -= ManaPotionTime)
                        {
                            mana += (1 + BasicStats.BonusManaPotion) * 2400;
                        }
                    }
                    else if (index == 4)
                    {
                        int _gemCount = 0;
                        for (double _gemTime = duration; _gemTime > 0; _gemTime -= ManaPotionTime, _gemCount++)
                        {
                            mana += (1 + BasicStats.BonusManaGem) * gemValue[_gemCount];
                        }
                    }
                    else if (index == 2)
                    {
                        mana += EvocationRegen * sequence[i].Duration;
                    }
                    else
                    {
                        mana -= mps * duration;
                    }
                }
                return mana;
            }

            public enum ReportMode
            {
                Listing,
                Compact
            }

            public double Evaluate(StringBuilder timing, EvaluationMode mode, params double[] data)
            {
                double time = 0;
                double mana = SequenceItem.Calculations.BasicStats.Mana;
                float[] gemValue = new float[] { 2400f, 2400f, 2400f, 1100f, 850f };

                ReportMode reportMode = ReportMode.Compact;

                bool coldsnap = SequenceItem.Calculations.CalculationOptions.ColdSnap == 1;
                float coldsnapCooldownDuration = 8 * 60 * (1 - 0.1f * SequenceItem.Calculations.CalculationOptions.IceFloes);

                int gemCount = 0;
                double potionCooldown = 0;
                double gemCooldown = 0;
                double trinket1Cooldown = 0;
                double trinket2Cooldown = 0;
                bool heroismUsed = false;
                double evocationCooldown = 0;
                double drumsCooldown = 0;
                double apCooldown = 0;
                double ivCooldown = 0;
                double combustionCooldown = 0;
                double coldsnapCooldown = 0;
                double trinket1time = -1;
                double trinket2time = -1;
                double flameCapTime = -1;
                double drumsTime = -1;
                double destructionTime = -1;
                double combustionTime = -1;
                double moltenFuryTime = -1;
                double heroismTime = -1;
                double apTime = -1;
                double ivTime = -1;
                bool potionWarning = false;
                bool gemWarning = false;
                bool trinket1warning = false;
                bool trinket2warning = false;
                bool apWarning = false;
                bool ivWarning = false;
                bool combustionWarning = false;
                bool drumsWarning = false;
                bool manaWarning = false;
                double combustionLeft = 0;
                double lastIVstart = 0;

                double unexplained = 0;

                if (timing != null) timing.Length = 0;
                if (timing != null) timing.Append("*");

                // for each cooldown compute how much time is unexplainable
                for (int i = 0; i < sequence.Count; i++)
                {
                    int index = sequence[i].Index;
                    double duration = sequence[i].Duration;
                    Spell spell = sequence[i].Spell;
                    CharacterCalculationsMage stats = sequence[i].Stats;
                    double mps = sequence[i].Mps;
                    if (index == 3 || index == 4) duration = 0;
                    double manabefore = mana;
                    bool cooldownContinuation = false;
                    if (!(drumsTime == -1 && flameCapTime == -1 && destructionTime == -1 && trinket1time == -1 && trinket2time == -1 && heroismTime == -1 && moltenFuryTime == -1 && combustionTime == -1 && apTime == -1 && ivTime == -1))
                    {
                        cooldownContinuation = true;
                    }
                    // Mana
                    if (mode == EvaluationMode.ManaBelow)
                    {
                        if (data.Length > 2)
                        {
                            if (data[2] == 3)
                            {
                                if (potionCooldown > 0) data[1] = Math.Max(data[1], time + potionCooldown);
                            }
                            else if (data[2] == 4)
                            {
                                if (gemCooldown > 0) data[1] = Math.Max(data[1], time + gemCooldown);
                            }
                        }
                    }
                    if (mps > 0)
                    {
                        double maxtime = mana / mps;
                        if (duration > maxtime)
                        {
                            // allow some leeway due to rounding errors from LP solver
                            if (!(i == sequence.Count - 1 && mps * duration < mana + 100))
                            {
                                unexplained += duration - maxtime;
                                if (timing != null && !manaWarning) timing.AppendLine("WARNING: Will run out of mana!");
                                manaWarning = true;
                            }
                        }
                        if (mode == EvaluationMode.ManaBelow)
                        {
                            double limit = data[0];
                            double aftertime = data[1];
                            double timetolimit = (mana - limit) / mps;
                            if (time + duration > aftertime)
                            {
                                if (time >= aftertime && mana < limit) return time;
                                if (time + timetolimit >= aftertime && timetolimit < duration) return time + timetolimit;
                                if (timetolimit < duration) return aftertime;
                            }
                        }
                    }
                    else
                    {
                        if (mode == EvaluationMode.ManaBelow)
                        {
                            double limit = data[0];
                            double aftertime = data[1];
                            if (time + duration > aftertime)
                            {
                                if (time >= aftertime && mana < limit) return time;
                                if (mana - mps * (aftertime - time) < limit) return aftertime;
                            }
                        }
                    }
                    if (mode == EvaluationMode.ManaAtTime && duration > 0)
                    {
                        double evalTime = data[0];
                        if (time + duration > evalTime)
                        {
                            return mana - mps * (evalTime - time);
                        }
                    }
                    mana -= mps * duration;
                    // Mana Potion
                    if (index == 3)
                    {
                        if (potionCooldown > 0.000001)
                        {
                            unexplained += sequence[i].Duration;
                            if (timing != null) timing.AppendLine("WARNING: Potion cooldown not ready!");
                        }
                        else
                        {
                            if (sequence[i].Duration > ManaPotionTime)
                            {
                                unexplained += sequence[i].Duration - ManaPotionTime;
                                if (timing != null) timing.AppendLine("WARNING: Potion ammount too big!");
                            }
                            if (timing != null) timing.AppendLine(TimeFormat(time) + ": Mana Potion (" + Math.Round(mana).ToString() + " mana)");
                            mana += (1 + BasicStats.BonusManaPotion) * 2400;
                            potionCooldown = 120;
                            potionWarning = false;
                        }
                    }
                    // Mana Gem
                    if (index == 4)
                    {
                        if (gemCooldown > 0.000001)
                        {
                            unexplained += sequence[i].Duration;
                            if (timing != null) timing.AppendLine("WARNING: Gem cooldown not ready!");
                        }
                        else
                        {
                            if (sequence[i].Duration > ManaPotionTime)
                            {
                                unexplained += sequence[i].Duration - ManaPotionTime;
                                if (timing != null) timing.AppendLine("WARNING: Gem ammount too big!");
                            }
                            if (timing != null) timing.AppendLine(TimeFormat(time) + ": Mana Gem (" + Math.Round(mana).ToString() + " mana)");
                            mana += (1 + BasicStats.BonusManaGem) * gemValue[gemCount];
                            gemCount++;
                            gemCooldown = 120;
                            gemWarning = false;
                        }
                    }
                    // Evocation
                    if (index == 2)
                    {
                        if (evocationCooldown > 0.000001)
                        {
                            unexplained += duration;
                            if (timing != null) timing.AppendLine("WARNING: Evocation cooldown not ready!");
                        }
                        else
                        {
                            if (duration > EvocationDuration)
                            {
                                unexplained += duration - EvocationDuration;
                                if (timing != null) timing.AppendLine("WARNING: Evocation duration too long!");
                            }
                            if (timing != null) timing.AppendLine(TimeFormat(time) + ": Evocation (" + Math.Round(mana).ToString() + " mana)");
                            mana += Math.Min(EvocationDuration, duration) * EvocationRegen;
                            evocationCooldown = 60 * 8;
                        }
                    }
                    if (mana < 0) mana = 0;
                    if (mana > BasicStats.Mana + 0.000001)
                    {
                        if (timing != null) timing.AppendLine("INFO: Mana overflow!");
                        mana = BasicStats.Mana;
                    }
                    if (mana > 0) manaWarning = false;
                    // Drums of Battle
                    if (index == 5)
                    {
                        if (drumsCooldown > 0.000001)
                        {
                            unexplained += duration;
                            if (timing != null && !drumsWarning) timing.AppendLine("WARNING: Drums of Battle cooldown not ready!");
                            drumsWarning = true;
                        }
                        else
                        {
                            //if (timing != null) timing.AppendLine(TimeFormat(time) + ": Drums of Battle (" + Math.Round(manabefore).ToString() + " mana)");
                            drumsCooldown = 120;
                            drumsTime = time;
                            drumsWarning = false;
                        }
                    }
                    else if (drumsTime >= 0)
                    {
                        if (stats != null && stats.DrumsOfBattle)
                        {
                            if (time + duration > drumsTime + 30 + 0.000001)
                            {
                                unexplained += time + duration - drumsTime - 30;
                                if (timing != null) timing.AppendLine("WARNING: Drums of Battle duration too long!");
                            }
                        }
                        else if (duration > 0 && 30 - (time - drumsTime) > 0.000001)
                        {
                            //unexplained += Math.Min(duration, 30 - (time - drumsTime));
                            if (timing != null) timing.AppendLine("INFO: Drums of Battle is still up!");
                        }
                    }
                    else
                    {
                        if (stats != null && stats.DrumsOfBattle)
                        {
                            unexplained += duration;
                            if (timing != null) timing.AppendLine("WARNING: Drums of Battle not activated!");
                        }
                    }
                    // Flame Cap
                    if (flameCapTime >= 0)
                    {
                        if (stats != null && stats.FlameCap)
                        {
                            if (time + duration > flameCapTime + 60 + 0.000001)
                            {
                                unexplained += time + duration - flameCapTime - 60;
                                if (timing != null) timing.AppendLine("WARNING: Flame Cap duration too long!");
                            }
                        }
                        else if (duration > 0 && 60 - (time - flameCapTime) > 0.000001)
                        {
                            //unexplained += Math.Min(duration, 15 - (time - apTime));
                            if (timing != null) timing.AppendLine("INFO: Flame Cap is still up!");
                        }
                    }
                    else
                    {
                        if (stats != null && stats.FlameCap)
                        {
                            if (gemCooldown > 0.000001)
                            {
                                unexplained += duration;
                                if (timing != null && !gemWarning) timing.AppendLine("WARNING: Flame Cap cooldown not ready!");
                                gemWarning = true;
                            }
                            else
                            {
                                if (timing != null && reportMode == ReportMode.Listing) timing.AppendLine(TimeFormat(time) + ": Flame Cap (" + Math.Round(manabefore).ToString() + " mana)");
                                gemCooldown = 180;
                                flameCapTime = time;
                                gemWarning = false;
                            }
                        }
                    }
                    // Destruction Potion
                    if (destructionTime >= 0)
                    {
                        if (stats != null && stats.DestructionPotion)
                        {
                            if (time + duration > destructionTime + 15 + 0.000001)
                            {
                                unexplained += time + duration - destructionTime - 15;
                                if (timing != null) timing.AppendLine("WARNING: Destruction Potion duration too long!");
                            }
                        }
                        else if (duration > 0 && 15 - (time - destructionTime) > 0.000001)
                        {
                            //unexplained += Math.Min(duration, 15 - (time - apTime));
                            if (timing != null) timing.AppendLine("INFO: Destruction Potion is still up!");
                        }
                    }
                    else
                    {
                        if (stats != null && stats.DestructionPotion)
                        {
                            if (potionCooldown > 0.000001)
                            {
                                unexplained += duration;
                                if (timing != null && !potionWarning) timing.AppendLine("WARNING: Destruction Potion cooldown not ready!");
                                potionWarning = true;
                            }
                            else
                            {
                                if (timing != null && reportMode == ReportMode.Listing) timing.AppendLine(TimeFormat(time) + ": Destruction Potion (" + Math.Round(manabefore).ToString() + " mana)");
                                potionCooldown = 120;
                                destructionTime = time;
                                potionWarning = false;
                            }
                        }
                    }
                    // Trinket1
                    if (trinket1time >= 0)
                    {
                        if (stats != null && stats.Trinket1)
                        {
                            if (time + duration > trinket1time + Trinket1Duration + 0.000001)
                            {
                                unexplained += time + duration - trinket1time - Trinket1Duration;
                                if (timing != null) timing.AppendLine("WARNING: " + SequenceItem.Calculations.Trinket1Name + " duration too long!");
                            }
                        }
                        else if (duration > 0 && Trinket1Duration - (time - trinket1time) > 0.000001)
                        {
                            //unexplained += Math.Min(duration, Trinket1Duration - (time - trinket1time));
                            if (timing != null) timing.AppendLine("INFO: " + SequenceItem.Calculations.Trinket1Name + " is still up!");
                        }
                    }
                    else
                    {
                        if (stats != null && stats.Trinket1)
                        {
                            if (trinket1Cooldown > 0.000001)
                            {
                                unexplained += duration;
                                if (timing != null && !trinket1warning) timing.AppendLine("WARNING: " + SequenceItem.Calculations.Trinket1Name + " cooldown not ready!");
                                trinket1warning = true;
                            }
                            else
                            {
                                if (timing != null && reportMode == ReportMode.Listing) timing.AppendLine(TimeFormat(time) + ": " + SequenceItem.Calculations.Trinket1Name + " (" + Math.Round(manabefore).ToString() + " mana)");
                                trinket1Cooldown = SequenceItem.Calculations.Trinket1Cooldown;
                                trinket1time = time;
                                trinket1warning = false;
                            }
                        }
                    }
                    // Trinket2
                    if (trinket2time >= 0)
                    {
                        if (stats != null && stats.Trinket2)
                        {
                            if (time + duration > trinket2time + Trinket2Duration + 0.000001)
                            {
                                unexplained += time + duration - trinket2time - Trinket2Duration;
                                if (timing != null) timing.AppendLine("WARNING: " + SequenceItem.Calculations.Trinket2Name + " duration too long!");
                            }
                        }
                        else if (duration > 0 && Trinket2Duration - (time - trinket2time) > 0.000001)
                        {
                            //unexplained += Math.Min(duration, Trinket2Duration - (time - trinket2time));
                            if (timing != null) timing.AppendLine("INFO: " + SequenceItem.Calculations.Trinket2Name + " is still up!");
                        }
                    }
                    else
                    {
                        if (stats != null && stats.Trinket2)
                        {
                            if (trinket2Cooldown > 0.000001)
                            {
                                unexplained += duration;
                                if (timing != null && !trinket2warning) timing.AppendLine("WARNING: " + SequenceItem.Calculations.Trinket2Name + " cooldown not ready!");
                                trinket2warning = true;
                            }
                            else
                            {
                                if (timing != null && reportMode == ReportMode.Listing) timing.AppendLine(TimeFormat(time) + ": " + SequenceItem.Calculations.Trinket2Name + " (" + Math.Round(manabefore).ToString() + " mana)");
                                trinket2Cooldown = SequenceItem.Calculations.Trinket2Cooldown;
                                trinket2time = time;
                                trinket2warning = false;
                            }
                        }
                    }
                    // Heroism
                    if (heroismTime >= 0)
                    {
                        if (stats != null && stats.Heroism)
                        {
                            if (time + duration > heroismTime + 40 + 0.000001)
                            {
                                unexplained += time + duration - heroismTime - 40;
                                if (timing != null) timing.AppendLine("WARNING: Heroism duration too long!");
                            }
                        }
                        else if (duration > 0 && 40 - (time - heroismTime) > 0.000001)
                        {
                            //unexplained += Math.Min(duration, 40 - (time - heroismTime));
                            if (timing != null) timing.AppendLine("INFO: Heroism is still up!");
                        }
                    }
                    else
                    {
                        if (stats != null && stats.Heroism)
                        {
                            if (heroismUsed)
                            {
                                unexplained += duration;
                                if (timing != null) timing.AppendLine("WARNING: Heroism cooldown not ready!");
                            }
                            else
                            {
                                if (timing != null && reportMode == ReportMode.Listing) timing.AppendLine(TimeFormat(time) + ": Heroism (" + Math.Round(manabefore).ToString() + " mana)");
                                heroismUsed = true;
                                heroismTime = time;
                            }
                        }
                    }
                    // Molten Fury
                    if (moltenFuryTime >= 0)
                    {
                        if (!(stats != null && stats.MoltenFury) && duration > 0)
                        {
                            //unexplained += duration;
                            if (timing != null) timing.AppendLine("INFO: Molten Fury is still up!");
                        }
                    }
                    else
                    {
                        if (stats != null && stats.MoltenFury)
                        {
                            if (time < moltenFuryStart - 0.000001)
                            {
                                unexplained += Math.Min(duration, moltenFuryStart - time);
                                if (timing != null) timing.AppendLine("WARNING: Molten Fury is not available yet!");
                            }
                            else
                            {
                                if (timing != null && reportMode == ReportMode.Listing) timing.AppendLine(TimeFormat(time) + ": Molten Fury (" + Math.Round(manabefore).ToString() + " mana)");
                                moltenFuryTime = time;
                            }
                        }
                    }
                    // Combustion
                    if (combustionTime >= 0)
                    {
                        if (stats != null && stats.Combustion)
                        {
                            if (duration / (stats.CombustionDuration * spell.CastTime / spell.CastProcs) >= combustionLeft + 0.000001)
                            {
                                unexplained += time + duration - combustionTime - (stats.CombustionDuration * spell.CastTime / spell.CastProcs);
                                if (timing != null) timing.AppendLine("WARNING: Combustion duration too long!");
                            }
                            combustionLeft -= duration / (stats.CombustionDuration * spell.CastTime / spell.CastProcs);
                        }
                        else if (spell != null && duration > 0 && combustionLeft > 0.000001)
                        {
                            //unexplained += Math.Min(duration, 15 - (time - apTime));
                            combustionTime = -1;
                            combustionLeft = 0;
                            if (timing != null) timing.AppendLine("INFO: Combustion is still up!");
                        }
                        else
                        {
                            combustionTime = -1;
                            combustionLeft = 0;
                        }
                    }
                    else
                    {
                        if (stats != null && stats.Combustion)
                        {
                            if (combustionCooldown > 0.000001)
                            {
                                unexplained += duration;
                                if (timing != null && !combustionWarning) timing.AppendLine("WARNING: Combustion cooldown not ready!");
                                combustionWarning = true;
                            }
                            else
                            {
                                if (timing != null && reportMode == ReportMode.Listing) timing.AppendLine(TimeFormat(time) + ": Combustion (" + Math.Round(manabefore).ToString() + " mana)");
                                combustionLeft = 1;
                                combustionCooldown = 180 + (stats.CombustionDuration * spell.CastTime / spell.CastProcs);
                                combustionTime = time;
                                combustionWarning = false;
                                combustionLeft -= duration / (stats.CombustionDuration * spell.CastTime / spell.CastProcs);
                            }
                        }
                    }
                    // Arcane Power
                    if (apTime >= 0)
                    {
                        if (stats != null && stats.ArcanePower)
                        {
                            if (time + duration > apTime + 15 + 0.000001)
                            {
                                unexplained += time + duration - apTime - 15;
                                if (timing != null) timing.AppendLine("WARNING: Arcane Power duration too long!");
                            }
                        }
                        else if (duration > 0 && 15 - (time - apTime) > 0.000001)
                        {
                            //unexplained += Math.Min(duration, 15 - (time - apTime));
                            if (timing != null) timing.AppendLine("INFO: Arcane Power is still up!");
                        }
                    }
                    else
                    {
                        if (stats != null && stats.ArcanePower)
                        {
                            if (apCooldown > 0.000001)
                            {
                                unexplained += duration;
                                if (timing != null && !apWarning) timing.AppendLine("WARNING: Arcane Power cooldown not ready!");
                                apWarning = true;
                            }
                            else
                            {
                                if (timing != null && reportMode == ReportMode.Listing) timing.AppendLine(TimeFormat(time) + ": Arcane Power (" + Math.Round(manabefore).ToString() + " mana)");
                                apCooldown = 180;
                                apTime = time;
                                apWarning = false;
                            }
                        }
                    }
                    // Icy Veins
                    if (ivTime >= 0)
                    {
                        if (stats != null && stats.IcyVeins)
                        {
                            if (time + duration > ivTime + 20 + 0.000001)
                            {
                                if (coldsnapCooldown <= (ivTime + 20 - time) + 0.000001)
                                {
                                    ivTime += 20;
                                    ivCooldown += 20;
                                    coldsnapCooldown = coldsnapCooldownDuration - (ivTime + 20 - time);
                                }
                                if (time + duration > ivTime + 20 + 0.000001)
                                {
                                    unexplained += time + duration - ivTime - 20;
                                    if (timing != null) timing.AppendLine("WARNING: Icy Veins duration too long!");
                                }
                            }
                        }
                        else if (duration > 0 && 20 - (time - ivTime) > 0.000001)
                        {
                            //unexplained += Math.Min(duration, 20 - (time - ivTime));
                            if (timing != null) timing.AppendLine("INFO: Icy Veins is still up!");
                        }
                    }
                    else
                    {
                        if (stats != null && stats.IcyVeins)
                        {
                            if (ivCooldown > 0.000001 && coldsnapCooldown > 0.000001)
                            {
                                unexplained += duration;
                                if (timing != null && !ivWarning) timing.AppendLine("WARNING: Icy Veins cooldown not ready!");
                                ivWarning = true;
                            }
                            else
                            {
                                if (ivCooldown > 0.000001 && coldsnapCooldown <= 0.000001)
                                {
                                    coldsnapCooldown = coldsnapCooldownDuration - (time - Math.Max(time + coldsnapCooldown, lastIVstart));
                                }
                                if (timing != null && reportMode == ReportMode.Listing) timing.AppendLine(TimeFormat(time) + ": Icy Veins (" + Math.Round(manabefore).ToString() + " mana)");
                                ivCooldown = 180;
                                ivTime = time;
                                ivWarning = false;
                                lastIVstart = time;
                            }
                        }
                    }
                    // Move time forward
                    if (mode == EvaluationMode.CooldownBreak)
                    {
                        double aftertime = data[0];
                        if (aftertime >= time && aftertime <= time + duration)
                        {
                            if (drumsTime == -1 && flameCapTime == -1 && destructionTime == -1 && trinket1time == -1 && trinket2time == -1 && heroismTime == -1 && moltenFuryTime == -1 && combustionTime == -1 && apTime == -1 && ivTime == -1)
                            {
                                return aftertime;
                            }
                        }
                        if (time >= aftertime && !cooldownContinuation)
                        {
                            return time;
                        }
                    }
                    string label = null;
                    switch (index)
                    {
                        case 0:
                            label = "Idle Regen";
                            break;
                        case 1:
                            label = "Wand";
                            break;
                        case 2:
                        case 3:
                        case 4:
                            break;
                        case 5:
                            label = "Drums of Battle";
                            break;
                        default:
                            label = spell.Name;
                            break;
                    }
                    if (reportMode == ReportMode.Listing)
                    {
                        if (timing != null && label != null && (i == 0 || (index <= 5 && index != sequence[i - 1].Index) || sequence[i].Stats != sequence[i - 1].Stats || sequence[i].Spell != sequence[i - 1].Spell)) timing.AppendLine(TimeFormat(time) + ": " + label + " (" + Math.Round(manabefore).ToString() + " mana)");
                    }
                    else if (reportMode == ReportMode.Compact)
                    {
                        if (timing != null && label != null && (i == 0 || (index <= 5 && index != sequence[i - 1].Index) || sequence[i].Stats != sequence[i - 1].Stats || sequence[i].Spell != sequence[i - 1].Spell))
                        {
                            timing.AppendLine(TimeFormat(time) + ": " + (string.IsNullOrEmpty(stats.BuffLabel) ? "" : (stats.BuffLabel + "+")) + label + " (" + Math.Round(manabefore).ToString() + " mana)");
                        }
                    }
                    time += duration;
                    apCooldown -= duration;
                    ivCooldown -= duration;
                    potionCooldown -= duration;
                    gemCooldown -= duration;
                    trinket1Cooldown -= duration;
                    trinket2Cooldown -= duration;
                    combustionCooldown -= duration;
                    drumsCooldown -= duration;
                    coldsnapCooldown -= duration;
                    if (apTime >= 0 && 15 - (time - apTime) <= 0.000001) apTime = -1;
                    if (ivTime >= 0 && 20 - (time - ivTime) <= 0.000001) ivTime = -1;
                    if (heroismTime >= 0 && 40 - (time - heroismTime) <= 0.000001) heroismTime = -1;
                    if (destructionTime >= 0 && 15 - (time - destructionTime) <= 0.000001) destructionTime = -1;
                    if (flameCapTime >= 0 && 60 - (time - flameCapTime) <= 0.000001) flameCapTime = -1;
                    if (trinket1time >= 0 && Trinket1Duration - (time - trinket1time) <= 0.000001) trinket1time = -1;
                    if (trinket2time >= 0 && Trinket2Duration - (time - trinket2time) <= 0.000001) trinket2time = -1;
                    if (drumsTime >= 0 && 30 - (time - drumsTime) <= 0) drumsTime = -1;
                }
                if (timing != null && unexplained > 0)
                {
                    timing.AppendLine();
                    timing.AppendLine(string.Format("Score: {0:F}", Math.Max(0, 100 - 100 * unexplained / FightDuration)));
                }

                switch (mode)
                {
                    case EvaluationMode.Unexplained:
                        return unexplained;
                    case EvaluationMode.ManaBelow:
                        return FightDuration;
                    case EvaluationMode.CooldownBreak:
                        return FightDuration;
                }
                return unexplained;
            }
        }

        public string ReconstructSequence()
        {
            if (!CalculationOptions.ReconstructSequence) return "*Disabled";
            if (FightDuration > 900) return "*Unavailable";
            List<int> validIndex = new List<int>();
            double fight = FightDuration;
            for (int i = 0; i < SolutionLabel.Length; i++)
            {
                if (Solution[i] > 0.01)
                {
                    if (i == 3 || i == 4)
                    {
                        // mana pot and mana gem do not actually take time
                        fight -= Solution[i];
                    }
                    validIndex.Add(i);
                }
            }

            SequenceItem.Calculations = this;
            Sequence sequence = new Sequence();

            for (int i = 0; i < validIndex.Count; i++)
            {
                sequence.Add(new SequenceItem(validIndex[i], Solution[validIndex[i]]));
            }

            StringBuilder timing = new StringBuilder();
            double bestUnexplained = double.PositiveInfinity;
            string bestTiming = "*";

            // evaluate sequence
            double unexplained;
            /*unexplained = sequence.Evaluate(timing, Sequence.EvaluationMode.Unexplained);
            if (unexplained < bestUnexplained)
            {
                bestUnexplained = unexplained;
                bestTiming = timing.ToString();
            }*/

            sequence.GroupMoltenFury();
            sequence.GroupHeroism();
            sequence.GroupCombustion();
            sequence.GroupArcanePower();
            sequence.GroupDestructionPotion();
            sequence.GroupTrinket1();
            sequence.GroupTrinket2();
            sequence.GroupIcyVeins(); // should come after trinkets because of coldsnap
            sequence.GroupDrumsOfBattle();
            sequence.GroupFlameCap();

            sequence.SortGroups();

            // mana gem/pot/evo positioning
            sequence.RepositionManaConsumption();

            // evaluate sequence
            unexplained = sequence.Evaluate(timing, Sequence.EvaluationMode.Unexplained);
            if (unexplained < bestUnexplained)
            {
                bestUnexplained = unexplained;
                bestTiming = timing.ToString();
            }

            return bestTiming;
        }

        public override Dictionary<string, string> GetCharacterDisplayCalculationValues()
        {
            if (CalculationOptions.SMPDisplay)
            {
                bool savedIncrementalOptimizations = CalculationOptions.IncrementalOptimizations;
                CalculationOptions.IncrementalOptimizations = false;
                CharacterCalculationsMage smp = (CharacterCalculationsMage)((CalculationsMage)Calculations.Instance).GetCharacterCalculations(Character, null, CalculationOptions, MageArmor, false, true);
                Dictionary<string, string> ret = smp.GetCharacterDisplayCalculationValuesInternal();
                ret["Dps"] = String.Format("{0:F}*{1:F}% Error margin", smp.DpsRating, Math.Abs(DpsRating - smp.DpsRating) / DpsRating * 100);
                CalculationOptions.IncrementalOptimizations = savedIncrementalOptimizations;
                return ret;
            }
            else
            {
                return GetCharacterDisplayCalculationValuesInternal();
            }
        }

        internal Dictionary<string, string> GetCharacterDisplayCalculationValuesInternal()
        {
            Dictionary<string, string> dictValues = new Dictionary<string, string>();
            dictValues.Add("Stamina", BasicStats.Stamina.ToString());
            dictValues.Add("Intellect", BasicStats.Intellect.ToString());
            dictValues.Add("Spirit", BasicStats.Spirit.ToString());
            dictValues.Add("Armor", BasicStats.Armor.ToString());
            dictValues.Add("Health", BasicStats.Health.ToString());
            dictValues.Add("Mana", BasicStats.Mana.ToString());
            dictValues.Add("Spell Crit Rate", String.Format("{0:F}%*{1} Spell Crit Rating", 100 * SpellCrit, BasicStats.SpellCritRating));
            dictValues.Add("Spell Hit Rate", String.Format("{0:F}%*{1} Spell Hit Rating", 100 * SpellHit, BasicStats.SpellHitRating));
            dictValues.Add("Spell Penetration", BasicStats.SpellPenetration.ToString());
            dictValues.Add("Casting Speed", String.Format("{0}*{1} Spell Haste Rating", CastingSpeed, BasicStats.SpellHasteRating));
            dictValues.Add("Arcane Damage", ArcaneDamage.ToString());
            dictValues.Add("Fire Damage", FireDamage.ToString());
            dictValues.Add("Frost Damage", FrostDamage.ToString());
            dictValues.Add("MP5", BasicStats.Mp5.ToString());
            dictValues.Add("Mana Regen", Math.Floor(ManaRegen * 5).ToString() + String.Format("*Mana Regen in 5SR: {0}\r\nMana Regen Drinking: {1}", Math.Floor(ManaRegen5SR * 5), Math.Floor(ManaRegenDrinking * 5)));
            dictValues.Add("Health Regen", Math.Floor(HealthRegenCombat * 5).ToString() + String.Format("*Health Regen Eating: {0}", Math.Floor(HealthRegenEating * 5)));
            dictValues.Add("Arcane Resist", (BasicStats.AllResist + BasicStats.ArcaneResistance).ToString());
            dictValues.Add("Fire Resist", (BasicStats.AllResist + BasicStats.FireResistance).ToString());
            dictValues.Add("Nature Resist", (BasicStats.AllResist + BasicStats.NatureResistance).ToString());
            dictValues.Add("Frost Resist", (BasicStats.AllResist + BasicStats.FrostResistance).ToString());
            dictValues.Add("Shadow Resist", (BasicStats.AllResist + BasicStats.ShadowResistance).ToString());
            dictValues.Add("Physical Mitigation", String.Format("{0:F}%", 100 * MeleeMitigation));
            dictValues.Add("Resilience", BasicStats.Resilience.ToString());
            dictValues.Add("Defense", Defense.ToString());
            dictValues.Add("Crit Reduction", String.Format("{0:F}%*Spell Crit Reduction: {0:F}%\r\nPhysical Crit Reduction: {1:F}%\r\nCrit Damage Reduction: {2:F}%", SpellCritReduction * 100, PhysicalCritReduction * 100, CritDamageReduction * 100));
            dictValues.Add("Dodge", String.Format("{0:F}%", 100 * Dodge));
            List<SpellId> spellList = new List<SpellId>() { SpellId.Wand, SpellId.ArcaneMissiles, SpellId.Scorch, SpellId.Fireball, SpellId.Pyroblast, SpellId.Frostbolt, SpellId.ArcaneBlast33, SpellId.ABAMP, SpellId.ABAM, SpellId.AB3AMSc, SpellId.ABAM3Sc, SpellId.ABAM3Sc2, SpellId.ABAM3FrB, SpellId.ABAM3FrB2, SpellId.ABFrB3FrB, SpellId.ABFrB3FrBSc, SpellId.ABFB3FBSc, SpellId.FireballScorch, SpellId.FireballFireBlast, SpellId.FireBlast, SpellId.ABAM3ScCCAM, SpellId.ABAM3Sc2CCAM, SpellId.ABAM3FrBCCAM, SpellId.ABAM3FrBScCCAM, SpellId.ABAMCCAM, SpellId.ABAM3CCAM, SpellId.ArcaneExplosion, SpellId.FlamestrikeSpammed, SpellId.Blizzard, SpellId.BlastWave, SpellId.DragonsBreath, SpellId.ConeOfCold/*, SpellId.ABAM3FrBCCAMFail*/ };
            Spell AB = GetSpell(SpellId.ArcaneBlast33);
            foreach (SpellId spell in spellList)
            {
                Spell s = GetSpell(spell);
                if (s != null)
                {
                    if (s is BaseSpell)
                    {
                        BaseSpell bs = s as BaseSpell;
                        dictValues.Add(s.Name, String.Format("{0:F} Dps*{1:F} Mps\r\n{2:F} Tps\r\n{3:F} sec\r\n{9:F} - {10:F} Hit\r\n{11:F} - {12:F} Crit{13}\r\n{4:F}x Amplify\r\n{5:F}% Crit Rate\r\n{6:F}% Hit Rate\r\n{7:F} Crit Multiplier\r\nAB Spam Tradeoff: {8:F} Dpm", s.DamagePerSecond, s.CostPerSecond - s.ManaRegenPerSecond, s.ThreatPerSecond, bs.CastTime - Latency, bs.SpellModifier, bs.CritRate * 100, bs.HitRate * 100, bs.CritBonus, (AB.DamagePerSecond - s.DamagePerSecond) / (AB.CostPerSecond - AB.ManaRegenPerSecond - s.CostPerSecond + s.ManaRegenPerSecond), bs.MinHitDamage, bs.MaxHitDamage, bs.MinCritDamage, bs.MaxCritDamage, ((bs.DotDamage > 0) ? ("\n" + bs.DotDamage.ToString("F") + " Dot") : "")));
                    }
                    else
                    {
                        dictValues.Add(s.Name, String.Format("{0:F} Dps*{1:F} Mps\r\n{2:F} Tps\r\nAB Spam Tradeoff: {3:F} Dpm\r\nAverage Cast Time: {4:F} sec\r\n{5}", s.DamagePerSecond, s.CostPerSecond - s.ManaRegenPerSecond, s.ThreatPerSecond, (AB.DamagePerSecond - s.DamagePerSecond) / (AB.CostPerSecond - AB.ManaRegenPerSecond - s.CostPerSecond + s.ManaRegenPerSecond), s.CastTime, s.Sequence));
                    }
                }
            }
            dictValues.Add("Total Damage", String.Format("{0:F}", DpsRating * FightDuration));
            dictValues.Add("Dps", String.Format("{0:F}", DpsRating));
            dictValues.Add("Tps", String.Format("{0:F}", Tps));
            dictValues.Add("Sequence", ReconstructSequence());
            StringBuilder sb = new StringBuilder("*");
            if (MageArmor != null) sb.AppendLine(MageArmor);
            Dictionary<string, double> combinedSolution = new Dictionary<string, double>();
            Dictionary<string, int> combinedSolutionData = new Dictionary<string, int>();
            for (int i = 0; i < SolutionLabel.Length; i++)
            {
                if (Solution[i] > 0.01)
                {
                    switch (i)
                    {
                        case 2:
                            sb.AppendLine(String.Format("{0}: {1:F}x", SolutionLabel[i], Solution[i] / EvocationDuration));
                            break;
                        case 3:
                        case 4:
                            sb.AppendLine(String.Format("{0}: {1:F}x", SolutionLabel[i], Solution[i] / ManaPotionTime));
                            break;
                        case 5:
                            sb.AppendLine(String.Format("{0}: {1:F}x", SolutionLabel[i], Solution[i] / GlobalCooldown));
                            break;
                        default:
                            double value;
                            combinedSolution.TryGetValue(SolutionLabel[i], out value);
                            combinedSolution[SolutionLabel[i]] = value + Solution[i];
                            combinedSolutionData[SolutionLabel[i]] = i;
                            //sb.AppendLine(String.Format("{2}{0}: {1:F} sec", SolutionLabel[i], Solution[i], (SolutionSegments == null) ? "" : (SolutionSegments[i].ToString() + " ")));
                            break;
                    }
                }
            }
            foreach (KeyValuePair<string, double> kvp in combinedSolution)
            {
                Spell s = SolutionSpells[combinedSolutionData[kvp.Key]];
                if (s != null)
                {
                    sb.AppendLine(String.Format("{0}: {1:F} sec ({2:F} dps, {3:F} mps, {4:F} tps)", kvp.Key, kvp.Value, s.DamagePerSecond, s.CostPerSecond - s.ManaRegenPerSecond, s.ThreatPerSecond));
                }
                else
                {
                    sb.AppendLine(String.Format("{0}: {1:F} sec", kvp.Key, kvp.Value));
                }
            }
            if (WaterElemental) sb.AppendLine(String.Format("Water Elemental: {0:F}x", WaterElementalDuration / 45f));
            dictValues.Add("Spell Cycles", sb.ToString());
            return dictValues;
        }

        public override float GetOptimizableCalculationValue(string calculation)
        {
            switch (calculation)
            {
                case "Health":
                    return BasicStats.Health;
                case "Nature Resistance":
                    return BasicStats.AllResist + BasicStats.NatureResistance;
                case "Fire Resistance":
                    return BasicStats.AllResist + BasicStats.FireResistance;
                case "Frost Resistance":
                    return BasicStats.AllResist + BasicStats.FrostResistance;
                case "Shadow Resistance":
                    return BasicStats.AllResist + BasicStats.ShadowResistance;
                case "Arcane Resistance":
                    return BasicStats.AllResist + BasicStats.ArcaneResistance;
            }
            return 0;
        }
    }
}
