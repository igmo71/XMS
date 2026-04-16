using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.Abstractions;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature.Document_РеализацияТоваровУслуг;

namespace XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;

public record Document_РеализацияТоваровУслуг_Notification : DocumentNotification;

internal class Document_РеализацияТоваровУслуг_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_РеализацияТоваровУслуг_NotificationHandler> logger)
    : DocumentNotificationHandler<Entity, Document_РеализацияТоваровУслуг_Notification>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Document_РеализацияТоваровУслуг_Notification>
{ }
