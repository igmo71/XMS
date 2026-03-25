using Microsoft.Extensions.Hosting;

namespace XMS.Integration.OneC.Abstractions;

public interface IDocument
{
    Guid Ref_Key { get; set; }
    string? DataVersion { get; set; }
    bool DeletionMark { get; set; }
    bool Posted { get; set; }
    string? Number { get; set; }
    DateTime Date { get; set; }

    static abstract string Uri { get; }
    static abstract string GetUriByRefKey(Guid refKey);
    static abstract string GetUriByDate(DateTime? from = null, DateTime? to = null);
    static abstract string GetExchangeName(IHostEnvironment hostEnvironment);
    static abstract string GetQueueName(IHostEnvironment hostEnvironment);
}

public interface IOneCDocumentItem
{
    Guid Ref_Key { get; set; }
    int LineNumber { get; set; }
}
