using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;

internal class Document_РеализацияТоваровУслуг_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_РеализацияТоваровУслуг_NotificationHandler> logger)
    : DocumentNotificationHandler<Document_РеализацияТоваровУслуг>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Document_РеализацияТоваровУслуг>
{ }
