using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;
using GadgeteerApp10.util;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace GadgeteerApp10.visual.panels {
    class PANEL_MainMenu : VisualPanel {

        public PANEL_MainMenu(Joystick joystick, DisplayN18 display, LEDStrip led) : base(joystick, display) {
            _led = led;

            ReserveElements(4);
            VisualTitle TITLE = AddTitle("Main Menu", _X_pd, _Y_pd);
            VisualButton BUTTON_TurnOn = AddButton("Turn on", _X_pd, TITLE.Bottom() + _Y_sp, TurnOn_clicked);
            VisualButton BUTTON_TurnOff = AddButton("Turn off", _X_pd, BUTTON_TurnOn.Bottom() +_Y_sp , TurnOff_clicked);
            VisualText TEXT_Quit = AddText("Back (esc)", _X_pd, display.Height - _Y_pd, VisualElement.Anchor.BottomLeft);

            BakeElements();

            BUTTON_TurnOn.SetAdjacent(null, null, BUTTON_TurnOff, null);
            BUTTON_TurnOff.SetAdjacent(BUTTON_TurnOn, null, null, null);

            VisualButton.SetActive(BUTTON_TurnOn);
            redrawInterface();
        }

        private void TurnOn_clicked(object o, EventArgs e) {
            _led.TurnLedOn(5);
        }

        private void TurnOff_clicked(object o, EventArgs e) {
            _led.TurnLedOff(5);
        }

        LEDStrip _led;
    }
}
