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

namespace GadgeteerApp10.visual {
    abstract class VisualPanel {

        public VisualPanel(Joystick joystick, DisplayN18 display) {
            _joystick = joystick;
            _display = display;

            VisualButton.LinkJoystick(_joystick);
            _display.Clear();
        }

        public void Update() {
            double len = _joystick.GetPosition().X * _joystick.GetPosition().X + _joystick.GetPosition().Y * _joystick.GetPosition().Y;
            if (len > _joystick_threshold && VisualButton.SetActive(_joystick.GetPosition())) {
                redrawInterface();
            }
        }

        protected void ReserveElements(int number) {
            _elements = new ElementVector(number);
        }

        protected VisualButton AddButton(string text, int X, int Y, EventHandler clicked) {
            VisualButton tmp = new VisualButton(text, X, Y);
            tmp.ButtonClicked += clicked;
            _elements.Add(tmp);
            return tmp;
        }

        protected VisualText AddText(string text, int X, int Y) {
            return new VisualText(text, X, Y);
        }

        protected VisualTitle AddTitle(string text, int X, int Y) {
            return new VisualTitle(text, X, Y);
        }

        protected void redrawInterface() {
            _visual_buffer++;
            if (_visual_buffer > _visual_buffer_threshold) {
                _display.Clear();
                _visual_buffer = 0;
            }

            for (int i = 0; i < _elements.Size(); i++)
                _elements[i].Draw(_display);
        }

        protected ElementVector _elements;
        protected Joystick _joystick;
        protected DisplayN18 _display;
        private int _visual_buffer;

        protected const int _X_pd = 4;
        protected const int _Y_pd = 4;
        protected const int _X_sp = 4;
        protected const int _Y_sp = 4;

        private const double _joystick_threshold = 0.8 * 0.8;
        private const int _visual_buffer_threshold = 10;

        //private VisualPanel _prev_panel = null;

        //public void SwitchPanel(VisualPanel next) {
        //    _ActivePanel = next;
        //    _ActivePanel._prev_panel = this;
        //}

        //private static VisualPanel _ActivePanel;
    }
}
