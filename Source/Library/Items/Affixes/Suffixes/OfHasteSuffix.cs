﻿// <copyright file="OfHasteSuffix.cs" company="federrot Software">
//     Copyright (c) federrot Software. All rights reserved.
// </copyright>
// <summary>
//     Defines the Zelda.Items.Affixes.Suffixes.OfHasteSuffix class.
// </summary>
// <author>Paul Ennemoser</author>

namespace Zelda.Items.Affixes.Suffixes
{
    using Zelda.Status;

    /// <summary>
    /// The 'of Haste' suffix adds '2 + 70% item-level' attack speed rating to an Item.
    /// </summary>
    internal sealed class OfHasteSuffix : ISuffix
    {
        /// <summary>
        /// Gets the localized name of this IAffix.
        /// </summary>
        public string LocalizedName
        {
            get
            {
                return AffixResources.OfHaste; 
            }
        }

        /// <summary>
        /// Applies this IAffix to an Item.
        /// </summary>
        /// <param name="item">
        /// The Item that gets directly modified by this IAffix.
        /// </param>
        /// <param name="baseItem">
        /// The base non-modified Item.
        /// </param>
        public void Apply( Item item, Item baseItem )
        {
            var equipment = (Equipment)item;

            // Calculate:
            int attackSpeedRating = 2 + (int)(0.7f * item.Level);

            // Apply:
            var effect = equipment.AdditionalEffectsAura.GetEffect<AttackSpeedEffect>(
                (x) => x.ManipulationType == StatusManipType.Rating && 
                       x.AttackType == Zelda.Attacks.AttackType.All
            );

            if( effect != null )
            {
                effect.Value += attackSpeedRating;
            }
            else
            {
                equipment.AdditionalEffectsAura.AddEffect(
                    new AttackSpeedEffect( Zelda.Attacks.AttackType.All, attackSpeedRating, StatusManipType.Rating )
                );
            }

            // Make the item more blue:
            equipment.MultiplyColor( 0.75f, 0.75f, 1.00f );
        }

        /// <summary>
        /// Gets a value indicating whether this IAffix could
        /// possibly applied to the given base <see cref="Item"/>.
        /// </summary>
        /// <param name="baseItem">
        /// The item this IAffix is supposed to be applied to.
        /// </param>
        /// <returns>
        /// True if this IAffix could possible applied to the given <paramref name="baseItem"/>;
        /// otherwise false.
        /// </returns>
        public bool IsApplyable( Item baseItem )
        {
            var equipment = baseItem as Equipment;
            return equipment != null && 
                (equipment.Slot == EquipmentSlot.Ring || 
                 equipment.Slot == EquipmentSlot.Trinket ||
                 equipment.Slot == EquipmentSlot.Gloves ||
                 equipment.Slot == EquipmentSlot.Cloak || 
                 equipment.Slot == EquipmentSlot.WeaponHand);
        }
    }
}
