﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Rawr.Healadin
{
    public partial class CalculationOptionsPanelHealadin : CalculationOptionsPanelBase
    {
        public CalculationOptionsPanelHealadin()
        {
            InitializeComponent();
        }
        
        protected override void LoadCalculationOptions()
        {
            if (!Character.CalculationOptions.ContainsKey("EnforceMetagemRequirements"))
                Character.CalculationOptions["EnforceMetagemRequirements"] = "No";

            if (!Character.CalculationOptions.ContainsKey("Rank1"))
                Character.CalculationOptions["Rank1"] = "11";
            if (!Character.CalculationOptions.ContainsKey("Rank2"))
                Character.CalculationOptions["Rank2"] = "9";
            if (!Character.CalculationOptions.ContainsKey("Rank3"))
                Character.CalculationOptions["Rank3"] = "7";
            if (!Character.CalculationOptions.ContainsKey("Rank4"))
                Character.CalculationOptions["Rank4"] = "5";
            if (!Character.CalculationOptions.ContainsKey("Rank5"))
                Character.CalculationOptions["Rank5"] = "4";

            if (!Character.CalculationOptions.ContainsKey("CycleFoL"))
                Character.CalculationOptions["CycleFoL"] = "10";
            if (!Character.CalculationOptions.ContainsKey("CycleHL1"))
                Character.CalculationOptions["CycleHL1"] = "3";
            if (!Character.CalculationOptions.ContainsKey("CycleHL2"))
                Character.CalculationOptions["CycleHL2"] = "2";
            if (!Character.CalculationOptions.ContainsKey("CycleHL3"))
                Character.CalculationOptions["CycleHL3"] = "0";
            if (!Character.CalculationOptions.ContainsKey("CycleHL4"))
                Character.CalculationOptions["CycleHL4"] = "1";
            if (!Character.CalculationOptions.ContainsKey("CycleHL5"))
                Character.CalculationOptions["CycleHL5"] = "0";

            if (!Character.CalculationOptions.ContainsKey("Length"))
                Character.CalculationOptions["Length"] = "5";

            cmbRank1.Value = decimal.Parse(Character.CalculationOptions["Rank1"]);
            cmbRank2.Value = decimal.Parse(Character.CalculationOptions["Rank2"]);
            cmbRank3.Value = decimal.Parse(Character.CalculationOptions["Rank3"]);
            cmbRank4.Value = decimal.Parse(Character.CalculationOptions["Rank4"]);
            cmbRank5.Value = decimal.Parse(Character.CalculationOptions["Rank5"]);

            cmbFoL.Value = decimal.Parse(Character.CalculationOptions["CycleFoL"]);
            cmbHL1.Value = decimal.Parse(Character.CalculationOptions["CycleHL1"]);
            cmbHL2.Value = decimal.Parse(Character.CalculationOptions["CycleHL2"]);
            cmbHL3.Value = decimal.Parse(Character.CalculationOptions["CycleHL3"]);
            cmbHL4.Value = decimal.Parse(Character.CalculationOptions["CycleHL4"]);
            cmbHL5.Value = decimal.Parse(Character.CalculationOptions["CycleHL5"]);

            cmbLength.Value = decimal.Parse(Character.CalculationOptions["Length"]);
        }

        private void cmbRank1_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["Rank1"] = cmbRank1.Value.ToString();
            lblHL1.Text = "Holy Light " + cmbRank1.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbRank2_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["Rank2"] = cmbRank2.Value.ToString();
            lblHL2.Text = "Holy Light " + cmbRank2.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbRank3_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["Rank3"] = cmbRank3.Value.ToString();
            lblHL3.Text = "Holy Light " + cmbRank3.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbRank4_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["Rank4"] = cmbRank4.Value.ToString();
            lblHL4.Text = "Holy Light " + cmbRank4.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbRank5_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["Rank5"] = cmbRank5.Value.ToString();
            lblHL5.Text = "Holy Light " + cmbRank5.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbFoL_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["CycleFoL"] = cmbFoL.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbHL1_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["CycleHL1"] = cmbHL1.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbHL2_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["CycleHL2"] = cmbHL2.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbHL3_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["CycleHL3"] = cmbHL3.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbHL4_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["CycleHL4"] = cmbHL4.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbHL5_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["CycleHL5"] = cmbHL5.Value.ToString();
            Character.OnItemsChanged();
        }

        private void cmbLength_ValueChanged(object sender, EventArgs e)
        {
            Character.CalculationOptions["Length"] = cmbLength.Value.ToString();
            Character.OnItemsChanged();
        }
    }
}