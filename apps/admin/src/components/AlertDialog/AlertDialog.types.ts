export type AlertDialogProps = {
  title: string;
  description: string;
  open?: boolean;
  onOpen?: (open: boolean) => void;
  onConfirm: () => void;
  onCancel: () => void;
};
