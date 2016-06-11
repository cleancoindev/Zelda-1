﻿// <copyright file="FixedPoisonSpellDamageMethod.cs" company="federrot Software">
//     Copyright (c) federrot Software. All rights reserved.
// </copyright>
// <summary>
//     Defines the Zelda.Attacks.FixedPoisonSpellDamageMethod class.
// </summary>
// <author>Paul Ennemoser (Tick)</author>

namespace Zelda.Attacks.Methods
{
    using Atom.Math;
    using Zelda.Saving;
    using Zelda.Status;
    using Zelda.Status.Damage;

    /// <summary>
    /// Defines an AttackDamageMethod that deals a fixed amount of
    /// magical fire spell damage.
    /// </summary>
    public sealed class FixedPoisonOverTimeSpellDamageMethod : AttackDamageMethod
    {
        /// <summary>
        /// Stores type information about the damage inflicted by the FixedPoisonSpellDamageMethod.
        /// </summary>
        private static readonly DamageTypeInfo DamageTypeInfo = DamageTypeInfo.Create(
            DamageSchool.Magical,
            DamageSource.Spell,
            ElementalSchool.Nature,
            SpecialDamageType.Poison | SpecialDamageType.DamagerOverTime
        );

        /// <summary>
        /// Gets or sets the damage range of the FixedPoisonSpellDamageMethod.
        /// </summary>
        public IntegerRange DamageRange
        {
            get;
            set;
        }

        /// <summary>
        /// Calculates the damage done by the <paramref name="user"/> on the <paramref name="target"/>
        /// using this <see cref="AttackDamageMethod"/>.
        /// </summary>
        /// <param name="user">The user of the attack.</param>
        /// <param name="target">The target of the attack.</param>
        /// <returns>The calculated result.</returns>
        public override AttackDamageResult GetDamageDone( Zelda.Status.Statable user, Zelda.Status.Statable target )
        {
            if( target.Resistances.TryResist( ElementalSchool.Nature ) )
                return AttackDamageResult.CreateResisted( DamageTypeInfo );

            int damage = this.DamageRange.GetRandomValue( this.rand );

            // Apply fixed modifiers:
            damage = target.DamageTaken.ApplyFixed( damage, DamageTypeInfo );

            // Apply multipliers:
            damage = target.DamageTaken.Apply( damage, DamageTypeInfo );

            return AttackDamageResult.CreateDamageOverTime( damage, DamageTypeInfo );
        }

        /// <summary>
        /// Serializes the data required to descripe this ISaveable.
        /// </summary>
        /// <param name="context">
        /// The context under which the serialization process takes place.
        /// Provides access to required objects.
        /// </param>
        public override void Serialize( Zelda.Saving.IZeldaSerializationContext context )
        {
            base.Serialize( context );

            const int CurrentVersion = 1;
            context.Write( CurrentVersion );

            // Data
            context.Write( this.DamageRange );
        }

        /// <summary>
        /// Deserializes the data required to descripe this ISaveable.
        /// </summary>
        /// <param name="context">
        /// The context under which the deserialization process takes place.
        /// Provides access to required objects.
        /// </param>
        public override void Deserialize( IZeldaDeserializationContext context )
        {
            base.Deserialize( context );

            const int CurrentVersion = 1;
            int version = context.ReadInt32();
            Atom.ThrowHelper.InvalidVersion( version, CurrentVersion, this.GetType() );

            // Data
            this.DamageRange = context.ReadIntegerRange();
        }
    }
}