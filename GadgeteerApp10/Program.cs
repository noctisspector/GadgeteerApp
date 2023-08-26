using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Hardware.UsbClient;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;
using GHI.Usb;
using GHI.Usb.Client;

using GadgeteerApp10.visual;
using GadgeteerApp10.visual.panels;

namespace GadgeteerApp10 {
    public partial class Program {

        void ProgramStarted() {
            _main_menu = new PANEL_MainMenu(joystick, display, led);

            GT.Timer timer = new GT.Timer(20);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(GT.Timer timer) {
            _main_menu.Update();
        }

        PANEL_MainMenu _main_menu;
    }
}
