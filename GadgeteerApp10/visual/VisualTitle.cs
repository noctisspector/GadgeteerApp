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
    public class VisualTitle : VisualText {

        public VisualTitle(string text, int X, int Y, Anchor anchor = 0) : base(text, X, Y, anchor) {
            _font = Resources.GetFont(Resources.FontResources.NinaB);
        }
    }
}
