import { useEffect, useState } from "react";
import { Flex } from "@radix-ui/themes";
import axios from "axios";
import { Preparation } from "../../models";
import { t } from "../../i18n";
import { OrderCard } from "../../components";
import { ORDER_STATUS } from "../../const";

import classNames from "./StartPage.module.css";

export const StartPage = () => {
  const [preparations, setPreparations] = useState<Preparation[]>([]);

  useEffect(() => {
    const timer = setInterval(async () => {
      const response = await axios.get("/api/v1/preparations");
      setPreparations(response.data);
    }, 2000);
    return () => clearTimeout(timer);
  }, []);

  const inPreparation = preparations.filter(
    (order) =>
      order.status === ORDER_STATUS.PENDING ||
      order.status === ORDER_STATUS.INPROGRESS
  );
  const done = preparations.filter(
    (order) => order.status === ORDER_STATUS.DONE
  );

  const cardConfigs = [
    {
      key: "preparing",
      title: t("orderCard.inPreparation"),
      orders: inPreparation,
    },
    { key: "done", title: t("orderCard.done"), orders: done },
  ];
  return (
    <Flex className={classNames.root} direction="column">
      <Flex gap="2">
        {cardConfigs.map(({ key, title, orders }) => (
          <OrderCard key={key} type={key} title={title} orders={orders} />
        ))}
      </Flex>
    </Flex>
  );
};
