using System.ComponentModel;

public enum DocumentType{
    [Description("PASSPORT")]
    PASSPORT,
    [Description("NIE")]
    NIE,
    [Description("DNI")]
    DNI,
    [Description("SOLITCUID INCIAL")]
    INCIAL,
    [Description("SOLITCUID DE RENOVACION")]
    RENOVACION
}