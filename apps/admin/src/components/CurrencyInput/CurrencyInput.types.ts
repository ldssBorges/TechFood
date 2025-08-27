export type CurrencyInputProps = {
  value: number | undefined;
  onChange: (value: number | undefined) => void;
  error?: string;
  id: string;
  name: string;
};
