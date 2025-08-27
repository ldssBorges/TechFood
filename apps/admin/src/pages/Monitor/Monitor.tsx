import {
  Button,
  Flex,
  Heading,
  SegmentedControl,
  Text,
} from "@radix-ui/themes";
import { t } from "../../i18n";
import { useEffect, useState } from "react";

import classNames from "./Monitor.module.css";
import axios from "axios";
import { PreparationMonitor } from "../../models/PreparationMonitor";
import { PreparationItem } from "./PreparationCard";

const segments = [
  { key: "PENDING", value: "Received" },
  { key: "INPROGRESS", value: "In-Preparation" },
  { key: "DONE", value: "Done" },
];

type ActionVerb = "start" | "finish" | "cancel";

type IOrderInformation = {
  preparation: PreparationMonitor | null;
  updatePreparationStatus: (id: string, status: ActionVerb) => any;
  updateOrderStatusToFinish: (orderId: string, preparationId: string) => any;
};

const OrderInformation = ({
  preparation,
  updatePreparationStatus,
  updateOrderStatusToFinish,
}: IOrderInformation) => {
  const redButtonName = () =>
    preparation?.status === "INPROGRESS" || preparation?.status === "DONE"
      ? "Cancel"
      : "Reject";

  const greenButtonName = () => {
    if (preparation?.status === "INPROGRESS") return "Finish";
    else if (preparation?.status === "DONE") return "Delivered";
    else return "Accept";
  };

  const greenButtonUpdateAction = () => {
    if (preparation?.preparationId == null) return;
    let status: ActionVerb = "start";
    if (preparation?.status === "INPROGRESS") status = "finish";
    if (preparation?.status === "DONE") {
      updateOrderStatusToFinish(
        preparation?.orderId,
        preparation?.preparationId
      );
      return;
    }

    updatePreparationStatus(preparation?.preparationId, status);
  };

  const redButtonUpdateAction = () => {
    if (!preparation?.preparationId) return;
    updatePreparationStatus(preparation?.preparationId, "cancel");
  };

  return (
    <Flex className={classNames.orderDescribe} direction="column" gap="4">
      <Flex justify="between" align="center">
        <Heading size="5" weight="bold">
          Order #{preparation?.number}
        </Heading>
        <Flex gap="3">
          <Button onClick={redButtonUpdateAction} color="red" size="3">
            {redButtonName()}
          </Button>
          <Button onClick={greenButtonUpdateAction} size="3">
            {greenButtonName()}
          </Button>
        </Flex>
      </Flex>

      <hr style={{ width: "100%", borderTop: "1px solid #ccc" }} />

      <Flex direction="column" gap="3">
        <Heading size="5">Items</Heading>

        {preparation?.products.map((x) => (
          <Flex
            className={classNames.product}
            key={x.name}
            gap="5"
            align="center"
          >
            <Flex className={classNames.imageContainer}>
              <img src={x.imageUrl} alt={x.name} />
            </Flex>
            <Flex direction="column">
              <Text>Name: {x.name}</Text>
              <Text>Quantity: {x.quantity}</Text>
            </Flex>
          </Flex>
        ))}
      </Flex>
    </Flex>
  );
};

export const Monitor = () => {
  const [orderList, setOrderList] = useState<PreparationMonitor[]>([]);
  const [preparationItem, setPreparationItem] =
    useState<PreparationMonitor | null>(null);
  const [statusOrder, setStatusOrder] = useState<string>("CREATED");

  const getPreparationOrder = async () => {
    const response = await axios.get<PreparationMonitor[]>(
      "/api/v1/Preparations/orders"
    );
    setOrderList(response.data);
  };

  const handleUpdatePreparationStatus = async (
    id: string,
    ActionVerb: ActionVerb
  ) => {
    await axios.patch(`/api/v1/Preparations/${id}/${ActionVerb}`);
    await getPreparationOrder();
    setPreparationItem(null);
  };

  const handleUpdateOrderStatusToFinish = async (orderId: string) => {
    await axios.patch(`/api/v1/Orders/${orderId}/finish`);
    await getPreparationOrder();
    setPreparationItem(null);
  };

  useEffect(() => {
    getPreparationOrder();
    console.log(orderList);
  }, []);

  return (
    <Flex className={classNames.root} gap="4">
      <Flex className={classNames.orderIn} direction="column" gap="4">
        <Heading>Order In</Heading>

        <SegmentedControl.Root
          onValueChange={setStatusOrder}
          defaultValue="CREATED"
        >
          {segments.map((x) => (
            <SegmentedControl.Item key={x.key} value={x.key}>
              {x.value}
            </SegmentedControl.Item>
          ))}
        </SegmentedControl.Root>

        <Flex direction="column" className={classNames.orders} gap="4">
          {orderList
            .filter((x) => x.status == statusOrder)
            .map((order) => (
              <PreparationItem
                key={order.preparationId}
                PreparationMonitor={order}
                onClick={() => setPreparationItem(order)}
              />
            ))}
        </Flex>
      </Flex>

      <Flex className={classNames.details} direction="column" gap="4">
        <Heading>Order Information</Heading>

        {preparationItem && (
          <OrderInformation
            preparation={preparationItem}
            updatePreparationStatus={handleUpdatePreparationStatus}
            updateOrderStatusToFinish={handleUpdateOrderStatusToFinish}
          />
        )}
      </Flex>
    </Flex>
  );
};
