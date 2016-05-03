using CommandsAndAspects.Application;
using CommandsAndAspects.Application.Aspects;
using CommandsAndAspects.Application.Validation;
using CommandsAndAspects.Domain;
using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace CommandsAndAspects.Infrastructure
{
    public class CommandsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderRepository>().To<OrderRepositoryMock>();

            //Validators
            Kernel.Bind(scanner => scanner
                .FromThisAssembly()
                .IncludingNonePublicTypes()
                .SelectAllClasses()
                .InheritedFrom(typeof(IValidator<>))
                .WhichAreNotGeneric()
                .BindSingleInterface());

            //Commands
            Kernel.Bind(scanner => scanner
                .FromThisAssembly()
                .IncludingNonePublicTypes()
                .SelectAllClasses()
                .InheritedFrom(typeof (ICommandHandler<>))
                .WhichAreNotGeneric()  //we may want to have generic, but not-aspect classes. If we do, we need better convention
                .BindSingleInterface()  //we may want to have more than one interface implemented by command. If we do, we need better convention 
                .Configure(c => c
                    .WhenInjectedInto(typeof(ValidationAspect<>))));

            //Aspects
            Bind(typeof (ICommandHandler<>)).To(typeof(ValidationAspect<>))
                .WhenInjectedInto(typeof(ErrorHandlingAspect<>));

            Bind(typeof(ICommandHandler<>)).To(typeof(ErrorHandlingAspect<>));
        }
    }
}
