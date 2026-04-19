using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

internal class Document_СписаниеБезналичныхДенежныхСредств_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_Service> logger)
    : DocumentService<Entity>(utClient, dbFactory, logger), IDocument_СписаниеБезналичныхДенежныхСредств_Service
{ }
