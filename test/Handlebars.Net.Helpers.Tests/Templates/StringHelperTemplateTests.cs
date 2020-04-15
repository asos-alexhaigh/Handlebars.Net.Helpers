﻿using System;
using FluentAssertions;
using HandlebarsDotNet;
using HandlebarsDotNet.Helpers;
using Xunit;

namespace Handlebars.Net.Helpers.Tests.Templates
{
    public class StringHelperTemplateTests
    {
        private readonly IHandlebars _handlebarsContext;

        public StringHelperTemplateTests()
        {
            _handlebarsContext = HandlebarsDotNet.Handlebars.Create();

            HandleBarsHelpers.Register(_handlebarsContext);
        }

        [Theory]
        [InlineData("{{Append \"foo\" \"bar\"}}", "foobar")]
        [InlineData("{{Append \"foo\" \"b\"}}", "foob")]
        [InlineData("{{Append \"foo\" 'b'}}", "foob")]
        [InlineData("{{Append \"foo\" (Append \"a\" \"b\")}}", "fooab")]
        public void Append(string template, string expected)
        {
            // Arrange
            var action = _handlebarsContext.Compile(template);

            // Act
            var result = action("");

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("{{#IsString \"Hello\"}}yes{{else}}no{{/IsString}}", "yes")]
        [InlineData("{{#IsString 1}}yes{{else}}no{{/IsString}}", "no")]
        public void IsString(string template, string expected)
        {
            // Arrange
            var action = _handlebarsContext.Compile(template);

            // Act
            var result = action("");

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("{{Split \"a,b,c\" ','}}", "[\"a\",\"b\",\"c\"]")]
        public void Split(string template, string expected)
        {
            // Arrange
            var action = _handlebarsContext.Compile(template);

            // Act
            var result = action("");

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("{{#StartsWith \"Hello\" \"He\"}}Hi{{else}}Goodbye{{/StartsWith}}", "Hi")]
        [InlineData("{{#StartsWith \"Hello\" \"xx\"}}Hi{{else}}Goodbye{{/StartsWith}}", "Goodbye")]
        [InlineData("{{#StartsWith \"Hello\" \"H\"}}Hi{{else}}Goodbye{{/StartsWith}}", "Hi")]
        [InlineData("{{#StartsWith \"Hello\" \"x\"}}Hi{{else}}Goodbye{{/StartsWith}}", "Goodbye")]
        [InlineData("{{#StartsWith \"Hello\" 'H'}}Hi{{else}}Goodbye{{/StartsWith}}", "Hi")]
        [InlineData("{{#StartsWith \"Hello\" 'x'}}Hi{{else}}Goodbye{{/StartsWith}}", "Goodbye")]
        public void StartsWith(string template, string expected)
        {
            // Arrange
            var action = _handlebarsContext.Compile(template);

            // Act
            var result = action("");

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("{{Append \"foo\"}}")]
        [InlineData("{{Append \"foo\" \"bar\" \"bar2\"}}")]
        public void InvalidNumberOfArgumentsShouldThrowHandlebarsException(string template)
        {
            // Arrange
            var handleBarsAction = _handlebarsContext.Compile(template);

            // Act and Assert
            Assert.Throws<HandlebarsException>(() => handleBarsAction(""));
        }

        [Theory]
        [InlineData("{{StartsWith \"foo\" 1}}")]
        public void InvalidArgumentTypeShouldThrowNotSupportedException(string template)
        {
            // Arrange
            var handleBarsAction = _handlebarsContext.Compile(template);

            // Act and Assert
            Assert.Throws<NotSupportedException>(() => handleBarsAction(""));
        }
    }
}