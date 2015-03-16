using System;
using System.Collections.Generic;
using System.Globalization;
using NWkHtmlToX.Common.Utilities;
using Xunit;

namespace NWkHtmlToX.Common.Tests.Utilities {
    public class ObjectDictionaryMapperTests {

        [Fact]
        public void GetDictionary_ShouldReturnDictionaryOfObjectProperties() {

            // Arrange
            var obj = new {
                TestString = "ABC",
                TestInt32 = 11,
                TestNullableInt32 = (int?) null,
                TestNullableBool = (bool?) false,
                TestDateTime = DateTime.Now,
                TestInnerObject = new {
                    TestString = "ABC",
                    TestInt32 = 11
                }
            };
            IDictionary<string, string> expectedResult = new Dictionary<string, string> {
                ["testString"] =  "ABC",
                ["testInt32"] = "11",
                ["testNullableBool"] = "false",
                ["testDateTime"] = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                ["testInnerObject.testString"] = "ABC",
                ["testInnerObject.testInt32"] = "11"
            };

            // Act
            var actualResult = ObjectDictionaryMapper.GetDictionary(obj);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}