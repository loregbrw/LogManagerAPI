namespace Application.Models.Requests.User;

using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Application.Enums;

public class UserCsv
{
    [AppAlias("CÓD FUNCIONÁRIOS")]
    public short? Code { get; set; }

    [AppStringLength(255)]
    [AppAlias("NOME FUNCIONÁRIO")]
    public string? Name { get; set; }

    [AppStringLength(255)]
    [AppAlias("E-MAIL")]
    public string? Email { get; set; }

    [AppStringLength(255)]
    [AppAlias("SETOR")]
    public string? Department { get; set; }

    [EnumDataType(typeof(ERole))]
    [AppAlias("ACESSO")]
    public ERole Role { get; set; } = ERole.DATA;
}