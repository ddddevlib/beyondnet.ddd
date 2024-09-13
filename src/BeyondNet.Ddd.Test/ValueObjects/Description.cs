﻿
using BeyondNet.Ddd.ValueObjects;

namespace BeyondNet.Ddd.ValueObjects
{
    public class Description : StringRequiredValueObject
    {
        protected Description(string value) : base(value)
        {
        }

        public static new Description Create(string value)
        {
            return new Description(value);
        }

        public static Description DefaultValue => new Description(string.Empty);
    }
}
