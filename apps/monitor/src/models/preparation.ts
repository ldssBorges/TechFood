export type PreparationStatus = "PENDING" | "INPROGRESS" | "DONE";

export interface Preparation {
  number: number;
  status: PreparationStatus;
}
