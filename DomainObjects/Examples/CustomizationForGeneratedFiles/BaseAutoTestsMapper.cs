﻿using System;
using NUnit.Framework;
using ShtrihM.Wattle3.DomainObjects.Common;
using ShtrihM.Wattle3.DomainObjects.Interfaces;
using ShtrihM.Wattle3.Examples.Common;
using ShtrihM.Wattle3.Examples.DomainObjects.Common;
using ShtrihM.Wattle3.Mappers;
using ShtrihM.Wattle3.Primitives;
using ShtrihM.Wattle3.Testing.Databases.PostgreSql;

// ReSharper disable once CheckNamespace
namespace ShtrihM.Wattle3.Examples.DomainObjects.Examples.Generated.Tests;

public abstract partial class BaseAutoTestsMapper
{
    protected string m_dbName;

    /// <summary>
    /// Создание тестовой БД.
    /// </summary>
    partial void DoBase_BeginSetUp()
    {
        CreateDb(out m_dbName, out m_dbConnectionString);

        m_timeService = new TimeService();

        var domainObjectIntergratorContext = new DomainObjectIntergratorContext();
        domainObjectIntergratorContext.AddObject(ExampleEntryPoint.WellknownDomainObjectIntergratorContextObjectNames.TimeService, m_timeService);
        m_contextMappers = domainObjectIntergratorContext;
    }

    /// <summary>
    /// Удаление тестовой БД.
    /// </summary>
    partial void DoBase_TearDown()
    {
        PostgreSqlDbHelper.DropDb(m_dbName);
    }

    public static void CreateDb(out string dbName, out string dbConnectionString)
    {
        Assert.IsTrue(DbCredentials.TryGetServerAdressForPostgreSql(out var serverAdress), "Определите адрес сервера с PostgreSQL.");
        Assert.IsTrue(DbCredentials.TryGetCredentialsForPostgreSql(out var credentials), "Определите параметры учётной записи подключения к PostgreSQL.");

        dbName = "examples_wattle3_" + DateTime.Now.ToString("yyyMMddhhmmss") + "_" + Guid.NewGuid().ToString("N");
        dbConnectionString = PostgreSqlDbHelper.GetDatabaseConnectionString(dbName, serverAdress: serverAdress, userCredentials: credentials);

        var sqlScript = typeof(WellknownDomainObjects).Assembly.GetResourceAsString("SqlScript.sql");
        PostgreSqlDbHelper.CreateDb(dbName, sqlScript: sqlScript);
    }
}
