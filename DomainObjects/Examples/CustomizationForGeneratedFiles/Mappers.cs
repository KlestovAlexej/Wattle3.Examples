﻿using ShtrihM.Wattle3.DomainObjects.Interfaces;
using ShtrihM.Wattle3.Mappers.PostgreSql;
using ShtrihM.Wattle3.Utils;
using Unity;

// ReSharper disable once CheckNamespace
namespace ShtrihM.Wattle3.Examples.DomainObjects.Examples.Generated.PostgreSql.Implements;

public partial class Mappers
{
    // ReSharper disable once ConvertToAutoProperty
    public IPostgreSqlMapperSelectFilterFactory SelectFilterFactory => m_selectFilterFactory;

    partial void OnExitConstructor(object context)
    {
        var intergratorContext = (IUnityContainer)context;
        var timeService = intergratorContext.Resolve<ITimeService>();
        var exceptionPolicy = intergratorContext.Resolve<IExceptionPolicy>();

        var mapper = MapperDocument.NewWithCache(this, timeService, exceptionPolicy);
        RemoveMapper(mapper.MapperId).SilentDispose();
        AddMapper(mapper);
    }
}

