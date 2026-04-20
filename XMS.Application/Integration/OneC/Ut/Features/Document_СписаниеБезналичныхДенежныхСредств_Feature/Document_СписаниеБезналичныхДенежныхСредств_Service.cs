using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.Integration.OneC.Ut;
using XMS.Application.Integration.OneC.Common;
using XMS.Application.Integration.OneC.Ut.ODataClient;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

internal class Document_СписаниеБезналичныхДенежныхСредств_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_Service> logger)
    : DocumentService<Document_СписаниеБезналичныхДенежныхСредств>(utClient, dbFactory, logger), IDocument_СписаниеБезналичныхДенежныхСредств_Service
{ }
