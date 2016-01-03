using System;
using NSpecRunner.Core.Domain;

namespace NSpecRunner.Core
{
    public class Wrapper : MarshalByRefObject
    {
        /// <summary>
        ///  Executes the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public NSpecResultModel Execute(ContextRunnerInvocator invocation, Func<ContextRunnerInvocator, NSpecResultModel> action)
        {
            return action(invocation);
        }

        /// <summary>
        /// Obtains a lifetime service object to control the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        /// An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime" /> property.
        /// </returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
        /// </PermissionSet>
        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}