import { z } from "zod";

export const productSchema = z
  .object({
    name: z.string().min(1, "Nome é obrigatório"),
    description: z.string().min(1, "Descrição é obrigatória"),
    categoryId: z.string().min(1, "Categoria é obrigatória"),

    file: z.custom<FileList | undefined>(),

    price: z.coerce
      .number({ invalid_type_error: "Preço é obrigatório" })
      .min(1, "Preço deve ser maior que zero"),

    imageUrl: z.string().optional(),
  })
  .superRefine((data, ctx) => {
    const { file, imageUrl } = data;
    // Se não tem imageUrl, validar o file
    if (!imageUrl) {
      if (!file || file.length === 0) {
        ctx.addIssue({
          path: ["file"],
          code: z.ZodIssueCode.custom,
          message: "Imagem é obrigatória",
        });
        return;
      }

      const allowedTypes = ["image/jpeg", "image/png", "image/webp"];
      const type = file[0].type;
      if (!allowedTypes.includes(type)) {
        ctx.addIssue({
          path: ["file"],
          code: z.ZodIssueCode.custom,
          message: "Formato inválido",
        });
      }

      const size = file[0].size;
      if (size > 5 * 1024 * 1024) {
        ctx.addIssue({
          path: ["file"],
          code: z.ZodIssueCode.custom,
          message: "Tamanho máximo é 5MB",
        });
      }
    }
  });

export type ProductFormData = z.infer<typeof productSchema>;
