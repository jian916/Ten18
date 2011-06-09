﻿using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;
using Ten18.Async;

namespace Ten18
{
    class CoroutineSynchronizationContext : SynchronizationContext
    {
        public CoroutineSynchronizationContext(Heart scheduler)
        {
            mScheduler = scheduler;
            mFactory = new TaskFactory(scheduler);
            SetWaitNotificationRequired();
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            mFactory.StartNew(() => d(state));            
        }
        
        public override void Send(SendOrPostCallback d, object state)
        {
            d(state);
        }
        
        public override int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
        {
            return base.Wait(waitHandles, waitAll, millisecondsTimeout);
        }
        
        public override void OperationStarted()
        {
            base.OperationStarted();
        }

        public override void  OperationCompleted()
        {
            base.OperationCompleted();
        }

        private Heart mScheduler;
        private TaskFactory mFactory;
    }
}
