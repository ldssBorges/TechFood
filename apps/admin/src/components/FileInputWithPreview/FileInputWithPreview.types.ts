export type FileInputWithPreviewProps = {
  value?: FileList;
  onChange: (files: FileList | undefined) => void;
  error?: any;
  name: string;
  imageUrl?: string;
};
