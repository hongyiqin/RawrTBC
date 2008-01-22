using System;
using System.Collections.Generic;
using System.Text;

namespace Rawr
{
	public class CalculationsCat : CalculationsBase
	{
		//my insides all turned to ash / so slow
		//and blew away as i collapsed / so cold
		private int _targetLevel = 73;
		private int _baseArmor = 7000;
		private int _powershift = 0;

		//TODO
		private System.Windows.Forms.Panel _calculationOptionsPanel = null;
		public override System.Windows.Forms.Panel CalculationOptionsPanel
		{
			get
			{
				if (_calculationOptionsPanel == null)
				{
					_calculationOptionsPanel = new System.Windows.Forms.Panel();
					
					System.Windows.Forms.Label labelTargetLevel = new System.Windows.Forms.Label();
					labelTargetLevel.Text = "Target Level: ";
					labelTargetLevel.AutoSize = true;
					labelTargetLevel.Location = new System.Drawing.Point(4, 4);
					_calculationOptionsPanel.Controls.Add(labelTargetLevel);

					System.Windows.Forms.ComboBox comboBoxTargetLevel = new System.Windows.Forms.ComboBox();
					comboBoxTargetLevel.Items.AddRange(new object[] { "70", "71", "72", "73" });
					comboBoxTargetLevel.Location = new System.Drawing.Point(labelTargetLevel.Right + 8, 4);
					comboBoxTargetLevel.SelectedValueChanged += new EventHandler(comboBoxTargetLevel_SelectedValueChanged);
					comboBoxTargetLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
					comboBoxTargetLevel.Text = _targetLevel.ToString();
					_calculationOptionsPanel.Controls.Add(comboBoxTargetLevel);
				}
				return _calculationOptionsPanel;
			}
		}

		void comboBoxTargetLevel_SelectedValueChanged(object sender, EventArgs e)
		{
			_targetLevel = int.Parse((sender as System.Windows.Forms.ComboBox).Text.ToString());
			if (CachedCharacter != null) CachedCharacter.OnItemsChanged();
		}

		private string[] _characterDisplayCalculationLabels = null;
		public override string[] CharacterDisplayCalculationLabels
		{
			get
			{
				if (_characterDisplayCalculationLabels == null)
					_characterDisplayCalculationLabels = new string[] {
					"Basic Stats:Health",
					"Basic Stats:Attack Power",
					"Basic Stats:Agility",
					"Basic Stats:Strength",
					"Basic Stats:Crit Rating",
					"Basic Stats:Hit Rating",
					"Basic Stats:Expertise Rating",
					"Basic Stats:Haste Rating",
					"Basic Stats:Armor Penetration",
					"Basic Stats:Weapon Damage",
					"Complex Stats:Avoided Attacks",
					"Complex Stats:White Crit",
					"Complex Stats:Yellow Crit",
					"Complex Stats:Attack Speed",
					"Complex Stats:Armor Mitigation",
					"Complex Stats:Shreds Per Cycle",
					"Complex Stats:Cycle Time",
					"Complex Stats:4cp Finishers",
					"Complex Stats:5cp Finishers",
					"Complex Stats:Mangle Damage",
					"Complex Stats:Shred Damage",
					"Complex Stats:Melee Damage",
					"Complex Stats:Rip Damage",
					"Complex Stats:Bite Damage",
					"Complex Stats:Overall Points",
					"Complex Stats:DPS Points"
				};
				return _characterDisplayCalculationLabels;
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
					_subPointNameColors.Add("DPS", System.Drawing.Color.Purple);
				}
				return _subPointNameColors;
			}
		}

		public override ComparisonCalculationBase CreateNewComparisonCalculation() { return new ComparisonCalculationCat(); }
		public override CharacterCalculationsBase CreateNewCharacterCalculations() { return new CharacterCalculationsCat(); }

