﻿// ----------------------------------------------------
// <copyright file="StatusEventArg.cs" company="StellerJay Enterprises, LLC." >
// Copyright (C) 2010 Rick A. Eichhorn. 
// </copyright>
// ----------------------------------------------------

using System;
using System.Drawing;

namespace Ione.Framework.Core.Events
{
    /// <summary>
    /// Passes the cropping rectangle and current cursor position to the host object
    /// </summary>
    public class StatusEventArg : EventArgs
    {
        public readonly Rectangle CroppingRect;
        public readonly Point CursorPos;

        public StatusEventArg(Point pos, Rectangle crop)
        {
            CursorPos = pos;
            CroppingRect = crop;
        }
    }
}
