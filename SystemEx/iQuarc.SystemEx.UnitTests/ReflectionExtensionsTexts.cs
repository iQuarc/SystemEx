using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using iQuarc.xUnitEx;
using Xunit;

namespace iQuarc.SystemEx.UnitTests
{
    public class ReflectionExtensionsTexts
    {
        [Fact]
        public void GetAttributeValue_AttributeExists_ValueReturned()
        {
            var providerStub = GetProviderStub(new SomeType("some value"));

            string actualValue = providerStub.GetAttributeValue<SomeType, string>(a => a.Value);

            Assert.Equal("some value", actualValue);
        }

        [Fact]
        public void GetAttributeValue_AttributeDoesNotExist_DefaultReturned()
        {
            var providerStub = GetProviderStub();

            string actualValue = providerStub.GetAttributeValue<SomeType, string>(a => a.Value, "default");

            Assert.Equal("default", actualValue);
        }

        private static ICustomAttributeProvider GetProviderStub(SomeType attribute = null)
        {
            object[] attributes = attribute != null ? new object[] {attribute} : new object[] {};

            Mock<ICustomAttributeProvider> providerStub = new Mock<ICustomAttributeProvider>();
            providerStub.Setup(p => p.GetCustomAttributes(It.IsAny<Type>(), It.IsAny<bool>()))
                        .Returns(attributes);
            return providerStub.Object;
        }

        [Fact]
        public void GetEditableProperties_TypeWithMoreTypesOfProperties_OnlyEditableReturned()
        {
            IEnumerable<PropertyInfo> properties = ReflectionExtensions.GetEditableProperties(new SomeType());

            var propNames = properties.Select(p => p.Name);
            AssertEx.AreEquivalent(propNames, "SimpleEditableProp", "ComplexEditableProp", "EnumEditableProp", "StringEditableProp");
        }

        [Fact]
        public void GetEditableSimpleProperties_TypeWithMoreTypesOfProperties_OnlySimpleEditablePropertiesReturned()
        {
            IEnumerable<PropertyInfo> properties = ReflectionExtensions.GetEditableSimpleProperties(new SomeType());

            var propNames = properties.Select(p => p.Name);
            AssertEx.AreEquivalent(propNames, "SimpleEditableProp", "EnumEditableProp", "StringEditableProp");
        }

        private class SomeType
        {
            public SomeType()
            {
            }

            public SomeType(string value)
            {
                Value = value;
            }

            public string Value { get; private set; }

            public int SimpleEditableProp { get; set; }

            public object ComplexEditableProp { get; set; }

            public DayOfWeek EnumEditableProp { get; set; }

            public string StringEditableProp { get; set; }
        }
    }
}