		public override CharacterCalculationsBase GetCharacterCalculations(Character character, Item additionalItem)
		{
			_cachedCharacter = character;
			Stats stats = GetCharacterStats(character, additionalItem);
			float levelDifference = (_targetLevel - 70f) * 0.2f;
			CharacterCalculationsCat calculatedStats = new CharacterCalculationsCat();
			calculatedStats.BasicStats = stats;
			calculatedStats.TargetLevel = _targetLevel;

			//Begin Toskk's 
			
			#region Calculate Basic Chances, Costs, and Damage

			float baseArmor = Math.Max(0f, this._baseArmor - stats.ArmorPenetration);
			float modArmor = (baseArmor / (baseArmor + 10557.5f )) * 100f;


			float critMultiplier = 2 * (1 + stats.BonusCritMultiplier);
			float attackPower = stats.AttackPower;

			float hitBonus = stats.HitRating * 52f / 82f / 1000f;
			float expertiseBonus = stats.ExpertiseRating * 52f / 82f / 2.5f * 0.0025f;

			float chanceCrit = Math.Min(0.75f, (stats.CritRating / 22.08f + (stats.Agility / 25f)) / 100f);
			float chanceDodge = Math.Max(0f, 0.05f - expertiseBonus);
			float chanceMiss = Math.Max(0f, 0.09f - hitBonus) + chanceDodge;
						
			float meleeDamage = stats.WeaponDamage + (768f + stats.AttackPower) / 14f;
			float mangleCost = 40f - stats.MangleCostReduction;
			float totalMangleCost = 1f / (1f - chanceMiss) * (mangleCost * (1f - chanceMiss) + mangleCost / 5f * chanceMiss);
			float mangleDamage = 1.1f * (meleeDamage * 1.6f + 264f + stats.BonusMangleDamage);

			float shredCost = 42f;
			float totalShredCost = chanceMiss * shredCost / 5f + (1f - chanceMiss) * shredCost;
			float shredDamage = meleeDamage * 2.25f + 405f + stats.BonusShredDamage;

			float ripCost = 30f;
			float totalRipCost = 1f / (1f - chanceMiss) * ripCost;

			float chanceWhiteCrit = Math.Min(chanceCrit, 0.75f - chanceMiss);

			float hasteBonus = stats.HasteRating / 15.76f / 100f;
			float attackSpeed = 1f / (1f + hasteBonus);
			float meleeTicker = attackSpeed;

			#endregion

			#region Mangle

			// Starting immediately after Ripping, with energy to Mangle and GCD running
			float dmgMelee = 0f;
			float cycleTime = 1.0f;
			float segmentTime = 1.0f;
			float energyCount = mangleCost  + 10f;

			dmgMelee += segmentTime / attackSpeed * meleeDamage * (0.7f * 0.25f + (0.75f - chanceMiss - chanceWhiteCrit) +
				critMultiplier * chanceWhiteCrit);
			energyCount += segmentTime / attackSpeed * stats.BloodlustProc;

			//GCD ends, Manlge hits

			energyCount -= totalMangleCost * (1f - segmentTime / 30f);
			float dmgMangles = meleeDamage * ((1f - chanceCrit) + critMultiplier * chanceCrit);
			float comboPoints = 1f + chanceCrit;
			energyCount += segmentTime / attackSpeed * stats.BloodlustProc;

			float terrorTicker = 0;
			if (stats.TerrorProc > 0)
			{
				attackPower += 65f * 0.85f * (1f + stats.BonusAgilityMultiplier) * (1f + stats.BonusAttackPowerMultiplier);
				chanceCrit += 65f * 0.85f * stats.BonusAgilityMultiplier / 25 / 100;
				terrorTicker = 10;
			}

			#endregion

			#region Shreds

			float numberAttacks = (4f - 3f * chanceCrit + 2f * (float)Math.Pow(chanceCrit, 2) - (float)Math.Pow(chanceCrit, 3)) / (1f - chanceMiss);
			float numberShreds = (numberAttacks - (1f / (1f - chanceMiss)));

			// Mangle Debuff running, waiting for energy to Shred number_shreds times

			energyCount += (numberShreds - 1f) * stats.BloodlustProc;

			segmentTime = (numberShreds * totalShredCost * (1f - numberShreds * totalShredCost / 10f / numberShreds / 30f)
				- energyCount) / 10f;
			segmentTime -= segmentTime / attackSpeed * stats.BloodlustProc / 10f;
			cycleTime += segmentTime;

			terrorTicker -= segmentTime;

			dmgMelee += segmentTime / attackSpeed * (stats.WeaponDamage + (768f + attackPower) / 14f) * 
				(0.7f * 0.25f + (0.75f - chanceMiss - chanceWhiteCrit) + critMultiplier * chanceWhiteCrit);

			float dmgPerShred = ((stats.WeaponDamage + (768f + attackPower) / 14f) * 2.25f + 405f + stats.BonusShredDamage);
			float dmgShreds = 0;
			if (segmentTime <= 12f)
				dmgShreds = numberShreds * dmgPerShred * 1.3f * ((1f - chanceMiss) * (1f - chanceCrit) + critMultiplier *
					(1f - chanceMiss) * chanceCrit);
			else
				dmgShreds = ((12f / segmentTime) * numberShreds * dmgPerShred * 1.3f * ((1f - chanceMiss) * (1f - chanceCrit) + 
					critMultiplier * (1f - chanceMiss) * chanceCrit)) + ((1f - 12f / segmentTime) * numberShreds * 
					dmgPerShred * ((1f - chanceMiss) * (1f - chanceCrit) + critMultiplier * (1f - chanceMiss) * chanceCrit));

			energyCount = 0;
			energyCount += stats.BloodlustProc;
			
			#endregion

			#region Powershifting

			if (_powershift > 0)
			{
				energyCount = 17 / _powershift;
			}
			//Ignoring the wolf helm since it's pretty craptacular

			// 5 Combo points generated, waiting for energy to Rip + Mangle

			segmentTime = (mangleCost + totalRipCost * (1f - totalRipCost / 10f / 30f) - energyCount) / 10f;
			segmentTime -= segmentTime / attackSpeed * stats.BloodlustProc / 10f;

			cycleTime += segmentTime;

			if (terrorTicker > segmentTime) terrorTicker = segmentTime;

			if (stats.TerrorProc > 0)
				dmgMelee += (terrorTicker / segmentTime) * (segmentTime / attackSpeed * (stats.WeaponDamage + (768f +
					attackPower) / 14f) * (0.7f * 0.25f + (0.75f - chanceMiss - chanceWhiteCrit) + critMultiplier *
					chanceWhiteCrit)) + (1f - terrorTicker / segmentTime) * (segmentTime / attackSpeed * meleeDamage *
					(0.7f * 0.25f + (0.75f - chanceMiss - chanceWhiteCrit) + critMultiplier * chanceWhiteCrit));
			else
				dmgMelee += segmentTime / attackSpeed * meleeDamage * (0.7f * 0.25f + (0.75f - chanceMiss -
					chanceWhiteCrit) + critMultiplier * chanceWhiteCrit);
			#endregion

			#region Rip

			float rip5 = (chanceCrit - (float)Math.Pow(chanceCrit, 2) + (float)Math.Pow(chanceCrit, 3) - (float)Math.Pow(chanceCrit, 4));
			float rip4 = 1f - rip5;

			// Checking on the possibility of completing the cycle too quickly

			if (cycleTime < 12f)
			{
				segmentTime = 12f - cycleTime;
				cycleTime += segmentTime;
				energyCount = segmentTime * 10f;

				dmgMelee += segmentTime / attackSpeed * meleeDamage * (0.7f * 0.25f + (0.75f - chanceMiss - chanceWhiteCrit) +
					critMultiplier * chanceWhiteCrit);

				energyCount += segmentTime / attackSpeed * stats.BloodlustProc;

				float extraShred = energyCount / (totalShredCost * (1f - segmentTime / 30f));

				dmgShreds += extraShred * shredDamage * 1.3f * ((1f - chanceMiss) * (1 - chanceCrit) +
					critMultiplier * (1f - chanceMiss) * chanceCrit);

				rip5 += extraShred * rip4;
				rip4 -= extraShred * rip4;

				numberShreds += extraShred;
			}

			float dmgRips = (rip5 * (attackPower * 0.24f + 1553f) + rip4 * (attackPower * 0.24f + 1272f))
				* 1.3f * (1f + stats.BonusRipDamageMultiplier);

			#endregion

			float dps = 1.1f * (((dmgMangles + dmgShreds + dmgMelee) * (1f - modArmor / 100f) + dmgRips) / cycleTime);

			calculatedStats.DPSPoints = dps * 100f;
			calculatedStats.OverallPoints = dps * 100f;
			calculatedStats.AvoidedAttacks = chanceMiss * 100f;
			calculatedStats.WhiteCrit = chanceWhiteCrit * 100f;
			calculatedStats.YellowCrit = (1f - chanceMiss) * chanceCrit * 100f;
			calculatedStats.ShredsPerCycle = numberShreds;
			calculatedStats.AttackSpeed = attackSpeed;
			calculatedStats.ArmorMitigation = modArmor;
			calculatedStats.MangleDamage = dmgMangles * (1f - modArmor / 100f) / ((dmgMangles + dmgShreds + dmgMelee) * (1f - modArmor / 100f) + dmgRips) * 100f;
			calculatedStats.MeleeDamage = dmgMelee * (1f - modArmor / 100f) / ((dmgMangles + dmgShreds + dmgMelee) * (1f - modArmor / 100f) + dmgRips) * 100f;
			calculatedStats.RipDamage = dmgRips / ((dmgMangles + dmgShreds + dmgMelee) * (1f - modArmor / 100f) + dmgRips) * 100f;
			calculatedStats.Finishers4cp = rip4 * 100f;
			calculatedStats.Finishers5cp = rip5 * 100f;
			calculatedStats.ShredDamage = dmgShreds * (1f - modArmor / 100f) / ((dmgMangles + dmgShreds + dmgMelee) * (1f - modArmor / 100f) + dmgRips) * 100f;
			calculatedStats.CycleTime = cycleTime;

			/*

			*/

			return calculatedStats;
		}

