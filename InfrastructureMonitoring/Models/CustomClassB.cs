﻿using System;
using ShtrihM.Wattle3.Infrastructures.Interfaces.Monitors;
using ShtrihM.Wattle3.Infrastructures.Monitors;

namespace ShtrihM.Wattle3.Examples.InfrastructureMonitoring.Models;

public class CustomClassB
{
    #region Infrastructure Monitor

    public class SnapShotInfrastructureMonitor : BaseCustomSnapShotInfrastructureMonitor
    {
        public SnapShotInfrastructureMonitor(
            IInfrastructureMonitor monitor,
            DateTimeOffset snapShotTime,
            string stackTrace,
            long count,
            CustomValue value)
            : base(monitor, snapShotTime, stackTrace)
        {
            Count = count;
            Value = value;

            AddCustomWellknownValue(WellknownCustomSnapShotInfrastructureMonitorValues.CustomClassB.Count, Count);
            AddCustomWellknownValue(WellknownCustomSnapShotInfrastructureMonitorValues.CustomClassB.Value, Value);
        }

        public long Count { get; }
        public CustomValue Value { get; }
    }

    public class InfrastructureMonitor : BaseInfrastructureMonitor
    {
        private readonly CustomClassB m_onwer;

        public InfrastructureMonitor(CustomClassB onwer)
            : base(
                WellknownCustomInfrastructureMonitors.CustomClassB,
                WellknownCustomInfrastructureMonitors.GetDisplayName(WellknownCustomInfrastructureMonitors.CustomClassB),
                WellknownCustomInfrastructureMonitors.GetDisplayName(WellknownCustomInfrastructureMonitors.CustomClassB))
        {
            m_onwer = onwer;
        }

        protected override ISnapShotInfrastructureMonitor DoGetSnapShot()
        {
            var result =
                new SnapShotInfrastructureMonitor(
                    this,
                    DateTimeOffset.Now,
                    StackTrace,
                    m_onwer.Count,
                    m_onwer.Value);

            return (result);
        }

        public SnapShotInfrastructureMonitor GetSnapShot()
        {
            var result = DoGetSnapShot();

            return (SnapShotInfrastructureMonitor)result;
        }
    }

    #endregion Infrastructure Monitor

    public CustomClassB()
    {
        Count = 1;
        Value =
            new CustomValue
            {
                Value1 = 2,
                Value2 = "3",
                Value3 = new byte[] { 4 },
            };
        Monitor = new InfrastructureMonitor(this);
    }

    public InfrastructureMonitor Monitor { get; }
    public long Count;
    public CustomValue Value;

}