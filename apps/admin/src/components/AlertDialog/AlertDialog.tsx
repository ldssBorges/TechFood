import { t } from "../../i18n";
import {
  AlertDialog as RadixAlertDialog,
  Button,
  Flex,
} from "@radix-ui/themes";
import { AlertDialogProps } from "./AlertDialog.types";

export const AlertDialog = ({
  title,
  description,
  open,
  onOpen,
  onConfirm,
  onCancel,
}: AlertDialogProps) => {
  return (
    <RadixAlertDialog.Root open={open} onOpenChange={onOpen}>
      <RadixAlertDialog.Content maxWidth="450px">
        <RadixAlertDialog.Title>{title}</RadixAlertDialog.Title>
        <RadixAlertDialog.Description size="3">
          {description}
        </RadixAlertDialog.Description>
        <Flex gap="3" mt="4" justify="end">
          <RadixAlertDialog.Cancel>
            <Button variant="soft" color="gray" onClick={() => onCancel()}>
              {t("labels.cancel")}
            </Button>
          </RadixAlertDialog.Cancel>
          <RadixAlertDialog.Action>
            <Button variant="solid" color="red" onClick={() => onConfirm()}>
              {t("labels.confirm")}
            </Button>
          </RadixAlertDialog.Action>
        </Flex>
      </RadixAlertDialog.Content>
    </RadixAlertDialog.Root>
  );
};
