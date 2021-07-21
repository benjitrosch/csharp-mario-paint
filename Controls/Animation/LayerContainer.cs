﻿using Pixel_Editor_Test_2.Controls.Events;
using Pixel_Editor_Test_2.Systems;
using Pixel_Editor_Test_2.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Editor_Test_2.Controls.Animation
{
    public partial class LayerContainer : EditorControl
    {
        public Layer Layer { get; private set; }
        public int LayerIndex { get; private set; }

        public LayerContainer(Layer layer, int layerIndex)
        {
            InitializeComponent();

            textLayerName.AutoSize = false;
            textLayerName.Size = new Size(64, 32);

            Layer = layer;
            LayerIndex = layerIndex;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            Session.Instance.Animation.OnAddKeyframe += (_o, f) => AddKeyframe(f);
            Session.Instance.OnActiveLayerChange += (_o, l) => CheckLayerSelection(l);

            RefreshKeyframes();
            UpdateTheme();
        }

        protected override void UpdateTheme()
        {
            frameLayout.BackColor = buttonVisibility.BackColor = Themes.ANIMATOR_COLOR;
            BackColor = divider.BackColor = Themes.TEXT_COLOR;

            CheckLayerSelection(Session.Instance.ActiveLayer);
        }

        private void RefreshKeyframes()
        {
            frameLayout.Controls.Clear();

            int i = 0;
            foreach (Image frame in Layer.Frames)
            {
                Keyframe newKeyframe = new Keyframe(i, LayerIndex);

                newKeyframe.Click += new EventHandler(ClickKeyframe);
                frameLayout.Controls.Add(newKeyframe);
                newKeyframe.Initialize();
                i++;
            }
        }

        public void AddKeyframe(KeyframeAddedEventArgs e)
        {
            Keyframe newKeyframe = new Keyframe(e.FrameIndex, LayerIndex);

            newKeyframe.Click += new EventHandler(ClickKeyframe);
            frameLayout.Controls.Add(newKeyframe);
        }

        private void ClickKeyframe(object sender, EventArgs e)
        {
            Keyframe keyframe = (Keyframe)sender;
            Session.Instance.Animation.GotoFrame(keyframe.FrameIndex);
            Session.Instance.SetActiveLayer(LayerIndex);
        }

        private void CheckLayerSelection(int layerIndex)
        {
            if (LayerIndex == layerIndex)
                textLayerName.BackColor = buttonVisibility.BackColor = Themes.BUTTON_HIGHLIGHT_COLOR;
            else
                textLayerName.BackColor = buttonVisibility.BackColor = Themes.ANIMATOR_COLOR;
        }
    }
}
