﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Rawr.HolyPriest
{
    public partial class CalculationOptionsPanelHolyPriest : CalculationOptionsPanelBase
    {
        public CharacterCalculationsHolyPriest Calculations;

        public CalculationOptionsPanelHolyPriest()
        {
            InitializeComponent();
        }

        private bool loading;

        protected override void LoadCalculationOptions()
        {
            loading = true;
            if (Character.CalculationOptions == null)
                Character.CalculationOptions = new CalculationOptionsPriest();

            CalculationOptionsPriest calcOpts = Character.CalculationOptions as CalculationOptionsPriest;
            cmbLength.Value = (decimal)calcOpts.Length;
            
            trkActivity.Value = (int)calcOpts.TimeInFSR;
            lblActivity.Text = trkActivity.Value + "%";

            loading = false;
        }

        private void cmbLength_ValueChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                CalculationOptionsPriest calcOpts = Character.CalculationOptions as CalculationOptionsPriest;
                calcOpts.Length = (float)cmbLength.Value;
                Character.OnItemsChanged();
            }
        }
        
        private void trkActivity_Scroll(object sender, EventArgs e)
        {
            if (!loading)
            {
                CalculationOptionsPriest calcOpts = Character.CalculationOptions as CalculationOptionsPriest;
                lblActivity.Text = trkActivity.Value + "%";
                calcOpts.TimeInFSR = trkActivity.Value;
                Character.OnItemsChanged();
            }
        }
        

        private void btnRenew_Click(object sender, EventArgs e)
        {
            CalculationsHolyPriest retCalc = new CalculationsHolyPriest();
            CharacterCalculationsHolyPriest p = retCalc.GetCharacterCalculations(Character) as CharacterCalculationsHolyPriest;

            List<Spell>[] spellList = new List<Spell>[] {
                                                Renew.GetAllRanks(p.BasicStats, Character.Talents),
                                                FlashHeal.GetAllRanks(p.BasicStats, Character.Talents), 
                                                GreaterHeal.GetAllRanks(p.BasicStats, Character.Talents),
                                                Heal.GetAllRanks(p.BasicStats, Character.Talents),
                                                PrayerOfHealing.GetAllRanks(p.BasicStats, Character.Talents, 3),
                                                PrayerOfHealing.GetAllRanks(p.BasicStats, Character.Talents, 4),
                                                PrayerOfHealing.GetAllRanks(p.BasicStats, Character.Talents, 5),
                                                BindingHeal.GetAllRanks(p.BasicStats, Character.Talents),
                                                PrayerOfMending.GetAllRanks(p.BasicStats, Character.Talents, 3),
                                                PrayerOfMending.GetAllRanks(p.BasicStats, Character.Talents, 4),
                                                PrayerOfMending.GetAllRanks(p.BasicStats, Character.Talents, 5),
                                                CircleOfHealing.GetAllRanks(p.BasicStats, Character.Talents, 3),
                                                CircleOfHealing.GetAllRanks(p.BasicStats, Character.Talents, 4),
                                                CircleOfHealing.GetAllRanks(p.BasicStats, Character.Talents, 5),
                                                HolyNova.GetAllRanks(p.BasicStats, Character.Talents, 3),
                                                HolyNova.GetAllRanks(p.BasicStats, Character.Talents, 4),
                                                HolyNova.GetAllRanks(p.BasicStats, Character.Talents, 5),
                                                Lightwell.GetAllRanks(p.BasicStats, Character.Talents) 
                                            };
            Graph graph = new Graph(spellList);
            graph.Show();
        }

        private void tbnTalents_Click(object sender, EventArgs e)
        {
            TalentForm talents = new TalentForm(this);
            talents.SetParameters(Character.Talents, Character.CharacterClass.Priest);
            talents.Show();
        }
    }

    [Serializable]
	public class CalculationOptionsPriest : ICalculationOptionBase
	{
		public string GetXml()
		{
			System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(CalculationOptionsPriest));
			StringBuilder xml = new StringBuilder();
			System.IO.StringWriter writer = new System.IO.StringWriter(xml);
			serializer.Serialize(writer, this);
			return xml.ToString();
		}

		public bool EnforceMetagemRequirements = false;
		public float Length = 5;
		public float ManaAmt = 2400;
		public float ManaTime = 2.5f;
		public float TimeInFSR = 80;
		public float Spriest = 0;
	}
}
