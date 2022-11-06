using MediatR;
using System;

namespace PurchaseOrderSystem.Application.Features.NotifyUserAboutFraudCheckResult
{
    public class NotifyUserAboutFraudCheckResultCommand : IRequest
    {
        public Guid FraudCheckResultId { get; private set; }

        public NotifyUserAboutFraudCheckResultCommand(Guid fraudCheckResultId)
        {
            FraudCheckResultId = fraudCheckResultId;
        }
    }
}
