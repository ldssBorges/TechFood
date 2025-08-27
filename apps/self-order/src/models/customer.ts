export interface Customer {
  id: string;
  name: string;
  email: string;
  documentType: "CPF" | "CNPJ";
  documentNumber: string;
}
