﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Ione.Framework.Core
{
    public abstract class EnumPolymorphic<T, TEnum> : IComparable, IConvertible
        where T : struct, IComparable<T>, IConvertible
        where TEnum : EnumPolymorphic<T, TEnum>, new()
    {
        private static bool namesInitialized = false;
        private static Dictionary<T, TEnum> registeredInstances = new Dictionary<T, TEnum>();

        public T Ordinal { get; private set; }
        protected object Data { get; private set; }

        private string name = null;
        private string Name
        {
            get
            {
                EnsureNamesInitialized();
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        protected EnumPolymorphic()
        {
        }

        protected void EnsureNamesInitialized()
        {
            if (!namesInitialized)
            {
                MemberInfo[] enumMembers = typeof(TEnum).GetMembers(
                        BindingFlags.Public
                        | BindingFlags.Static
                        | BindingFlags.GetField);

                foreach (FieldInfo enumMember in enumMembers)
                {
                    TEnum enumValue = enumMember.GetValue(null) as TEnum;

                    if (enumValue != null)
                        enumValue.Name = enumMember.Name;
                }
                namesInitialized = true;
            }
        }

        protected static TEnum Register(Nullable<T> ordinal = null, object data = null)
        {
            return Register<TEnum>(ordinal, data);
        }

        protected static TEnum Register<TEnumInstance>(Nullable<T> ordinal = null, object data = null)
            where TEnumInstance : TEnum, new()
        {
            StackFrame frame = new StackFrame(1);
            if (frame.GetMethod().Name == "Register")
                frame = new StackFrame(2);

            MethodBase enumConstructor = frame.GetMethod();
            if (enumConstructor.DeclaringType != typeof(TEnum))
                throw new EnumInitializationException("Enum members cannot be registered from other enums.");

            if (!ordinal.HasValue)
            {
                ordinal = registeredInstances.Any()
                    ? registeredInstances.Keys.Max().PlusOne()
                    : default(T);
            }

            TEnum instance = new TEnumInstance();
            instance.Ordinal = ordinal.Value;
            instance.Data = data;

            registeredInstances.Add(ordinal.Value, instance);

            namesInitialized = false;

            return instance;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public int CompareTo(object target)
        {
            EnumPolymorphic<T, TEnum> typedTarget = target as EnumPolymorphic<T, TEnum>;

            if (typedTarget == null)
                throw new ArgumentException("Comparison can only occur between compatible enums.", "target");

            return this.Ordinal.CompareTo(typedTarget.Ordinal);
        }

        public static implicit operator T(EnumPolymorphic<T, TEnum> x)
        {
            return x.Ordinal;
        }

        public static explicit operator EnumPolymorphic<T, TEnum>(T x)
        {
            TEnum enumInstance;
            if (!registeredInstances.TryGetValue(x, out enumInstance))
                throw new ArgumentException(
                    string.Format("PolymorphicEnum value {0} not found", x, "x"));

            return enumInstance;
        }

        public static bool TryParse(string value, out TEnum result)
        {
            return TryParse(value, false, out result);
        }

        public static bool TryParse(string value,
                                    bool ignoreCase,
                                    out TEnum result)
        {
            TEnum[] instances = registeredInstances
                .Values
                .Where(
                    e => e.Name.Equals(
                        value,
                        ignoreCase
                            ? StringComparison.InvariantCultureIgnoreCase
                            : StringComparison.InvariantCulture))
                .ToArray();

            if (instances.Length == 1)
            {
                result = instances[0];
                return true;
            }
            else
            {
                result = default(TEnum);
                return false;
            }
        }

        public static TEnum Parse(string value)
        {
            return Parse(value, false);
        }

        public static TEnum Parse(string value, bool ignoreCase)
        {
            TEnum result;

            if (!TryParse(value, ignoreCase, out result))
                throw new ArgumentException(string.Format("PolymorphicEnum value {0} not found", value, "value"));

            return result;
        }

        public static IEnumerable<TEnum> GetValues()
        {
            return registeredInstances.Values.ToArray();
        }

        #region Consistency enforcement

        private bool IsRegistered
        {
            get { return registeredInstances.Values.Contains(this); }
        }

        protected TEnum Checked(TEnum value)
        {
            if (!value.IsRegistered)
                throw new EnumUnregisteredException(
                    "This enum is not registered");

            return value;
        }

        protected void Checked(Action a)
        {
            if (!IsRegistered)
                throw new EnumUnregisteredException(
                    "This enum is not registered");

            a.Invoke();
        }

        protected TReturn Checked<TReturn>(Func<TReturn> f)
        {
            if (!IsRegistered)
                throw new EnumUnregisteredException(
                    "This enum is not registered");

            return f.Invoke();
        }

        #endregion

        #region IConvertible

        TypeCode IConvertible.GetTypeCode()
        {
            return this.Ordinal.GetTypeCode();
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return this.Ordinal.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return this.Ordinal.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return this.Ordinal.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return this.Ordinal.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return this.Ordinal.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return this.Ordinal.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return this.Ordinal.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return this.Ordinal.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return this.Ordinal.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return this.Ordinal.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return this.Ordinal.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return this.Ordinal.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return this.Ordinal.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return this.Ordinal.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return this.Ordinal.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return this.Ordinal.ToUInt64(provider);
        }

        #endregion
    }

    public abstract class EnumPolymorphic<TEnum> : EnumPolymorphic<int, TEnum>
        where TEnum : EnumPolymorphic<int, TEnum>, new()
    {
    }
}
