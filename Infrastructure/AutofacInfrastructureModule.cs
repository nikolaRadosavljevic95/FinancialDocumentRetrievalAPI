using Application.Handlers;
using Application.Interfaces;
using Autofac;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Services;
using MediatR;
using System.Reflection;
using Module = Autofac.Module;

namespace Infrastructure;

public class AutofacInfrastructureModule : Module
{
    private readonly List<Assembly> _assemblies = [];

    public AutofacInfrastructureModule(Assembly? callingAssembly = null)
    {
        AddToAssembliesIfNotNull(callingAssembly);
    }

    private void AddToAssembliesIfNotNull(Assembly? assembly)
    {
        if (assembly != null)
        {
            _assemblies.Add(assembly);
        }
    }

    protected override void Load(ContainerBuilder builder)
    {
        LoadAssemblies();
        RegisterServices(builder);
        RegisterMediatR(builder);
        RegisterValidators(builder);
    }

    private void LoadAssemblies()
    {
        var coreAssembly = Assembly.GetAssembly(typeof(Client));
        var infrastructureAssembly = Assembly.GetAssembly(typeof(AutofacInfrastructureModule));
        var useCasesAssembly = Assembly.GetAssembly(typeof(AnonymizeFinancialDocumentCommandHandler));

        AddToAssembliesIfNotNull(coreAssembly);
        AddToAssembliesIfNotNull(infrastructureAssembly);
        AddToAssembliesIfNotNull(useCasesAssembly);
    }

    private static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<ClientService>()
            .As<IClientService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ProductService>()
            .As<IProductService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<TenantService>()
            .As<ITenantService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<FinancialDocumentService>()
            .As<IFinancialDocumentService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ProductASerializationStrategy>()
            .As<IProductCodeSerializationStrategy>()
            .Keyed<IProductCodeSerializationStrategy>("ProductA")
            .InstancePerLifetimeScope();

        builder.RegisterType<ProductBSerializationStrategy>()
            .As<IProductCodeSerializationStrategy>()
            .Keyed<IProductCodeSerializationStrategy>("ProductB")
            .InstancePerLifetimeScope();

        builder.RegisterType<ProductDefaultSerializationStrategy>()
            .As<IProductCodeSerializationStrategy>()
            .Keyed<IProductCodeSerializationStrategy>("Default")
            .InstancePerLifetimeScope();
    }

    private void RegisterMediatR(ContainerBuilder builder)
    {
        builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        builder
              .RegisterAssemblyTypes([.. _assemblies])
              .AsClosedTypesOf(typeof(IRequestHandler<,>))
              .AsImplementedInterfaces();
    }

    private void RegisterValidators(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes([.. _assemblies])
               .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
    }
}