		public override Stats GetCharacterStats(Character character, Item additionalItem)
		{
			//TODO: Find correct BonusCritMultiplier, AttackPower, Strength, CritRating
			Stats statsRace = character.Race == Character.CharacterRace.NightElf ? 
				new Stats() { 
					Health = 3434, 
					Strength = 68, 
					Agility = 75, 
					Stamina = 82, 
					DodgeRating = 59, 
					BonusCritMultiplier = 0.1f, 
					CritRating = 90f, 
					BonusAttackPowerMultiplier = 1.1f,
					BonusAgilityMultiplier = 1.03f,
					BonusStrengthMultiplier = 1.03f,
					BonusStaminaMultiplier = 1.03f} : 
				new Stats() { 
					Health = 3434, 
					Strength = 72, 
					Agility = 64, 
					Stamina = 85, 
					DodgeRating = 40, 
					BonusCritMultiplier = 0.1f, 
					CritRating = 90f, 
					BonusAttackPowerMultiplier = 1.1f,
					BonusAgilityMultiplier = 1.03f,
					BonusStrengthMultiplier = 1.03f,
					BonusStaminaMultiplier = 1.03f}; 
			Stats statsBaseGear = GetItemStats(character, additionalItem);
			Stats statsEnchants = GetEnchantsStats(character);
			Stats statsBuffs = GetBuffsStats(character.ActiveBuffs);

			Stats statsGearEnchantsBuffs = statsBaseGear + statsEnchants + statsBuffs;


			float agiBase = (float)Math.Floor(statsRace.Agility * statsRace.BonusAgilityMultiplier);
			float agiBonus = (float)Math.Floor(statsGearEnchantsBuffs.Agility * statsRace.BonusAgilityMultiplier);
			float strBase = (float)Math.Floor(statsRace.Strength * statsRace.BonusStrengthMultiplier);
			float strBonus = (float)Math.Floor(statsGearEnchantsBuffs.Strength * statsRace.BonusStrengthMultiplier);
			float staBase = (float)Math.Floor(statsRace.Stamina * statsRace.BonusStaminaMultiplier);
			float staBonus = (float)Math.Floor(statsGearEnchantsBuffs.Stamina * statsRace.BonusStaminaMultiplier);
						
			Stats statsTotal = new Stats();
			statsTotal.BonusAttackPowerMultiplier = ((1 + statsRace.BonusAttackPowerMultiplier) * (1 + statsGearEnchantsBuffs.BonusAttackPowerMultiplier)) - 1;
			statsTotal.BonusAgilityMultiplier = ((1 + statsRace.BonusAgilityMultiplier) * (1 + statsGearEnchantsBuffs.BonusAgilityMultiplier)) - 1;
			statsTotal.BonusStrengthMultiplier = ((1 + statsRace.BonusStrengthMultiplier) * (1 + statsGearEnchantsBuffs.BonusStrengthMultiplier)) - 1;
			statsTotal.BonusStaminaMultiplier = ((1 + statsRace.BonusStaminaMultiplier) * (1 + statsGearEnchantsBuffs.BonusStaminaMultiplier)) - 1;
			statsTotal.Agility = (agiBase + (float)Math.Floor((agiBase * statsBuffs.BonusAgilityMultiplier) + agiBonus * (1 + statsBuffs.BonusAgilityMultiplier)));
			statsTotal.Strength = (strBase + (float)Math.Floor((strBase * statsBuffs.BonusStrengthMultiplier) + strBonus * (1 + statsBuffs.BonusStrengthMultiplier)));
			statsTotal.Stamina = (staBase + (float)Math.Round((staBase * statsBuffs.BonusStaminaMultiplier) + staBonus * (1 + statsBuffs.BonusStaminaMultiplier)));
			statsTotal.DefenseRating = statsRace.DefenseRating + statsGearEnchantsBuffs.DefenseRating;
			statsTotal.DodgeRating = statsRace.DodgeRating + statsGearEnchantsBuffs.DodgeRating;
			statsTotal.Resilience = statsRace.Resilience + statsGearEnchantsBuffs.Resilience;
			statsTotal.Health = (float)Math.Round(((statsRace.Health + statsGearEnchantsBuffs.Health + (statsTotal.Stamina * 10f)) * (character.Race == Character.CharacterRace.Tauren ? 1.05f : 1f)));
			statsTotal.Armor = (float)Math.Round((statsGearEnchantsBuffs.Armor + statsRace.Armor + (statsTotal.Agility * 2f)) * (1 + statsBuffs.BonusArmorMultiplier));
			statsTotal.Miss = statsBuffs.Miss;
			statsTotal.ArmorPenetration = statsRace.ArmorPenetration + statsGearEnchantsBuffs.ArmorPenetration;
			statsTotal.AttackPower = (float)Math.Floor((statsRace.AttackPower + statsGearEnchantsBuffs.AttackPower + statsTotal.Agility + (statsTotal.Strength * 2)) *  (1f + statsTotal.BonusAttackPowerMultiplier));
			statsTotal.BloodlustProc = statsRace.BloodlustProc + statsGearEnchantsBuffs.BloodlustProc;
			statsTotal.BonusCritMultiplier = ((1 + statsRace.BonusCritMultiplier) * (1 + statsGearEnchantsBuffs.BonusCritMultiplier)) - 1;
			statsTotal.BonusMangleDamage = statsRace.BonusMangleDamage + statsGearEnchantsBuffs.BonusMangleDamage;
			statsTotal.BonusRipDamageMultiplier = ((1 + statsRace.BonusRipDamageMultiplier) * (1 + statsGearEnchantsBuffs.BonusRipDamageMultiplier)) - 1;
			statsTotal.BonusShredDamage = statsRace.BonusShredDamage + statsGearEnchantsBuffs.BonusShredDamage;
			statsTotal.CritRating = statsRace.CritRating + statsGearEnchantsBuffs.CritRating;
			statsTotal.ExpertiseRating = statsRace.ExpertiseRating + statsGearEnchantsBuffs.ExpertiseRating;
			statsTotal.HasteRating = statsRace.HasteRating + statsGearEnchantsBuffs.HasteRating;
			statsTotal.HitRating = statsRace.HitRating + statsGearEnchantsBuffs.HitRating;
			statsTotal.MangleCostReduction = statsRace.MangleCostReduction + statsGearEnchantsBuffs.MangleCostReduction;
			statsTotal.TerrorProc = statsRace.TerrorProc + statsGearEnchantsBuffs.TerrorProc;
			statsTotal.WeaponDamage = statsRace.WeaponDamage + statsGearEnchantsBuffs.WeaponDamage;

			return statsTotal;
		}

