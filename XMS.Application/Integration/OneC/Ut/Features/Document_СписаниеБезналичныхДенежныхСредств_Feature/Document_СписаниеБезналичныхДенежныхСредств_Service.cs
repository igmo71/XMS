using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Integrations.OneC.Ut;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

internal class Document_СписаниеБезналичныхДенежныхСредств_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_Service> logger)
    : DocumentService<Document_СписаниеБезналичныхДенежныхСредств>(utClient, dbFactory, logger), IDocument_СписаниеБезналичныхДенежныхСредств_Service
{ }
