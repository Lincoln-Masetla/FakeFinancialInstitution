using FakeFinancialInstitution.Domain.Contexts;
using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeFinancialInstitution.Domain.UseCases
{
    public abstract class DomainUseCase<T>
    {

        protected DomainContext Context { get; private set; }

        protected bool IsValidState { get; private set; } = true;

        protected DomainUseCase(DomainContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<T> ExecuteAsync()
        {
            return Task.Run(async () =>
            {
                try
                {
                    var validState = await VerifyCommandState();
                    if (!this.IsValidState)
                        return validState;

                    return await ExecuteInternal();
                }
                catch (Exception ex)
                {
                    throw new CommandExecutionException(ex);
                }
            });
        }


        private Task<T> VerifyCommandState()
        {
            var verifiedState = VerifyStateInternal();
            this.IsValidState = verifiedState.Item1;
            return Task.FromResult(verifiedState.Item2);
        }

        protected abstract Tuple<bool, T> VerifyStateInternal();

        protected abstract Task<T> ExecuteInternal();
    }

    /// <summary>
    /// Represents an exception that is thrown by a domain command when encountering an exception within the scope of execution
    /// </summary>
    public class CommandExecutionException : Exception
    {
        public CommandExecutionException(Exception innerException)
          : this($"An error occurred executing the command", innerException)
        {
        }

        public CommandExecutionException(string message, Exception innerException)
            : base($"{message}", innerException)
        {
        }

        public CommandExecutionException(string message)
          : base(message)
        {
        }
    }
}