		//TODO
		public override ComparisonCalculationBase[] GetCombatTable(CharacterCalculationsBase currentCalculations)
		{
			CharacterCalculationsCat currentCalculationsCat = currentCalculations as CharacterCalculationsCat;
			ComparisonCalculationCat calcMiss = new ComparisonCalculationCat();
			ComparisonCalculationCat calcDodge = new ComparisonCalculationCat();
			ComparisonCalculationCat calcCrit = new ComparisonCalculationCat();
			ComparisonCalculationCat calcCrush = new ComparisonCalculationCat();
			ComparisonCalculationCat calcHit = new ComparisonCalculationCat();
			//if (currentCalculationsCat != null)
			//{
			//    calcMiss.Name = "    Miss    ";
			//    calcDodge.Name = "   Dodge   ";
			//    calcCrit.Name = "  Crit  ";
			//    calcCrush.Name = " Crush ";
			//    calcHit.Name = "Hit";

			//    float crits = 2f + (0.2f * (_targetLevel - 70)) - currentCalculationsCat.CappedCritReduction;
			//    float crushes = _targetLevel == 73 ? Math.Max(Math.Min(100f - (crits + (currentCalculationsCat.DodgePlusMiss)), 15f), 0f) : 0f;
			//    float hits = Math.Max(100f - (crits + crushes + (currentCalculationsCat.DodgePlusMiss)), 0f);

			//    calcMiss.OverallPoints = calcMiss.MitigationPoints = currentCalculationsCat.Miss;
			//    calcDodge.OverallPoints = calcDodge.MitigationPoints = currentCalculationsCat.Dodge;
			//    calcCrit.OverallPoints = calcCrit.SurvivalPoints = crits;
			//    calcCrush.OverallPoints = calcCrush.SurvivalPoints = crushes;
			//    calcHit.OverallPoints = calcHit.SurvivalPoints = hits;
			//}
			return new ComparisonCalculationBase[] { calcMiss, calcDodge, calcCrit, calcCrush, calcHit };
		}

