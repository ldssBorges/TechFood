import { createContext, useContext, useState } from "react";
import { Customer } from "../models";

export type CustomerContextType = {
  customer: Customer | null;
  setCustomer: (customer: Customer | null) => void;
};

const CustomerContext = createContext<CustomerContextType | undefined>(
  undefined
);

export const CustomerProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [customer, setCustomer] = useState<Customer | null>(null);

  return (
    <CustomerContext.Provider value={{ customer, setCustomer }}>
      {children}
    </CustomerContext.Provider>
  );
};

export const useCustomer = () => {
  const context = useContext(CustomerContext);
  if (context === undefined) {
    throw new Error("useCustomer must be used within a CustomerProvider");
  }

  return context;
};
