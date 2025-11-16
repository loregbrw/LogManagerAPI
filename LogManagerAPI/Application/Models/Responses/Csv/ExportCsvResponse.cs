namespace Application.Models.Responses.Csv;

public record ExportCsvResponse(
    Stream Content,
    string FileName,
    string ContentType
);