using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace GadgeteerApp10.visual {
    public class VisualButton : VisualElement {

        public VisualButton(string text, int X, int Y, Anchor anchor = 0) : base(text, X, Y, anchor) {
            _draw_border = true;
            _draw_text = true;
        }

        public void SetAdjacent(VisualButton top, VisualButton right, VisualButton bottom, VisualButton left) {
            _top = top;
            _rig = right;
            _bot = bottom;
            _lef = left;
        }

        protected new bool Enabled() {
            return this == _ActiveButton;
        }

        // Event handlers
        public event EventHandler ButtonClicked;
        public event EventHandler ButtonReleased;

        // Adjacent buttons
        private VisualButton _top;
        private VisualButton _rig;
        private VisualButton _bot;
        private VisualButton _lef;

        public static bool SetActive(VisualButton button) {
            bool r = _ActiveButton != button;
            _ActiveButton = button;
            return r;
        }

        public static bool SetActive(Joystick.Position dir) {
            bool r = false;
            r |= SetActiveIf(_ActiveButton._rig, dir.X > _min_joystick_threshold);
            r |= SetActiveIf(_ActiveButton._top, dir.Y > _min_joystick_threshold);
            r |= SetActiveIf(_ActiveButton._lef, dir.X < -_min_joystick_threshold);
            r |= SetActiveIf(_ActiveButton._bot, dir.Y < -_min_joystick_threshold);
            return r;
        }

        private static bool SetActiveIf(VisualButton button, bool cond) {
            bool r = (button != null && cond);
            if (r) r &= SetActive(button);
            return r;
        }

        private static void _click_handler(Joystick sender, Joystick.ButtonState state) {
            if (_ActiveButton != null && _ActiveButton.ButtonClicked != null) {
                _ActiveButton.ButtonClicked.Invoke(_ActiveButton, null);
            }
        }

        private static void _release_handler(Joystick sender, Joystick.ButtonState state) {
            if (_ActiveButton != null && _ActiveButton.ButtonReleased != null) {
                _ActiveButton.ButtonReleased.Invoke(_ActiveButton, null);
            }
        }

        public static void LinkJoystick(Joystick joystick) {
            joystick.JoystickPressed += _click_handler;
            joystick.JoystickReleased += _release_handler;
        }

        private static VisualButton _ActiveButton;
        private const double _min_joystick_threshold = 0.3;
    }
}
