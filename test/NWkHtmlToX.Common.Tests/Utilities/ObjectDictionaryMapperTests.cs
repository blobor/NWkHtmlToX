using System;
using System.Collections.Generic;
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
                TestDateTime = DateTime.Now,
                TestInnerObject = new {
                    TestString = "ABC",
                    TestInt32 = 11
                }
            };
            IDictionary<string, string> expectedResult = new Dictionary<string, string> {
                ["testString"] =  "ABC",
                ["testInt32"] = "11",
                ["testDateTime"] = DateTime.Now.ToString(),
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