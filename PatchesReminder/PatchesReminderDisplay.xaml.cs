﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Utility.Logging;

namespace PatchesReminder
{
    /// <summary>
    /// Interaction logic for PatchesReminderDisplay.xaml
    /// </summary>
    public partial class PatchesReminderDisplay : UserControl
    {
        public PatchesReminderDisplay()
        {
            InitializeComponent();
            UpdatePosition();
            Hide();
        }

        public void UpdatePosition()
        {
            //GIANT text across opponents battlefield, perhaps image also?
            Canvas.SetRight(this, Core.OverlayWindow.Width / 2 + Width / 2);
            Canvas.SetTop(this, Core.OverlayWindow.Height * 50 / 100);
        }

        public void Show()
        {
            UpdatePosition();
            Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            Visibility = Visibility.Hidden;
        }

        public string DisplayText
        {
            get => displayText.Text;
            set => displayText.Text = value;
        }
    }
}
