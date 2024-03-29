﻿using ShtrihM.Wattle3.Examples.UniqueRegisters.Examples.Generated.PostgreSql.Implements;
using ShtrihM.Wattle3.Mappers.Interfaces;

// ReSharper disable once CheckNamespace
namespace ShtrihM.Wattle3.Examples.UniqueRegisters.Examples.Generated.Tests;

public partial class AutoTestsMapperTransactionKey
{
    private IPartitionsManager m_partitions;

    partial void DoSetUp()
    {
        m_partitions = ((MapperTransactionKey)m_mapper).Partitions;

        using var session = m_mappers.OpenSession();
        m_partitions.CreatedDefaultPartition(session);
        session.Commit();
    }
}

