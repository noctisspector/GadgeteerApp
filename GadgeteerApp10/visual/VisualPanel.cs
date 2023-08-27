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
            double len = System.Math.Abs(_joystick.GetPosition().X) + System.Math.Abs(_joystick.GetPosition().Y);
            if (len > _joystick_threshold && VisualButton.SetActive(_joystick.GetPosition())) {
                redrawInterface();
            }
        }

        protected void ReserveElements(int number) {
            _elements = new ElementVector(number);
        }

        protected void BakeElements() {
            for (int i = 0; i < _elements.Size(); i++)
                _elements[i].Bake();
        }

        protected void redrawInterface() {
            for (int i = 0; i < _elements.Size(); i++)
                _elements[i].Draw(_display);
        }

        // This framework doesn't support generic types
        protected ElementVector _elements;

        protected Joystick _joystick;
        protected DisplayN18 _display;

        protected const int _X_pd = 4;
        protected const int _Y_pd = 4;
        protected const int _X_sp = 4;
        protected const int _Y_sp = 4;

        private const double _joystick_threshold = 0.8;

        // Methods for adding specific elements

        protected VisualButton AddButton(string text, int X, int Y, EventHandler clicked, VisualElement.Anchor anchor = 0) {
            VisualButton tmp = new VisualButton(text, X, Y, anchor);
            tmp.ButtonClicked += clicked;
            _elements.Add(tmp);
            return tmp;
        }

        protected VisualText AddText(string text, int X, int Y, VisualElement.Anchor anchor = 0) {
            VisualText tmp = new VisualText(text, X, Y, anchor);
            _elements.Add(tmp);
            return tmp;
        }

        protected VisualTitle AddTitle(string text, int X, int Y, VisualElement.Anchor anchor = 0) {
            VisualTitle tmp = new VisualTitle(text, X, Y, anchor);
            _elements.Add(tmp);
            return tmp;
        }
    }
}
