﻿namespace Rawr.Moonkin
{
    partial class CalculationOptionsPanelMoonkin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTargetLevel = new System.Windows.Forms.Label();
            this.cmbTargetLevel = new System.Windows.Forms.ComboBox();
            this.txtLatency = new System.Windows.Forms.TextBox();
            this.lblLatency = new System.Windows.Forms.Label();
            this.chkMetagem = new System.Windows.Forms.CheckBox();
            this.btnTalents = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTargetLevel
            // 
            this.lblTargetLevel.AutoSize = true;
            this.lblTargetLevel.Location = new System.Drawing.Point(3, 7);
            this.lblTargetLevel.Name = "lblTargetLevel";
            this.lblTargetLevel.Size = new System.Drawing.Size(70, 13);
            this.lblTargetLevel.TabIndex = 0;
            this.lblTargetLevel.Text = "Target Level:";
            // 
            // cmbTargetLevel
            // 
            this.cmbTargetLevel.FormattingEnabled = true;
            this.cmbTargetLevel.Items.AddRange(new object[] {
            "70",
            "71",
            "72",
            "73"});
            this.cmbTargetLevel.Location = new System.Drawing.Point(108, 4);
            this.cmbTargetLevel.Name = "cmbTargetLevel";
            this.cmbTargetLevel.Size = new System.Drawing.Size(93, 21);
            this.cmbTargetLevel.TabIndex = 1;
            this.cmbTargetLevel.SelectedIndexChanged += new System.EventHandler(this.cmbTargetLevel_SelectedIndexChanged);
            // 
            // txtLatency
            // 
            this.txtLatency.Location = new System.Drawing.Point(108, 31);
            this.txtLatency.Name = "txtLatency";
            this.txtLatency.Size = new System.Drawing.Size(93, 20);
            this.txtLatency.TabIndex = 2;
            this.txtLatency.TextChanged += new System.EventHandler(this.txtLatency_TextChanged);
            // 
            // lblLatency
            // 
            this.lblLatency.AutoSize = true;
            this.lblLatency.Location = new System.Drawing.Point(3, 34);
            this.lblLatency.Name = "lblLatency";
            this.lblLatency.Size = new System.Drawing.Size(48, 13);
            this.lblLatency.TabIndex = 3;
            this.lblLatency.Text = "Latency:";
            // 
            // chkMetagem
            // 
            this.chkMetagem.AutoSize = true;
            this.chkMetagem.Location = new System.Drawing.Point(6, 309);
            this.chkMetagem.Name = "chkMetagem";
            this.chkMetagem.Size = new System.Drawing.Size(178, 17);
            this.chkMetagem.TabIndex = 9;
            this.chkMetagem.Text = "Enforce Metagem Requirements";
            this.chkMetagem.UseVisualStyleBackColor = true;
            this.chkMetagem.CheckedChanged += new System.EventHandler(this.chkMetagem_CheckedChanged);
            // 
            // btnTalents
            // 
            this.btnTalents.Location = new System.Drawing.Point(6, 57);
            this.btnTalents.Name = "btnTalents";
            this.btnTalents.Size = new System.Drawing.Size(195, 23);
            this.btnTalents.TabIndex = 8;
            this.btnTalents.Text = "Talents";
            this.btnTalents.UseVisualStyleBackColor = true;
            this.btnTalents.Click += new System.EventHandler(this.btnTalents_Click);
            // 
            // CalculationOptionsPanelMoonkin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkMetagem);
            this.Controls.Add(this.btnTalents);
            this.Controls.Add(this.lblLatency);
            this.Controls.Add(this.txtLatency);
            this.Controls.Add(this.cmbTargetLevel);
            this.Controls.Add(this.lblTargetLevel);
            this.Name = "CalculationOptionsPanelMoonkin";
            this.Size = new System.Drawing.Size(204, 338);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTargetLevel;
        private System.Windows.Forms.ComboBox cmbTargetLevel;
        private System.Windows.Forms.TextBox txtLatency;
        private System.Windows.Forms.Label lblLatency;
        private System.Windows.Forms.CheckBox chkMetagem;
        private System.Windows.Forms.Button btnTalents;

    }
}

