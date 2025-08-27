import { Flex, Heading, IconButton } from "@radix-ui/themes";
import classNames from "./PreparationItem.module.css";
import { ArrowRightIcon } from "lucide-react";
import { PreparationMonitor } from "../../../models/PreparationMonitor";
import clsx from "clsx";

type IPreparationMonitorItemProps = {
  PreparationMonitor: PreparationMonitor;
  selected?: boolean;
  onClick?: () => void;
};

const InfoButton = () => {
  return (
    <Flex>
      <IconButton>
        <ArrowRightIcon size="15"></ArrowRightIcon>
      </IconButton>
    </Flex>
  );
};

export const PreparationItem = ({
  PreparationMonitor,
  selected,
  onClick,
}: IPreparationMonitorItemProps) => {
  return (
    <Flex
      className={clsx(classNames.orderItem, selected && classNames.selected)}
      justify="between"
      align="center"
      onClick={onClick}
    >
      <Flex direction="column" gap="1">
        <Flex direction="column" gap="3">
          <Flex
            className={classNames.actions}
            direction="row"
            justify="between"
            gap="4"
            align="center"
          >
            <Heading
              className={classNames.title}
              size="5"
              weight="bold"
              color="gray"
            >
              Order #{PreparationMonitor.number}
            </Heading>
          </Flex>

          <Flex direction="row" gap="2" align="center"></Flex>
        </Flex>
      </Flex>

      <InfoButton />
    </Flex>
  );
};
