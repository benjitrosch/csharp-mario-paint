﻿using Pixel_Editor_Test_2.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Editor_Test_2.Controls
{
    public partial class ActiveColors : UserControl
    {
        public ActiveColors()
        {
            InitializeComponent();
        }

        private void ActiveColors_Load(object sender, EventArgs e)
        {
            Session.Instance.OnPrimaryColorChange += PrimaryColorChange;
            Session.Instance.OnSecondaryColorChange += SecondaryColorChange;
        }

        private void PrimaryColorChange(object sender, Color color)
        {
            buttonPrimaryColor.BackColor = color;
            buttonPrimaryColor.Text = color.R + ", " + color.G + ", " + color.B;
        }

        private void SecondaryColorChange(object sender, Color color)
        {
            buttonSecondaryColor.BackColor = color;
            buttonSecondaryColor.Text = color.R + ", " + color.G + ", " + color.B;
        }

        private void buttonSavePrimaryColor_Click(object sender, EventArgs e)
        {
            Session.Instance.SaveColorToPalette(buttonPrimaryColor.BackColor);
        }

        private void buttonSaveSecondaryColor_Click(object sender, EventArgs e)
        {
            Session.Instance.SaveColorToPalette(buttonSecondaryColor.BackColor);
        }
    }
}