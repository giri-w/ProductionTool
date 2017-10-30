//-----------------------------------------------------------------------
// <copyright file="EventArgs.cs" company="Finapres Medical Systems B.V.">
//     Copyright (C) 2010 Finapres Medical Systems B.V. (Finapres). All Rights Reserved.
//     Reproduction or disclosure of this file or its contents without the prior
//     written consent of Finapres is prohibited.
// </copyright>
// <disclaimer>
//     The software is provided 'as is' without any guarantees or warranty. Although
//     Finapres has attempted to find and correct any bugs in the software, Finapres is not
//     responsible for any damage or losses of any kind caused by the use or misuse
//     of the software.
// </disclaimer>
//-----------------------------------------------------------------------
using System;

namespace Demcon.ProductionTool.General
{

    /// <summary>
    /// Default Event args
    /// </summary>
    public class EventArgs<T> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the EventArgs class
        /// </summary>
        /// <param name="value">The value</param>
        public EventArgs(T value)
        {
            Value = value;
        }

        public T Value
        {
            get;
            set;
        }
    }
}