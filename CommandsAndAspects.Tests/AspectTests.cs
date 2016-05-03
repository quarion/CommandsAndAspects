using System;
using System.Linq;
using CommandsAndAspects.Application;
using CommandsAndAspects.Application.Aspects;
using CommandsAndAspects.Application.Validation;
using CommandsAndAspects.Domain;
using CommandsAndAspects.Infrastructure;
using FakeItEasy;
using Ninject;
using NUnit.Framework;

namespace CommandsAndAspects.Tests
{
    /// <summary>
    /// Tests the creation of aspects as decorators
    /// Note: Those tests do not provide full coverage of meaningfull cases. But this is not required for the demonstration. Feel free to hook up your debugger here and step through the code.
    /// </summary>
    [TestFixture]
    public class AspectTests
    {
        [TestFixture]
        class ManualSetupTests
        {
            private ICommandHandler<BookOrderCommand> _commandHandler;
            private IValidator<BookOrderCommand> _validator;
            private ICommandHandler<BookOrderCommand> _baseCommand;
            private ValidationAspect<BookOrderCommand> _validationAspect;

            [SetUp]
            public void BeforeEachTest()
            {
                _validator = A.Fake<IValidator<BookOrderCommand>>();
                _baseCommand = A.Fake<ICommandHandler<BookOrderCommand>>();
                _validationAspect = new ValidationAspect<BookOrderCommand>(_baseCommand, _validator);
                _commandHandler = new ErrorHandlingAspect<BookOrderCommand>(_validationAspect);
            }


            [Test]
            public void DecoratorsChain_GivenSetupIsPerformendManually_CallsAllAspectsInCorrectOrder()
            {
                using (var scope = Fake.CreateScope())
                {
                    // Act
                    var command = new BookOrderCommand() { DeliveryAddres = "address" };
                    _commandHandler.Execute(command);


                    // Assert
                    using (scope.OrderedAssertions())
                    {
                        A.CallTo(() => _validator.Validate(command)).MustHaveHappened();
                        A.CallTo(() => _baseCommand.Execute(command)).MustHaveHappened();
                    }
                }
            }
        }

        static readonly object[] ExtractCases =
        {
            new object[] { typeof(ICommandHandler<BookOrderCommand>), typeof(ErrorHandlingAspect<BookOrderCommand>)},
            new object[] { typeof(ICommandHandler<VoidOrderCommand>), typeof(ErrorHandlingAspect<VoidOrderCommand>)},
        };

        [Test, TestCaseSource(nameof(ExtractCases))]
        public void Aspects_GivenSetupIsPerformedWithNinject_CreatesCorrectClassInstances(Type typeToExtract, Type expectedResult)
        {
            IKernel kernel = new StandardKernel(new CommandsNinjectModule());
            var commandHandler = kernel.Get(typeToExtract);

            Assert.That(commandHandler, Is.TypeOf(expectedResult));
        }
    }
}
