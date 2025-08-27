import { NumericFormat } from "react-number-format";
import { Text, TextField } from "@radix-ui/themes";
import { CurrencyInputProps } from "./CurrencyInput.types";

export const CurrencyInput = ({
  value,
  onChange,
  error,
  id,
}: CurrencyInputProps) => {
  return (
    <div>
      <NumericFormat
        id={id}
        value={value ?? undefined}
        thousandSeparator="."
        decimalSeparator=","
        prefix="R$ "
        decimalScale={2}
        fixedDecimalScale
        allowNegative={false}
        customInput={TextField.Root}
        placeholder="R$ 0,00"
        onValueChange={(values) => {
          const floatValue = values.floatValue;
          onChange(floatValue ?? undefined);
        }}
      />
      {error && <Text color="red">{error}</Text>}
    </div>
  );
};
