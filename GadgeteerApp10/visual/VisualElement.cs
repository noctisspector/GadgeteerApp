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
    public abstract class VisualElement {

        public enum Anchor {
            TopLeft = 0,
            BottomLeft = 1
        }

        public VisualElement(string text, int X, int Y, Anchor anchor = 0) {
            _X = X;
            _Y = Y;
            _text = text;
            _anchor = anchor;

            if (_compute_size) {
                _font.ComputeExtent(_text, out _size_X, out _size_Y);
                _size_X += _padding_X * 2;
                _size_Y += _padding_Y * 2;
            }

            if (anchor == Anchor.BottomLeft)
                _Y -= _size_Y;
        }

        public void Draw(DisplayN18 display) {
            // This is needed to prevent the chip from freezing up and not accepting new assemblies
            if (_X + _size_X >= display.Width || _Y + _size_Y >= display.Height) 
                throw new InvalidOperationException("Invalid element position.");

            display.Draw(Enabled() ? _img_enabled : _img_disabled, _X, _Y);
        }

        public void Bake() {
            _img_disabled = BakeColor(_color_disabled);
            _img_enabled = BakeColor(_color_enabled);
        }

        private Bitmap BakeColor(GT.Color color) {
            /*Bitmap bmp = new Bitmap(_size_X + 10, _size_Y + 10);

            if (_draw_border) {
                bmp.DrawLine(color, 1, 0, 0, _size_X, 0);
                bmp.DrawLine(color, 1, 0, 0, 0, _size_Y);
                bmp.DrawLine(color, 1, _size_X, 0, _size_X, _size_Y);
                bmp.DrawLine(color, 1, 0, _size_Y, _size_X, _size_Y);
            }

            if (_draw_text) {
                bmp.DrawText(_text, _font, color, _padding_X, _padding_Y);
            }
            
            return bmp;*/
        }

        public int Right() {
            return _size_X + _X;
        }

        public int Bottom() {
            return _size_Y + _Y;
        }

        protected virtual bool Enabled() {
            return true;
        }

        // Properties
        protected string _text;
        protected int _X;
        protected int _Y;

        protected int _size_X = 0;
        protected int _size_Y = 0;
        protected Font _font = Resources.GetFont(Resources.FontResources.small);
        protected int _padding_X = 4;
        protected int _padding_Y = 2;
        protected Anchor _anchor = Anchor.TopLeft;

        protected bool _draw_border = false;
        protected bool _draw_text = false;
        protected bool _compute_size = true;

        protected GT.Color _color_disabled = GT.Color.DarkGray;
        protected GT.Color _color_enabled = GT.Color.White;

        private Bitmap _img_disabled;
        private Bitmap _img_enabled;
    }
}
