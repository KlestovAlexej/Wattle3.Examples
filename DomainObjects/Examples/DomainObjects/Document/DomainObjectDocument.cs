﻿using ShtrihM.Wattle3.DomainObjects.DomainObjects;
using ShtrihM.Wattle3.DomainObjects.Interfaces;
using ShtrihM.Wattle3.Examples.DomainObjects.Common;
using ShtrihM.Wattle3.Mappers.Primitives.MutableFields;
using System;
using System.Runtime.CompilerServices;
using ShtrihM.Wattle3.Examples.DomainObjects.Examples.Generated.Interface;
using ShtrihM.Wattle3.Primitives;

namespace ShtrihM.Wattle3.Examples.DomainObjects.Examples.DomainObjects.Document;

[DomainObjectDataMapper]
public class DomainObjectDocument : BaseDomainObject<DomainObjectDocument>, IDomainObjectDocument
{
    /// <summary>
    /// Версия данных в БД.
    /// </summary>
    [DomainObjectFieldValue]
    public long Revision { get; }

    /// <summary>
    /// Дата-время создания.
    /// </summary>
    [DomainObjectFieldValue]
    public DateTime CreateDate { get; }

    /// <summary>
    /// Дата-время модификации.
    /// </summary>
    [DomainObjectFieldValue]
    public DateTime ModificationDate { get; private set; }

    /// <summary>
    /// Доле документа - дата-время.
    /// </summary>
    public DateTime Value_DateTime
    {
        get => m_value_DateTime.Value;
        set
        {
            if (m_value_DateTime.SetValue(value))
            {
                DoUpdated();
            }
        }
    }

    /// <summary>
    /// Доле документа - число.
    /// </summary>
    public long Value_Long
    {
        get => m_value_Long.Value;
        set
        {
            if (m_value_Long.SetValue(value))
            {
                DoUpdated();
            }
        }
    }

    /// <summary>
    /// Доле документа - число с поддержкой null.
    /// </summary>
    public int? Value_Int
    {
        get => m_value_Int.Value;
        set
        {
            if (m_value_Int.SetValue(value))
            {
                DoUpdated();
            }
        }
    }

    /// <summary>
    /// Метод доменного объекта с доменнной логикой.
    /// </summary>
    public string Method(DateTime value_DateTime, long value_Long, int? value_Int)
    {
        var changed = m_value_DateTime.SetValue(value_DateTime);
        changed = m_value_Long.SetValue(value_Long) || changed;
        changed = m_value_Int.SetValue(value_Int) || changed;

        if (changed)
        {
            DoUpdated();
        }

        return "Test";
    }

    /// <summary>
    /// Удалить доменный объект.
    /// </summary>
    public void Delete()
    {
        var unitOfWork = m_entryPoint.UnitOfWorkProvider.Instance;

        unitOfWork.AddDelete(this);
    }

    /// <summary>
    /// Верификация версии данных в БД.
    /// </summary>
    public void Version()
    {
        var unitOfWork = m_entryPoint.UnitOfWorkProvider.Instance;

        unitOfWork.AddVersion(this);
    }

    /// <summary>
    /// Регистрация изменения.
    /// </summary>
    private void DoUpdated()
    {
        var unitOfWork = m_entryPoint.UnitOfWorkProvider.Instance;
        var entryPoint = (ExampleEntryPoint)unitOfWork.EntryPoint;

        ModificationDate = entryPoint.TimeService.NowDateTime;

        unitOfWork.AddUpdate(this);
    }

    #region Ритуальный код

    [DomainObjectFieldValue]
    private MutableField<DateTime> m_value_DateTime;

    [DomainObjectFieldValue]
    private MutableField<long> m_value_Long;

    [DomainObjectFieldValue]
    private MutableFieldNullable<int> m_value_Int;

    private readonly IEntryPoint m_entryPoint;

    /// <summary>
    /// Восстановить экземпляр из данных БД.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DomainObjectDocument(
        IEntryPoint entryPoint,
        DocumentDtoActual data)
        : base(data.Id, false)
    {
        m_entryPoint = entryPoint;
        Revision = data.Revision;
        CreateDate = data.CreateDate.SpecifyKindLocal();
        ModificationDate = data.ModificationDate.SpecifyKindLocal();
        m_value_DateTime = new MutableField<DateTime>(data.Value_DateTime);
        m_value_Long = new MutableField<long>(data.Value_Long);
        m_value_Int = new MutableFieldNullable<int>(data.Value_Int);
    }

    /// <summary>
    /// Создать экземпляр по данным шаблона.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DomainObjectDocument(
        IEntryPoint entryPoint,
        long identity,
        DateTime createDate,
        DomainObjectTemplateDocument template)
        : base(identity, true)
    {
        m_entryPoint = entryPoint;
        CreateDate = createDate;
        ModificationDate = createDate;
        m_value_DateTime = new MutableField<DateTime>(template.Value_DateTime);
        m_value_Long = new MutableField<long>(template.Value_Long);
        m_value_Int = new MutableFieldNullable<int>(template.Value_Int);
    }

    public override Guid TypeId => WellknownDomainObjects.Document;

    #endregion
}