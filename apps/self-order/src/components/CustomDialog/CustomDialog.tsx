import { t } from "../../i18n";
import { AlertDialog, Button, Flex } from "@radix-ui/themes";

interface ICustomDialogProps {
  title: string;
  description: string;
  textBotton1: string;
  textBotton2: string;
  dialogOpen: boolean;
  setDialogOpen: (open: boolean) => void;
  onConfirm: () => void;
  onCancel: () => void;
}

export const CustomDialog = ({
  title,
  description,
  dialogOpen,
  setDialogOpen,
  onConfirm,
  onCancel,
  textBotton1,
  textBotton2,
}: ICustomDialogProps) => {
  return (
    <AlertDialog.Root open={dialogOpen} onOpenChange={setDialogOpen}>
      <AlertDialog.Content maxWidth="450px">
        <AlertDialog.Title>{title}</AlertDialog.Title>
        <AlertDialog.Description size="2">
          {description}
        </AlertDialog.Description>
        <Flex gap="3" mt="4" justify="end">
          <AlertDialog.Cancel>
            <Button variant="soft" color="gray" onClick={() => onConfirm()}>
              {t(textBotton1)}
            </Button>
          </AlertDialog.Cancel>
          <AlertDialog.Action>
            <Button variant="solid" color="red" onClick={() => onCancel()}>
              {t(textBotton2)}
            </Button>
          </AlertDialog.Action>
        </Flex>
      </AlertDialog.Content>
    </AlertDialog.Root>
  );
};
