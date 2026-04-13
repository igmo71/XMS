using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

internal class Document_СписаниеБезналичныхДенежныхСредств_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_Service> logger)
    : DocumentService<Entity>(utClient, dbFactory, logger), IDocument_СписаниеБезналичныхДенежныхСредств_Service
{ }
