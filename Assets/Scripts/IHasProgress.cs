using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> onProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
