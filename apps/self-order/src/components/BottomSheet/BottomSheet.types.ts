export type BottomSheetProps = {
  onClose?: () => void;
  showCloseButton?: boolean;
  closeOnOverlayClick?: boolean;
} & React.HTMLAttributes<HTMLDivElement>;
