﻿// <copyright file="LocalizedCategoryAttribute.cs" company="federrot Software">
//     Copyright (c) federrot Software. All rights reserved.
// </copyright>
// <summary>Defines the Zelda.NpcCreator.Design.LocalizedCategoryAttribute class.</summary>
// <author>Paul Ennemoser</author>

namespace Zelda.NpcCreator
{
    using System.ComponentModel;

    /// <summary>
    /// Defines a localized CategoryAttribute
    /// that uses the <see cref="Zelda.NpcCreator.Properties.Resources"/> resource manager
    /// to lock-up resource information. This is a sealed class.
    /// </summary>
    internal sealed class LocalizedCategoryAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedCategoryAttribute"/> class.
        /// </summary>
        /// <param name="resourceName">The name of the string resource that is locked-up.</param>
        public LocalizedCategoryAttribute( string resourceName )
            : base( Properties.Resources.ResourceManager.GetString( resourceName ) )
        {
        }
    }
}
