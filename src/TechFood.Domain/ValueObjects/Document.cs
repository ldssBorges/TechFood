using TechFood.Common.Exceptions;
using TechFood.Domain.Enums;
using TechFood.Domain.Validations;

namespace TechFood.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(
        DocumentType type,
        string value)
    {
        Type = type;
        Value = value;
        Validate();
    }
    public DocumentType Type { get; set; }

    public string Value { get; set; }

    private void Validate()
    {
        if (Type == DocumentType.CPF && !ValidaDocument.ValidarCPF(Value))
        {
            throw new DomainException(Common.Resources.Exceptions.Customer_ThrowDocumentCPFInvalid);
        }
    }
}
