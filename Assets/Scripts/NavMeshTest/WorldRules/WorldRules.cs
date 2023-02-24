using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NavMeshTest.WorldRules
{
    [CreateAssetMenu(fileName = "WorldRules", menuName = "Create WorldRules", order = 51)]
    public class WorldRules : ScriptableObject
    {
        [TabGroup("Attack Speed")] [SerializeField]
        private float VerySlow = 4f;

        [TabGroup("Attack Speed")] [SerializeField]
        private float Slow = 2f;

        [TabGroup("Attack Speed")] [SerializeField]
        private float Normal = 1f;

        [TabGroup("Attack Speed")] [SerializeField]
        private float Fast = 0.75f;

        [TabGroup("Attack Speed")] [SerializeField]
        private float VeryFast = 0.5f;

        [TabGroup("Attack Difference")] [SerializeField]
        private float MaxDamage = 1.5f;

        [TabGroup("Attack Difference")] [SerializeField]
        private float GreatDamage = 1.35f;

        [TabGroup("Attack Difference")] [SerializeField]
        private float BigDamage = 1.2f;

        [TabGroup("Attack Difference")] [SerializeField]
        private float SmallDamage = 0.8f;

        [TabGroup("Attack Difference")] [SerializeField]
        private float MinorDamage = 0.6f;

        [TabGroup("Attack Difference")] [SerializeField]
        private float MinDamage = 0.35f;

        public float GetAttackSpeed(AttackSpeed attackSpeed) =>
            attackSpeed switch
            {
                AttackSpeed.VerySlow => VerySlow,
                AttackSpeed.Slow => Slow,
                AttackSpeed.Normal => Normal,
                AttackSpeed.Fast => Fast,
                AttackSpeed.VeryFast => VeryFast,
                _ => throw new NotImplementedException("No such Attack speed"),
            };

        public float GetDamageCoefficient(ArmorType armorType, AttackType attackType) =>
            (armorType, attackType) switch
            {
                (ArmorType.AbsoluteArmor, AttackType.Absolute) => 1f,
                (ArmorType.AbsoluteArmor, var x) when x == AttackType.Energy || x == AttackType.Dark ||
                                                      x == AttackType.Light => MinorDamage,
                (ArmorType.AbsoluteArmor, _) => MinDamage,
                (_, AttackType.Absolute) => GreatDamage,
                (ArmorType.NoArmor, AttackType.Common) => 1f,
                (_, AttackType.Common) => SmallDamage,
                (ArmorType.NoArmor, _) => BigDamage,
                (ArmorType.ElementalArmor, var x) when x == AttackType.Fire || x == AttackType.Water ||
                                                       x == AttackType.Wind || x == AttackType.Earth => 1f,
                (ArmorType.ElementalArmor, AttackType.Energy) => BigDamage,
                (ArmorType.ElementalArmor, _) => SmallDamage,
                (ArmorType.EnergyArmor, AttackType.Energy) => 1f,
                (ArmorType.EnergyArmor, var x) when x == AttackType.Fire || x == AttackType.Water ||
                                                    x == AttackType.Wind || x == AttackType.Earth => BigDamage,
                (ArmorType.EnergyArmor, _) => SmallDamage,
                (_, var x) when x == AttackType.Fire || x == AttackType.Water ||
                                x == AttackType.Wind || x == AttackType.Earth => 1f,
                (ArmorType.LightArmor, AttackType.Dark) => MaxDamage,
                (ArmorType.DarkArmor, AttackType.Light) => MaxDamage,
                (_, var x) when x == AttackType.Light || x == AttackType.Dark ||
                                x == AttackType.Energy => SmallDamage,
                _ => throw new NotImplementedException($"No such pair ({armorType} {attackType})"),
            };
    }

    public enum AttackSpeed
    {
        VerySlow,
        Slow,
        Normal,
        Fast,
        VeryFast
    }

    public enum ArmorType
    {
        NoArmor,
        ElementalArmor,
        EnergyArmor,
        LightArmor,
        DarkArmor,
        AbsoluteArmor
    }

    public enum AttackType
    {
        Common,
        Fire,
        Water,
        Wind,
        Earth,
        Light,
        Dark,
        Energy,
        Absolute
    }
}