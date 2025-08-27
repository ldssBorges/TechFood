import { createContext, useCallback, useContext, useState } from "react";
import axios from "axios";
import { OrderItem, PaymentType } from "../models";
import { useCustomer } from "./CustomerProvider";

export type OrderContextType = {
  id?: string;
  items: OrderItem[];
  discount: number;
  cuponCode?: string;
  paymentMethod: PaymentType | undefined;
  paymentQrCode?: string;
  subtotal: number;
  total: number;
  addItem: (item: OrderItem) => void;
  removeItem: (item: OrderItem) => void;
  updateItem: (item: OrderItem) => void;
  applyDiscount: (code: string) => Promise<void>;
  createPayment: (method: PaymentType) => Promise<void>;
  confirmPayment: () => Promise<void>;
  createOrder: () => Promise<void>;
  clearOrder: () => void;
};

const OrderContext = createContext<OrderContextType | undefined>(undefined);

export const OrderProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [id, setId] = useState<string | undefined>();
  const [items, setItems] = useState<OrderItem[]>([]);
  const [discount, setDiscount] = useState<number>(0);
  const [cuponCode, setCuponCode] = useState<string | undefined>();
  const [paymentId, setPaymentId] = useState<string | undefined>();
  const [paymentMethod, setPaymentMethod] = useState<PaymentType | undefined>();
  const [paymentQrCode, setPaymentQrCode] = useState<string | undefined>();

  const { customer } = useCustomer();

  const subtotal = items.reduce(
    (total, item) => total + item.unitPrice * item.quantity,
    0
  );

  const total = subtotal - (discount || 0);

  const addItem = useCallback((item: OrderItem) => {
    setItems((prev) => [...prev, item]);
  }, []);

  const updateItem = useCallback((item: OrderItem) => {
    setItems((prev) => prev.map((i) => (i === item ? { ...item } : i)));
  }, []);

  const removeItem = useCallback((item: OrderItem) => {
    setItems((prev) => prev.filter((i) => i !== item));
  }, []);

  const createOrder = useCallback(async () => {
    const result = await axios.post<{
      id: string;
    }>("/api/v1/orders", {
      customerId: customer?.id,
      items,
    });

    setId(result.data.id);
  }, [customer?.id, items]);

  const applyDiscount = useCallback(async (code: string) => {
    const result = await axios.post<{
      discount: number;
    }>("/api/v1/coupons", {
      code,
    });
    setCuponCode(code);
    setDiscount(result.data.discount);
  }, []);

  const createPayment = useCallback(
    async (method: PaymentType) => {
      const { data } = await axios.post<{
        id: string;
        qrCodeData: string;
      }>("/api/v1/payments", {
        orderId: id,
        type: method,
      });

      setPaymentId(data.id);
      setPaymentMethod(method);
      setPaymentQrCode(data.qrCodeData);
    },
    [id]
  );

  const confirmPayment = useCallback(async () => {
    await axios.patch(`/api/v1/payments/${paymentId}`);
  }, [paymentId]);

  const clearOrder = useCallback(() => {
    setId(undefined);
    setItems([]);
    setDiscount(0);
    setCuponCode(undefined);
    setPaymentMethod(undefined);
    setPaymentQrCode(undefined);
  }, []);

  return (
    <OrderContext.Provider
      value={{
        id,
        items,
        discount,
        cuponCode,
        paymentMethod,
        paymentQrCode,
        subtotal,
        total,
        addItem,
        removeItem,
        updateItem,
        createPayment,
        confirmPayment,
        applyDiscount,
        createOrder,
        clearOrder,
      }}
    >
      {children}
    </OrderContext.Provider>
  );
};

export const useOrder = () => {
  const context = useContext(OrderContext);
  if (context === undefined) {
    throw new Error("useOrder must be used within a OrderProvider");
  }

  return context;
};
