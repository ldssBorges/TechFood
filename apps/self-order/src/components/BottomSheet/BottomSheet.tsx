import React from "react";
import { Box, Card, Flex, IconButton } from "@radix-ui/themes";
import { XIcon } from "lucide-react";
import { BottomSheetProps } from "./BottomSheet.types";

import classNames from "./BottomSheet.module.css";

export const BottomSheet = React.forwardRef(
  (
    {
      onClose,
      showCloseButton = true,
      closeOnOverlayClick = true,
      children,
      ...rest
    }: BottomSheetProps,
    ref
  ) => {
    return (
      <Flex
        ref={ref as any}
        className={classNames.root}
        direction="column"
        align="center"
        {...rest}
      >
        <Box
          onClick={closeOnOverlayClick ? onClose : undefined}
          className={classNames.overlay}
        />
        <Card className={classNames.card}>
          <Flex direction="column" align="center">
            {showCloseButton && (
              <IconButton
                variant="outline"
                size="2"
                aria-label="Close"
                onClick={onClose}
              >
                <XIcon />
              </IconButton>
            )}
          </Flex>
          {children}
        </Card>
      </Flex>
    );
  }
);

BottomSheet.displayName = "BottomSheet";
