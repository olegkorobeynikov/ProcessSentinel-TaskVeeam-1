using System;
using FluentAssertions;
using NUnit.Framework;
using ProcessSentinel;

namespace ProcessSentinelTests
{
    public class CommandReaderTests
    {
        [TestCaseSource(nameof(CommandParserTestsSourceLists))]
        public void Should_parse_command_string_and_return_expected(string rawCommand, Command expectedCommand)
        {
            var commandResult = CommandParser.Parse(rawCommand);
                
            commandResult.Should().BeEquivalentTo(expectedCommand);
        }

        private static readonly object[] CommandParserTestsSourceLists =
        {
            new object[] { "notepad 5 1", new Command() {ProcessName = "notepad", TimeToLiveMinutes = 5, TimePollingMinutes = 1} },
            new object[] { " notepad 5 1 ", new Command() {ProcessName = "notepad", TimeToLiveMinutes = 5, TimePollingMinutes = 1} },
            new object[] { "notepad 0 1", null },
            new object[] { "notepad 0 1 1", null },
            new object[] { "notepad 1 0", null  },
            new object[] { "notepad a 1", null  },
            new object[] { "notepad 1 a", null  },
            new object[] { "notepad 5", null },
            new object[] { "5 1", null  },
            new object[] { "1", null  },
            new object[] { "", null  },
        };

        [Test]
        public void Should_return_exception_when_parse_rawCommand_which_is_null_string()
        {
            Action act = () => CommandParser.Parse((string)null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_return_exception_when_parse_rawCommand_which_is_null_string_array()
        {
            Action act = () => CommandParser.Parse((string[])null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_return_exception_when_tryParse_rawCommand_which_is_null_string()
        {
            Action act = () => CommandParser.TryParse((string)null, out var commandResult);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_return_exception_when_tryParse_rawCommand_which_is_null_string_array()
        {
            Action act = () => CommandParser.TryParse((string[])null, out var commandResult);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_successful_tryParse_string()
        {
            var rawCommand = "1 1 1";
            var parseResult = CommandParser.TryParse(rawCommand, out var commandResult);

            var expectedCommand = new Command()
                { ProcessName = "1", TimeToLiveMinutes = 1, TimePollingMinutes = 1 };
            commandResult.Should().BeEquivalentTo(expectedCommand);
            parseResult.Should().BeTrue();
        }

        [Test]
        public void Should_fail_tryParse_string()
        {
            var rawCommand = "1 1 1 1";
            var parseResult = CommandParser.TryParse(rawCommand, out var commandResult);

            commandResult.Should().BeNull();
            parseResult.Should().BeFalse();
        }

        [Test]
        public void Should_successful_tryParse_string_array()
        {
            var rawCommand = new string[] { "1", "1", "1" };
            var parseResult = CommandParser.TryParse(rawCommand, out var commandResult);

            var expectedCommand = new Command()
                { ProcessName = "1", TimeToLiveMinutes = 1, TimePollingMinutes = 1 };
            commandResult.Should().BeEquivalentTo(expectedCommand);
            parseResult.Should().BeTrue();
        }

        [Test]
        public void Should_fail_tryParse_string_array()
        {
            var rawCommand = new string[] { "1", "1" };
            var parseResult = CommandParser.TryParse(rawCommand, out var commandResult);

            commandResult.Should().BeNull();
            parseResult.Should().BeFalse();
        }

        [Test]
        public void Should_successful_parse_string_array()
        {
            var rawCommand = new string[] { "1", "1", "1" };
            var commandResult = CommandParser.Parse(rawCommand);

            var expectedCommand = new Command()
                { ProcessName = "1", TimeToLiveMinutes = 1, TimePollingMinutes = 1 };
            commandResult.Should().BeEquivalentTo(expectedCommand);
        }
    }
}