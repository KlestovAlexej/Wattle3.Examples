﻿using ShtrihM.Wattle3.Examples.UniqueRegisters.Examples.Generated.Interface;
using ShtrihM.Wattle3.Mappers.PostgreSql;

// ReSharper disable All

namespace ShtrihM.Wattle3.Examples.UniqueRegisters.Examples.Generated.Implements;

public partial class MapperTransactionKey
{
    IPartitionsManager IMapperTransactionKey.Partitions => Partitions;
}

