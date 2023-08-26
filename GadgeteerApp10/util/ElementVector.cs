using System;
using Microsoft.SPOT;
using System.Collections;

using GadgeteerApp10.visual;

namespace GadgeteerApp10.util {
    class ElementVector {

        public ElementVector() {
            _data = new VisualElement[0];
            _size = 0;
        }

        public ElementVector(int capacity) {
            _data = new VisualElement[capacity];
            _size = 0;
        }

        public VisualElement this[int index] {
            get {
                VerifyIndex(index);
                return _data[index];
            }
            set {
                VerifyIndex(index);
                _data[index] = value;
            }
        }

        public VisualElement Add(VisualElement item) {
            UpdateCapacity(_size++);
            _data[_size - 1] = item;
            return item;
        }

        public int Size() {
            return _size;
        }

        private void UpdateCapacity(int min) {
            if (min < _data.Length) return;

            VisualElement[] tmp = new VisualElement[_data.Length * 2];
            Array.Copy(_data, tmp, _size);
            _data = tmp;
        }

        private void VerifyIndex(int index) {
            if (index < 0 || index >= _size)
                throw new IndexOutOfRangeException();
        }

        private VisualElement[] _data;
        private int _size;
    }
}
