export interface PreparationMonitor {
  preparationId: string;
  orderId: string;
  number: number;
  status: PreparationStatus;
  products: PreparationProduct[];
}

export interface PreparationProduct {
  imageUrl: string;
  name: string;
  quantity: number;
}

export type PreparationStatus =
  | "PENDING"
  | "INPROGRESS"
  | "DONE"
  | "FINISH"
  | "REJECT";