		public override string[] GetRelevantStats(Stats stats)
		{
			List<string> listStats = new List<string>(
				new string[] {
					stats.Agility.ToString() + " Agility",
					stats.Strength.ToString() + " Strength",
					stats.AttackPower.ToString() + " AP",
					stats.CritRating.ToString() + " Crit",
					stats.HasteRating.ToString() + " Haste",
					stats.Stamina.ToString() + " Stamina"
				});

			if (stats.ExpertiseRating > 0)
				listStats.Add(stats.ExpertiseRating + " Expertise");
			if (stats.ArmorPenetration > 0)
				listStats.Add(stats.ArmorPenetration + " Armor Penetration");
			if (stats.BloodlustProc > 0)
				listStats.Add("Bloodlust Proc");
			if (stats.TerrorProc > 0)
				listStats.Add("Terror Dmg");
			if (stats.BonusMangleDamage > 0)
				listStats.Add(stats.BonusMangleDamage + " Mangle Dmg");
			if (stats.BonusShredDamage > 0)
				listStats.Add(stats.BonusShredDamage + " Shred Dmg");
			if (stats.WeaponDamage > 0)
				listStats.Add(stats.WeaponDamage + " Weapon Dmg");

			return listStats.ToArray(); ;
		}

		public override bool HasRelevantStats(Stats stats)
		{
			return (stats.Agility + stats.ArmorPenetration + stats.AttackPower + stats.BloodlustProc +
				stats.BonusAgilityMultiplier + stats.BonusAttackPowerMultiplier + stats.BonusCritMultiplier +
				stats.BonusMangleDamage + stats.BonusRipDamageMultiplier + stats.BonusShredDamage +
				stats.BonusStaminaMultiplier + stats.BonusStrengthMultiplier + stats.CritRating + stats.ExpertiseRating +
				stats.HasteRating + stats.Health + stats.HitRating + stats.MangleCostReduction + stats.Stamina +
				stats.Strength + stats.TerrorProc + stats.WeaponDamage) > 0;
		}
	}

    public class CharacterCalculationsCat : CharacterCalculationsBase
    {
		private float _overallPoints = 0f;
		public override float OverallPoints
		{
			get { return _overallPoints; }
			set { _overallPoints = value; }
		}

		private float[] _subPoints = new float[] { 0f };
		public override float[] SubPoints
		{
			get { return _subPoints; }
			set { _subPoints = value; }
		}

		public float DPSPoints
		{
			get { return _subPoints[0]; }
			set { _subPoints[0] = value; }
		}

		private Stats _basicStats;
		public Stats BasicStats
		{
			get { return _basicStats; }
			set { _basicStats = value; }
		}

		private int _targetLevel;
		public int TargetLevel
		{
			get { return _targetLevel; }
			set { _targetLevel = value; }
		}

		private float _avoidedAttacks;
		public float AvoidedAttacks
		{
			get { return _avoidedAttacks; }
			set { _avoidedAttacks = value; }
		}

		private float _whiteCrit;
		public float WhiteCrit
		{
			get { return _whiteCrit; }
			set { _whiteCrit = value; }
		}

		private float _yellowCrit;
		public float YellowCrit
		{
			get { return _yellowCrit; }
			set { _yellowCrit = value; }
		}

		private float _attackSpeed;
		public float AttackSpeed
		{
			get { return _attackSpeed; }
			set { _attackSpeed = value; }
		}

		private float _armorMitigation;
		public float ArmorMitigation
		{
			get { return _armorMitigation; }
			set { _armorMitigation = value; }
		}

		private float _shredsPerCycle;
		public float ShredsPerCycle
		{
			get { return _shredsPerCycle; }
			set { _shredsPerCycle = value; }
		}

		private float _cycleTime;
		public float CycleTime
		{
			get { return _cycleTime; }
			set { _cycleTime = value; }
		}

		private float _finishers4cp;
		public float Finishers4cp
		{
			get { return _finishers4cp; }
			set { _finishers4cp = value; }
		}

		private float _finishers5cp;
		public float Finishers5cp
		{
			get { return _finishers5cp; }
			set { _finishers5cp = value; }
		}

		private float _mangleDamage;
		public float MangleDamage
		{
			get { return _mangleDamage; }
			set { _mangleDamage = value; }
		}

		private float _shredDamage;
		public float ShredDamage
		{
			get { return _shredDamage; }
			set { _shredDamage = value; }
		}

		private float _meleeDamage;
		public float MeleeDamage
		{
			get { return _meleeDamage; }
			set { _meleeDamage = value; }
		}

		private float _ripDamage;
		public float RipDamage
		{
			get { return _ripDamage; }
			set { _ripDamage = value; }
		}

		private float _ferociousBiteDamage;
		public float FerociousBiteDamage
		{
			get { return _ferociousBiteDamage; }
			set { _ferociousBiteDamage = value; }
		}

		public override Dictionary<string, string> GetCharacterDisplayCalculationValues()
		{
			float critRating = BasicStats.CritRating;
			if (Calculations.CachedCharacter.ActiveBuffs.Contains("Improved Judgement of the Crusade"))
				critRating -= 66.24f;
			if (Calculations.CachedCharacter.Race == Character.CharacterRace.NightElf)
				critRating -= 90f;
			else
				critRating -= 90f;

			float hitRating = BasicStats.HitRating;
			if (Calculations.CachedCharacter.ActiveBuffs.Contains("Improved Faerie Fire"))
				hitRating -= 47.3077f;
			if (Calculations.CachedCharacter.ActiveBuffs.Contains("Heroic Presence"))
				hitRating -= 15.769f;

			float armorPenetration = BasicStats.ArmorPenetration;
			if (Calculations.CachedCharacter.ActiveBuffs.Contains("Faerie Fire"))
				armorPenetration -= 610f;
			if (Calculations.CachedCharacter.ActiveBuffs.Contains("Sunder Armor (x5)"))
				armorPenetration -= 2600f;
			if (Calculations.CachedCharacter.ActiveBuffs.Contains("Curse of Recklessness"))
				armorPenetration -= 800f;
			if (Calculations.CachedCharacter.ActiveBuffs.Contains("Expose Armor (5cp)"))
				armorPenetration -= 2000f;
			if (Calculations.CachedCharacter.ActiveBuffs.Contains("Improved Expose Armor (5cp)"))
				armorPenetration -= 1000f;
			
			Dictionary<string, string> dictValues = new Dictionary<string, string>();
			dictValues.Add("Health", BasicStats.Health.ToString());
			dictValues.Add("Attack Power", BasicStats.AttackPower.ToString());
			dictValues.Add("Agility", BasicStats.Agility.ToString());
			dictValues.Add("Strength", BasicStats.Strength.ToString());
			dictValues.Add("Crit Rating", critRating.ToString());
			dictValues.Add("Hit Rating", hitRating.ToString());
			dictValues.Add("Expertise Rating", BasicStats.ExpertiseRating.ToString());
			dictValues.Add("Haste Rating", BasicStats.HasteRating.ToString());
			dictValues.Add("Armor Penetration", armorPenetration.ToString());
			dictValues.Add("Weapon Damage", "+" + BasicStats.WeaponDamage.ToString());
			dictValues.Add("Avoided Attacks", AvoidedAttacks.ToString() + "%");
			dictValues.Add("White Crit", WhiteCrit.ToString() + "%");
			dictValues.Add("Yellow Crit", YellowCrit.ToString() + "%");
			dictValues.Add("Attack Speed", AttackSpeed.ToString() + "s");
			dictValues.Add("Armor Mitigation", ArmorMitigation.ToString() + "%");
			dictValues.Add("Shreds Per Cycle", ShredsPerCycle.ToString());
			dictValues.Add("Cycle Time", CycleTime.ToString() + "s");
			dictValues.Add("4cp Finishers", Finishers4cp.ToString() + "%");
			dictValues.Add("5cp Finishers", Finishers5cp.ToString() + "%");
			dictValues.Add("Mangle Damage", MangleDamage.ToString() + "%");
			dictValues.Add("Shred Damage", ShredDamage.ToString() + "%");
			dictValues.Add("Melee Damage", MeleeDamage.ToString() + "%");
			dictValues.Add("Rip Damage", RipDamage.ToString() + "%");
			dictValues.Add("Bite Damage", FerociousBiteDamage.ToString() + "%");
			dictValues.Add("Overall Points", OverallPoints.ToString());
			dictValues.Add("DPS Points", DPSPoints.ToString());
			
			return dictValues;
		}
    }

	public class ComparisonCalculationCat : ComparisonCalculationBase
	{
		private string _name = string.Empty;
		public override string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private float _overallPoints = 0f;
		public override float OverallPoints
		{
			get { return _overallPoints; }
			set { _overallPoints = value; }
		}

		private float[] _subPoints = new float[] { 0f };
		public override float[] SubPoints
		{
			get { return _subPoints; }
			set { _subPoints = value; }
		}

		public float DPSPoints
		{
			get { return _subPoints[0]; }
			set { _subPoints[0] = value; }
		}

		private Item _item = null;
		public override Item Item
		{
			get { return _item; }
			set { _item = value; }
		}

		private bool _equipped = false;
		public override bool Equipped
		{
			get { return _equipped; }
			set { _equipped = value; }
		}

		public override string ToString()
		{
			return string.Format("{0}: ({1}O {2}DPS)", Name, Math.Round(OverallPoints), Math.Round(DPSPoints));
		}
	}
}