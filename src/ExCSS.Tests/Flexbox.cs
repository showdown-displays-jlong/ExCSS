﻿using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace ExCSS.Tests
{
    public class Flexbox : CssConstructionFunctions
    {
        [Theory]
        [MemberData(nameof(FlexDirectionTestDataValues))]
        public void FlexDirectionLegalValues(string value)
            => TestForLegalValue<FlexDirectionProperty>(PropertyNames.FlexDirection, value);

        [Theory]
        [MemberData(nameof(FlexWrapTestDataValues))]
        public void FlexWrapLegalValues(string value)
            => TestForLegalValue<FlexWrapProperty>(PropertyNames.FlexWrap, value);

        [Theory]
        [MemberData(nameof(OrderTestDataValues))]
        public void OrderLegalValues(string value)
            => TestForLegalValue<OrderProperty>(PropertyNames.Order, value);

        [Theory]
        [MemberData(nameof(FlexBasisTestDataValues))]
        public void FlexBasisLegalValues(string value)
            => TestForLegalValue<FlexBasisProperty>(PropertyNames.FlexBasis, value);

        [Theory]
        [MemberData(nameof(FlexGrowShrinkTestDataValues))]
        public void FlexGrowLegalValues(string value)
            => TestForLegalValue<FlexGrowProperty>(PropertyNames.FlexGrow, value);

        [Theory]
        [MemberData(nameof(FlexGrowShrinkTestDataValues))]
        public void FlexShrinkLegalValues(string value)
            => TestForLegalValue<FlexShrinkProperty>(PropertyNames.FlexShrink, value);

        [Theory]
        [MemberData(nameof(AlignContentTestDataValues))]
        public void AlignContentLegalValues(string value)
            => TestForLegalValue<AlignContentProperty>(PropertyNames.AlignContent, value);

        [Theory]
        [MemberData(nameof(AlignContentInvalidPrefixTestDataValues))]
        public void AlignContentIllegalPrefixValues(string value)
        {
            var snippet = $"align-content: {value}";
            var property = ParseDeclaration(snippet);
            Assert.Equal("align-content", property.Name);
            Assert.False(property.HasValue);
        }

        public static IEnumerable<object[]> FlexDirectionTestDataValues
            => FlexDirectionProperty.KeywordValues.ToObjectArray();

        public static IEnumerable<object[]> FlexWrapTestDataValues
            => FlexWrapProperty.KeywordValues.ToObjectArray();

        public static IEnumerable<object[]> AlignContentTestDataValues
        {
            get
            {
                return new[]
                {
                    new object[] { Keywords.Center },
                    new object[] { Keywords.Start },
                    new object[] { Keywords.End },
                    new object[] { Keywords.FlexStart },
                    new object[] { Keywords.FlexEnd },
                    new object[] { Keywords.Normal },
                    new object[] { Keywords.Baseline },
                    new object[] { $"{Keywords.First} {Keywords.Baseline}" },
                    new object[] { $"{Keywords.Last} {Keywords.Baseline}" },
                    new object[] { Keywords.SpaceBetween },
                    new object[] { Keywords.SpaceAround },
                    new object[] { Keywords.SpaceEvenly },
                    new object[] { Keywords.Stretch },
                    new object[] { $"{Keywords.Safe} {Keywords.Center}" },
                    new object[] { $"{Keywords.Unsafe} {Keywords.Center}" },
                }.Union(Property.GlobalKeywordValues.ToObjectArray());
            }
        }

        public static IEnumerable<object[]> AlignContentInvalidPrefixTestDataValues
        {
            get
            {
                return new[]
                {
                    new object[] { $"{Keywords.Safe} {Keywords.Start}" },
                    new object[] { $"{Keywords.Safe} {Keywords.End}" },
                    new object[] { $"{Keywords.Safe} {Keywords.FlexStart}" },
                    new object[] { $"{Keywords.Safe} {Keywords.FlexEnd}" },

                    new object[] { $"{Keywords.Unsafe} {Keywords.Start}" },
                    new object[] { $"{Keywords.Unsafe} {Keywords.End}" },
                    new object[] { $"{Keywords.Unsafe} {Keywords.FlexStart}" },
                    new object[] { $"{Keywords.Unsafe} {Keywords.FlexEnd}" },

                    new object[] { $"{Keywords.First} {Keywords.Start}" },
                    new object[] { $"{Keywords.First} {Keywords.End}" },
                    new object[] { $"{Keywords.First} {Keywords.FlexStart}" },
                    new object[] { $"{Keywords.First} {Keywords.FlexEnd}" },
                
                    new object[] { $"{Keywords.Last} {Keywords.Start}" },
                    new object[] { $"{Keywords.Last} {Keywords.End}" },
                    new object[] { $"{Keywords.Last} {Keywords.FlexStart}" },
                    new object[] { $"{Keywords.Last} {Keywords.FlexEnd}" },
                };
            }
        }

        public static IEnumerable<object[]> FlexGrowShrinkTestDataValues
        {
            get
            {
                return new[]
                {
                    new object[] { "3" },
                    new object[] { "0.6" }
                }.Union(Property.GlobalKeywordValues.ToObjectArray());
            }
        }

        public static IEnumerable<object[]> FlexBasisTestDataValues
        {
            get
            {
                return new[]
                {
                    new object[] { "10em" },
                    new object[] { "3px" },
                    new object[] { "50%" }
                }.Union(FlexBasisProperty.KeywordValues.Union(Property.GlobalKeywordValues).ToObjectArray());
            }
        }

        public static IEnumerable<object[]> OrderTestDataValues
        {
            get
            {
                return new[]
                {
                    new object[] { "-1" },
                    new object[] { "1" },
                }.Union(Property.GlobalKeywordValues.ToObjectArray());
            }
        }

        private void TestForLegalValue<TProp>(string propertyName, string value) where TProp : Property
        {
            var snippet = $"{propertyName}: {value}";
            var property = ParseDeclaration(snippet);
            Assert.Equal(propertyName, property.Name);
            Assert.False(property.IsImportant);
            Assert.IsType<TProp>(property);
            var concrete = (TProp)property;
            Assert.True(concrete.HasValue);
            Assert.Equal(value, concrete.Value);
        }
    }
}
